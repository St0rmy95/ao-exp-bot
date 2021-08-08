using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
namespace Protocols
{
    public class BodyConditionTable
    {
        public enum BodyCondition : ulong
        {
            BODYCON_SET_OR_CLEAR_MASK = (ulong)0x0000000000000001,
            BODYCON_FLY_MASK = (ulong)0x0000000000000002,
            BODYCON_LANDING_MASK = (ulong)0x0000000000000040,
            BODYCON_LANDED_MASK = (ulong)0x0000000000000004,
            BODYCON_DEAD_MASK = (ulong)0x0000000000000008,
            BODYCON_NOT_USED1_MASK = (ulong)0x0000000000000010,
            BODYCON_WEAPON_POSITION_MASK = (ulong)0x0000000000000020,
            BODYCON_BOOSTER1_MASK = (ulong)0x0000000000000080,
            BODYCON_BOOSTER2_MASK = (ulong)0x0000000000000100,
            BODYCON_BOOSTER3_MASK = (ulong)0x0000000000000200,
            BODYCON_BOOSTER4_MASK = (ulong)0x0000000000000400,
            BODYCON_NIGHTFLY_MASK = (ulong)0x0000000000000800,
            BODYCON_EXPLOSION_MASK = (ulong)0x0000000000001000,
            BODYCON_DAMAGE1_MASK = (ulong)0x0000000000002000,
            BODYCON_DAMAGE2_MASK = (ulong)0x0000000000004000,
            BODYCON_DAMAGE3_MASK = (ulong)0x0000000000008000,
            BODYCON_CREATION_MASK = (ulong)0x0000000000010000,
            BODYCON_FIRE_MASK = (ulong)0x0000000000020000,
            BODYCON_BULLET_MASK = (ulong)0x0000000000040000,
            BODYCON_HIT_MASK = (ulong)0x0000000000080000,
            RADAR_BODYCON_BOOSTER_ON = (ulong)0x0000000000000008,
            RADAR_BODYCON_BOOSTER_OFF = (ulong)0x0000000000000002,
            BODYCON_SPRAY_MASK = (ulong)0x0000000000100000,
            BODYCON_APRE_MASK = (ulong)0x0000000000200000,
            BODYCON_SHAKEING_MASK = (ulong)0x0000000004000000,
            BODYCON_FREEZING_MASK = (ulong)0x0000000008000000,
            BODYCON_ATTACKSLOW_MASK = (ulong)0x0000000010000000,
            BODYCON_SLOW_MASK = (ulong)0x0000000020000000,
            BODYCON_NOATTACK_MASK = (ulong)0x0000000040000000,
            BODYCON_ILLUSION_MASK = (ulong)0x0000000080000000,
            BODYCON_FIGHTING_MASK = (ulong)0x0000000100000000,
            BODYCON_EVENT_HANDLE_MASK = (ulong)0x0000000200000000,
            BODYCON_STOP_MASK = (ulong)0x0000000400000000,
            BODYCON_STEALTH_MASK = (ulong)0x0000000800000000,
            BODYCON_GHOST_MASK = (ulong)0x0000001000000000,
            BODYCON_CLOAKING_MASK = (ulong)0x0000002000000000,
            BODYCON_FALL_MASK = (ulong)0x0000004000000000,
            BODYCON_SKILLBAN_MASK = (ulong)0x0000008000000000,
            BODYCON_RESIST_MASK = (ulong)0x0000010000000000,
            BODYCON_SHIELD_MASK = (ulong)0x0000020000000000,
            BODYCON_BOOSTER5_MASK = (ulong)0x0000040000000000,
            BODYCON_TAKEOFF_MASK = (ulong)0x0000080000000000,
            BODYCON_SIEGE_ON_MASK = (ulong)0x0000100000000000,
            BODYCON_SIEGE_OFF_MASK = (ulong)0x0000200000000000,
            BODYCON_DECOY_MASK = (ulong)0x0000400000000000,
            BODYCON_CHARACTER_MODE_STOP = (ulong)0x0001000000000000,
            BODYCON_CHARACTER_MODE_WALK = (ulong)0x0002000000000000,
            BODYCON_CHARACTER_MODE_RUN = (ulong)0x0004000000000000,
            BODYCON_EVENT_SELECTCHANNEL_MASK = (ulong)0x0008000000000000,
            BODYCON_ROLLING_MASK = (ulong)0x0010000000000000,
            BODYCON_PET_POSITION_MASK = (ulong)0x0020000000000000,
            BODYCON_MARK_POSITION_MASK = (ulong)0x0040000000000000,
            BODYCON_MON_PREATTACK1_MASK = (ulong)0x0000000000100000,
            BODYCON_MON_FIREATTACK1_MASK = (ulong)0x0000000000200000,
            BODYCON_MON_PREATTACK2_MASK = (ulong)0x0000000000400000,
            BODYCON_MON_FIREATTACK2_MASK = (ulong)0x0000000000800000,
            BODYCON_MON_PREATTACK3_MASK = (ulong)0x0000000001000000,
            BODYCON_MON_FIREATTACK3_MASK = (ulong)0x0000000002000000,
            BODYCON_MON_PREATTACK4_MASK = (ulong)0x0000000004000000,
            BODYCON_MON_FIREATTACK4_MASK = (ulong)0x0000000008000000,
            BODYCON_MON_PREATTACK5_MASK = (ulong)0x0000000010000000,
            BODYCON_MON_FIREATTACK5_MASK = (ulong)0x0000000020000000,
            BODYCON_MON_PRECONTROLSKILL_MASK = (ulong)0x0000000040000000,
            BODYCON_MON_FIRECONTROLSKILL_MASK = (ulong)0x0000000080000000,
            BODYCON_MON_AUTODESTROYED_MASK = (ulong)0x0000000100000000,
            BODYCON_MON_BUILDING = (ulong)0x0000001000000000,
            BODYCON_MON_BUILDED = (ulong)0x0000002000000000,
            BODYCON_MON_PREATTACK6_MASK = (ulong)0x0000010000000000,
            BODYCON_MON_FIREATTACK6_MASK = (ulong)0x0000020000000000,
            BODYCON_MON_PREATTACK7_MASK = (ulong)0x0000040000000000,
            BODYCON_MON_FIREATTACK7_MASK = (ulong)0x0000080000000000,
            BODYCON_MON_PREATTACK8_MASK = (ulong)0x0000100000000000,
            BODYCON_MON_FIREATTACK8_MASK = (ulong)0x0000200000000000,
            BODYCON_MON_PREATTACK9_MASK = (ulong)0x0000400000000000,
            BODYCON_MON_FIREATTACK9_MASK = (ulong)0x0000800000000000,
            BODYCON_MON_PREATTACK10_MASK = (ulong)0x0001000000000000,
            BODYCON_MON_FIREATTACK10_MASK = (ulong)0x0002000000000000,
            BODYCON_MON_PREATTACK11_MASK = (ulong)0x0004000000000000,
            BODYCON_MON_FIREATTACK11_MASK = (ulong)0x0008000000000000,
            BODYCON_MON_PREATTACK12_MASK = (ulong)0x0010000000000000,
            BODYCON_MON_FIREATTACK12_MASK = (ulong)0x0020000000000000,
            BODYCON_MON_PREATTACK13_MASK = (ulong)0x0040000000000000,
            BODYCON_MON_FIREATTACK13_MASK = (ulong)0x0080000000000000,
            BODYCON_MON_PREATTACK14_MASK = (ulong)0x0100000000000000,
            BODYCON_MON_FIREATTACK14_MASK = (ulong)0x0200000000000000,
            BODYCON_MON_PREATTACK15_MASK = (ulong)0x0400000000000000,
            BODYCON_MON_FIREATTACK15_MASK = (ulong)0x0800000000000000,
            BODYCON_SHIELD_ON_MASK = BODYCON_FIRE_MASK,
            BODYCON_SHIELD_OFF_MASK = BODYCON_HIT_MASK,
            BODYCON_SHIELD_ING_MASK = BODYCON_BULLET_MASK,
            BODYCON_CHARGING_MASK = BODYCON_LANDED_MASK,
            BODYCON_USE_SKILL_MASK = BODYCON_FLY_MASK,
            BODYCON_TARGET_USE_SKILL_MASK = BODYCON_DEAD_MASK,


