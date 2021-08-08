using AceNetworking;
using Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OsrExpBooster
{
    class PreSocket : AceClientSocket
    {
        ushort[] version = new ushort[4] { 4, 6, 0, 20 };
        char[] serverGroupName;
        char[] passwordHash;
        byte[] packetKey;
        //GameSocket GSocket;
        public PreSocket(byte[] xorKey)
            : base(xorKey, false)
        {
            packetKey = xorKey;
            SetAliveMessageTypes(new ushort[1] { Protocol.T_PC_CONNECT_ALIVE });
        }

        public PreSocket(string xorKey)
            : base(xorKey, false)
        {
            packetKey = ASCIIEncoding.ASCII.GetBytes(xorKey);
            SetAliveMessageTypes(new ushort[1] { Protocol.T_PC_CONNECT_ALIVE });
        }



        public override void OnConnect()
        {
            Out.WriteLine("PreServer Connected!", "PreServer", ConsoleColor.DarkGreen);
        }

        public override void OnConnectFail()
        {
            Out.WriteLine("PreServer socket refused the connection!", "PreServer", ConsoleColor.Red);
        }

        public override void OnDisconnect(PublicCommonDefines.DisconnectionReason reason)
        {
            Out.WriteLine("PreServer socket disconnected! Reason:" + reason, "PreServer", ConsoleColor.Red);
        }

        public override void OnReceivedPacket(byte[] data, int len)
        {
            int nBytesUsed = 0;
            ushort nRecvType = 0;

            while (nBytesUsed < len)
            {
                nRecvType = BitConverter.ToUInt16(data, nBytesUsed);
                nBytesUsed += 2;
                switch (nRecvType)
                {
                    case Protocol.T_ERROR:
                        OnReceivedErrorFromServer(data, len, ref nBytesUsed);
                        return;
                    case Protocol.T_PC_CONNECT_SINGLE_FILE_UPDATE_INFO:
                        {
                            nBytesUsed += Marshal.SizeOf(new Protocol.MSG_PC_CONNECT_SINGLE_FILE_UPDATE_INFO());
                            SendNextVersionPacket(version, false);
                        }
                        break;
                    case Protocol.T_PC_CONNECT_SINGLE_FILE_VERSION_CHECK_OK:
                        {
                            SendNextVersionPacket(version, false);
                        }
                        break;
                    case Protocol.T_PC_CONNECT_UPDATE_INFO:
                        {
                            HandleUpdateInfo(data, len, ref nBytesUsed);
                        }
                        break;
                    case Protocol.T_PC_CONNECT_REINSTALL_CLIENT:
                        {
                            HandleReinstallClient(data, len, ref nBytesUsed);
                        }
                        break;
                    case Protocol.T_PC_CONNECT_VERSION_OK:
                        {
                            Out.WriteLine();
                            Out.WriteLine(String.Format("Version acquired: {0}.{1}.{2}.{3}", version[0], version[1], version[2], version[3]), "ServerGroup", ConsoleColor.Green);
                            Out.WriteLine("Acquiring server group...", "ServerGroup", ConsoleColor.DarkYellow);
                            this.SendPacket(Protocol.T_PC_CONNECT_GET_SERVER_GROUP_LIST);
                        }
                        break;
                    case Protocol.T_PC_CONNECT_LOGIN_BLOCKED:
                        {
                            HandleAccountBlocked(data, len, ref nBytesUsed);
                        }
                        break;
                    case Protocol.T_PC_CONNECT_GET_SERVER_GROUP_LIST_OK:
                        {
                            HandleServerGroupListOk(data, len, ref nBytesUsed);
                        }
                        break;
                    case Protocol.T_PC_CONNECT_LOGIN_OK:
                        {
                            HandleLoginOK(data, len, ref nBytesUsed);
                        }
                        break;
                    case 0x10f2:
                        {
                            Out.WriteLine("Mac address check bypassed!", Out.APP, ConsoleColor.Green);
                        }
                        break;
                    default: Out.WriteLine(String.Format("PreSocket Invalid opcode! {0:X}", nRecvType), Out.ERROR); break;
                }
            }
        }



        public void HandleLoginOK(byte[] data, int len, ref int nBytesUsed)
        {
            Out.WriteLine();
            Out.WriteLine(" ##### Login succeeded! ####", "PreServer", ConsoleColor.Green);
            Out.WriteLine();
            //  Protocol.MSG_PC_CONNECT_LOGIN_OK packet = Operations.ByteArrayToStruct<Protocol.MSG_PC_CONNECT_LOGIN_OK>(data, new Protocol.MSG_PC_CONNECT_LOGIN_OK().GetType());
            Protocol.MSG_PC_CONNECT_LOGIN_OK packet = Operations.ByteArrayToStruct<Protocol.MSG_PC_CONNECT_LOGIN_OK>(data, ref nBytesUsed);
            Out.WriteLine(String.Format("Retrieved FieldServer network info[{0}:{1}]", new string(packet.FieldServerIP).TrimEnd(), packet.FieldServerPort), "FieldServer", ConsoleColor.DarkYellow);
            Out.WriteLine(String.Format("Retrieved IMServer network info[{0}:{1}]", new string(packet.IMServerIP).TrimEnd(), packet.IMServerPort), "IMServer", ConsoleColor.DarkYellow);
            //GSocket = new GameSocket(new string(packet.FieldServerIP), packet.FieldServerPort, new string(packet.IMServerIP), packet.IMServerPort, serverGroupName, packetKey, packet.AccountName, passwordHash);
            
            Program.fs.Connect(Program.ip, packet.FieldServerPort);
            Program.fs.SendAuthentication(loginpacket.AccountName, loginpacket.Password);

            this.Disconnect(PublicCommonDefines.DisconnectionReason.SOCKET_CLOSED_BY_CLIENT);
        }
        public void HandleAccountBlocked(byte[] data, int len, ref int nBytesUsed)
        {
            //Protocol.MSG_PC_CONNECT_LOGIN_BLOCKED blockedPacket = Operations.ByteArrayToStruct<Protocol.MSG_PC_CONNECT_LOGIN_BLOCKED>(data, new Protocol.MSG_PC_CONNECT_LOGIN_BLOCKED().GetType());
            Protocol.MSG_PC_CONNECT_LOGIN_BLOCKED blockedPacket = Operations.ByteArrayToStruct<Protocol.MSG_PC_CONNECT_LOGIN_BLOCKED>(data, ref nBytesUsed);
            Out.WriteLine("Received account blocked data!", "PreServer", ConsoleColor.DarkYellow);
            Out.WriteLine();
            Out.WriteLine("*******************************", "Ban Data", ConsoleColor.Red);
            Out.WriteLine("Start Date: " + blockedPacket.atimeStart.GetDateTimeString(), "Ban Data", ConsoleColor.Red);
            Out.WriteLine("End Date: " + blockedPacket.atimeEnd.GetDateTimeString(), "Ban Data", ConsoleColor.Red);
            Out.WriteLine("Account Name: " + new string(blockedPacket.szAccountName).Trim(), "Ban Data", ConsoleColor.Red);
            Out.WriteLine("Reason: " + new string(blockedPacket.szBlockedReasonForUser).Trim(), "Ban Data", ConsoleColor.Red);
            Out.WriteLine("Block Type: " + blockedPacket.nBlockedType, "Ban Data", ConsoleColor.Red);
            Out.WriteLine("*******************************", "Ban Data", ConsoleColor.Red);
            Out.WriteLine();

           // StartLogInRoutine();
        }

        /// <summary>
        /// <para>Stormys super cool method of gettint a pw without showing the actual chars</para>
        /// </summary>
        /// <returns></returns>
        static string GetPWNoneVisible()
        {
            ConsoleKeyInfo key;
            string password = "";
            do
            {
                key = Console.ReadKey(true);

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        password = password.Substring(0, (password.Length - 1));
                        Console.Write("\b \b");
                    }
                }
            }
            // Stops Receving Keys Once Enter is Pressed
            while (key.Key != ConsoleKey.Enter);

            return password;
        }

        Protocol.MSG_PC_CONNECT_LOGIN loginpacket;
        public void StartLogInRoutine()
        {
            string name = Program.id;
            string psw = Program.password;
            loginpacket = new Protocol.MSG_PC_CONNECT_LOGIN();
            loginpacket.MGameSEX = loginpacket.MGameYear = 1;
            loginpacket.WebLoginAuthKey = Operations.ToCharArray("", 30);

            loginpacket.AccountName = Operations.ToCharArray(name, 20);
            loginpacket.PrivateIP = Operations.ToCharArray("127.0.0.1", 16);

            loginpacket.ClientIP = loginpacket.PrivateIP;
            loginpacket.LoginType = (char)0;
            byte[] Hash;
            using (MD5 md5Hash = MD5.Create())
            {

                Hash = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(psw));

            }
            passwordHash = psw.ToCharArray();
            loginpacket.Password = Hash;
            loginpacket.FieldServerGroupName = serverGroupName;
            this.SendPacket(Protocol.T_PC_CONNECT_LOGIN, loginpacket);
        }

        public void HandleServerGroupListOk(byte[] data, int len, ref int nBytesUsed)
        {
            Out.WriteLine();
            //Protocol.MSG_PC_CONNECT_GET_SERVER_GROUP_LIST_OK packet = Operations.ByteArrayToStruct<Protocol.MSG_PC_CONNECT_GET_SERVER_GROUP_LIST_OK>(data, new Protocol.MSG_PC_CONNECT_GET_SERVER_GROUP_LIST_OK().GetType());
            Protocol.MSG_PC_CONNECT_GET_SERVER_GROUP_LIST_OK packet = Operations.ByteArrayToStruct<Protocol.MSG_PC_CONNECT_GET_SERVER_GROUP_LIST_OK>(data, ref nBytesUsed);
            nBytesUsed--;
            Protocol.MEX_SERVER_GROUP_INFO_FOR_LAUNCHER[] groups = Operations.StructsArrayParse<Protocol.MEX_SERVER_GROUP_INFO_FOR_LAUNCHER>(data, packet.NumOfServerGroup, ref nBytesUsed);
            Out.WriteLine("Acquired server group: " + new string(packet.ServerGroup.ServerGroupName).Trim() + " [" + ((packet.ServerGroup.Crowdedness <= 0) ? "Maintenance" : "Alive") + "]", "ServerGroup", (packet.ServerGroup.Crowdedness <= 0) ? ConsoleColor.DarkRed : ConsoleColor.DarkGreen);
            serverGroupName = packet.ServerGroup.ServerGroupName;
            if (packet.ServerGroup.Crowdedness < 0)
            {
                Out.WriteLine("All servers are currently under maintenance, you're unable to log in.", "Error", ConsoleColor.Red);
                Console.ReadKey();
                Environment.Exit(0);
            }
            StartLogInRoutine();
        }
        public override void OnReceivedPacketError(PublicCommonDefines.PacketDecryptError errorCode)
        {
            Out.WriteLine("Unable to decrypt incoming packet: " + errorCode, "Error", ConsoleColor.Red);
        }

        public override void OnDispatchPacketException(Exception ex, bool bSend)
        {
        }

        public void HandleReinstallClient(byte[] data, int len, ref int nBytesUsed)
        {
            // Protocol.MSG_PC_CONNECT_REINSTALL_CLIENT packet = Operations.ByteArrayToStruct<Protocol.MSG_PC_CONNECT_REINSTALL_CLIENT>(data, new Protocol.MSG_PC_CONNECT_REINSTALL_CLIENT().GetType());
            Protocol.MSG_PC_CONNECT_REINSTALL_CLIENT packet = Operations.ByteArrayToStruct<Protocol.MSG_PC_CONNECT_REINSTALL_CLIENT>(data, ref nBytesUsed);
            version = new ushort[4];
            version = packet.LastVersion;
            Out.WriteLine(String.Format("Reinstall client packet received!", packet.LastVersion[0], packet.LastVersion[1], packet.LastVersion[2], packet.LastVersion[3]), "Version", ConsoleColor.DarkGreen);
            SendNextVersionPacket(version, false);

        }

        public void OnReceivedErrorFromServer(byte[] data, int len, ref int nBytesUsed)
        {
            //  Protocol.MSG_ERROR errData = Operations.ByteArrayToStruct<Protocol.MSG_ERROR>(data, new Protocol.MSG_ERROR().GetType());
            Protocol.MSG_ERROR errData = Operations.ByteArrayToStruct<Protocol.MSG_ERROR>(data, ref nBytesUsed);
            Protocol.Errors err = (Protocol.Errors)errData.ErrorCode;
            switch (err)
            {
                case Protocol.Errors.ERR_COMMON_LOGIN_FAILED:
                case Protocol.Errors.ERR_DB_NO_SUCH_ACCOUNT:
                case Protocol.Errors.ERR_PERMISSION_DENIED:
                    {
                        Out.WriteLine("Invalid Account inserted: " + err, "Authentication", ConsoleColor.DarkRed);
                        StartLogInRoutine();
                    }
                    break;

                default:
                    {
                        Out.WriteLine(String.Format("Received Error from Preserver. MsgType[{0}] ErrorCode[{1}] Param1[{2}] Param2[{3}]", errData.MsgType,
                            errData.ErrorCode, errData.ErrParam1, errData.ErrParam2), "Server Error", ConsoleColor.Red);

                    }
                    break;
            }
            if (errData.StringLength > 0)
                nBytesUsed += errData.StringLength;
            if (errData.CloseConnection)
            {
                Out.WriteLine("PreServer socket disconnected as requested by server.", "PreServer", ConsoleColor.Red);
                this.Disconnect(PublicCommonDefines.DisconnectionReason.SOCKET_CLOSED_BY_CLIENT);
            }


        }

        public void CalculateNextVersion(ref ushort[] version)
        {
            version[3]++;
            if (version[3] >= 99)
            {
                version[3] = 0;
                version[2]++;
            }

            if (version[2] >= 99)
            {
                version[2] = 0;
                version[1]++;
            }

            if (version[1] >= 99)
            {
                version[1] = 0;
                version[0]++;
            }
        }

        public void HandleUpdateInfo(byte[] data, int length, ref int nBytesUsed)
        {
            //  Protocol.MSG_PC_CONNECT_UPDATE_INFO packet = Operations.ByteArrayToStruct<Protocol.MSG_PC_CONNECT_UPDATE_INFO>(data, new Protocol.MSG_PC_CONNECT_UPDATE_INFO().GetType());
            Protocol.MSG_PC_CONNECT_UPDATE_INFO packet = Operations.ByteArrayToStruct<Protocol.MSG_PC_CONNECT_UPDATE_INFO>(data, ref nBytesUsed);
            SendNextVersionPacket(packet.UpdateVersion, false);
        }
        public void SendNextVersionPacket(ushort[] version, bool bSetNextVersion = true)
        {
            if (bSetNextVersion)
                CalculateNextVersion(ref version);

            Protocol.MSG_PC_CONNECT_VERSION ver = new Protocol.MSG_PC_CONNECT_VERSION();
            ver.ClientVersion = new ushort[4];
            ver.ClientVersion = version;
            this.SendPacket(Protocol.T_PC_CONNECT_VERSION, ver);
            Out.WriteLine(String.Format("Trying next version: {0}.{1}.{2}.{3}", version[0], version[1], version[2], version[3]), "Version", ConsoleColor.DarkYellow);
        }
        public void InitVersionCheckBruteForce()
        {
            SendMacAddr();
            Thread.Sleep(500);
            Out.WriteLine("Sending single version check packet...", "Version", ConsoleColor.DarkYellow);
            this.SendPacket(Protocol.T_PC_CONNECT_SINGLE_FILE_VERSION_CHECK, new Protocol.MSG_PC_CONNECT_SINGLE_FILE_VERSION_CHECK());
            Out.WriteLine("Starting version bruteforce...", "Version", ConsoleColor.DarkYellow);

        }
        public void SendMacAddr()
        {
            Protocol.MSG_PC_CONNECT_SEND_GET_BLOCKED_MAC_ADDR packet = new Protocol.MSG_PC_CONNECT_SEND_GET_BLOCKED_MAC_ADDR();
            byte[] macBytes = new byte[6];
            new Random((int)(DateTime.Now.Ticks & 0xffffffff)).NextBytes(macBytes);
            string macString = string.Format("{0}-{1}-{2}-{3}-{4}-{5}", macBytes[0], macBytes[1], macBytes[2], macBytes[3], macBytes[4], macBytes[5]);
            packet.macAddr = Operations.ToCharArray(macString, 50);
            SendPacket(0x10f1, packet);
        }
    }
}
