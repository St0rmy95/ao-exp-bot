using AceNetworking;
using Protocols;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace OsrExpBooster
{
    public class FieldSocket : AceClientSocket
    {
        public uint accountUniqueNumber = 0;
        public uint usedCharacter = 0;
        public Protocol.CHARACTER usedCharacterInfo;
        public UsersManagement userMgmt = null;
        bool bflag = false;
        bool bflag2 = false;
        public void SendMovePacket(Protocol.AVECTOR3 pos)
        {
            if(bflag && !bflag2)
            {
                new Thread(() => Program.init()).Start();
                bflag2 = true;
                return;
            }

            Protocol.MSG_FC_MOVE movePacket = new Protocol.MSG_FC_MOVE();
            movePacket.ClientIndex = usedCharacterInfo.ClientIndex;
            movePacket.TimeGap = 9999;
            movePacket.PositionVector = pos;
            movePacket.TargetVector = usedCharacterInfo.TargetVector.ConvertToAVEC();
            movePacket.UpVector = new Protocol.AVECTOR3() { y = 1 };
            SendPacket(Protocol.T_FC_MOVE, movePacket);
        }

        public FieldSocket(byte[] pXorString)
            : base(pXorString, false)
        {
            SetAliveMessageTypes(new ushort[1] { Protocols.Protocol.T_FC_CONNECT_ALIVE });
        }

        public void SendAuthentication(char[] accountName, byte[] password)
        {
            Protocol.MSG_FC_CONNECT_LOGIN login = new Protocol.MSG_FC_CONNECT_LOGIN();
            login.AccountName = accountName;
            login.Password = Operations.ToCharArray(BitConverter.ToString(password).Replace("-", string.Empty), 33);
            login.PrivateIP = Operations.ToCharArray("127.0.0.1", 16);
            //login.Padding = new int[2];
            SendPacket(Protocol.T_FC_CONNECT_LOGIN, login);
        }
        public override void OnConnect()
        {
            base.OnConnect();
            Out.WriteLine("FieldServer connected!", Out.APP);
        }

        public override void OnReceivedPacketError(PublicCommonDefines.PacketDecryptError errorCode)
        {
            Out.WriteLine("FieldServer socket wasn't able to decrypt last packet. Error: " + errorCode, Out.ERROR);
        }
        public override void OnDisconnect(PublicCommonDefines.DisconnectionReason reason)
        {
            bflag = true;
            Console.WriteLine("FieldServer socket terminated! Reason: " + reason, Out.ERROR);

        }

        public void HandleString(byte[] data, int len, ref int nBytesUsed, int type)
        {
            //data = data.Skip<byte>(nBytesUsed).ToArray<byte>();
            //switch (type)
            //{
            //    case 128:
            //        {
            //            Protocol.MSG_FC_STRING_128 serverString = Operations.ByteArrayToStruct<Protocol.MSG_FC_STRING_128>(data, new Protocol.MSG_FC_STRING_128().GetType());
            //            nBytesUsed += Marshal.SizeOf(serverString);
            //            Console.WriteLine(new string(serverString.String).Replace("\0", ""), serverString.PrintType.ToString(), ConsoleColor.DarkBlue);
            //        }
            //        break;
            //    case 256:
            //        {
            //            Protocol.MSG_FC_STRING_256 serverString = Operations.ByteArrayToStruct<Protocol.MSG_FC_STRING_256>(data, new Protocol.MSG_FC_STRING_256().GetType());
            //            nBytesUsed += Marshal.SizeOf(serverString);
            //            Console.WriteLine(new string(serverString.String).Replace("\0", ""), serverString.PrintType.ToString(), ConsoleColor.DarkBlue);
            //        }
            //        break;
            //    case 512:
            //        {
            //            Protocol.MSG_FC_STRING_512 serverString = Operations.ByteArrayToStruct<Protocol.MSG_FC_STRING_512>(data, new Protocol.MSG_FC_STRING_512().GetType());
            //            nBytesUsed += Marshal.SizeOf(serverString);
            //            Console.WriteLine(new string(serverString.String).Replace("\0", ""), serverString.PrintType.ToString(), ConsoleColor.DarkBlue);
            //        }
            //        break;
            //    case -1:
            //        {
            //            Protocol.MSG_FC_WORLD_NOTIFICATION serverString = Operations.ByteArrayToStruct<Protocol.MSG_FC_WORLD_NOTIFICATION>(data, new Protocol.MSG_FC_WORLD_NOTIFICATION().GetType());
            //            nBytesUsed += Marshal.SizeOf(serverString);
            //            Console.WriteLine(new string(serverString.String).Replace("\0", ""), "Notification", ConsoleColor.DarkCyan);
            //        }
            //        break;
            //}
        }

        public override void OnReceivedPacket(byte[] data, int nLength)
        {
            int nBytesUsed = 0;
            ushort nRecvType = 0;

            while (nBytesUsed < nLength)
            {
                nRecvType = BitConverter.ToUInt16(data, nBytesUsed);
                nBytesUsed += 2;

                switch (nRecvType)
                {
                    case Protocol.T_FC_CONNECT_LOGIN_OK:
                        {
                            Console.WriteLine("Client authenticated to FieldServer!", Out.GAME);
                            HandleLoginOk(data, nLength, ref nBytesUsed);
                        }
                        break;
                    case Protocol.T_FC_CHARACTER_GAMESTART_OK:
                        {
                            HandleCharacterGameStartOk(data, nLength, ref nBytesUsed);
                        }
                        break;
                    case Protocol.T_FC_CHARACTER_GET_CHARACTER_OK:
                        {
                            HandleGetCharacterOk(data, nLength, ref nBytesUsed);
                        }
                        break;
                    case Protocol.T_FC_CONNECT_SYNC_TIME:
                        {
                            nBytesUsed += Marshal.SizeOf(new Protocol.MSG_FC_CONNECT_SYNC_TIME().GetType());
                            Out.WriteLine("Received server time!", Out.GAME);
                        }
                        break;
                    case Protocol.T_FC_CONNECT_ARENASERVER_INFO:
                        {
                            nBytesUsed += Marshal.SizeOf(new Protocol.MSG_FC_CONNECT_ARENASERVER_INFO().GetType());
                            Out.WriteLine(String.Format("Received start packet: 0x{0:X}", nRecvType), "FieldServer", ConsoleColor.Gray);
                        }
                        break;
                    case Protocol.T_FC_CHARACTER_CHANGE_EXP:
                        {
                            HandleEXPChange(data, nLength, ref nBytesUsed);
                        }
                        break;
                    case Protocol.T_FC_CHARACTER_CHANGE_CURRENTHPDPSPEP:
                        {
                            HandleParamChange(data, nLength, ref nBytesUsed);
                        }
                        break;
                    case Protocol.T_FC_COLLECTION_INFO:
                        {
                            HandlePutCollectionInfo(data, nLength, ref nBytesUsed);
                        }
                        break;
                    case Protocol.T_FC_STRING_128:
                        {
                            HandleString(data, nLength, ref nBytesUsed, 128);
                        }
                        break;
                    case Protocol.T_FC_STRING_256:
                        {
                            HandleString(data, nLength, ref nBytesUsed, 256);
                        }
                        break;
                    case Protocol.T_FC_STRING_512:
                        {
                            HandleString(data, nLength, ref nBytesUsed, 512);
                        }
                        break;
                    case Protocol.T_FC_WORLD_NOTIFICATION:
                        {
                            HandleString(data, nLength, ref nBytesUsed, -1);
                        }
                        break;
                    case Protocol.T_FC_INFO_MSWARINFO_DISPLAY_OPTION_OK:
                        {
                            HandleDisplayInfo(data, nLength, ref nBytesUsed);
                        }
                        break;
                    case Protocol.T_FC_MOVE_OK:
                        {
                            HandleMoveOKPacket(data, nLength, ref nBytesUsed);
                        }
                        break;
                    case Protocol.T_FC_ITEM_PET_HEADER:
                        {
                            Out.WriteLine("Starting acquiring PET data...", "Item", ConsoleColor.DarkYellow);
                        }
                        break;
                    case Protocol.T_FC_ITEM_PET_DONE:
                        {
                            Out.WriteLine("PET data acquired!", "Item", ConsoleColor.DarkGreen);
                        }
                        break;
                    case Protocol.T_FC_STORE_PUT_ITEM_HEADER:
                        {
                            HandleStoreItemHeader(data, nLength, ref nBytesUsed);
                        }
                        break;
                    case Protocol.T_FC_STORE_PUT_ITEM_DONE:
                        {
                            HandlePutItemDone(data, nLength, ref nBytesUsed);
                        }
                        break;
                    case Protocol.T_FC_ITEM_PUT_ENCHANT_HEADER:
                        {
                            Out.WriteLine("Starting acquiring enchant data...", "Item", ConsoleColor.DarkYellow);
                        }
                        break;
                    case Protocol.T_FC_ITEM_PUT_ENCHANT:
                        {
                            HandleEnchantInsert(data, nLength, ref nBytesUsed);
                        }
                        break;
                    case Protocol.T_FC_ITEM_PUT_ENCHANT_DONE:
                        {
                            Out.WriteLine("Enchant data acquired!", "Item", ConsoleColor.DarkGreen);
                        }
                        break;
                    case Protocol.T_FC_COLLECTION_MONTHLY_AROMOR_EVENT_INIT:
                        {
                            Out.WriteLine("Starting acquiring collection monthly event...", "Item", ConsoleColor.DarkYellow);
                        }
                        break;
                    case Protocol.T_FC_COLLECTION_MONTHLY_AROMOR_EVENT_INFO:
                        {
                            HandleCollectionEventInfo(data, nLength, ref nBytesUsed);
                        }
                        break;
                    case Protocol.T_FC_SKILL_SETUP_SKILL_OK_HEADER:
                        {
                            Out.WriteLine("Starting acquiring skill data...", "Item", ConsoleColor.DarkYellow);
                        }
                        break;
                    case Protocol.T_FC_SKILL_SETUP_SKILL_OK_DONE:
                        {
                            Out.WriteLine("Skill data acquired!", "Item", ConsoleColor.DarkGreen);
                        }
                        break;
                    case Protocol.T_FC_SKILL_SETUP_SKILL_OK:
                        {
                            nBytesUsed += 56;
                        }
                        break;
                    case Protocol.T_FC_CHARACTER_CHANGE_TOTALGEAR_STAT:
                        {
                            HandleCharacterStatChange(data, nLength, ref nBytesUsed);
                        }
                        break;
                    case Protocol.T_FC_QUEST_PUT_ALL_QUEST_HEADER:
                        {
                            Out.WriteLine("Starting acquiring quest data...", "Quest", ConsoleColor.DarkYellow);
                        }
                        break;
                    case Protocol.T_FC_QUEST_PUT_ALL_QUEST:
                        {
                            HandleQuestInfo(data, nLength, ref nBytesUsed);
                        }
                        break;
                    case Protocol.T_FC_QUEST_PUT_ALL_QUEST_DONE:
                        {
                            Out.WriteLine("Quest data acquired!", "Quest", ConsoleColor.DarkGreen);
                        }
                        break;
                    case Protocol.T_FC_QUEST_PUT_ALL_QUEST_MONSTER_COUNT_HEADER:
                        {
                            Out.WriteLine("Starting acquiring quest monster data...", "Quest", ConsoleColor.DarkYellow);
                        }
                        break;
                    case Protocol.T_FC_QUEST_PUT_ALL_QUEST_MONSTER_COUNT:
                        {
                            HandleQuestMonsterInfo(data, nLength, ref nBytesUsed);
                        }
                        break;
                    case Protocol.T_FC_QUEST_PUT_ALL_QUEST_MONSTER_COUNT_DONE:
                        {
                            Out.WriteLine("Quest monster data acquired!", "Quest", ConsoleColor.DarkGreen);
                        }
                        break;
                    case Protocol.T_FC_CHARACTER_CHANGE_BODYCONDITION:
                        {
                            HandleBodyCondChange(data, nLength, ref nBytesUsed);
                        }
                        break;
                    case Protocol.T_FC_EVENT_NOTIFY_MSG_GET_OK:
                        {

                            nBytesUsed += Marshal.SizeOf(new Protocol.MSG_FC_EVENT_NOTIFY_MSG_GET_OK().GetType());
                            Out.WriteLine("Event Got");
                        }
                        break;
                    case Protocol.T_FC_CHARACTER_CHANGE_CHARACTER_MODE_OK:
                        {
                            HandleChangeCharacterModeOk(data, nLength, ref nBytesUsed);
                        }
                        break;
                    case Protocol.T_FC_WAR_INFLUENCE_DATA:
                        {
                            nBytesUsed += Marshal.SizeOf(new Protocol.MSG_FC_WAR_INFLUENCE_DATA().GetType());
                        }
                        break;
                    case Protocol.T_FC_STORE_UPDATE_ITEM_COUNT:
                        {
                            nBytesUsed += Marshal.SizeOf(new Protocol.MSG_FC_STORE_UPDATE_ITEM_COUNT());
                        }
                        break;
                    case Protocol.T_FC_OUTPOST_WAR_INFO:
                        {
                            HandleOutPostInfo(data, nLength, ref nBytesUsed);
                        }
                        break;
                    case Protocol.T_FC_CHARACTER_CHANGE_STATUS:
                        {
                            nBytesUsed += Marshal.SizeOf(new Protocol.MSG_FC_CHARACTER_CHANGE_STATUS());
                            Out.WriteLine("Changed current status!");
                        }
                        break;
                    case Protocol.T_FC_MONSTER_MOVE_OK:
                        {
                            HandleMonMoveOk(data, nLength, ref nBytesUsed);
                        }
                        break;
                    case Protocol.T_FC_CHARACTER_GET_OTHER_INFO_OK:
                        {
                            HandleGetOtherInfoOk(data, nLength, ref nBytesUsed);
                        }
                        break;
                    case 0x3a05:
                        {
                            Protocol.MSG_FC_MONSTER_CHANGE_HP info = Operations.ByteArrayToStruct<Protocol.MSG_FC_MONSTER_CHANGE_HP>(data, ref nBytesUsed);

                        }
                        break;
                    case Protocol.T_ERROR:
                        {
                            Protocol.MSG_ERROR info = Operations.ByteArrayToStruct<Protocol.MSG_ERROR>(data, ref nBytesUsed);
                            if (info.ErrorCode == 0x2606)
                            {
                                Refill();
                                break;
                            }

                            if (info.ErrorCode == 0x2400 || info.ErrorCode == 0x2401)
                                Debugger.Break();

                            if (info.ErrorCode == 0x2008)
                                break;

                            Out.WriteLine(string.Format("Received error {0:x}", info.ErrorCode), Out.ERROR);
                        }
                        break;
                    case Protocol.T_FC_BATTLE_ATTACK_OK:
                        {
                            HandleBattleAttackOk(data, nLength, ref nBytesUsed);
                        }
                        break;
                    case Protocol.T_FC_ITEM_SHOW_ITEM:
                        {
                            HandleItemShow(data, nLength, ref nBytesUsed);
                        }
                        break;
                    case Protocol.T_FC_EVENT_WARP_SAME_FIELD_SERVER:
                        {
                            HandleWarpSameMap(data, nLength, ref nBytesUsed);
                        }
                        break;

                    case Protocol.T_FC_CHARACTER_CHANGE_CURRENTHP:
                  //  case Protocol.T_FC_CHARACTER_CHANGE_CURRENTHP + 1:
                  //  case Protocol.T_FC_CHARACTER_CHANGE_CURRENTHP + 2:
                    case Protocol.T_FC_CHARACTER_CHANGE_CURRENTHP + 3:
                        {
                            Refill();
                        }
                        break;
                    case Protocol.T_FC_ITEM_HIDE_ITEM:
                        {
                            HandleItemHide(data, nLength, ref nBytesUsed);
                        }
                        break;
                    default:
                        // Console.WriteLine(String.Format("FieldSocket Invalid opcode! 0x{0:X}", nRecvType), "Error", ConsoleColor.Red);
                        // if(exploit == null)
                        //     this.Disconnect(PublicCommonDefines.DisconnectionReason.SOCKET_CLOSED_BY_CLIENT_ERROR);

                        nBytesUsed += 2;
                        break;
                }
            }
        }

        List<Protocol.MSG_FC_ITEM_SHOW_ITEM> dropItemList = new List<Protocol.MSG_FC_ITEM_SHOW_ITEM>();
        public void HandleItemHide(byte[] data, int len, ref int nBytesUsed)
        {
            Protocol.MSG_FC_ITEM_HIDE_ITEM packet = Operations.ByteArrayToStruct<Protocol.MSG_FC_ITEM_HIDE_ITEM>(data, ref nBytesUsed);

            if (packet.ClientIndex == usedCharacterInfo.ClientIndex)
            {
                var item = dropItemList.Where((x) => x.ItemFieldIndex == packet.ItemFieldIndex).First();
                string itemName = AceData.itemNames.ContainsKey((uint)item.ItemNum) ? AceData.itemNames[(uint)item.ItemNum] : "Unknown";

                Out.WriteLine(string.Format("Picked item! => {0} [{1}]", itemName, (uint)item.ItemNum), Out.GAME);
            }
        }
        public void HandleItemShow(byte[] data, int len, ref int nBytesUsed)
        {
            Protocol.MSG_FC_ITEM_SHOW_ITEM packet = Operations.ByteArrayToStruct<Protocol.MSG_FC_ITEM_SHOW_ITEM>(data, ref nBytesUsed);
           // usedCharacterInfo.PositionVector = packet.Position.ToDXVector();
            Protocol.MSG_FC_ITEM_GET_ITEM pickItem = new Protocol.MSG_FC_ITEM_GET_ITEM();
            pickItem.ClientIndex = usedCharacterInfo.ClientIndex;
            pickItem.ItemFieldIndex = packet.ItemFieldIndex;
            dropItemList.Add(packet);
            SendPacket(0x3902, pickItem);
        }
        public void SendBodyConditionChange()
        {
            Protocol.MSG_FC_CHARACTER_CHANGE_BODYCONDITION packet = new Protocol.MSG_FC_CHARACTER_CHANGE_BODYCONDITION();
            packet.ClientIndex = usedCharacterInfo.ClientIndex;
            packet.BodyCondition = usedCharacterInfo.BodyCondition;
            SendPacket(Protocol.T_FC_CHARACTER_CHANGE_BODYCONDITION, packet);
        }
        public void HandleBattleAttackOk(byte[] data, int len, ref int nBytesUsed)
        {
            Protocol.MSG_FC_BATTLE_ATTACK_OK info = Operations.ByteArrayToStruct<Protocol.MSG_FC_BATTLE_ATTACK_OK>(data, ref nBytesUsed);


            Protocol.MSG_FC_BATTLE_ATTACK_FIND attackPacket2 = new Protocol.MSG_FC_BATTLE_ATTACK_FIND();
            attackPacket2.AttackType = info.AttackType; // secondary weapon
            attackPacket2.AttackIndex = info.AttackIndex;
            attackPacket2.ItemNum = info.ItemNum;
            attackPacket2.WeaponIndex = info.WeaponIndex;

            attackPacket2.TargetInfo = info.TargetInfo;
            attackPacket2.TargetInfo.TargetPosition = new Protocol.AVECTOR3();
            SendPacket(Protocol.T_FC_BATTLE_ATTACK_FIND, attackPacket2);
        }
        public void HandleGetOtherInfoOk(byte[] data, int len, ref int nBytesUsed)
        {
            Protocol.MSG_FC_CHARACTER_GET_OTHER_INFO_OK info = Operations.ByteArrayToStruct<Protocol.MSG_FC_CHARACTER_GET_OTHER_INFO_OK>(data, ref nBytesUsed);
            userMgmt.HandleGetOtherInfoOk(info.CharacterInfo);
        }

        public void Shoot(Protocol.MSG_FC_MONSTER_MOVE_OK monTargetInfo)
        {
            Out.WriteLine($"Attacking {monTargetInfo.MonsterIndex} with primary/secondary", "Game");
            Protocol.MSG_FC_BATTLE_ATTACK attackPacket = new Protocol.MSG_FC_BATTLE_ATTACK();
            attackPacket.AttackType = 101; // secondary weapon
            attackPacket.FirePosition = new Protocol.AVECTOR3() { x = 1, y = 0, z = 1 };
            attackPacket.SkillNum = 4589893;
            attackPacket.TargetInfo = new Protocol.MEX_TARGET_INFO()
            {
                TargetIndex = monTargetInfo.MonsterIndex,
                TargetItemFieldIndex = 0,
                TargetPosition = monTargetInfo.PositionVector,
                MultiTargetIndex = 0
            };
            SendPacket(Protocol.T_FC_BATTLE_ATTACK, attackPacket);

            attackPacket.AttackType = 1; // secondary weapon
            SendPacket(Protocol.T_FC_BATTLE_ATTACK, attackPacket);


            if (Program.move)
            {
                usedCharacterInfo.PositionVector = monTargetInfo.PositionVector.ToDXVector();
                usedCharacterInfo.TargetVector = monTargetInfo.TargetVector.ToDXVector();
            }
            
        }

        bool msg = true;
        public void HandleMonMoveOk(byte[] data, int len, ref int nBytesUsed)
        {
            Protocol.MSG_FC_MONSTER_MOVE_OK monMove = Operations.ByteArrayToStruct<Protocol.MSG_FC_MONSTER_MOVE_OK>(data, ref nBytesUsed);
            
            if (userMgmt.otherUsersInMap || userMgmt.gmOnline)
            {
                if (msg)
                {
                    Out.WriteLine("Detected " + (userMgmt.otherUsersInMap ? "user" : "gm") + " in map! Stopping...", Out.GAME, ConsoleColor.DarkGreen);
                    msg = false;
                }

            //    return;
            }

            if (!msg)
            {
                Out.WriteLine("Threat left! Lets continue...", Out.GAME, ConsoleColor.DarkGreen);
                msg = true;
            }

            Shoot(monMove);
            
        }
        public void HandleBodyCondChange(byte[] data, int len, ref int nBytesUsed)
        {
            Protocol.MSG_FC_CHARACTER_CHANGE_BODYCONDITION packet = Operations.ByteArrayToStruct<Protocol.MSG_FC_CHARACTER_CHANGE_BODYCONDITION>(data, ref nBytesUsed);


        }
        public void HandleMoveOKPacket(byte[] data, int len, ref int nBytesUsed)
        {
            Protocol.MSG_FC_MOVE_OK packet = Operations.ByteArrayToStruct<Protocol.MSG_FC_MOVE_OK>(data, ref nBytesUsed);

            if (packet.ClientIndex == usedCharacterInfo.ClientIndex)
            {
                usedCharacterInfo.PositionVector = packet.PositionVector.ToDXVector();
                usedCharacterInfo.UpVector = packet.UpVector.ToDXVector();
                usedCharacterInfo.TargetVector = packet.TargetVector.ToDXVector();
            }
        }

        public void HandleChangeCharacterModeOk(byte[] data, int len, ref int nBytesUsed)
        {
            Protocol.MSG_FC_CHARACTER_CHANGE_CHARACTER_MODE_OK packet = Operations.ByteArrayToStruct<Protocol.MSG_FC_CHARACTER_CHANGE_CHARACTER_MODE_OK>(data.Skip<byte>(nBytesUsed).ToArray<byte>(), new Protocol.MSG_FC_CHARACTER_CHANGE_CHARACTER_MODE_OK().GetType());
            nBytesUsed += Marshal.SizeOf(packet);
            Out.WriteLine("Your mode has been changed to " + ((packet.CharacterMode0 == 1) ? "character." : "gear."), Out.GAME, ConsoleColor.DarkGreen);
            
        }

        public void HandleOutPostInfo(byte[] data, int len, ref int nBytesUsed)
        {
            Protocol.MSG_FC_OUTPOST_WAR_INFO packetHeader = Operations.ByteArrayToStruct<Protocol.MSG_FC_OUTPOST_WAR_INFO>(data.Skip<byte>(nBytesUsed).ToArray<byte>(), new Protocol.MSG_FC_OUTPOST_WAR_INFO().GetType());
            nBytesUsed += Marshal.SizeOf(packetHeader);
            Protocol.SOUTPOST_WAR_INFO[] info = Operations.StructsArrayParse<Protocol.SOUTPOST_WAR_INFO>(data.Skip<byte>(nBytesUsed).ToArray<byte>(), packetHeader.OutPostWarInfoListCount, ref nBytesUsed);
            foreach (Protocol.SOUTPOST_WAR_INFO x in info)
            {
                Out.WriteLine("OutPost running -> " + x.MapIndex, "Info", ConsoleColor.DarkYellow);
            }
        }
        public void HandleQuestMonsterInfo(byte[] data, int len, ref int nBytesUsed)
        {
            Protocol.MSG_FC_QUEST_PUT_ALL_QUEST_MONSTER_COUNT headerPacket = Operations.ByteArrayToStruct<Protocol.MSG_FC_QUEST_PUT_ALL_QUEST_MONSTER_COUNT>(data.Skip<byte>(nBytesUsed).ToArray<byte>(), new Protocol.MSG_FC_QUEST_PUT_ALL_QUEST_MONSTER_COUNT().GetType());
            nBytesUsed += Marshal.SizeOf(nBytesUsed);
            //Protocol.MEX_QUEST_MONSTER_COUNT[] quests = Operations.StructsArrayParse<Protocol.MEX_QUEST_MONSTER_COUNT>(data.Skip<byte>(nBytesUsed).ToArray<byte>(), headerPacket.NumOfMonsterCount, ref nBytesUsed);
            //foreach (Protocol.MEX_QUEST_MONSTER_COUNT x in quests)
            //{
            //    Out.WriteLine("Acquired monsterQuestInfo! -> " + x.QuestIndex, "Info", ConsoleColor.DarkYellow);
            //}
        }
        public void HandleQuestInfo(byte[] data, int len, ref int nBytesUsed)
        {
            Protocol.MSG_FC_QUEST_PUT_ALL_QUEST headerPacket = Operations.ByteArrayToStruct<Protocol.MSG_FC_QUEST_PUT_ALL_QUEST>(data.Skip<byte>(nBytesUsed).ToArray<byte>(), new Protocol.MSG_FC_QUEST_PUT_ALL_QUEST().GetType());
            nBytesUsed += Marshal.SizeOf(nBytesUsed);
            Protocol.MEX_QUEST_INFO[] quests = Operations.StructsArrayParse<Protocol.MEX_QUEST_INFO>(data.Skip<byte>(nBytesUsed).ToArray<byte>(), headerPacket.NumOfQuest, ref nBytesUsed);
            foreach (Protocol.MEX_QUEST_INFO x in quests)
            {
           //     Out.WriteLine("Acquired questInfo! -> " + x.QuestIndex, "Info", ConsoleColor.DarkYellow);
            }
        }
        public void HandleCharacterStatChange(byte[] data, int len, ref int nBytesUsed)
        {

            Protocol.MSG_FC_CHARACTER_CHANGE_TOTALGEAR_STAT packet = Operations.ByteArrayToStruct<Protocol.MSG_FC_CHARACTER_CHANGE_TOTALGEAR_STAT>(data.Skip<byte>(nBytesUsed).ToArray<byte>(), new Protocol.MSG_FC_CHARACTER_CHANGE_TOTALGEAR_STAT().GetType());
            nBytesUsed += Marshal.SizeOf(packet);
            Out.WriteLine(String.Format("Your stats has been changed! ATK[{0}] DEF[{1}] DOD[{2}] FUE[{3}] SHI[{4}] SOU[{5}]", packet.GearStat1.AttackPart,
                packet.GearStat1.DefensePart, packet.GearStat1.DodgePart, packet.GearStat1.FuelPart, packet.GearStat1.ShieldPart, packet.GearStat1.SoulPart), Out.GAME);
        }

        public void HandleCollectionEventInfo(byte[] data, int len, ref int nBytesUsed)
        {
            Protocol.MSG_FC_COLLECTION_MONTHLY_AROMOR_EVENT_INFO packet = Operations.ByteArrayToStruct<Protocol.MSG_FC_COLLECTION_MONTHLY_AROMOR_EVENT_INFO>(data.Skip<byte>(nBytesUsed).ToArray<byte>(), new Protocol.MSG_FC_COLLECTION_MONTHLY_AROMOR_EVENT_INFO().GetType());
            nBytesUsed += Marshal.SizeOf(packet);
            Out.WriteLine("Received collection event info -> " + packet.CollectionShapeNum, "Item", ConsoleColor.DarkYellow);
        }
        public void HandlePutCollectionInfo(byte[] data, int len, ref int nBytesUsed)
        {
            Protocol.COLLECTION_INFO packet = Operations.ByteArrayToStruct<Protocol.COLLECTION_INFO>(data.Skip<byte>(nBytesUsed).ToArray<byte>(), new Protocol.COLLECTION_INFO().GetType());
            nBytesUsed += Marshal.SizeOf(new Protocol.COLLECTION_INFO().GetType());
            Out.WriteLine("Received armor collection info -> " + packet.CollectionType, "Item", ConsoleColor.DarkYellow);
        }
        public void HandlePutItemDone(byte[] data, int len, ref int nBytesUsed)
        {
            Protocol.MSG_FC_STORE_PUT_ITEM_DONE packet = Operations.ByteArrayToStruct<Protocol.MSG_FC_STORE_PUT_ITEM_DONE>(data.Skip<byte>(nBytesUsed).ToArray<byte>(), new Protocol.MSG_FC_STORE_PUT_ITEM_DONE().GetType());
            nBytesUsed += Marshal.SizeOf(packet);
            Out.WriteLine(String.Format("Inventory items acquired! [{0}]", packet.NumOfItem), "Item", ConsoleColor.DarkGreen);
        }

        public void HandleEnchantInsert(byte[] data, int len, ref int nBytesUsed)
        {
            Protocol.MSG_FC_ITEM_PUT_ENCHANT packet = Operations.ByteArrayToStruct<Protocol.MSG_FC_ITEM_PUT_ENCHANT>(data.Skip<byte>(nBytesUsed).ToArray<byte>(), new Protocol.MSG_FC_ITEM_PUT_ENCHANT().GetType());
            nBytesUsed += Marshal.SizeOf(packet);
            Out.WriteLine(String.Format("Acquired enchant info: E{0} -> {1}:{2}", packet.Enchant.EnchantItemNum, packet.Enchant.TargetItemNum, packet.Enchant.TargetItemUniqueNumber), "Item", ConsoleColor.DarkYellow);
        }
        public void HandleStoreItemHeader(byte[] data, int len, ref int nBytesUsed)
        {
            Protocols.Protocol.MSG_FC_STORE_PUT_ITEM_HEADER packet = Operations.ByteArrayToStruct<Protocol.MSG_FC_STORE_PUT_ITEM_HEADER>(data.Skip<byte>(nBytesUsed).ToArray<byte>(), new Protocol.MSG_FC_STORE_PUT_ITEM_HEADER().GetType());
            nBytesUsed += Marshal.SizeOf(packet);
            Out.WriteLine("Starting acquiring inventory info...", "Item", ConsoleColor.DarkYellow);
        }
        public void HandleDisplayInfo(byte[] data, int len, ref int nBytesUsed)
        {
            Protocol.MSG_FC_INFO_MSWARINFO_DISPLAY_OPTION_OK packet = new Protocol.MSG_FC_INFO_MSWARINFO_DISPLAY_OPTION_OK();
            nBytesUsed += Marshal.SizeOf(packet);
        }

        public void HandleParamChange(byte[] data, int len, ref int nBytesUsed)
        {
            Protocol.MSG_FC_CHARACTER_CHANGE_CURRENTHPDPSPEP packet = Operations.ByteArrayToStruct<Protocol.MSG_FC_CHARACTER_CHANGE_CURRENTHPDPSPEP>(data.Skip<byte>(nBytesUsed).ToArray<byte>(), new Protocol.MSG_FC_CHARACTER_CHANGE_CURRENTHPDPSPEP().GetType());
            nBytesUsed += Marshal.SizeOf(packet);

            Out.WriteLine(String.Format("Your params has been changed: DP[{0}] SP[{1}] HP[{2}] EP[{3}]", packet.CurrentDP, packet.CurrentSP, packet.CurrentHP, packet.CurrentEP), "Info", ConsoleColor.DarkYellow);
          //   Refill();
            if(packet.CurrentHP == 0)
            {
                Protocol.MSG_FC_CHARACTER_DEAD_GAMESTART reborn = new Protocol.MSG_FC_CHARACTER_DEAD_GAMESTART();
                reborn.bRebirthInCityMap = false;
                reborn.CharacterUniqueNumber = usedCharacterInfo.CharacterUniqueNumber;
                reborn.ClientIndex = usedCharacterInfo.ClientIndex;
                SendPacket(Protocol.T_FC_CHARACTER_DEAD_GAMESTART, reborn);

                Out.WriteLine("Character dead! Respawning...", Out.GAME);
                Thread.Sleep(1000);
                this.Disconnect(PublicCommonDefines.DisconnectionReason.SOCKET_CLOSED_BY_CLIENT);
            }
        }
        public void HandleEXPChange(byte[] data, int len, ref int nBytesUsed)
        {
            Protocol.MSG_FC_CHARACTER_CHANGE_EXP packet = Operations.ByteArrayToStruct<Protocol.MSG_FC_CHARACTER_CHANGE_EXP>(data.Skip<byte>(nBytesUsed).ToArray<byte>(), new Protocol.MSG_FC_CHARACTER_CHANGE_EXP().GetType());
            nBytesUsed += Marshal.SizeOf(packet);

           // Out.WriteLine(("Your experience has been changed: " + packet.Experience), "Info", ConsoleColor.DarkYellow);
            usedCharacterInfo.Experience = packet.Experience;
        }

        public void Refill()
        {

            foreach (var entry in AceData.mapPads)
            {
                if (entry.midx == usedCharacterInfo.MapChannelIndex.MapIndex)
                {
                    SendPacket(0x3617, (int)entry.bidx);
                }
            }
            Protocols.Protocol.MSG_FC_SHOP_REQUEST_REPAIR repair = new Protocols.Protocol.MSG_FC_SHOP_REQUEST_REPAIR();
            repair.Count = int.MaxValue;

            foreach(int param in Protocol.refillOptions)
            {
                repair.DesParam = param;
                SendPacket(Protocol.T_FC_SHOP_REQUEST_REPAIR, repair);
            }
            Out.WriteLine("Character refilled!", Out.GAME);
        }
        public void HandleCharacterGameStartOk(byte[] data, int len, ref int nBytesUsed)
        {
            Protocol.MSG_FC_CHARACTER_GAMESTART_OK packet = Operations.ByteArrayToStruct<Protocol.MSG_FC_CHARACTER_GAMESTART_OK>(data.Skip<byte>(nBytesUsed).ToArray<byte>(), new Protocol.MSG_FC_CHARACTER_GAMESTART_OK().GetType());
            nBytesUsed += Marshal.SizeOf(packet);

            Out.WriteLine("");
            Out.WriteLine("");
            Out.WriteLine("");
            Out.WriteLine("");
            Out.WriteLine("");
            Out.WriteLine(" #### Character Joined In-Game ####", "Info", ConsoleColor.DarkMagenta);
            Out.WriteLine("");
            Out.WriteLine("************************************", "Info", ConsoleColor.DarkMagenta);
            Out.WriteLine("GameServer: " + new string(packet.ServerGroupName0).Replace("\0", ""), "Info", ConsoleColor.DarkMagenta);
            Out.WriteLine("Game Publisher: " + new string(packet.GamePublisher).Replace("\0", ""), "Info", ConsoleColor.DarkMagenta);
            Out.WriteLine("ServerName: " + new string(packet.MainORTestServerName).Replace("\0", ""), "Info", ConsoleColor.DarkMagenta);
            Out.WriteLine("FieldServer: " + new string(packet.FieldServerIP).Replace("\0", "") + ":" + packet.FieldServerPort, "Info", ConsoleColor.DarkMagenta);
            Out.WriteLine(String.Format("Position: X[{0}] Y[{1}] Z[{2}]", packet.PositionVector.x, packet.PositionVector.y, packet.PositionVector.z), "Info", ConsoleColor.DarkMagenta);
            Out.WriteLine(String.Format("Map: {0}({1})", packet.MapInfo.MapIndex, packet.MapInfo.ChannelIndex), "Info", ConsoleColor.DarkMagenta);
            Out.WriteLine("Weather: " + Convert.ToString(packet.MapWeather, 16), "Info", ConsoleColor.DarkMagenta);
            Out.WriteLine("BodyCondition: " + Convert.ToString((long)packet.BodyCondition, 2), "Info", ConsoleColor.DarkMagenta);
            Out.WriteLine("Mode: " + (packet.CharacterMode0 == 1 ? "Character" : "Gear"), "Info", ConsoleColor.DarkMagenta);
            Out.WriteLine("************************************", "Info", ConsoleColor.DarkMagenta);
            Out.WriteLine("");
            Out.WriteLine("");
            Out.WriteLine("");
            Out.WriteLine("");

            usedCharacterInfo.PositionVector = packet.PositionVector.ToDXVector();

            usedCharacterInfo.BodyCondition = packet.BodyCondition;
            usedCharacterInfo.CharacterMode = packet.CharacterMode0;
            usedCharacterInfo.MapChannelIndex = packet.MapInfo;
            userMgmt = new UsersManagement(this);



            new Thread(() =>
            {
                while (true)
                {
                    SendMovePacket(usedCharacterInfo.PositionVector.ConvertToAVEC());
                    Thread.Sleep(100);
                }
            }).Start();


        }

        public void HandleWarpSameMap(byte[] data, int len, ref int nBytesUsed)
        {
            Protocol.MSG_FC_EVENT_WARP_SAME_FIELD_SERVER packet = Operations.ByteArrayToStruct<Protocol.MSG_FC_EVENT_WARP_SAME_FIELD_SERVER>(data, ref nBytesUsed);
            SendPacket(Protocol.T_FC_EVENT_WARP_SAME_FIELD_SERVER + 1);
            string mapName = "Unknwon";
            foreach (var entry in AceData.mapInfos)
            {
                if(entry.mapIndex == packet.MapChannelIndex.MapIndex)
                {
                    mapName = entry.mapName;
                    break;
                }
            }
            usedCharacterInfo.MapChannelIndex.MapIndex = packet.MapChannelIndex.MapIndex;
            usedCharacterInfo.MapChannelIndex.ChannelIndex = packet.MapChannelIndex.ChannelIndex;
            userMgmt.otherUsersInMap = true;
            Out.WriteLine("You've been warped successfully to " + mapName, Out.GAME, ConsoleColor.Green);
        }

        public void HandleGetCharacterOk(byte[] data, int len, ref int nBytesUsed)
        {
            Protocol.MSG_FC_CHARACTER_GET_CHARACTER_OK packet = Operations.ByteArrayToStruct<Protocol.MSG_FC_CHARACTER_GET_CHARACTER_OK>(data.Skip<byte>(nBytesUsed).ToArray<byte>(), new Protocol.MSG_FC_CHARACTER_GET_CHARACTER_OK().GetType());
            nBytesUsed += Marshal.SizeOf(packet);
            usedCharacterInfo = packet.characterInfo;

            if(len - Marshal.SizeOf(packet) > 0)
            {
                Out.WriteLine("Err: " + Marshal.SizeOf(packet) + " " + len);
            }


            Protocol.MSG_FC_CHARACTER_GAMESTART gamestart = new Protocol.MSG_FC_CHARACTER_GAMESTART();
            gamestart.ClientIndex = usedCharacterInfo.ClientIndex;
            gamestart.CharacterUniqueNumber = usedCharacter;
            SendPacket(Protocol.T_FC_CHARACTER_GAMESTART, gamestart);
        }
        public void HandleLoginOk(byte[] data, int len, ref int nBytesUsed)
        {
            Protocol.MSG_FC_CONNECT_LOGIN_OK loginok = Operations.ByteArrayToStruct<Protocol.MSG_FC_CONNECT_LOGIN_OK>(data.Skip<byte>(nBytesUsed).ToArray<byte>(), new Protocol.MSG_FC_CONNECT_LOGIN_OK().GetType());
            nBytesUsed += Marshal.SizeOf(loginok);
            Out.WriteLine("");
            Out.WriteLine("");
            Out.WriteLine("");
            Out.WriteLine(" ### Received Login Info ### ", "Info", ConsoleColor.DarkRed);
            Out.WriteLine("");
            accountUniqueNumber = loginok.AccountUniqueNumber;
            for (int i = 0; i < loginok.NumCharacters; i++)
            {
                Out.WriteLine("     [" + loginok.Characters[i].CharacterUniqueNumber + "] " + new string(loginok.Characters[i].CharacterName).Replace("\0", ""), "Info", ConsoleColor.DarkRed);
            }

            usedCharacter = loginok.Characters[0].CharacterUniqueNumber;

            Protocol.MSG_FC_CHARACTER_GET_CHARACTER getChar = new Protocol.MSG_FC_CHARACTER_GET_CHARACTER();
            getChar.AccountUniqueNumber = accountUniqueNumber;
            getChar.CharacterUniqueNumber = usedCharacter;
            getChar.shutdownmins = 0;
            SendPacket(Protocol.T_FC_CHARACTER_GET_CHARACTER, getChar);

            
        }

        public void MoveToMap(ushort mapIdx)
        {
            if(mapIdx == usedCharacterInfo.MapChannelIndex.MapIndex)
            {
                Out.WriteLine("You're already in this map!", Out.ERROR);
            }
            foreach (var entry in AceData.mapInfos)
            {
                if(entry.mapIndex == mapIdx)
                {
                    Out.WriteLine("Sending warp request for " + entry.mapName, Out.GAME);
                    Protocol.MSG_FC_EVENT_SELECT_CHANNEL warp = new Protocol.MSG_FC_EVENT_SELECT_CHANNEL();
                    warp.MapChannelIndex = new Protocol.MAP_CHANNEL_INDEX() { ChannelIndex = 0, MapIndex = mapIdx };
                    warp.WarpTargetIndex = mapIdx;
                    warp.ClientIndex = usedCharacterInfo.ClientIndex;
                    SendPacket(Protocol.T_FC_EVENT_SELECT_CHANNEL, warp);


                    return;
                }
            }

            Out.WriteLine("No such map found for " + mapIdx + " index!", Out.ERROR);
        }
    }
}