            BODYCON_MON_ATTACK1_MASK = (ulong)(BODYCON_MON_PREATTACK1_MASK | BODYCON_MON_FIREATTACK1_MASK),
            BODYCON_MON_ATTACK2_MASK = (ulong)(BODYCON_MON_PREATTACK2_MASK | BODYCON_MON_FIREATTACK2_MASK),
            BODYCON_MON_ATTACK3_MASK = (ulong)(BODYCON_MON_PREATTACK3_MASK | BODYCON_MON_FIREATTACK3_MASK),
            BODYCON_MON_ATTACK4_MASK = (ulong)(BODYCON_MON_PREATTACK4_MASK | BODYCON_MON_FIREATTACK4_MASK),
            BODYCON_MON_ATTACK5_MASK = (ulong)(BODYCON_MON_PREATTACK5_MASK | BODYCON_MON_FIREATTACK5_MASK),
            BODYCON_MON_ATTACK6_MASK = (ulong)(BODYCON_MON_PREATTACK6_MASK | BODYCON_MON_FIREATTACK6_MASK),
            BODYCON_MON_ATTACK7_MASK = (ulong)(BODYCON_MON_PREATTACK7_MASK | BODYCON_MON_FIREATTACK7_MASK),
            BODYCON_MON_ATTACK8_MASK = (ulong)(BODYCON_MON_PREATTACK8_MASK | BODYCON_MON_FIREATTACK8_MASK),
            BODYCON_MON_ATTACK9_MASK = (ulong)(BODYCON_MON_PREATTACK9_MASK | BODYCON_MON_FIREATTACK9_MASK),
            BODYCON_MON_ATTACK10_MASK = (ulong)(BODYCON_MON_PREATTACK10_MASK | BODYCON_MON_FIREATTACK10_MASK),
            BODYCON_MON_ATTACK11_MASK = (ulong)(BODYCON_MON_PREATTACK11_MASK | BODYCON_MON_FIREATTACK11_MASK),
            BODYCON_MON_ATTACK12_MASK = (ulong)(BODYCON_MON_PREATTACK12_MASK | BODYCON_MON_FIREATTACK12_MASK),
            BODYCON_MON_ATTACK13_MASK = (ulong)(BODYCON_MON_PREATTACK13_MASK | BODYCON_MON_FIREATTACK13_MASK),
            BODYCON_MON_ATTACK14_MASK = (ulong)(BODYCON_MON_PREATTACK14_MASK | BODYCON_MON_FIREATTACK14_MASK),
            BODYCON_MON_ATTACK15_MASK = (ulong)(BODYCON_MON_PREATTACK15_MASK | BODYCON_MON_FIREATTACK15_MASK),
            BODYCON_MON_CONTROLSKILL_MASK = (ulong)(BODYCON_MON_PRECONTROLSKILL_MASK | BODYCON_MON_FIRECONTROLSKILL_MASK),




            BODYCON_MON_PREATTACK_ALL_MASK = (ulong)(BODYCON_MON_PREATTACK1_MASK | BODYCON_MON_PREATTACK2_MASK
                                                       | BODYCON_MON_PREATTACK3_MASK | BODYCON_MON_PREATTACK4_MASK
                                                       | BODYCON_MON_PREATTACK5_MASK | BODYCON_MON_PREATTACK6_MASK
                                                       | BODYCON_MON_PREATTACK7_MASK | BODYCON_MON_PREATTACK8_MASK
                                                       | BODYCON_MON_PREATTACK9_MASK | BODYCON_MON_PREATTACK10_MASK
                                                       | BODYCON_MON_PREATTACK11_MASK | BODYCON_MON_PREATTACK12_MASK
                                                       | BODYCON_MON_PREATTACK13_MASK | BODYCON_MON_PREATTACK14_MASK
                                                       | BODYCON_MON_PREATTACK15_MASK | BODYCON_MON_PRECONTROLSKILL_MASK),
            BODYCON_MON_FIREATTACK_ALL_MASK = (ulong)(BODYCON_MON_FIREATTACK1_MASK | BODYCON_MON_FIREATTACK2_MASK
                                                       | BODYCON_MON_FIREATTACK3_MASK | BODYCON_MON_FIREATTACK4_MASK
                                                       | BODYCON_MON_FIREATTACK5_MASK | BODYCON_MON_FIREATTACK6_MASK
                                                       | BODYCON_MON_FIREATTACK7_MASK | BODYCON_MON_FIREATTACK8_MASK
                                                       | BODYCON_MON_FIREATTACK9_MASK | BODYCON_MON_FIREATTACK10_MASK
                                                       | BODYCON_MON_FIREATTACK11_MASK | BODYCON_MON_FIREATTACK12_MASK
                                                       | BODYCON_MON_FIREATTACK13_MASK | BODYCON_MON_FIREATTACK14_MASK
                                                       | BODYCON_MON_FIREATTACK15_MASK | BODYCON_MON_FIRECONTROLSKILL_MASK),

            BODYCON_MON_ATTACKALL_MASK = (ulong)(BODYCON_MON_ATTACK1_MASK | BODYCON_MON_ATTACK2_MASK
                                                       | BODYCON_MON_ATTACK3_MASK | BODYCON_MON_ATTACK4_MASK
                                                       | BODYCON_MON_ATTACK5_MASK | BODYCON_MON_ATTACK6_MASK
                                                       | BODYCON_MON_ATTACK7_MASK | BODYCON_MON_ATTACK8_MASK
                                                       | BODYCON_MON_ATTACK9_MASK | BODYCON_MON_ATTACK10_MASK
                                                       | BODYCON_MON_ATTACK11_MASK | BODYCON_MON_ATTACK12_MASK
                                                       | BODYCON_MON_ATTACK13_MASK | BODYCON_MON_ATTACK14_MASK
                                                       | BODYCON_MON_ATTACK15_MASK | BODYCON_MON_CONTROLSKILL_MASK),
            BODYCON_EX_STATE_CLEAR_MASK = (ulong)(BODYCON_FLY_MASK | BODYCON_LANDING_MASK
                                                       | BODYCON_LANDED_MASK | BODYCON_DEAD_MASK
                                                       | BODYCON_NOT_USED1_MASK | BODYCON_CHARACTER_MODE_STOP
                                                       | BODYCON_CHARACTER_MODE_WALK | BODYCON_CHARACTER_MODE_RUN),

            BODYCON_BOOSTER_EX_STATE_CLEAR_MASK = (ulong)(BODYCON_BOOSTER1_MASK | BODYCON_BOOSTER2_MASK
                                                       | BODYCON_BOOSTER3_MASK | BODYCON_BOOSTER4_MASK | BODYCON_BOOSTER5_MASK
                                                       | BODYCON_SIEGE_ON_MASK | BODYCON_SIEGE_OFF_MASK),

            BODYCON_CHARACTER_ANIMATION_TIME = (ulong)(BODYCON_BOOSTER_EX_STATE_CLEAR_MASK
                                                       | BODYCON_LANDING_MASK
                                                       | BODYCON_LANDED_MASK
                                                       | BODYCON_DEAD_MASK
                                                       | BODYCON_NOT_USED1_MASK
                                                       | BODYCON_ROLLING_MASK),
            BODYCON_KEEPING_MASK = (ulong)(BODYCON_EX_STATE_CLEAR_MASK),
            BODYCON_CHARACTER_MODE_MASK = (ulong)(BODYCON_CHARACTER_MODE_STOP | BODYCON_CHARACTER_MODE_WALK | BODYCON_CHARACTER_MODE_RUN),
            BODYCON_MON_BUILD_MASK = (ulong)(BODYCON_MON_BUILDING | BODYCON_MON_BUILDED),
        }
        public static void ClearBodyConditionBit(ref ulong Var, BodyCondition Mask) { Var &= ~((ulong)Mask); }
        public static void SetBodyConditionBit(ref ulong Var, BodyCondition Mask)
        {
            if ((Mask & BodyCondition.BODYCON_EX_STATE_CLEAR_MASK) != 0)
            {
                ClearBodyConditionBit(ref Var, BodyCondition.BODYCON_EX_STATE_CLEAR_MASK);
            }

            if ((Mask & BodyCondition.BODYCON_BOOSTER_EX_STATE_CLEAR_MASK) != 0)
            {
                ClearBodyConditionBit(ref Var, BodyCondition.BODYCON_BOOSTER_EX_STATE_CLEAR_MASK);
            }
            ulong s = (ulong)Mask;
            Var |= s;
        }
    }
    public class Protocol
    {
        public const ushort T_PC_CONNECT_LOGIN = (ushort)((0x10 << 8) | 0x08);
        public const ushort T_PC_CONNECT_GET_SERVER_GROUP_LIST = (ushort)((0x10 << 8) | 0x13);
        public const ushort T_PC_CONNECT_GET_SERVER_GROUP_LIST_OK = (ushort)((0x10 << 8) | 0x14);
        public const ushort T_PC_CONNECT_SINGLE_FILE_VERSION_CHECK = (ushort)((0x10 << 8) | 0x10);
        public const ushort T_PC_CONNECT_SINGLE_FILE_VERSION_CHECK_OK = (ushort)((0x10 << 8) | 0x11);
        public const ushort T_PC_CONNECT_SINGLE_FILE_UPDATE_INFO = (ushort)((0x10 << 8) | 0x12);
        public const ushort T_PC_CONNECT_VERSION = (ushort)((0x10 << 8) | 0x04);
        public const ushort T_PC_CONNECT_REINSTALL_CLIENT = (ushort)((0x10 << 8) | 0x07);
        public const ushort T_PC_CONNECT_UPDATE_INFO = (ushort)((0x10 << 8) | 0x05);
        public const ushort T_PC_CONNECT_VERSION_OK = (ushort)((0x10 << 8) | 0x06);
        public const ushort T_PC_CONNECT_LOGIN_BLOCKED = (ushort)((0x10 << 8) | 0xF0);
        public const ushort T_PC_CONNECT_LOGIN_OK = (ushort)((0x10 << 8) | 0x09);
        public const ushort T_PC_CONNECT_ALIVE = (ushort)((0x10 << 8) | 0x03);
        public const ushort T_ERROR = (ushort)((0xFE << 8) | 0x00);

