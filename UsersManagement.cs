using Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OsrExpBooster
{
    public class UsersManagement
    {
        FieldSocket sock;
        public UsersManagement(FieldSocket parent)
        {
            sock = parent;
            new Thread(() => ServerUserScan()).Start();
        }

        public List<Protocol.MEX_OTHER_CHARACTER_INFO> userInfo = new List<Protocol.MEX_OTHER_CHARACTER_INFO>();
        object userInfoLock = new object();
        List<Protocol.MEX_OTHER_CHARACTER_INFO> userInfoCache = new List<Protocol.MEX_OTHER_CHARACTER_INFO>();

        public bool otherUsersInMap = true;
        public bool gmOnline = true;
        public void HandleGetOtherInfoOk(Protocol.MEX_OTHER_CHARACTER_INFO otherInfoOk)
        {
            userInfoCache.Add(otherInfoOk);
        }
        public List<Protocol.MEX_OTHER_CHARACTER_INFO> GetUserList()
        {
            return new List<Protocol.MEX_OTHER_CHARACTER_INFO>(userInfo);
        }
        public void UpdateFlags()
        {

            bool gmFound = false;
            bool otherUserFound = false;
            foreach (var user in userInfo)
            {
                if (user.MapChannelIndex == sock.usedCharacterInfo.MapChannelIndex
                    && user.ClientIndex != sock.usedCharacterInfo.ClientIndex)
                {
                    otherUserFound = true;
                 }

                if(user.Race == 130 || user.Race == 256)
                {
                    gmFound = true;
                }
            }
            otherUsersInMap = otherUserFound;
            gmOnline = gmFound;


        }

        byte[] BuildBuffer(ref ushort currentIdx)
        {
            byte[] buffer = new byte[0];

            for (int i = 0; i < 372 && currentIdx < 9999; i++, currentIdx++)
            {
                buffer = buffer.Concat(BitConverter.GetBytes(Protocol.T_FC_CHARACTER_GET_OTHER_INFO).Concat(BitConverter.GetBytes(currentIdx)).ToArray()).ToArray();
            }

            return buffer;
        }
        
        public void ServerUserScan()
        {
            while (sock.Connected)
            {
                userInfoCache.Clear();
                ushort idx = 0;
                while(idx <= 9998)
                {
                    byte[] buf = BuildBuffer(ref idx);
                    sock.SendPacket(buf);
                }

                Thread.Sleep(3000);
                lock (userInfoLock) { userInfo = userInfoCache; }
                UpdateFlags();
            }
        }
    }
}
