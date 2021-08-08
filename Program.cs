using Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OsrExpBooster
{
    class Program
    {
        static byte[] xorkey = new byte[] {
            0x76, 0x6D, 0x64, 0x6C, 0x66, 0x6A, 0x68, 0x75, 0x64,
            0x38, 0x33, 0x30, 0x70, 0x77, 0x6B, 0x6C, 0x64, 0x6C,
            0x6B, 0x76, 0x5B, 0x5D, 0x66, 0x10, 0x6A, 0x64, 0x6D,
            0x76, 0x6C, 0x64, 0x3B, 0x73, 0x6B, 0x2C, 0x6D, 0x63,
            0x75, 0x69, 0x65, 0x38, 0x72, 0x69, 0x6A, 0x6D, 0x66,
            0x76, 0x6B, 0x69, 0x64, 0x66, 0x6F, 0x33, 0x34, 0x30,
            0x2D, 0x70, 0x66, 0x6C, 0x63, 0x6C, 0x2C, 0x3B, 0x64,
            0x73, 0x64, 0x5D, 0x75, 0x30, 0x33, 0x75, 0x34, 0x30,
            0x6A, 0x76, 0x63, 0x6F, 0x6E, 0x76, 0x6E, 0x30, 0x38,
            0x38, 0x39, 0x32, 0x68, 0x30, 0x6E, 0x6E, 0x6C, 0x73,
            0x6E, 0x6C, 0x64, 0x73, 0x66, 0x2F, 0x2C, 0x3B, 0x76,
            0x6D, 0x73, 0x5B, 0x70, 0x66, 0x2D, 0x32, 0x66, 0x6A,
            0x64, 0x5D, 0x75, 0x30, 0x33, 0x75, 0x34, 0x30, 0x6A,
            0x76, 0x63, 0x6F, 0x6E, 0x76, 0x6E, 0x30, 0x38, 0x32
            };

        public static string id = "elpaso";
        public static string password = "elpaso123";
        public static string ip = "91.134.12.104";
        public static FieldSocket fs = null;
        public static PreSocket ps = null;
        public static bool move = true;



        public static void init()
        {
            if(fs != null)
            {
                fs.Dispose();
                fs = null;
            }

            fs = new FieldSocket(xorkey);


            if (ps != null)
            {
                ps.Dispose();
                ps = null;

            }
            ps = new PreSocket(xorkey);
            ps.Connect(ip, 15100);
            ps.InitVersionCheckBruteForce();
        }

        static void ChangeWinTitle()
        {
            string mapName = "Unknown";
            foreach (var entry in AceData.mapInfos)
            {
                if(entry.mapIndex == fs.usedCharacterInfo.MapChannelIndex.MapIndex)
                {
                    mapName = entry.mapName;
                    break;
                }
            }
            string titleString = string.Format("IP: {0} | Character Name: {1} | Level: {2} | Map: {3}", ip, new string(fs.usedCharacterInfo.CharacterName).Replace("\0", ""), fs.usedCharacterInfo.Level, mapName);
            titleString += string.Format(" | Position: [ X:{0} Y:{1} Z:{2} ] | Experience: {3}", 
                fs.usedCharacterInfo.PositionVector.x, fs.usedCharacterInfo.PositionVector.y, fs.usedCharacterInfo.PositionVector.z, fs.usedCharacterInfo.Experience);

            if(fs.userMgmt != null)
            {
                titleString += string.Format(" | User count: {0} | Other user in map: {1} | Staff online: {2}", fs.userMgmt.GetUserList().Count, fs.userMgmt.otherUsersInMap, fs.userMgmt.gmOnline);
            }

            Console.Title = titleString;
        }

        static void Main(string[] args)
        {

            init();
            new Thread(() =>
            {
                while(true)
                {
                    if ((fs == null) && ps == null)
                        Console.Title = "Disconnected!";
                    else if (ps != null && fs.usedCharacterInfo.CharacterName == null)
                        Console.Title = "Authenticating";
                    else if (fs != null)
                        ChangeWinTitle();

                    Thread.Sleep(100);
                }
            }).Start();
            while (true)
            {
                string outline = Out.ReadLine();
                string[] cmdparams = outline.Split(' ');
                int paramCount = cmdparams.Length - 1;

                switch(cmdparams[0].ToLower())
                {
                    case "warp":
                        {
                            if (paramCount != 1)
                            {
                                Out.WriteLine("Invalid command!", Out.ERROR);
                                break;
                            }

                            fs.MoveToMap(Convert.ToUInt16(cmdparams[1]));
                        }
                        break;
                    case "list":
                        {
                            if(paramCount != 1)
                            {
                                Out.WriteLine("Invalid command!", Out.ERROR);
                                break;
                            }

                            var list = fs.userMgmt.GetUserList();
                            foreach(var entry in list)
                            {
                                if ((cmdparams[1] == "staff" || cmdparams[1] == "all") && (entry.Race == 130 || entry.Race == 256))
                                {
                                    Out.WriteLine(string.Format("Staff => {0} [Uid{1}|Race{2}]", new string(entry.CharacterName), entry.CharacterUniqueNumber, entry.Race), Out.APP);
                                }
                                else if((cmdparams[1] == "user" || cmdparams[1] == "all") && entry.Race == 2)
                                {
                                    Out.WriteLine(string.Format("User => {0} [Uid{1}]", new string(entry.CharacterName), entry.CharacterUniqueNumber), Out.APP);
                                }
                                else if((cmdparams[1] == "map"))
                                {
                                    if (entry.MapChannelIndex == fs.usedCharacterInfo.MapChannelIndex)
                                        Out.WriteLine(string.Format("User in map => {0} [Uid{1}]", new string(entry.CharacterName), entry.CharacterUniqueNumber), Out.APP);
                                }
                                else
                                {
                                    Out.WriteLine("Invalid command!", Out.ERROR);
                                    break;
                                }
                            }
                        }
                        break;
                    case "move":
                        {
                            if(paramCount != 1)
                            {
                                Out.WriteLine("Invalid command!", Out.ERROR);
                                break;
                            }

                            if(cmdparams[1] == "start" && move == false)
                            {
                                move = true;
                                Out.WriteLine("Bot movement enabled!", Out.ERROR);
                            }
                            else if(cmdparams[1] == "stop" && move == true)
                            {
                                move = false;
                                Out.WriteLine("Bot movement disabled!");
                            }
                            else
                            {
                                Out.WriteLine("Invalid command!", Out.ERROR);
                                break;
                            }
                        }
                        break;
                    case "help":
                        {
                            Out.WriteLine("warp <map index> / warps your character to a specified map (always channel 0)");
                            Out.WriteLine("list <type [staff|user|map|all]> / retrieve the user count of the specific category");
                            Out.WriteLine("move <action [start|stop]> / enable the bot auto moving");
                        }
                        break;
                }
            }
        }
    }
}