        public const ushort T_FC_CONNECT_CLOSE = (ushort)((0x31 << 8) | 0x02);
        public const ushort T_FC_CONNECT_ALIVE = (ushort)((0x31 << 8) | 0x03);
        public const ushort T_FC_CONNECT_LOGIN = (ushort)((0x31 << 8) | 0x04);
        public const ushort T_FC_CONNECT_LOGIN_OK = (ushort)((0x31 << 8) | 0x05);
        public const ushort T_FC_CONNECT_SYNC_TIME = (ushort)((0x31 << 8) | 0x06);
        public const ushort T_FC_CONNECT_NOTIFY_SERVER_SHUTDOWN = (ushort)((0x31 << 8) | 0x07);
        public const ushort T_FC_CONNECT_NETWORK_CHECK = (ushort)((0x31 << 8) | 0x08);
        public const ushort T_FC_CONNECT_NETWORK_CHECK_OK = (ushort)((0x31 << 8) | 0x09);
        public const ushort T_FC_CONNECT_ARENASERVER_INFO = (ushort)((0x31 << 8) | 0x0B);
        public const ushort T_FC_CHARACTER_DELETE = (ushort)((0x32 << 8) | 0x02);
        public const ushort T_FC_CHARACTER_CONNECT_GAMESTART = (ushort)((0x32 << 8) | 0x08);
        public const ushort T_FC_CHARACTER_CONNECT_GAMESTART_OK = (ushort)((0x32 << 8) | 0x09);
        public const ushort T_FC_CHARACTER_GET_CHARACTER = (ushort)((0x32 << 8) | 0x04);
        public const ushort T_FC_CHARACTER_GET_CHARACTER_OK = (ushort)((0x32 << 8) | 0x05);
        public const ushort T_FC_CHARACTER_GAMESTART = (ushort)((0x32 << 8) | 0x06);
        public const ushort T_FC_CHARACTER_GAMESTART_OK = (ushort)((0x32 << 8) | 0x07);
        public const ushort T_FC_CHARACTER_CHANGE_EXP = (ushort)((0x32 << 8) | 0x19);
        public const ushort T_FC_CHARACTER_CHANGE_CURRENTHPDPSPEP = (ushort)((0x32 << 8) | 0x1F);
        public const ushort T_FC_CHARACTER_GET_OTHER_INFO = (ushort)((0x32 << 8) | 0x12);
        public const ushort T_FC_CHARACTER_GET_OTHER_INFO_OK = (ushort)((0x32 << 8) | 0x13);
        public const ushort T_FC_CITY_GET_BUILDING_LIST = (ushort)((0x34 << 8) | 0x00);
        public const ushort T_FC_COLLECTION_INFO = (ushort)((0xD2 << 8) | 0x00);
        public const ushort T_FC_INFO_MSWARINFO_DISPLAY_OPTION_OK = (ushort)((0x38 << 8) | 0x4A);
        public const ushort T_FC_STRING_128 = (ushort)((0x43 << 8) | 0x01);
        public const ushort T_FC_STRING_256 = (ushort)((0x43 << 8) | 0x02);
        public const ushort T_FC_STRING_512 = (ushort)((0x43 << 8) | 0x03);
        public const ushort T_FC_WORLD_NOTIFICATION = (ushort)((0x43 << 8) | 0x04);
        public const ushort T_FC_MOVE_OK = (ushort)((0x3B << 8) | 0x01);
        public const ushort T_FC_ITEM_PET_HEADER = (ushort)((0x23 << 8) | 0x49);
        public const ushort T_FC_ITEM_PET_DONE = (ushort)((0x23 << 8) | 0x54);
        public const ushort T_FC_STORE_PUT_ITEM_HEADER = (ushort)((0x24 << 8) | 0x01);
        public const ushort T_FC_COLLECTION_MONTHLY_AROMOR_EVENT_INIT = (ushort)((0xD2 << 8) | 0x02);
        public const ushort T_FC_COLLECTION_MONTHLY_AROMOR_EVENT_INFO = (ushort)((0xD2 << 8) | 0x03);
        public const ushort T_FC_CHARACTER_CHANGE_TOTALGEAR_STAT = (ushort)((0x22 << 8) | 0x17);
        public const ushort T_FC_QUEST_PUT_ALL_QUEST_HEADER = (ushort)((0x3D << 8) | 0x06);
        public const ushort T_FC_QUEST_PUT_ALL_QUEST = (ushort)((0x3D << 8) | 0x07);
        public const ushort T_FC_QUEST_PUT_ALL_QUEST_DONE = (ushort)((0x3D << 8) | 0x08);
        public const ushort T_FC_QUEST_PUT_ALL_QUEST_MONSTER_COUNT_HEADER = (ushort)((0x3D << 8) | 0x0C);
        public const ushort T_FC_QUEST_PUT_ALL_QUEST_MONSTER_COUNT = (ushort)((0x3D << 8) | 0x0D);
        public const ushort T_FC_QUEST_PUT_ALL_QUEST_MONSTER_COUNT_DONE = (ushort)((0x3D << 8) | 0x0E);
        public const ushort T_FC_CHARACTER_CHANGE_BODYCONDITION = (ushort)((0x22 << 8) | 0x1A);
        public const ushort T_FC_CHARACTER_CHANGE_CHARACTER_MODE = (ushort)((0x22 << 8) | 0x50);
        public const ushort T_FC_CHARACTER_CHANGE_CHARACTER_MODE_OK = (ushort)((0x22 << 8) | 0x51);
        public const ushort T_FC_MOVE = (ushort)((0x3B << 8) | 0x00);
        public const ushort T_FC_EVENT_NOTIFY_MSG_GET = (ushort)((0x36 << 8) | 0x30);
        public const ushort T_FC_EVENT_NOTIFY_MSG_GET_OK = (ushort)((0x36 << 8) | 0x31);
        public const ushort T_FC_WAR_INFLUENCE_DATA = (ushort)((0x4A << 8) | 0x25);
        public const ushort T_FC_STORE_UPDATE_ITEM_COUNT = (ushort)((0x24 << 8) | 0x08);
        public const ushort T_FC_OUTPOST_WAR_INFO = (ushort)((0x4E << 8) | 0x0C);

        public const ushort T_FC_ITEM_UPDATE_WINDOW_ITEM_LIST = (ushort)((0x23 << 8) | 0x0A);


        public const ushort T_FC_STORE_INSERT_ITEM = (ushort)((0x24 << 8) | 0x06);
        public const ushort T_FC_STORE_PUT_ITEM_DONE = (ushort)((0x24 << 8) | 0x03);
        public const ushort T_FC_ITEM_PUT_ENCHANT_HEADER = (ushort)((0x23 << 8) | 0x15);
        public const ushort T_FC_ITEM_PUT_ENCHANT = (ushort)((0x23 << 8) | 0x14);
        public const ushort T_FC_ITEM_PUT_ENCHANT_DONE = (ushort)((0x23 << 8) | 0x17);

