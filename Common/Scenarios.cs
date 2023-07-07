using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FF4FreeEnterprisePR.Inventory.Monster;

namespace FF4FreeEnterprisePR.Common
{
    public static class Scenarios
    {
        public class scenario
        {
            public int bossId { get; set; }
            public int rewardId { get; set; }
			public string bossScript { get; set; }
			public string winScript { get; set; }
            public int bossBattleBackground { get; set; }
            public int bossbgm { get; set; }
            public List<int> itemLocations { get; set; }
            public int battleFlagGroupID { get; set; }
			public int characterXP { get; set; }
        }

        // Boss battle musics:
        // 1 = Baron castle
        // 7 = Battle 1
        // 12 = Battle 2 (Regular Boss)
        // 13 = Bomb Ring (Mist... probably)
        // 14 = Rydia (Maybe Mist as well?)
        // 16 = Sorrow and Loss (Damcyan or Upper Babil 1?)
        // 17 = Edward's Harp (SandRuby or Magnetic Cave)
        // 20 = Run!  (Evil Wall)
        // 21 = Suspicion (somewhere?)
        // 22 = Golbez (Dwarf Castle 2)
        // 27 = Fiend Battle
        // 35 = Dancing Calbrena (Dwarf Castle 1, Lower Babil)
        // 42 = Final Battle
        public static List<scenario> getScripts()
        {
			return new List<scenario>()
			{
				// Mist Cave
				new scenario {
					bossId = 0,
					rewardId = 0,
                    bossScript = @"Res\Map\Map_30011\Map_30011_1\sc_e_0007.json",
                    winScript =  @"Res\Map\Map_30011\Map_30011_1\sc_e_0007_1.json",
                    bossBattleBackground = 2,
                    bossbgm = 12,
                    itemLocations = { },
                    battleFlagGroupID = 13,
					characterXP = 700,
                },
                // Mist Town
				new scenario {
					bossId = 1,
					rewardId = 1,
					bossScript = @"Res\Map\Map_20070\Map_20070_1\sc_e_0008.json",
					winScript =  @"Res\Map\Map_20070\Map_20070_1\sc_e_0008_5.json",
					bossBattleBackground = 22,
					bossbgm = 0,
					itemLocations = { },
					battleFlagGroupID = 4,
					characterXP = 20000,
				},
				// Kaipo Inn
				new scenario {
					bossId = 2,
					rewardId = 2,
					bossScript = @"Res\Map\Map_20031\Map_20031_2\sc_e_0009_1.json",
					winScript =  @"Res\Map\Map_20031\Map_20031_2\sc_e_0009_2.json",
					bossBattleBackground = 17,
					bossbgm = 12,
					itemLocations = { },
					battleFlagGroupID = 0,
					characterXP = 21000,
				},
				// Kaipo Sandruby
				new scenario {
					bossId = 3,
					rewardId = 3,
					bossScript = @"Res\Map\Map_20030\Map_20030\sc_e_0018_1.json",
					winScript =  @"Res\Map\Map_20031\Map_20031_5\sc_e_0018_3.json",
					bossBattleBackground = 18,
					bossbgm = 7,
					itemLocations = { },
					battleFlagGroupID = 13,
					characterXP = 19000,
				},
				// Underground Waterway
				new scenario {
					bossId = 4,
					rewardId = 4,
					bossScript = @"Res\Map\Map_30021\Map_30021_10\sc_e_0014.json",
					winScript =  @"Res\Map\Map_30021\Map_30021_10\sc_e_0014_1.json",
					bossBattleBackground = 4,
					bossbgm = 12,
					itemLocations = { },
					battleFlagGroupID = 13,
					characterXP = 1700,
				},
                // Damcyan
				new scenario {
					bossId = 5,
					rewardId = 5,
					bossScript = @"Res\Map\Map_20041\Map_20041_3\sc_e_0016.json",
					winScript =  @"Res\Map\Map_20041\Map_20041_3\sc_e_0016_1.json",
					bossBattleBackground = 17,
					bossbgm = 12,
					itemLocations = { },
                    battleFlagGroupID = 4,
					characterXP = 1000,
				},
                // Antlion Cave
				new scenario {
					bossId = 6,
					rewardId = 6,
					bossScript = @"Res\Map\Map_30031\Map_30031_5\sc_e_0017.json",
					winScript =  @"Res\Map\Map_30031\Map_30031_5\sc_e_0017_1.json",
					bossBattleBackground = 2,
					bossbgm = 12,
					itemLocations = { },
					battleFlagGroupID = 13,
					characterXP = 2000,
				},
                // Mt. Hobs
				new scenario {
					bossId = 7,
					rewardId = 7,
					bossScript = @"Res\Map\Map_30060\Map_30060\sc_e_0020_1.json",
					winScript =  @"Res\Map\Map_30060\Map_30060\sc_e_0020_2.json",
					bossBattleBackground = 6,
					bossbgm = 0,
					itemLocations = { },
					battleFlagGroupID = 13,
					characterXP = 3400,
				},
                // Fabul
				new scenario {
					bossId = 8,
					rewardId = 8,
					bossScript = @"Res\Map\Map_20051\Map_20051_4\sc_e_0021_11.json",
					winScript =  @"Res\Map\Map_20051\Map_20051_12\sc_e_0022_1.json",
					bossBattleBackground = 14,
					bossbgm = 21,
					itemLocations = { },
					battleFlagGroupID = 4,
					characterXP = 7000,
				},
				// Mt. Ordeals - Milon
				new scenario {
					bossId = 9,
					rewardId = -1,
					bossScript = @"Res\Map\Map_30110\Map_30110\sc_e_0027.json",
					winScript =  @"",
					bossBattleBackground = 6,
					bossbgm = 12,
					itemLocations = { },
					battleFlagGroupID = 13,
					characterXP = -1,
				},
				// Mt. Ordeals - Milon Z
				new scenario {
					bossId = 10,
					rewardId = -1,
					bossScript = @"Res\Map\Map_30110\Map_30110\sc_e_0028.json",
					winScript =  @"",
					bossBattleBackground = 6,
					bossbgm = 27,
					itemLocations = { },
					battleFlagGroupID = 13,
					characterXP = -1,
				},
				// Mt. Ordeals - DK Cecil
				new scenario {
					bossId = 11,
					rewardId = 9,
					bossScript = @"Res\Map\Map_30111\Map_30111_1\sc_e_0029_1.json",
					winScript =  @"Res\Map\Map_30111\Map_30111_1\sc_e_0029_2.json",
					bossBattleBackground = 14,
					bossbgm = 54,
					itemLocations = { },
					battleFlagGroupID = 4,
					characterXP = 15000,
				},
				// Baron Inn - Guard
				new scenario {
					bossId = 12,
					rewardId = -1,
					bossScript = @"Res\Map\Map_20021\Map_20021_1\sc_e_0032.json",
					winScript =  @"",
					bossBattleBackground = 17,
					bossbgm = 0,
					itemLocations = { },
					battleFlagGroupID = 4,
					characterXP = -1,
				},
				// Baron Inn - Yang
				new scenario {
					bossId = 13,
					rewardId = 10,
					bossScript = @"Res\Map\Map_20021\Map_20021_1\sc_e_0032_1.json",
					winScript =  @"Res\Map\Map_20021\Map_20021_1\sc_e_0032_2.json",
					bossBattleBackground = 17,
					bossbgm = 0,
					itemLocations = { },
					battleFlagGroupID = 4,
					characterXP = 20500,
				},
				// Baron - Baigan
				new scenario {
					bossId = 14,
					rewardId = -1,
					bossScript = @"Res\Map\Map_20011\Map_20011_1\sc_e_0033.json",
					winScript =  @"",
					bossBattleBackground = 17,
					bossbgm = 12,
					itemLocations = { },
					battleFlagGroupID = 13,
					characterXP = -1,
				},
				// Baron - Kainazzo
				new scenario {
					bossId = 15,
					rewardId = 11,
					bossScript = @"Res\Map\Map_20011\Map_20011_4\sc_e_0034.json",
					winScript =  @"Res\Map\Map_20011\Map_20011_4\sc_e_0034_1.json",
					bossBattleBackground = 17,
					bossbgm = 27,
					itemLocations = { },
					battleFlagGroupID = 13,
					characterXP = 28000,
				},
				// Baron - Odin
				new scenario {
					bossId = 16,
					rewardId = 22,
					bossScript = @"Res\Map\Map_20011\Map_20011_17\sc_e_0108.json",
					winScript =  @"Res\Map\Map_20011\Map_20011_17\sc_e_0108_1.json",
					bossBattleBackground = 17,
					bossbgm = 12,
					itemLocations = { },
					battleFlagGroupID = 13,
					characterXP = 110000,
				},
				// Magnetic Cave
				new scenario {
					bossId = 17,
					rewardId = 12,
					bossScript = @"Res\Map\Map_30131\Map_30131_10\sc_e_0037.json",
					winScript =  @"Res\Map\Map_30131\Map_30131_10\sc_e_0082.json",
					bossBattleBackground = 14,
					bossbgm = 12,
					itemLocations = { },
					battleFlagGroupID = 13,
					characterXP = 20000,
				},
				// Tower of Zot - Magus
				new scenario {
					bossId = 18,
					rewardId = -1,
					bossScript = @"Res\Map\Map_30141\Map_30141_5\sc_e_0041.json",
					winScript =  @"",
					bossBattleBackground = 19,
					bossbgm = 12,
					itemLocations = { },
					battleFlagGroupID = 13,
					characterXP = -1,
				},
				// Tower of Zot - Valvalis
				new scenario {
					bossId = 19,
					rewardId = 13,
					bossScript = @"Res\Map\Map_30141\Map_30141_8\sc_e_0044.json",
					winScript =  @"Res\Map\Map_30141\Map_30141_8\sc_e_0044_1.json",
					bossBattleBackground = 19,
					bossbgm = 27,
					itemLocations = { },
					battleFlagGroupID = 13,
					characterXP = 30000,
				},
				// Dwarf Castle - Dolls
				new scenario {
					bossId = 20,
					rewardId = -1,
					bossScript = @"Res\Map\Map_20151\Map_20151_16\sc_e_0047_1.json",
					winScript =  @"",
					bossBattleBackground = 14,
					bossbgm = 35,
					itemLocations = { },
					battleFlagGroupID = 13,
					characterXP = -1,
				},
				// Dwarf Castle - Golbez
				new scenario {
					bossId = 21,
					rewardId = 14,
					bossScript = @"Res\Map\Map_20151\Map_20151_16\sc_e_0047_2.json",
					winScript =  @"Res\Map\Map_20151\Map_20151_2\sc_e_0047_4.json",
					bossBattleBackground = 14,
					bossbgm = 0,
					itemLocations = { },
					battleFlagGroupID = 13,
					characterXP = 70000,
				},
				// Babel - Dr. Lugae
				new scenario {
					bossId = 22,
					rewardId = 15,
					bossScript = @"Res\Map\Map_30151\Map_30151_16\sc_e_0051.json",
					winScript =  @"Res\Map\Map_30151\Map_30151_16\sc_e_0051_2.json",
					bossBattleBackground = 19,
					bossbgm = 35,
					itemLocations = { },
					battleFlagGroupID = 13,
					characterXP = 80000,
				},
				// Babel - Super Cannon
				new scenario {
					bossId = 23,
					rewardId = 16,
					bossScript = @"Res\Map\Map_30151\Map_30151_12\sc_e_0052.json",
					winScript =  @"Res\Map\Map_20310\Map_20310\sc_e_0053_2.json",
					bossBattleBackground = 19,
					bossbgm = 7,
					itemLocations = { },
					battleFlagGroupID = 0,
					characterXP = 73000,
				},
				// Adamant Grotto / Tail Cave
				new scenario {
					bossId = -1,
					rewardId = 17,
					bossScript = @"",
					winScript =  @"Res\Map\Map_20141\Map_20141_1\sc_e_0105.json",
					bossBattleBackground = 19,
					bossbgm = 7,
					itemLocations = { },
					battleFlagGroupID = 0,
					characterXP = 80000,
				},
				// Feymarch - Asura
				new scenario {
					bossId = 24,
					rewardId = 18,
					bossScript = @"Res\Map\Map_20161\Map_20161_9\sc_e_0120.json",
					winScript =  @"Res\Map\Map_20161\Map_20161_9\sc_e_0120_1.json",
					bossBattleBackground = 10,
					bossbgm = 12,
					itemLocations = { },
					battleFlagGroupID = 13,
					characterXP = 80000,
				},
				// Feymarch - Leviathan
				new scenario {
					bossId = 25,
					rewardId = 19,
					bossScript = @"Res\Map\Map_20161\Map_20161_9\sc_e_0121.json",
					winScript =  @"Res\Map\Map_20161\Map_20161_9\sc_e_0121_1.json",
					bossBattleBackground = 10,
					bossbgm = 12,
					itemLocations = { },
					battleFlagGroupID = 13,
					characterXP = 85000,
				},
				// Sealed Cave
				new scenario {
					bossId = 26,
					rewardId = 20,
					bossScript = @"Res\Map\Map_30201\Map_30201_22\sc_e_0064.json",
					winScript =  @"Res\Map\Map_30201\Map_30201_23\sc_e_0063.json",
					bossBattleBackground = 9,
					bossbgm = 20,
					itemLocations = { },
					battleFlagGroupID = 13,
					characterXP = 110000,
				},
				// Sylvan Cave - PostPan
				new scenario {
					bossId = 27,
					rewardId = 21,
					bossScript = @"Res\Map\Map_30181\Map_30181_5\sc_e_0112.json",
					winScript =  @"Res\Map\Map_30181\Map_30181_5\sc_e_0110.json",
					bossBattleBackground = 11,
					bossbgm = 7,
					itemLocations = { },
					battleFlagGroupID = 0,
					characterXP = 100000,
				},
				// Upper Babil - Eblan King and Queen
				new scenario {
					bossId = 28,
					rewardId = -1,
					bossScript = @"Res\Map\Map_30171\Map_30171_6\sc_e_0056.json",
					winScript =  @"",
					bossBattleBackground = 19,
					bossbgm = 12,
					itemLocations = { },
					battleFlagGroupID = 4,
					characterXP = -1,
				},
				// Upper Babil - Rubicante
				new scenario {
					bossId = 29,
					rewardId = -1,
					bossScript = @"Res\Map\Map_30171\Map_30171_6\sc_e_0057.json",
					winScript =  @"",
					bossBattleBackground = 19,
					bossbgm = 0,
					itemLocations = { },
					battleFlagGroupID = 11,
					characterXP = -1,
				},
				// Reward 22 skipped - see Odin spot, bossId = 16
				// Bahamut
				new scenario {
					bossId = 30,
					rewardId = 23,
					bossScript = @"Res\Map\Map_30221\Map_30221_3\sc_e_0125.json",
					winScript =  @"Res\Map\Map_30221\Map_30221_3\sc_e_0125_1.json",
					bossBattleBackground = 12,
					bossbgm = 12,
					itemLocations = { },
					battleFlagGroupID = 13,
					characterXP = 180000,
				},
				// Giant Of Babel - Elements
				new scenario {
					bossId = 31,
					rewardId = 30,
					bossScript = @"Res\Map\Map_30231\Map_30231_6\sc_e_0071.json",
					winScript =  @"Res\Map\Map_30231\Map_30231_6\sc_e_0071_1.json",
					bossBattleBackground = 20,
					bossbgm = 0,
					itemLocations = { },
					battleFlagGroupID = 13,
					characterXP = -1,
				},
				// Giant Of Babel - CPU
				new scenario {
					bossId = 32,
					rewardId = 24,
					bossScript = @"Res\Map\Map_30231\Map_30231_7\sc_e_0072.json",
					winScript =  @"Res\Map\Map_30231\Map_30231_7\sc_e_0072_1.json",
					bossBattleBackground = 20,
					bossbgm = 12,
					itemLocations = { },
					battleFlagGroupID = 13,
					characterXP = 250000,
				},
				// Lunar Subterranne - Murasame
				new scenario {
					bossId = 33,
					rewardId = 25,
					bossScript = @"Res\Map\Map_30251\Map_30251_3\sc_e_0116.json",
					winScript =  @"Res\Map\Map_30251\Map_30251_3\sc_e_0116_1.json",
					bossBattleBackground = 12,
					bossbgm = 27,
					itemLocations = { },
					battleFlagGroupID = 13,
					characterXP = 210000,
				},          
				// Lunar Subterranne-Crystal Sword
				new scenario {
					bossId = 34,
					rewardId = 26,
					bossScript = @"Res\Map\Map_30251\Map_30251_7\sc_e_0117.json",
					winScript =  @"Res\Map\Map_30251\Map_30251_7\sc_e_0117_1.json",
					bossBattleBackground = 12,
					bossbgm = 27,
					itemLocations = { },
					battleFlagGroupID = 13,
					characterXP = 240000,
				},
				// Lunar Subterranne-White Spear
				new scenario {
					bossId = 35,
					rewardId = 27,
					bossScript = @"Res\Map\Map_30251\Map_30251_16\sc_e_0114.json",
					winScript =  @"Res\Map\Map_30251\Map_30251_16\sc_e_0114_1.json",
					bossBattleBackground = 12,
					bossbgm = 27,
					itemLocations = { },
					battleFlagGroupID = 13,
					characterXP = 220000,
				},
				// Lunar Subterranne-Ribbons
				new scenario {
					bossId = 36,
					rewardId = 28,
					bossScript = @"Res\Map\Map_30251\Map_30251_17\sc_e_0118.json",
					winScript =  @"Res\Map\Map_30251\Map_30251_17\sc_e_0118_1.json",
					bossBattleBackground = 12,
					bossbgm = 27, 
					itemLocations = { }, 
					battleFlagGroupID = 13,
					characterXP = 260000,
				},
				// Lunar Subterranne-Masamune
				new scenario {
					bossId = 37,
					rewardId = 29,
					bossScript = @"Res\Map\Map_30251\Map_30251_18\sc_e_0115.json",
					winScript =  @"Res\Map\Map_30251\Map_30251_18\sc_e_0115_1.json",
					bossBattleBackground = 14, 
					bossbgm = 27, 
					itemLocations = { }, 
					battleFlagGroupID = 13,
					characterXP = 290000,
				},
			};
        }
    }
}