        public const ushort T_FC_SKILL_SETUP_SKILL = (ushort)((0x41 << 8) | 0x02);
        public const ushort T_FC_SKILL_SETUP_SKILL_OK_HEADER = (ushort)((0x41 << 8) | 0x03);
        public const ushort T_FC_SKILL_SETUP_SKILL_OK = (ushort)((0x41 << 8) | 0x04);
        public const ushort T_FC_SKILL_SETUP_SKILL_OK_DONE = (ushort)((0x41 << 8) | 0x05);

        public const ushort T_IC_CONNECT_LOGIN = (ushort)((0x16 << 8) | 0x04);
        public const ushort T_IC_CONNECT_LOGIN_OK = (ushort)((0x16 << 8) | 0x05);
        public const ushort T_IC_STRING_128 = (ushort)((0x84 << 8) | 0x01);
        public const ushort T_IC_STRING_256 = (ushort)((0x84 << 8) | 0x02);
        public const ushort T_IC_STRING_512 = (ushort)((0x84 << 8) | 0x03);
        public const ushort T_IC_WORLD_NOTIFICATION = (ushort)((0x84 << 8) | 0x04);

        public const ushort T_IC_CHAT_ALL = (ushort)((0x81 << 8) | 0x00);
        public const ushort T_IC_CHAT_MAP = (ushort)((0x81 << 8) | 0x01);
        public const ushort T_IC_CHAT_REGION = (ushort)((0x81 << 8) | 0x02);
        public const ushort T_IC_CHAT_PTOP = (ushort)((0x81 << 8) | 0x03);
        public const ushort T_IC_CHAT_PARTY = (ushort)((0x81 << 8) | 0x04);
        public const ushort T_IC_CHAT_GUILD = (ushort)((0x81 << 8) | 0x05);
        public const ushort T_IC_CHAT_SELL_ALL = (ushort)((0x81 << 8) | 0x30);
        public const ushort T_IC_CHAT_CASH_ALL = (ushort)((0x81 << 8) | 0x31);
        public const ushort T_IC_CHAT_INFLUENCE_ALL = (ushort)((0x81 << 8) | 0x32);
        public const ushort T_IC_CHAT_ARENA = (ushort)((0x81 << 8) | 0x33);
        public const ushort T_IC_CHAT_WAR = (ushort)((0x81 << 8) | 0x34);
        public const ushort T_IC_CHAT_CHATROOM = (ushort)((0x81 << 8) | 0x35);
        public const ushort T_IC_CHAT_INFINITY = (ushort)((0x81 << 8) | 0x36);
        public const ushort T_IC_CHAT_CNC = (ushort)((0x81 << 8) | 0x37);

        public const ushort T1_FC_CHARACTER_CREATE = (ushort)(0x22 << 8 | 0x00);
        public const ushort T_FC_CHARACTER_CHANGE_STATUS = (ushort)(0x32 << 8 | 0x1D);
        public const ushort T_FC_MONSTER_MOVE_OK = (ushort)(0x3A << 8 | 0x02);
        public const ushort T_FC_BATTLE_ATTACK = (ushort)(0x12 << 8 | 0x00);
        public const ushort T_FC_BATTLE_ATTACK_OK = (ushort)(0x12 << 8 | 0x01);
        public const ushort T_FC_BATTLE_ATTACK_FIND = (ushort)(0x12 << 8 | 0x02);
        public const ushort T_FC_BATTLE_ATTACK_FIND_OK = (ushort)(0x12 << 8 | 0x03);
        public const ushort T_FC_BATTLE_SHOW_DAMAGE = (ushort)(0x12 << 8 | 0x28);
        public const ushort T_FC_CHARACTER_DEAD_GAMESTART = (ushort)(0x32 << 8 | 0x2f);
        public const ushort T_FC_SHOP_REQUEST_REPAIR = (ushort)(0x40 << 8 | 0x0c);
        public const ushort T_FC_ITEM_SHOW_ITEM = (ushort)(0x39 << 8 | 0x00);
        public const ushort T_FC_CHARACTER_CHANGE_CURRENTHP = (ushort)(0x32 << 8 | 0x20);
        public const ushort T_FC_EVENT_SELECT_CHANNEL = (ushort)(0x36 << 8 | 0x11);
        public const ushort T_FC_EVENT_WARP_SAME_FIELD_SERVER = (ushort)(0x36 << 8 | 0x03);
        public const ushort T_FC_ITEM_HIDE_ITEM = (ushort)(0x39 << 8 | 0x01);
        

        public enum Errors
        {
            ERR_COMMON_LOGIN_FAILED = (ushort)(0x2001),
            ERR_PROTOCOL_INVALID_PRESERVER_CLIENT_STATE = (ushort)(0x2451),
            ERR_PROTOCOL_EMPTY_ACCOUNTNAME = (ushort)(0x2410),
            ERR_PROTOCOL_DUPLICATE_LOGIN = (ushort)(0x2411),
            ERR_PROTOCOL_ALL_FIELD_SERVER_NOT_ALIVE = (ushort)(0x2455),
            ERR_PROTOCOL_IM_SERVER_NOT_ALIVE = (ushort)(0x245F),
            ERR_COMMON_SERVICE_TEMPORARILY_PAUSED = (ushort)(0x2005),
            ERR_PROTOCOL_LIMIT_GROUP_USER_COUNT = (ushort)(0x2414),
            ERR_PROTOCOL_ACCOUNT_BLOCKED = (ushort)(0x2415),
            ERR_COMMON_INVALID_CLIENT_VERSION = (ushort)(0x2006),
            ERR_DB_NO_SUCH_ACCOUNT = (ushort)(0x2106),
            ERR_PERMISSION_DENIED = (ushort)(0x401D),
            ERR_NO_SEARCH_CHARACTER = (ushort)(0x4018),
            ERR_DB_EXECUTION_FAILED = (ushort)0x210D
        }
        public const ulong BODYCON_SET_OR_CLEAR_MASK = (ulong)0x0000000000000001;

        

        public static List<int> refillOptions = new List<int>()
        {
            13,
            89,
            14,
            15,

            77,
            78
        };

        public enum StringPrintType
        {
            Chat,
            Static,
            ClientDbgOut,
            Notice,
            PopUp
        }

        public struct MSG_PC_CONNECT_SEND_GET_BLOCKED_MAC_ADDR
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
            public char[] macAddr;
        };
        public struct MSG_FC_EVENT_WARP_SAME_FIELD_SERVER
        {
            public MAP_CHANNEL_INDEX MapChannelIndex;
            public ushort MapWeather;
            public AVECTOR3 PositionVector;
            public byte CharacterMode0;		// 2005-07-27 by cmkwon
        } ;

        public struct MSG_FC_EVENT_SELECT_CHANNEL
        {
            public ushort ClientIndex;
            public int WarpTargetIndex;	// Å¬¶óÀÌ¾ðÆ®´Â ÀÌ Á¤º¸¸¦ MSG_FC_EVENT_SUGGEST_CHANNELS·ÎºÎÅÍ ±×´ë·Î º¹»çÇØ¼­ ¼­¹ö¿¡ ³Ñ°ÜÁÖ¾î¾ß ÇÔ
            public MAP_CHANNEL_INDEX MapChannelIndex;
        };

        public struct MSG_FC_SHOP_REQUEST_REPAIR
        {
            public int DesParam;			// ¼ö¸®ÇÒ ºÎºÐ(DES_HP, DES_DP, DES_EP, DES_SP, DES_BULLET_01, DES_BULLET_02)
            public int Count;				// ¾î´À ¾ç¸¸Å­ ¼ö¸®ÇÒ °ÍÀÎÁö
        };
        public struct MSG_FC_ITEM_SHOW_ITEM
        {
            public uint ItemFieldIndex;				// ½Àµæ Àü±îÁö ¼­¹ö°¡ ÀÓ½Ã·Î °ü¸®ÇÏ´Â ¹øÈ£
            public int ItemNum;					// Å¬¶óÀÌ¾ðÆ®¿¡ ¾ÆÀÌÅÛÀÇ Á¾·ù¸¦ º¸¿©ÁÖ±â À§ÇØ º¸³¿
            public uint FirstCharacterUID;			// ¾ÆÀÌÅÛ ½Àµæ °¡´ÉÇÑ Ã¹¹øÂ° Ä³¸¯ÅÍ
            public int Amount;						// ¾ÆÀÌÅÛÀÇ °³¼ö
            public AVECTOR3 Position;
            public byte DropItemType;				// ¶³¾îÁø ¾ÆÀÌÅÛÀÇ Á¾·ù(½Àµæ¿ë ¾ÆÀÌÅÛ, °ø°Ý¿ë(¸¶ÀÎ·ù) ¾ÆÀÌÅÛ µîµî)
        };

        public struct MSG_FC_ITEM_HIDE_ITEM
        {
            public uint ItemFieldIndex;				// ½Àµæ Àü±îÁö ¼­¹ö°¡ ÀÓ½Ã·Î °ü¸®ÇÏ´Â ¹øÈ£
            public ushort ClientIndex;				// ¾ÆÀÌÅÛÀ» ¸ÔÀº characterÀÇ client index
        };

        public struct MSG_FC_EVENT_ENTER_BUILDING_OK
        {
            public uint BuildingIndex;
            public bool SendShopItemList;
        } ;
        public struct MSG_FC_MONSTER_CHANGE_HP
        {
            public ushort MonsterIndex;
            public int CurrentHP;
        };
        public struct MEX_TARGET_INFO
        {
            public AVECTOR3 TargetPosition;			// °ø°Ý Å¬¶óÀÌ¾ðÆ®ÀÇ È­¸é¿¡¼­ÀÇ Å¸ÄÏ Æ÷Áö¼Ç
            public ushort TargetIndex;			// °ø°Ý ´ë»ó ClientIndex or MonterIndex, 0ÀÌ¸é ItemFieldIndex¸¸ À¯È¿
            public uint TargetItemFieldIndex;
            public ushort MultiTargetIndex;
        };

        public struct MSG_FC_BATTLE_ATTACK_OK
        {
            public ushort AttackIndex;
            public MEX_TARGET_INFO TargetInfo;
            public AVECTOR3 FirePosition;		// ¹«±âÀÇ ¹ß»ç À§Ä¡
            public byte AttackType;			// °ø°Ý Å¸ÀÔ, ATT_TYPE_XXX
            public ushort WeaponIndex;		// Å¬¶óÀÌ¾ðÆ®¿¡¼­ ¹ß»çµÈ ÃÑ¾ËÀÇ ÀÎµ¦½º, ¼­¹ö¿¡¼­ »ý¼º, CUID16Generator »ç¿ë
            public uint ItemNum;			// ¹«±âÀÇ ItemNum
            public ushort RemainedBulletFuel;	// ³²Àº ÃÑ¾Ë(È¤Àº Fuel)ÀÇ ¼ö
            public uint SkillNum;			// ½ºÅ³ »ç¿ë ½Ã »ç¿ë
            public ushort DelegateClientIdx;
        }
        public struct MSG_FC_BATTLE_ATTACK
        {
            public MEX_TARGET_INFO TargetInfo;
            public AVECTOR3 FirePosition;
            public byte AttackType;
            public uint SkillNum;
        }

        public struct MSG_FC_BATTLE_ATTACK_FIND
        {
            public ushort AttackIndex;
            public MEX_TARGET_INFO TargetInfo;
            public uint ItemNum;
            public ushort WeaponIndex;
            public byte AttackType;
        };
        public struct MSG_FC_MONSTER_MOVE_OK
        {
            public ushort MonsterIndex;
            public ushort TargetIndex;
            public AVECTOR3 PositionVector;
            public AVECTOR3 TargetVector;
        };
        public struct MSG_FC_STRING_128
        {
            public StringPrintType PrintType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
            public char[] String;
            public ushort SecurityNumber;
        }

        public struct MSG_FC_CHARACTER_CHANGE_STATUS
        {
            public ushort ClientIndex;
            public byte Status;
        };

        public struct MSG_FC_STRING_256
        {
            public StringPrintType PrintType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public char[] String;
            public ushort SecurityNumber;
        }

        public struct MSG_FC_STRING_512
        {
            public StringPrintType PrintType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
            public char[] String;
            public ushort SecurityNumber;
        }

        public struct MSG_FC_WORLD_NOTIFICATION
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
            public char[] String;
            public bool Notification;
            public ushort SecurityNumber;
        }


        public struct ServerDateTime
        {
            public ushort Year;
            public byte Month;
            public byte Day;
            public byte Hour;
            public byte Minute;
            public byte Second;
            public string GetDateTimeString()
            {
                return Year.ToString("D3") + "-" + Month.ToString("D2") + "-" + Day.ToString("D2") + " " + Hour.ToString("D2") + ":" + Minute.ToString("D2");
            }

        }


        public struct MSG_PC_CONNECT_LOGIN_OK
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] AccountName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] FieldServerIP;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] IMServerIP;
            public ushort FieldServerPort;
            public ushort IMServerPort;
            public bool OpeningMoviePlay;
        }



        public struct MSG_PC_CONNECT_LOGIN_BLOCKED
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] szAccountName;
            public int nBlockedType;
            public ServerDateTime atimeStart;
            public ServerDateTime atimeEnd;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 200)]
            public char[] szBlockedReasonForUser;
        }



        public struct MSG_ERROR
        {
            public ushort MsgType;
            public ushort ErrorCode;
            public bool CloseConnection;
            public int ErrParam1;
            public int ErrParam2;
            public ushort StringLength;
        }

        public struct MSG_GENERATED_ERROR // not serializable
        {
            public MSG_ERROR ErrorPacket;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 60)]
            public char[] ErrorString;
        }


        public struct MSG_PC_CONNECT_LOGIN
        {
            public char LoginType;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] AccountName;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] Password;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] FieldServerGroupName;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] PrivateIP;

            public int MGameSEX;
            public int MGameYear;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
            public char[] WebLoginAuthKey;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] ClientIP;

            // [MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
            // public char[] SelectiveShutdownInfo;

        }


        public struct MSG_PC_CONNECT_GET_SERVER_GROUP_LIST_OK
        {
            public int NumOfServerGroup;
            public MEX_SERVER_GROUP_INFO_FOR_LAUNCHER ServerGroup;
        }


        public struct MEX_SERVER_GROUP_INFO_FOR_LAUNCHER
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] ServerGroupName;
            public int Crowdedness;
        }

        public struct MSG_PC_CONNECT_SINGLE_FILE_VERSION_CHECK
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public ushort[] DeleteFileListVersion;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public ushort[] NoticeVersion;
        }


        public struct MSG_PC_CONNECT_SINGLE_FILE_UPDATE_INFO
        {
            public int nAutoUpdateServerType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public ushort[] NewDeleteFileListVersion;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public ushort[] NewNoticeVersion;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
            public char[] FtpIP;
            public ushort FtpPort;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] FtpAccountName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] FtpPassword;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public char[] DeleteFileListDownloadPath;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public char[] NoticeFileDownloadPath;
        }


        public struct MSG_PC_CONNECT_VERSION
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public ushort[] ClientVersion;
        }


        public struct MSG_PC_CONNECT_REINSTALL_CLIENT
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public ushort[] LastVersion;
        }

        public struct MSG_PC_CONNECT_UPDATE_INFO
        {
            public int nAutoUpdateServerType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public ushort[] OldVersion;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public ushort[] UpdateVersion;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
            public char[] FtpIP;
            public ushort FtpPort;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] FtpAccountName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] FtpPassword;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public char[] FtpUpdateDownloadDir;
        }


        public struct MSG_FC_CONNECT_LOGIN
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] AccountName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 33)]
            public char[] Password;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] PrivateIP;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public int[] Padding;
        }

        public enum EQUIP_POS
        {
            POS_NONE = -1,

            POS_PROW = 0,
            POS_PROWIN,
            POS_PROWOUT,
            POS_WINGIN,
            POS_WINGOUT,

            POS_CENTER,
            POS_REAR,
            POS_ACCESSORY_UNLIMITED,
            POS_ACCESSORY_TIME_LIMIT,
            POS_PET,

            MAX_EQUIP_POS,

            POS_HIDDEN_ITEM = 98,
            POS_INVALID_POSITION = 99

        }

        public struct CHARACTER_RENDER_INFO
        {
            public int RI_Prow;
            public int RI_ProwIn;
            public int RI_ProwOut;
            public int RI_WingIn;
            public int RI_WingOut;
            public int RI_Center;
            public int RI_Rear;
            public int RI_AccessoryUnLimited;
            public int RI_AccessoryTimeLimit;

            public bool RI_Invisible;
            public int RI_Prow_ShapeItemNum;
            public int RI_WingIn_ShapeItemNum;
            public int RI_Center_ShapeItemNum;
            public int RI_ProwOut_ShapeItemNum;
            public int RI_WingOut_ShapeItemNum;
            public int RI_ProwOut_EffectItemNum;
            public int RI_WingOut_EffectItemNum;
            public int RI_MonsterUnitKind_ForTransformer;

            public int RI_Pet;
            public int RI_Pet_ShapeItemNum;
            bool SetRenderInfoWithPOS(int i_nPos, int i_nItemNum, int i_nShapeItemNum, int i_nEffectItemNum)
            {
                switch ((EQUIP_POS)i_nPos)
                {

                    case EQUIP_POS.POS_PROW:
                        {
                            RI_Prow = i_nItemNum;
                            RI_Prow_ShapeItemNum = i_nShapeItemNum;
                        }
                        break;

                    case EQUIP_POS.POS_PROWIN:
                        {
                            RI_ProwIn = i_nItemNum;
                        }
                        break;

                    case EQUIP_POS.POS_PROWOUT:
                        {
                            RI_ProwOut = i_nItemNum;
                            RI_ProwOut_ShapeItemNum = i_nShapeItemNum;
                            RI_ProwOut_EffectItemNum = i_nEffectItemNum;
                        }
                        break;

                    case EQUIP_POS.POS_WINGIN:
                        {
                            RI_WingIn = i_nItemNum;
                            RI_WingIn_ShapeItemNum = i_nShapeItemNum;
                        }
                        break;

                    case EQUIP_POS.POS_WINGOUT:
                        {
                            RI_WingOut = i_nItemNum;
                            RI_WingOut_ShapeItemNum = i_nShapeItemNum;
                            RI_WingOut_EffectItemNum = i_nEffectItemNum;
                        }
                        break;

                    case EQUIP_POS.POS_CENTER:
                        {
                            RI_Center = i_nItemNum;
#if _REWORKED_COLORSHOP
					if (i_nEffectItemNum)
					{
						RI_Center_ShapeItemNum = i_nEffectItemNum;
					}
					else
#endif
                            RI_Center_ShapeItemNum = i_nShapeItemNum;
                        }
                        break;

                    case EQUIP_POS.POS_REAR:
                        {
                            RI_Rear = i_nItemNum;
                        }
                        break;

                    case EQUIP_POS.POS_ACCESSORY_UNLIMITED:
                        {
                            RI_AccessoryUnLimited = i_nItemNum;
                        }
                        break;
                    case EQUIP_POS.POS_ACCESSORY_TIME_LIMIT:
                        {

                            RI_AccessoryTimeLimit = i_nItemNum;
                        }
                        break;

                    case EQUIP_POS.POS_PET:
                        {
                            RI_Pet = i_nItemNum;
                            RI_Pet_ShapeItemNum = i_nShapeItemNum;
                        }
                        break;

                    default:
                        {
                            return false;
                        }
                }
                return true;
            }

        }


        public struct FC_CONNECT_LOGIN_INFO
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] CharacterName;
            public uint CharacterUniqueNumber;
            public ushort Race;
            public ushort UnitKind;
            public byte PilotFace;
            public byte Gender;
            public int RacingPoint;
            public CHARACTER_RENDER_INFO CharacterRenderInfo;
            public bool ShutDownMINS;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public uint[] Padding;
        } ;



        public struct MSG_FC_CONNECT_LOGIN_OK
        {
            public uint AccountUniqueNumber;
            public byte NumCharacters;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public FC_CONNECT_LOGIN_INFO[] Characters;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] VoIP1to1ServerIP;
            public ushort VoIP1to1ServerPort;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] VoIPNtoNServerIP;
            public ushort VoIPNtoNServerPort;
            public byte bIsUseSecondaryPasswordSystem;
            public byte bIsSetSecondaryPassword;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public uint[] Padding;
            public bool BIsTestServer;
            public byte DBNum;
        }

        public struct GEAR_STAT
        {
            public short AttackPart;
            public short DefensePart;
            public short FuelPart;
            public short SoulPart;
            public short ShieldPart;
            public short DodgePart;
        }


        public struct MSG_FC_CHARACTER_DELETE
        {
            public uint AccountUniqueNumber;
            public uint CharacterUniqueNumber;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 33)]
            public char[] CurrentSecPassword;
        }


        public struct AS
        {
            public ushort x;
            public ushort y;
        }


        public struct MSG_IC_CONNECT_LOGIN
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] AccountName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] ServerName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] CharacterName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 33)]
            public char[] Password;
            public byte LoginType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public uint[] Padding;
        }


        public struct MSG_FC_CHARACTER_CONNECT_GAMESTART
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] AccountName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 33)]
            public char[] Password;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] PrivateIP;
            public uint AccountUniqueNumber;
            public uint CharacterUniqueNumber;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public uint[] Padding;
        }
        public struct SArenaPlayInfo
        {
            public int nPlayCount;									// ³²Àº ÇÃ·¹ÀÌ Ä«¿îÆ®
            public ServerDateTime atimeLastPlayTime;							// ¸¶Áö¸·À¸·Î ÇÃ·¹ÀÌ ÇÑ ½Ã°£
            public ServerDateTime atimeResetTime;								// ¾Æ·¹³ª ÀÔÀåÈ½¼ö ÃÊ±âÈ­ ½Ã°£
        }

        public struct MSG_FC_ITEM_GET_ITEM
        {
            public ushort ClientIndex;
            public uint ItemFieldIndex;					// ½Àµæ Àü±îÁö ¼­¹ö°¡ ÀÓ½Ã·Î °ü¸®ÇÏ´Â ¹øÈ£
        };

        public struct MAP_CHANNEL_INDEX
        {
            public ushort MapIndex;
            public ushort ChannelIndex;

            public static bool operator ==(MAP_CHANNEL_INDEX t1, MAP_CHANNEL_INDEX t2)
            {
                return t1.MapIndex == t2.MapIndex && t1.ChannelIndex == t2.ChannelIndex;
            }

            public static bool operator !=(MAP_CHANNEL_INDEX t1, MAP_CHANNEL_INDEX t2)
            {
                return t1.MapIndex != t2.MapIndex || t1.ChannelIndex != t2.ChannelIndex;
            }
        }

        public struct D3DXVECTOR3
        {
            public float x;
            public float y;
            public float z;

            public AVECTOR3 ConvertToAVEC()
            {
                AVECTOR3 s = new AVECTOR3();
                s.x = Convert.ToInt16(x);
                s.y = Convert.ToInt16(y);
                s.z = Convert.ToInt16(z);
                return s;
            }
        }
        public struct CHARACTER
        {
            public ushort ClientIndex;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] AccountName;
            public uint AccountUniqueNumber;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] CharacterName;
            public uint CharacterUniqueNumber;
            public byte Gender;
            public byte PilotFace;
            public byte CharacterMode;
            public ushort Race;
            public ushort UnitKind;
            public byte InfluenceType;
            public byte SelectableInfluenceMask;
            public byte AutoStatType;
            public GEAR_STAT GearStat;
            public GEAR_STAT TotalGearStat;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
            public char[] GuildName;
            public uint GuildUniqueNumber;
            public byte Level;
            public double Experience;
            public double DownExperience;
            public int DownSPIOnDeath;
            public ulong BodyCondition;
            public int Propensity;
            public byte Status;
            public ushort PKWinPoint;
            public ushort PKLossPoint;
            public ushort Material;
            public short HP;
            public float CurrentHP;
            public short DP;
            public float CurrentDP;
            public short SP;
            public short CurrentSP;
            public short EP;
            public float CurrentEP;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public char[] PetName;
            public byte PetLevel;
            public double PetExperience;
            public MAP_CHANNEL_INDEX MapChannelIndex;
            public D3DXVECTOR3 PositionVector;
            public D3DXVECTOR3 TargetVector;
            public D3DXVECTOR3 UpVector;
            public byte MaxLevel;
            public byte BonusStat;
            public byte BonusStatPoint;
            public ulong LastPartyID;
            public int RacingPoint;
            public long TotalPlayTime;
            public ServerDateTime CreatedTime;
            public ServerDateTime LastStartedTime;
            public ServerDateTime LevelUpTime;
            public int WarPoint;
            public int CumulativeWarPoint;
            public int ArenaWin;
            public int ArenaLose;
            public int ArenaDisConnect;
            public long PCBangTotalPlayTime;
            public int SecretInfoOption;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] NickName;
            SArenaPlayInfo ArenaPlayInfo;
            public byte bUsingReturnItem;
        }


        public struct MSG_FC_CHARACTER_GET_CHARACTER
        {
            public uint AccountUniqueNumber;
            public uint CharacterUniqueNumber;
            public int shutdownmins;
        }


        public struct MSG_FC_CHARACTER_GET_CHARACTER_OK
        {
            public CHARACTER characterInfo;
        }


        public struct MSG_FC_CHARACTER_GAMESTART
        {
            public ushort ClientIndex;
            public uint CharacterUniqueNumber;
        }


        public struct MSG_FC_CONNECT_SYNC_TIME
        {
            public uint CurrentTime;
        }

        public struct AVECTOR3
        {
            public short x;
            public short y;
            public short z;

            public bool IsNull()
            {
                return x == 0 && y == 0 && z == 0;
            }

            public D3DXVECTOR3 ToDXVector()
            {
                D3DXVECTOR3 f = new D3DXVECTOR3();
                f.x = x;
                f.y = y;
                f.z = z;
                return f;
            }
        }


        public struct MSG_FC_CHARACTER_GAMESTART_OK
        {
            public ushort ClientIndex;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] FieldServerIP;
            public int FieldServerPort;
            public byte CharacterMode0;
            public AVECTOR3 PositionVector;
            public ushort MapWeather;
            public bool bMemberPCBang;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] ServerGroupName0;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] MainORTestServerName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] GamePublisher;
            public MAP_CHANNEL_INDEX MapInfo;
            public ulong BodyCondition;
            public float CurrentHP;
            public float CurrentDP;
            public float CurrentEP;
            public float CurrentSP;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public uint[] Padding;
        }



        public struct MSG_FC_CONNECT_ARENASERVER_INFO
        {
            public ushort MainServer_ID;
            public ushort ArenaServer_ID;
            public ushort AFS_Port;
            public ushort AIS_Port;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] AFS_IP;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public char[] AIS_IP;
        }


        public struct MSG_FC_CHARACTER_CHANGE_EXP
        {
            public ushort ClientIndex;
            public double Experience;
        }


        public struct MSG_FC_CHARACTER_CHANGE_CURRENTHPDPSPEP
        {
            public ushort ClientIndex;
            public float CurrentHP;
            public float CurrentDP;
            public short CurrentSP;
            public float CurrentEP;
        }


        public struct COLLECTION_INFO
        {
            public int CollectionType;
            public uint AccountUID;
            public uint CharacterUID;
            public int ShapeNum;
            public int ShapeItemNum;
            public byte EnchantLevel;
            public uint RemainSeconds;
            public ServerDateTime EndTime;
            public int ActivedCount;
        }


        public struct SMSWARINFO_DISPLAY
        {
            public ushort MapIndex;
            public int MapInfluenceType;
            public int HPRate;
            public int TelePortState;
            public ServerDateTime TelePortBuildingStartTime;
            public ServerDateTime TelePortBuildingEndTime;
        }


        public struct MSG_FC_INFO_MSWARINFO_DISPLAY
        {
            public int MSWarInfoDisPlayListCount;
            //_ARRAY(SMSWARINFO_DISPLAY);
        }


        public struct MSG_FC_INFO_MSWARINFO_DISPLAY_OPTION_OK
        {
            public short MSWarOptionType;
        }


        public struct MSG_FC_MOVE_BIT_FLAG
        {
            public byte CharacterMode0;
            public byte Invisible0;
            public byte ChargingSkill;
            public byte HyperShot;
            public bool bSearchEye_1;
            public bool bSearchEye_2;
            public bool bUsingBarialSkill;
            public bool bUsingInvicibleSkill;
        }


        public struct MSG_FC_MOVE_OK
        {
            public ushort ClientIndex;
            public MSG_FC_MOVE_BIT_FLAG moveBitFlag;
            public AVECTOR3 PositionVector;
            public AVECTOR3 TargetVector;
            public AVECTOR3 UpVector;
        }
        public struct MSG_FC_MOVE
        {
            public ushort ClientIndex;
            public ushort TimeGap;
            public byte DistanceGap;		// Client¿Í °°ÀÌ »èÁ¦ÇÒ ¿¹Á¤ÀÓ
            public AVECTOR3 PositionVector;
            public AVECTOR3 TargetVector;
            public AVECTOR3 UpVector;
        }

        public struct ITEM
        {

            public int ItemNum;
            public byte Kind;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public char[] ItemName;
            public float AbilityMin;
            public float AbilityMax;
            public ushort ReqRace;
            public GEAR_STAT ReqGearStat;
            public ushort ReqUnitKind;
            public byte ReqMinLevel;
            public byte ReqMaxLevel;
            public byte ReqItemKind;
            public ushort Weight;
            public float HitRate;
            public byte Defense;
            public float FractionResistance;
            public byte NaturalFaction;
            public byte SpeedPenalty;
            public ushort Range;
            public byte Position;
            public byte Scarcity;
            public float Endurance;
            public short AbrasionRate;
            public ushort Charging;
            public byte Luck;
            public ushort MinTradeQuantity;
            public uint SellingPrice;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public ushort[] ArrDestParameter;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public float[] ArrParameterValue;
            public uint ReAttacktime;
            public int Time;

            public ushort RepeatTime;
            public ushort Material;
            public ushort ReqMaterial;
            public float RangeAngle;
            public byte UpgradeNum;
            public int LinkItem;
            public byte MultiTarget;
            public ushort ExplosionRange;
            public ushort ReactionRange;
            public byte ShotNum;
            public byte MultiNum;
            public ushort AttackTime;
            public byte ReqSP;
            public int SummonMonster;
            public int NextSkill;
            public byte SkillLevel;
            public short SkillHitRate;
            public byte SkillType;
            public byte SkillTargetType;
            public byte Caliber;
            public byte OrbitType;
            public ulong ItemAttribute;
            public float BoosterAngle;
            public int CameraPattern;
            public int SourceIndex;
            [MarshalAs(UnmanagedType.ByValArray)]
            int[] pParamOverlapIdxList;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 200)]
            public char[] Description;
            public byte EnchantCheckDestParam;
            public int InvokingDestParamID;
            public int InvokingDestParamIDByUse;
            [MarshalAs(UnmanagedType.ByValArray)]
            INVOKING_WEAR_ITEM_DESTPARAM[] pInvokingDestParamList;
            [MarshalAs(UnmanagedType.ByValArray)]
            INVOKING_WEAR_ITEM_DESTPARAM[] pInvokingDestParamByUseList;

            public byte IsTenderDropItem;
        }

        public struct INVOKING_WEAR_ITEM_DESTPARAM
        {
            public int InvokingDestParamID;
            public ushort InvokingDestParam;
            public float InvokingDestParamValue;
            public int InvokingEffectIdx;
        }

        public struct ITEM_BASE
        {
            public byte Kind;
            public ulong UniqueNumber;
            public int ItemNum;
            public ITEM ItemInfo;
        }
        public struct FIXED_TERM_INFO
        {
            public bool bActive;
            public ServerDateTime StartDate;
            public ServerDateTime EndDate;
            public uint TimerUID;
            public int nStatShapeItemNum;
            public byte nStatLevel;
        }

        public struct ITEM_GENERAL_BASE
        {
            public byte Kind;
            public ulong UniqueNumber;
            public int ItemNum;
            public ITEM ItemInfo;
            public uint AccountUniqueNumber;
            public uint Possess;
            public byte ItemStorage;
            public byte Wear;
            public int CurrentCount;
            public int ItemWindowIndex;
            public short NumOfEnchants;
            public int PrefixCodeNum;
            public int SuffixCodeNum;
            public float CurrentEndurance;
            public int ColorCode;
            public int ShapeItemNum;
            public ulong MainSvrItemUID;
            public int UsingTimeStamp;
            public ServerDateTime UsingStartTime;
            public float DesWeight;
            public ServerDateTime CreatedTime;
            public int CoolingTimeStamp;
            public ServerDateTime CoolingStartTime;
            public FIXED_TERM_INFO FixedTermShape;
            public int nMonthlyOptionItemNum;
            public ServerDateTime atMonthlyEventEndDate;
        }


        public struct MSG_FC_STORE_INSERT_ITEM
        {
            public uint FromCharacterUniqueNumber;
            public byte ItemInsertionType;
            public ITEM_GENERAL_BASE ItemGeneral;
        }


        public struct MSG_FC_STORE_PUT_ITEM_HEADER
        {
            public uint PossessCharacter;
            public byte ItemStorage0;
        }

        public struct ENCHANT
        {
            public ulong TargetItemUniqueNumber;
            public int TargetItemNum;
            public int EnchantItemNum;
            public ulong SequenceNumber_DB;
        }


        public struct MSG_FC_ITEM_PUT_ENCHANT
        {
            public ENCHANT Enchant;
        }


        public struct MSG_FC_STORE_PUT_ITEM_DONE
        {
            public uint NumOfItem;
            public byte ItemStorage0;
        }


        public struct MSG_FC_COLLECTION_MONTHLY_AROMOR_EVENT_INFO
        {
            public int CollectionShapeNum;
            public int nOptionItemNum;
            public ServerDateTime atEndDate;
        }


        public struct MSG_FC_CHARACTER_CHANGE_TOTALGEAR_STAT
        {
            public ushort ClientIndex;
            public byte byAutoStatType;
            public GEAR_STAT GearStat1;
        }
        public struct MEX_QUEST_INFO
        {
            public int QuestIndex;
            public byte QuestState;
            public long QuestPlayTimeStamp;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] szCityWarServerGroupName;
        }
        public struct MSG_FC_QUEST_PUT_ALL_QUEST
        {
            public float fVCNInflDistributionPercent;
            public float fANIInflDistributionPercent;
            public int NumOfQuest;
            //ARRAY_(MEX_QUEST_INFO);
        }

        public struct MEX_QUEST_MONSTER_COUNT
        {
            public int QuestIndex;
            public int MonsterUniqueNumber;
            public int Count;
        }
        public struct MSG_FC_QUEST_PUT_ALL_QUEST_MONSTER_COUNT
        {
            public int NumOfMonsterCount;
            //ARRAY_(MEX_QUEST_MONSTER_COUNT);	
        }
        public struct MSG_FC_CHARACTER_CHANGE_BODYCONDITION
        {
            public ushort ClientIndex;
            public ulong BodyCondition;
        }

        public struct MSG_FC_CHARACTER_CHANGE_CHARACTER_MODE
        {
            public byte CharacterMode0;
            public AVECTOR3 PositionAVec3;
            public AVECTOR3 TargetAVec3;
        }
        public struct MSG_FC_CHARACTER_CHANGE_CHARACTER_MODE_OK
        {
            public ushort ClientIndex;
            public byte CharacterMode0;
            public AVECTOR3 PositionAVec3;
            public AVECTOR3 TargetAVec3;
        };

        public struct MSG_FC_EVENT_NOTIFY_MSG_GET_OK
        {
            public ulong NotifyMsgUID;
            public uint CharacterUID;
            public byte NotifyMsgType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
            public char[] NotifyMsgString;
        }

        public struct MSG_FC_WAR_INFLUENCE_DATA
        {
            public byte byInfluenceType;
            public float fHPRepairRate;
            public float fDPRepairRate;
            public float fSPRepairRate;
        }

        public struct MSG_FC_STORE_UPDATE_ITEM_COUNT
        {
            public ulong ItemUniqueNumber;
            public int NewCount;
            public byte ItemUpdateType;
        }
        public struct SOUTPOST_WAR_INFO
        {
            public byte OutPostState;
            public byte OutPostResetIngInfluence;
            public ushort MapIndex;
            public int OutPostWarResetRamainSecondTime;
            public ServerDateTime OutPostWarStartTime;
            public ServerDateTime OutPostWarEndTime;
        }

        public struct MSG_FC_OUTPOST_WAR_INFO
        {
            public int OutPostWarInfoListCount;
            //	_ARRAY(SOUTPOST_WAR_INFO);	
        }

        public struct NORMAL_CHAT
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] FromCharacterName;
            public byte MessageLength;
        }

        public struct INFLUENCED_CHAT
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] FromCharacterName;
            public byte MessageLength;
            public byte FromInfluenceType;
        }

        public struct MSG_IC_CHAT_WAR
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] FromCharacterName;
            public byte MessageLength;
            public byte InfluenceID;
        }

        public struct MSG_IC_CHAT_CNC
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] FromCharacterName;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public char[] InfluenceOrStaffPrefix;	// Like BCU or CoMa
            public byte MessageLength;
        }

        public enum ChatType : ushort
        {
            WAR = T_IC_CHAT_WAR,
            CNC = T_IC_CHAT_CNC,
            REGION = T_IC_CHAT_REGION,
            MAP = T_IC_CHAT_MAP,
            PARTY = T_IC_CHAT_PARTY,
            GUILD = T_IC_CHAT_GUILD,
            ALL = T_IC_CHAT_ALL,
            CHATROOM = T_IC_CHAT_CHATROOM,
            INFINITY = T_IC_CHAT_INFINITY,
            ARENA = T_IC_CHAT_ARENA,
            SELL_ALL = T_IC_CHAT_SELL_ALL,
            CASH_ALL = T_IC_CHAT_CASH_ALL,
            PTOP = T_IC_CHAT_PTOP,
            INFLUENCE_ALL = T_IC_CHAT_INFLUENCE_ALL
        }

        public static string GetInflTypeName(byte inflType)
        {
            switch (inflType)
            {
                case 1:
                    return "FreeSKA";
                case 2:
                    return "BCU";
                case 4:
                    return "ANI";
                case 6:
                    return "Staff";
                default: return "N/A";
            }
        }

        public struct MSG_FC_CHARACTER_GET_OTHER_INFO
        {
            public ushort ClientIndex;
        } ;

        public struct MEX_OTHER_CHARACTER_INFO
        {
            public long BodyCondition;
            public int Propensity;
            public uint CharacterUniqueNumber;
            public uint GuildUniqueNumber;
            public MAP_CHANNEL_INDEX MapChannelIndex;
            public AVECTOR3 PositionVector;
            public AVECTOR3 TargetVector;
            public AVECTOR3 UpVector;
            public ushort ClientIndex;
            public ushort Race;
            public ushort UnitKind;
            public ushort PKWinPoint;
            public ushort PKLossPoint;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] CharacterName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public char[] szCharacterMent;
            public byte Gender;
            public byte PilotFace;
            public byte CharacterMode0;
            public byte InfluenceType;
            public byte Level1;
            public byte CityWarTeamType;
            public byte Status;
            public byte UsingReturnItem;

        };

        public struct MSG_FC_CHARACTER_GET_OTHER_INFO_OK
        {
            public MEX_OTHER_CHARACTER_INFO CharacterInfo;
            public CHARACTER_RENDER_INFO CharacterRenderInfo;
        } ;

        public struct MSG_FC_ITEM_UPDATE_WINDOW_ITEM_LIST
        {
            public int NumOfItems;
        };

        public struct ITEM_WINDOW_INFO
        {
            public ulong ItemUniqueNumber;
            public byte ItemKind;
            public byte Wear;
            public int ItemWindowIndex;
        } ;

        public struct MSG_FC_CHARACTER_CREATE
        {
            public int AccountUniqueNumber;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] CharacterName;
            public ushort UnitKind;
            public ushort Race;
            public byte AutoStatType1;
            public GEAR_STAT GearStat1;
            public byte Gender;
            public byte PilotFace;
            public byte InfluenceType0;
        } ;

        public struct MSG_FC_CHARACTER_DEAD_GAMESTART
        {
            public ushort ClientIndex;
            public uint CharacterUniqueNumber;
            public bool bRebirthInCityMap;			// TRUE:¼¼·Âº°µµ½Ã¸Ê ºÎÈ°, FALSE:ÇöÀç¸Ê¿¡¼­ ºÎÈ°
        };

        public struct MSG_FC_CHARACTER_GET_MONSTER_INFO_OK
        {
            public ushort MonsterIndex;
            public int CurrentHP;
            public int MonsterUnitKind;
            public short MonsterForm;
            public AVECTOR3 PositionVector;
            public AVECTOR3 TargetVector;
            public ulong BodyCondition;
            public int MaxHP;
        };
    }
}
