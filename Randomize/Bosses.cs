using CsvHelper;
using FF4FreeEnterprisePR.Common;
using FF4FreeEnterprisePR.Inventory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FF4FreeEnterprisePR.Common.Scenarios;

namespace FF4FreeEnterprisePR.Randomize
{
	public static class Bosses
	{
		static List<List<int>> locationMonsters = new List<List<int>>
		{
			new List<int> { 4, 3, 1, 8, 5, 11 },
			new List<int> { 1, 13, 12, 5, 10 },
			new List<int> { 1, 13, 12, 5, 10, 11, 22 },
			new List<int> { }, // Pick any monster at the Sandruby spot.
			new List<int> { 17, 15, 42, 14, 16, 20, 22, 9, 43, 19 },
			new List<int> { 1, 28, 8, 9, 13, 12 },
			new List<int> { 1, 28, 27, 16, 49, 36, 2, 28, 23, 35 },
			new List<int> { 36, 35, 48, 37, 38, 86, 87 },
			new List<int> { 28, 32, 37, 38, 86, 87, 23, 22 },
			new List<int> { 8, 1, 9, 28, 32, 41, 37 }, // For Mt. Ordeals part 1, use Mysidia outside
			new List<int> { 35, 36, 47, 48, 51, 16, 94 }, // For part 2, use Mt. Ordeals monsters
			new List<int> { }, // For part 3, pick any monster.
			new List<int> { }, // For Baron part 1 and 2, pick any monster.
			new List<int> { },
			new List<int> { 56, 44, 43, 15, 74, 55, 57, 58 },
			new List<int> { }, // For the Baron King spot, pick any monster.
			new List<int> { }, // For the Baron Odin spot, pick any monster.
			new List<int> { 67, 61, 66, 6, 52, 64, 53, 62, 7, 34 }, // For the Magnetic Cave, add monsters from outside of Troia
			new List<int> { 71, 72, 29, 89, 118, 121, 68, 69, 123, 113, 78, 91 },
			new List<int> { }, // For Part 2 of Zot, pick any monster.
			new List<int> { 25, 83, 77, 26, 119, 24, 18, 142 }, // Include any monster in the underground map.
			new List<int> { }, // For Golbez, just pick any monster.
			new List<int> { 71, 101, 83, 25, 26, 104, 91, 88, 130 },
			new List<int> { }, // Find any monster at the Super Cannon
			new List<int> { 122, 116, 40, 103, 158, 76, 143 },
			new List<int> { }, // For the Leviathan spot, pick any monster
			new List<int> { 80, 120, 70, 75, 127, 142, 124 },
			new List<int> { 140, 126, 85, 125, 96, 114 },
			new List<int> { }, // For the post pan fight, pick any monster
			new List<int> { 50, 6, 102, 90, 26, 48, 60, 77, 148 }, // For the Eblan King/Queen fight, use the Eblan Cave monsters
			new List<int> { 106, 110, 100, 79, 99, 63, 65, 134, 60, 95, 133 }, 
			new List<int> { 152, 139, 147, 54, 141, 161 }, 
			new List<int> { }, // Pick any monster at the Elements spot.  We want any monster to shine with 99,000 HP...
			new List<int> { 151, 138, 137, 111, 105, 146, 109 }, 
			new List<int> { 29, 115, 107, 144, 154, 131, 130 }, // For the Murasame spot, let's involve monsters on the moon overworld
			new List<int> { }, // Use any monster at the Crystal Sword spot
			new List<int> { 152, 139, 147, 54, 141, 157, 149, 135, 161, 160, 156 },
			new List<int> { }, // Same for the Ribbons/Lunarsaur spot
			new List<int> { 161, 160, 156, 145, 129, 132 }
		};

		static List<List<int>> bossStats = new List<List<int>>
		{
			//           Level,    HP,   MP,    XP,   Gil, AT CT, AGI, INT, SPI, MGI, ATK, DF, MDF, ACC
			new List<int> { 10,   570,   58,   700,   200,     2,   5,  10,  10,  10,  16,  5,  31, 90 }, // Mist Cave (Mist Dragon)
			new List<int> { 10,  4000,  200,  6000,  1600,     2,  15,  25,  25,  25,  40, 10,  41, 90 }, // Mist Town (Rydia)
			new List<int> { 10,  5500,   33,  7120,  1880,     3,   2,   0,   0,   0,  62,  2,  12, 91 }, // Kaipo Inn (General/Baron Soldiers)    
			new List<int> { 18,  5000,   80,  8000,  1920,     2,  15,   5,   5,   5,  50, 10,  25, 95 }, // Kaipo Sandruby (Waterhag)
			new List<int> { 10,  2350,  293,  1200,   500,     2,  31,   0,   0,   0,  22,  0,  25, 99 }, // Waterfall (Octomammoth)
			new List<int> { 10,  1250,   50,  1000,   200,     3,  25,   2,   2,   2,  30, 10,  30, 90 }, // Damcyan (random battle) - 5
			new List<int> {  2,  1100,  137,  1500,   800,     2,   5,   0,   0,   0,  11,  3,  11, 85 }, // Antlion
			new List<int> { 15,  2000, 1375,  1900,  1200,     3,   7,   5,   5,   5,  30,  1,   9, 80 }, // Mt. Hobs (Mom Bomb)
			new List<int> { 15,  1522,  188,  4069,  1050,     3,   6,  10,  10,  10,  36,  2,  11, 90 }, // Fabul Gauntlet
			new List<int> { 15,  3300,  537,  3400,  2400,     3,   8,  15,  15,  15,  35,  2,   0, 90 }, // Ordeals (Milon)
			new List<int> { 15,  3523,  440,  3600,  2500,     3,   9,  31,  31,  31,  46,  1,  22, 99 }, // Ordeals (Milon Z) - 10
			new List<int> { 17,  2000,  565,  2000,  1000,     3,   5,   0,   0,   0,  46,  0,  11, 99 }, // Ordeals (DK Cecil)
			new List<int> { 18,   560,   70,  1420,   460,     4,  11,  26,  26,  26,  40,  3,  14, 70 }, // Baron Inn (Guard)
			new List<int> { 18,  4000, 7750,  4000,  2000,     6,   4,   0,   0,   0,  86,  0,   0, 99 }, // Baron Inn (Yang)
			new List<int> {  9,  5392,  665,  4820,  3000,     5,   8,   9,   9,   9,  58,  1,  11, 70 }, // Baron Castle (Baigan)
			new List<int> { 16,  5312,  664,  5500,  4000,     3,  15,  29,  29,  29,  44,  2,  48, 99 }, // Baron Castle (Cagnazzo) - 15
			new List<int> { 53, 20001, 2500, 18000,     0,     9,  53,  95,  95,  95, 116,  5,  38, 85 }, // Baron Castle (Odin)
			new List<int> { 19,  3927,  490,  6000,  5000,     6,  11,  15,  15,  15,  80,  1, 254, 90 }, // Magnetic Cavern (Dark Dragon)
			new List<int> { 16,  9880, 1220,  7500,  9000,     3,   7,  11,  11,  11,  36,  2,  11, 85 }, // Zot (Magus Sisters)
			new List<int> { 36,  8636, 1079,  9000,  5500,     6,  18,  63,  63,  63,  82,  0,  12, 90 }, // Zot (Valvalis)
			new List<int> { 47, 10529, 1315, 12000,  5000,     8,  16,  41,  41,  41, 106,  2,  25, 90 }, // Dwarf Castle - Dolls - 20
			new List<int> { 47, 12000, 2875, 15000, 10000,     6,  27,   1,   1,   1,  86,  0,   0, 99 }, // Dwarf Castle - Golbez
			new List<int> { 32, 19089, 2386, 21101,  8500,     6,  27,  24,  24,  24,  86,  1,  11, 90 }, // Lower Babil (Dr. Lugae)
			new List<int> { 16,  4000,   24,  5790,   135,     5,  18,   0,   0,   0,  56,  0,   0, 70 }, // Lower Babil (Dark Imps)
			new List<int> { 63, 31005, 3875, 20000,     0,    10,  86,  69,  69,  69, 134,  3,  37, 99 }, // Feymarch - Asura
			new List<int> { 79, 50001, 6250, 28000,     0,    13,  53,  34,  34,  34, 174,  5,  54, 99 }, // Feymarch - Leviathan - 25
			new List<int> { 37, 28000, 3500, 23000,  8000,     6,  86,  79,  79,  79,  84,  3,  29, 90 }, // Sealed Cave (Evil Wall)
			new List<int> { 45, 12600, 1575, 16923,  1374,     9,  30,   0,   0,   0, 112,  3,  23, 75 }, // Sylvan Cave (Post-Pan; Use 3 Malboros)
			new List<int> { 15, 10000, 7500,  8000,  3000,     9,  53,   0,   0,   0, 116,  0,   0, 85 }, // Upper Babil-Eblan
			new List<int> { 50, 34000, 4250, 18000,  7000,     8,  38,  16,  16,  16,  80,  3,  37, 70 }, // Upper Babil-Rubicante
			new List<int> { 47, 45001, 5625, 35000,     0,    13,  86,  17,  17,  17, 174,  1,   4, 99 }, // Bahamut Cave - 30
			new List<int> { 79, 99000, 7500, 62500, 10000,    10,  99,  31,  31,  31, 132,  3,   5, 90 }, // Elemental Lords
			new List<int> { 48, 36000, 4500, 50000, 10333,    13,  38, 127, 127, 127, 174,  4,  38, 99 }, // CPU
			new List<int> { 98, 32700, 4087, 55000,     0,    12,  43,  31,  31,  31, 156,  5,  48, 80 }, // Murasame (White Dragon)
			new List<int> { 99, 60000, 7500, 64000,     0,    12,  80,   8,   8,   8, 160,  5,  52, 90 }, // Crystal Sword (Dark Bahamut)
			new List<int> { 96, 33333, 4166, 31108,   550,    11,  29,   0,   0,   0, 146,  5,  38, 90 }, // White Spear (Plague) - 35
			new List<int> { 99, 46000, 5750, 59000,     0,    11,  43,  54,  54,  54, 144,  4, 254, 85 }, // Ribbons (Lunasaur)
			new List<int> { 62, 50000, 6250, 61000,     0,    11,  38, 127, 127, 127, 150,  4,  40, 99 }, // Masamune (Ogopogo) - 37
		};

		static List<int> normalMonsters = new List<int>
		{
			1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 32, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59,
			60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 109, 110, 111,
			112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 123, 124, 125, 126, 127, 128, 129, 130, 131, 132, 133, 134, 135, 136, 137, 138, 139, 140, 141, 142, 143, 144, 145, 146, 147, 148, 149, 150, 151, 152, 154,
			156, 157, 158, 159, 160, 161
		};

		static List<int> invalidSummonLocations = new List<int> { 0, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };

		//static List<string> bosses = new List<string> // 39 bosses
		//{
		//	"Mist Dragon",
		//	"General/Baron Soldiers",
		//	"Octomammoth",
		//	"Antlion",
		//	"Mom Bomb",
		//	"Fabul Gauntlet", // 5 normal battles
		//	"Milon",
		//	"Milon Z",
		//	"DK Cecil",
		//	"2 Guards", // This will be randomized
		//	"Yang",
		//	"Baigan",
		//	"Cagnazzo",
		//	"Dark Dragon",
		//	"Magus Sisters",
		//	"Barbariccia",
		//	"Calcobrena",
		//	"Golbez",
		//	"Dr. Lugae", // 2 battles
		//	"3 Dark Imps", // This will be randomized
		//	"Queen/King Eblan",
		//	"Rubicante",
		//	"Demon Wall",
		//	"Asura",
		//	"Leviathan",
		//	"Odin",
		//	"Bahamut",
		//	"Elemental Lords",
		//	"CPU",
		//	"White Dragon",
		//	"Dark Bahamut",
		//	"Plague",
		//	"Lunasaurs",
		//	"Ogopogo",
		//	"Regular Monster",
		//	"Regular Monster",
		//	"Regular Monster",
		//	"Random Monster"
		//	// Note:  Regular monsters should be reformed with boss bit = 1
		//};

		public static void establishBosses(string directory, string dataDirectory, Random r1)
		{
			List<List<int>> pairings = new List<List<int>>();
			List<int> validLocations = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 };
			List<int> validBosses = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37 };

			// Debug
			//bool firstLocation = true;
			while (validLocations.Count > 0)
			{
				int locationID = validLocations[r1.Next() % validLocations.Count];
				int bossID = validBosses[r1.Next() % validBosses.Count];

				// Debug
				//if (firstLocation) { locationID = 0; bossID = 18; firstLocation = false; }

				pairings.Add(new List<int>() { locationID, bossID });
				validLocations.Remove(locationID);
				validBosses.Remove(bossID);
			}

			List<singleMonster> allMonsters;
			List<singleGroup> groups;

			// Need to load monster stats and infuse them with the randomized boss locations.
			using (StreamReader reader = new(Path.Combine("csv", "monster.csv")))
			using (CsvReader csv = new(reader, System.Globalization.CultureInfo.InvariantCulture))
				allMonsters = csv.GetRecords<singleMonster>().ToList();

			using (StreamReader reader = new(Path.Combine("csv", "monster_party.csv")))
			using (CsvReader csv = new(reader, System.Globalization.CultureInfo.InvariantCulture))
				groups = csv.GetRecords<singleGroup>().ToList();

			List<scenario> scenarios = getScripts();

			singleGroup dkDragonGroup = groups.Where(c => c.id == 537).Single();
			dkDragonGroup.id = 599;

			foreach (List<int> pairing in pairings)
			{
				scenario currentReward = scenarios.Where(c => c.bossId == pairing[0]).SingleOrDefault();

				singleMonster monster;
				singleGroup monsterParty = new singleGroup();
				switch (pairing[1])
				{
					case 0: // Mist Dragon
						monster = allMonsters.Where(c => c.id == 162).Single();
						adjustMonsterStats(monster, pairing[0], 100);

						monsterParty = groups.Where(c => c.id == 223).ToList()[0];
						break;
					case 1: // Soldiers / Baron Captain
						monster = allMonsters.Where(c => c.id == 31).Single();
						adjustMonsterStats(monster, pairing[0], 80, 46);
						monster = allMonsters.Where(c => c.id == 30).Single();
						adjustMonsterStats(monster, pairing[0], 10, 18);

						monsterParty = groups.Where(c => c.id == 238).Single();
						break;
					case 2: // Octomammoth
						monster = allMonsters.Where(c => c.id == 163).Single();
						adjustMonsterStats(monster, pairing[0], 100);

						monsterParty = groups.Where(c => c.id == 224).Single();
						break;
					case 3: // Antlion
						monster = allMonsters.Where(c => c.id == 164).Single();
						adjustMonsterStats(monster, pairing[0], 100);

						monsterParty = groups.Where(c => c.id == 225).Single();
						break;
					case 4:
						// Mom Bomb is worth 100% experience...
						monster = allMonsters.Where(c => c.id == 165).Single();
						adjustMonsterStats(monster, pairing[0], 76, 100);

						// The three grey bombs are worth a combined 72% experience...
						monster = allMonsters.Where(c => c.id == 87).Single().clone(301);
						adjustMonsterStats(monster, pairing[0], 5, 24);
						allMonsters.Add(monster);

						// The three bombs are worth a combined 27% experience...
						monster = allMonsters.Where(c => c.id == 86).Single().clone(302);
						adjustMonsterStats(monster, pairing[0], 3, 9);
						allMonsters.Add(monster);

						// Adjust monster party to 301/302, not 86/87.
						monsterParty = groups.Where(c => c.id == 536).Single();
						monsterParty.monster2 = 302;
						monsterParty.monster7 = 302;
						monsterParty.monster9 = 302;
						monsterParty.monster1 = 301;
						monsterParty.monster3 = 301;
						monsterParty.monster8 = 301;
						monsterParty = groups.Where(c => c.id == 226).Single();
						break;
					case 5:
						// Fabul Gauntlet; curate monster battles based on the location.
						// No need to change monster stats though!
						if (currentReward != null)
							groups = FabulGauntlet(pairing[0], groups, r1, currentReward.bossBattleBackground, currentReward.bossbgm, currentReward.battleFlagGroupID);
						else
							groups = FabulGauntlet(pairing[0], groups, r1, 1, 12, 13);
						break;
					case 6: // Milon
						monster = allMonsters.Where(c => c.id == 166).Single();
						adjustMonsterStats(monster, pairing[0], 80, 100);
						monster = allMonsters.Where(c => c.id == 212).Single();
						adjustMonsterStats(monster, pairing[0], 5, 0);

						monsterParty = groups.Where(c => c.id == 227).Single();
						break;
					case 7: // Milon Z
						monster = allMonsters.Where(c => c.id == 167).Single();
						adjustMonsterStats(monster, pairing[0], 100);

						monsterParty = groups.Where(c => c.id == 228).Single();
						break;
					case 8: // DK Cecil
						monster = allMonsters.Where(c => c.id == 206).Single();
						adjustMonsterStats(monster, pairing[0], 100);

						monsterParty = groups.Where(c => c.id == 247).Single();
						break;
					case 9: // 2 guards
						// Do not allow Summoners or Security Eyes to appear in early boss spots
						int normalMonsterID = 122;
						if (invalidSummonLocations.Contains(normalMonsterID))
						{
							while (normalMonsterID == 122 && normalMonsterID == 151)
								normalMonsterID = normalMonsters[r1.Next() % normalMonsters.Count];
						} else
						{
							normalMonsterID = normalMonsters[r1.Next() % normalMonsters.Count];
						}
						normalMonsterID = SummonerAdjust(normalMonsterID, r1);

						monster = allMonsters.Where(c => c.id == normalMonsterID).Single().clone(303);
						adjustMonsterStats(monster, pairing[0], 50, 50, true);
						monster.boss = 1;
						monster.resistance_condition = 111;
						allMonsters.Add(monster);

						monsterParty = groups.Where(c => c.id == 251).Single();
						monsterParty.monster3 = 303;
						monsterParty.monster4 = 303;
						break;
					case 10: // Yang
						monster = allMonsters.Where(c => c.id == 205).Single();
						adjustMonsterStats(monster, pairing[0], 100);

						monsterParty = groups.Where(c => c.id == 243).Single();
						break;
					case 11: // Baigan
						monster = allMonsters.Where(c => c.id == 168).Single();
						adjustMonsterStats(monster, pairing[0], 90, 100);
						monster = allMonsters.Where(c => c.id == 169).Single();
						adjustMonsterStats(monster, pairing[0], 5, 0);
						monster = allMonsters.Where(c => c.id == 170).Single();
						adjustMonsterStats(monster, pairing[0], 5, 0);

						monsterParty = groups.Where(c => c.id == 229).Single();
						break;
					case 12: // Cagnazzo
						monster = allMonsters.Where(c => c.id == 171).Single();
						adjustMonsterStats(monster, pairing[0], 100);
						adjustCagnazzo(bossStats[pairing[0]][1], directory); // Change Turtle Defense HP

						monsterParty = groups.Where(c => c.id == 230).Single();
						break;
					case 13: // Dark Dragon
						monster = allMonsters.Where(c => c.id == 196).Single();
						adjustMonsterStats(monster, pairing[0], 100);

						// Draw from the Dark Dragon monster party, not the weakened Dark Elf monster party.
						monsterParty = groups.Where(c => c.id == 599).Single();
						break;
					case 14: // Magus Sisters
						monster = allMonsters.Where(c => c.id == 174).Single();
						adjustMonsterStats(monster, pairing[0], 26, 33);
						monster = allMonsters.Where(c => c.id == 175).Single();
						adjustMonsterStats(monster, pairing[0], 48, 34);
						monster = allMonsters.Where(c => c.id == 176).Single();
						adjustMonsterStats(monster, pairing[0], 26, 33);

						monsterParty = groups.Where(c => c.id == 233).Single();
						break;
					case 15: // Barbariccia
						monster = allMonsters.Where(c => c.id == 178).Single();
						adjustMonsterStats(monster, pairing[0], 100);

						monsterParty = groups.Where(c => c.id == 235).Single();
						break;
					case 16: // Calcobrena
						// Award full points for Calcobrena.
						monster = allMonsters.Where(c => c.id == 180).Single();
						adjustMonsterStats(monster, pairing[0], 50, 100);
						// Award only 51% XP for Calco and Brena separated.
						monster = allMonsters.Where(c => c.id == 179).Single();
						adjustMonsterStats(monster, pairing[0], 13, 10);
						monster = allMonsters.Where(c => c.id == 211).Single();
						adjustMonsterStats(monster, pairing[0], 4, 7);

						monsterParty = groups.Where(c => c.id == 424).Single();
						break;
					case 17: // Golbez
						monster = allMonsters.Where(c => c.id == 181).Single();
						adjustMonsterStats(monster, pairing[0], 90, 100);
						monster = allMonsters.Where(c => c.id == 182).Single();
						monster.hp = 500;
						adjustMonsterStats(monster, pairing[0], 10, 0);
						monster.initial_condition = 0; // Remove invincibility from the Shadow Dragon
													   // TODO:  Make monster party Golbez + Shadow Dragon

						monsterParty = groups.Where(c => c.id == 425).Single();
						monsterParty.monster3 = 182;
						break;
					case 18: // Dr. Lugae
						// Doctor and Barnabas
						monster = allMonsters.Where(c => c.id == 183).Single();
						adjustMonsterStats(monster, pairing[0], 25, 0);
						monster = allMonsters.Where(c => c.id == 184).Single();
						adjustMonsterStats(monster, pairing[0], 25, 0);
						monster = allMonsters.Where(c => c.id == 213).Single();
						adjustMonsterStats(monster, pairing[0], 20, 0);
						// The REAL fight!
						monster = allMonsters.Where(c => c.id == 185).Single();
						adjustMonsterStats(monster, pairing[0], 50, 100);
						monster.drop_rate = 5;
						monster.drop_content_id1 = 2;
						monster.drop_content_id2 = 2;
						monster.drop_content_id3 = 2;
						monster.drop_content_id4 = 3;

						monsterParty = groups.Where(c => c.id == 426).Single();
						monsterParty.id = pairing[0] == 17 ? 537 : 600 + (5 * pairing[0]) + 3;
						if (currentReward != null)
						{
							monsterParty.battle_bgm_asset_id = currentReward.bossbgm;
							monsterParty.battle_background_asset_id = currentReward.bossBattleBackground;
						}
						monsterParty.battle_flag_group_id = 4; // Continue music and do not show victory screen after defeat of first part.  Also consider "1, 3, 5" if "2" doesn't work.
						monsterParty = groups.Where(c => c.id == 438).Single();
						break;
					case 19: // 3 Dark Imps (pick any monster)
						 // Do not allow Summoners or Security Eyes to appear in early boss spots
						int darkImpMonsterID = 122;
						if (invalidSummonLocations.Contains(darkImpMonsterID))
						{
							while (darkImpMonsterID == 122 && darkImpMonsterID == 151)
								darkImpMonsterID = normalMonsters[r1.Next() % normalMonsters.Count];
						}
						else
						{
							darkImpMonsterID = normalMonsters[r1.Next() % normalMonsters.Count];
						}
						darkImpMonsterID = SummonerAdjust(darkImpMonsterID, r1);

						monster = allMonsters.Where(c => c.id == darkImpMonsterID).Single().clone(304);
						adjustMonsterStats(monster, pairing[0], 34, 34, true);
						monster.id = 304;
						monster.boss = 1;
						monster.resistance_condition = 111;
						allMonsters.Add(monster);

						monsterParty = groups.Where(c => c.id == 257).Single();
						monsterParty.monster1 = 304;
						monsterParty.monster2 = 304;
						monsterParty.monster3 = 304;
						break;
					case 20: // Queen/King Eblan
						monster = allMonsters.Where(c => c.id == 186).Single();
						adjustMonsterStats(monster, pairing[0], 50, 50);
						monster = allMonsters.Where(c => c.id == 187).Single();
						adjustMonsterStats(monster, pairing[0], 50, 50);

						monsterParty = groups.Where(c => c.id == 255).Single();
						break;
					case 21: // Rubicante
						monster = allMonsters.Where(c => c.id == 188).Single();
						adjustMonsterStats(monster, pairing[0], 100);

						monsterParty = groups.Where(c => c.id == 256).Single();
						break;
					case 22: // Demon Wall
						monster = allMonsters.Where(c => c.id == 192).Single();
						adjustMonsterStats(monster, pairing[0], 100);

						monsterParty = groups.Where(c => c.id == 432).Single();
						break;
					case 23: // Asura
						monster = allMonsters.Where(c => c.id == 193).Single();
						adjustMonsterStats(monster, pairing[0], 100);

						monsterParty = groups.Where(c => c.id == 433).Single();
						break;
					case 24: // Leviathan
						monster = allMonsters.Where(c => c.id == 190).Single();
						adjustMonsterStats(monster, pairing[0], 100);

						monsterParty = groups.Where(c => c.id == 430).Single();
						break;
					case 25: // Odin
						monster = allMonsters.Where(c => c.id == 189).Single();
						adjustMonsterStats(monster, pairing[0], 100);

						monsterParty = groups.Where(c => c.id == 252).Single();
						break;
					case 26: // Bahamut
						monster = allMonsters.Where(c => c.id == 191).Single();
						adjustMonsterStats(monster, pairing[0], 100);

						monsterParty = groups.Where(c => c.id == 431).Single();
						break;
					case 27: // Elemental Lords
						monster = allMonsters.Where(c => c.id == 194).Single();
						adjustMonsterStats(monster, pairing[0], 60);
						monster = allMonsters.Where(c => c.id == 195).Single();
						adjustMonsterStats(monster, pairing[0], 40);
						monster = allMonsters.Where(c => c.id == 228).Single();
						adjustMonsterStats(monster, pairing[0], 50);
						monster = allMonsters.Where(c => c.id == 227).Single();
						adjustMonsterStats(monster, pairing[0], 30);

						// Adjust AI scripts for HP value breaks.  66% for part 1, 18% for part 2, 66% for part 3.
						adjustElementalLords(bossStats[pairing[0]][1], directory);

						monsterParty = groups.Where(c => c.id == 434).Single();
						break;
					case 28: // CPU
						monster = allMonsters.Where(c => c.id == 198).Single();
						adjustMonsterStats(monster, pairing[0], 84, 100);
						monster = allMonsters.Where(c => c.id == 199).Single();
						adjustMonsterStats(monster, pairing[0], 8, 0);
						monster = allMonsters.Where(c => c.id == 214).Single();
						adjustMonsterStats(monster, pairing[0], 8, 0);

						monsterParty = groups.Where(c => c.id == 222).Single();
						break;
					case 29: // White Dragon
						monster = allMonsters.Where(c => c.id == 159).Single();
						adjustMonsterStats(monster, pairing[0], 100);

						monsterParty = groups.Where(c => c.id == 508).Single();
						break;
					case 30: // Dark Bahamut
						monster = allMonsters.Where(c => c.id == 153).Single();
						adjustMonsterStats(monster, pairing[0], 100);

						monsterParty = groups.Where(c => c.id == 509).Single();
						break;
					case 31: // Plague
						monster = allMonsters.Where(c => c.id == 108).Single();
						adjustMonsterStats(monster, pairing[0], 100);

						monsterParty = groups.Where(c => c.id == 511).Single();
						break;
					case 32: // Lunarsaurs
						monster = allMonsters.Where(c => c.id == 150).Single();
						adjustMonsterStats(monster, pairing[0], 50, 50);

						monsterParty = groups.Where(c => c.id == 510).Single();
						break;
					case 33: // Ogopogo
						monster = allMonsters.Where(c => c.id == 155).Single();
						adjustMonsterStats(monster, pairing[0], 100);

						monsterParty = groups.Where(c => c.id == 507).Single();
						break;
					case 34:
					case 35:
					case 36:
					case 37:
						if (locationMonsters[pairing[0]].Count == 0 || pairing[1] == 37)
						{
							// Do not allow Summoners or Security Eyes to appear in early boss spots
							int singleMonsterID2 = 122;
							if (invalidSummonLocations.Contains(singleMonsterID2))
							{
								while (singleMonsterID2 == 122 && singleMonsterID2 == 151)
									singleMonsterID2 = normalMonsters[r1.Next() % normalMonsters.Count];
							}
							else
							{
								singleMonsterID2 = normalMonsters[r1.Next() % normalMonsters.Count];
							}

							singleMonsterID2 = SummonerAdjust(singleMonsterID2, r1);
							monster = allMonsters.Where(c => c.id == singleMonsterID2).Single().clone(400 + pairing[0]);
						}
						else
						{
							List<int> locSingleMonsters = locationMonsters[pairing[0]];
							int singleMonsterID = locSingleMonsters[r1.Next() % locSingleMonsters.Count];
							monster = allMonsters.Where(c => c.id == singleMonsterID).Single().clone(400 + pairing[0]);
						}
						adjustMonsterStats(monster, pairing[0], 100);
						monster.boss = 1;
						monster.resistance_condition = 111;
						allMonsters.Add(monster);

						monsterParty = new singleGroup();
						monsterParty.appearance_production = 1;
						monsterParty.get_value = 0;
						monsterParty.get_ap = 0;
						monsterParty.monster5 = monster.id;
						groups.Add(monsterParty);

						break;
				}

				// We'll set the first monster group to 537.  If it's the Fabul Gauntlet or Dr. Lugae, it will be set elsewhere.
				if (pairing[0] == 17 && pairing[1] != 5 && pairing[1] != 18) // Dark Dragon party
					monsterParty.id = 537;
				else if (pairing[1] != 5)
					monsterParty.id = 600 + (5 * pairing[0]) + 4; // Adjust monster party ID to be 600 + (5 * Location ID).

				monsterParty.script_name = 0;
				monsterParty.not_escape = 1;

				// if Pairing[1] == 5, engage Fabul Gauntlet sequence.  If pairing[1] == 18, engage Dr. Lugae sequence.  Adjust numbers as required.
				// Skip if the scenario entry hasn't been recorded yet; that part is still under construction.
				if (currentReward == null) { continue; }
				monsterParty.battle_bgm_asset_id = currentReward.bossbgm;
				monsterParty.battle_background_asset_id = currentReward.bossBattleBackground;
				monsterParty.battle_flag_group_id = currentReward.battleFlagGroupID;
				// Do NOT run a back attack if Wyvern or Golbez are selected at the Milon Z fight.
				monsterParty.battle_pattern1 = pairing[0] == 10 && pairing[1] != 30 && pairing[1] != 17 ? 0 : 1;
				monsterParty.battle_pattern2 = pairing[0] == 10 && pairing[1] != 30 && pairing[1] != 17 ? 1 : 0;

				// If we draw the Calcobrena fight, we'll need to clone the monster party back into fight 424, the original ID of the fight.
				if (pairing[1] == 16)
				{
					singleGroup newParty = monsterParty.clone(424);
					groups.Add(newParty);
				}

				// Open the boss JSON to set up boss fights.
				string json = File.ReadAllText(currentReward.bossScript);
				EventJSON jEvents = JsonConvert.DeserializeObject<EventJSON>(json);

				var rewardItem = jEvents.Mnemonics.Where(c => c.comment == "SetFG1").Single();
				rewardItem.mnemonic = (pairing[1] == 5 ? "ResetFlag" : "SetFlag");
				rewardItem = jEvents.Mnemonics.Where(c => c.comment == "SetFG2").Single();
				rewardItem.mnemonic = (pairing[1] == 5 ? "ResetFlag" : "SetFlag");
				rewardItem = jEvents.Mnemonics.Where(c => c.comment == "SetFG3").Single();
				rewardItem.mnemonic = (pairing[1] == 5 ? "ResetFlag" : "SetFlag");
				rewardItem = jEvents.Mnemonics.Where(c => c.comment == "SetFG4").Single();
				rewardItem.mnemonic = (pairing[1] == 5 || pairing[1] == 18 ? "ResetFlag" : "SetFlag");

				JsonSerializer serializer = new JsonSerializer();

				using (StreamWriter sw = new StreamWriter(Path.Combine(directory, currentReward.bossScript)))
				using (JsonWriter writer = new JsonTextWriter(sw))
				{
					serializer.Serialize(writer, jEvents);
				}

				//		static List<string> locations = new List<string> { // 38 locations total
				//	"Mist Cave",
				//	"Mist Town",
				//	"Kaipo Inn",
				//	"Kaipo Sandruby",
				//	"Waterfall",
				//	"Damcyan", // 5
				//	"Antlion Cave",
				//	"Mt. Hobs",
				//	"Fabul",
				//	"Ordeals-Milon",
				//	"Ordeals-Milon Z", // 10
				//	"Ordeals-DK Cecil",
				//	"Baron Inn Guards",
				//	"Baron Inn Yang",
				//	"Baron Castle Baigan",
				//	"Baron Castle King", // 15
				//	"Baron Superboss", // Baron key
				//	"Magnetic Cavern",
				//	"Tower Of Zot - Magus",
				//	"Tower Of Zot - Valvalis",
				//	"Dwarf Castle - Dolls", // 20
				//	"Dwarf Castle - Golbez",
				//	"Lower Babil",
				//	"Babil-Super Cannon",
				//	"Feymarch-Asura",
				//	"Feymarch-Leviathan", // 25
				//	"Sealed Cave",
				//	"Sylvan Cave-PostPan",
				//	"Upper Babil-Eblan",
				//	"Upper Babil-Rubicante",
				//	"Cave Bahamut", // 30
				//	"Giant Of Babil-Elements",
				//	"Giant Of Babil-CPU",
				//	"Lunar Subterranne-Murasame",
				//	"Lunar Subterranne-Crystal Sword",
				//	"Lunar Subterranne-White Spear", // 35
				//	"Lunar Subterranne-Ribbons",
				//	"Lunar Subterranne-Masamune" // 37
				//	// Upper Babil ALWAYS rewards with the Falcon with the drill installed
				//};
			}

			// Finally, change the Tricker party group to 9 of a single monster.  This is because Tricker's won't load for some reason or another.
			var trickerGroup = groups.Where(c => c.id == 405).Single();
			var trickerMonsterID = normalMonsters[r1.Next() % normalMonsters.Count];
			trickerMonsterID = SummonerAdjust(trickerMonsterID, r1);
			trickerGroup.monster1 = trickerMonsterID;
			trickerGroup.monster2 = trickerMonsterID;
			trickerGroup.monster3 = trickerMonsterID;
			trickerGroup.monster4 = trickerMonsterID;
			trickerGroup.monster5 = trickerMonsterID;
			trickerGroup.monster6 = trickerMonsterID;
			trickerGroup.monster7 = trickerMonsterID;
			trickerGroup.monster8 = trickerMonsterID;
			trickerGroup.monster9 = trickerMonsterID;
			trickerGroup.monster1_x_position = 60;
			trickerGroup.monster2_x_position = 55;
			trickerGroup.monster3_x_position = 50;
			trickerGroup.monster4_x_position = 35;
			trickerGroup.monster5_x_position = 30;
			trickerGroup.monster6_x_position = 25;
			trickerGroup.monster7_x_position = 10;
			trickerGroup.monster8_x_position = 5;
			trickerGroup.monster9_x_position = 0;

			using (StreamWriter writer = new(Path.Combine(dataDirectory, "monster.csv")))
			using (CsvWriter csv = new(writer, System.Globalization.CultureInfo.InvariantCulture))
			{
				csv.WriteRecords(allMonsters);
			}

			groups.Sort((a, b) => b.id.CompareTo(a.id));

			using (StreamWriter writer = new(Path.Combine(dataDirectory, "monster_party.csv")))
			using (CsvWriter csv = new(writer, System.Globalization.CultureInfo.InvariantCulture))
			{
				csv.WriteRecords(groups);
			}
		}

		private static singleMonster adjustMonsterStats(singleMonster monster, int locationID, int percentage, int xpPercentage = 100, bool normalDisappear = false)
		{
			// Change Level, HP, MP, XP, Gil, Attack Count, AGI, INT, Spirit, Magic, ATK, Defense, Magic Def, Accuracy
			monster.lv = bossStats[locationID][0];
			// Add 10,000 HP for Mom Bomb.  It will explode when 76% of the boss HP count is exhausted.
			monster.hp = (monster.id == 165 ? 10000 : 0) + bossStats[locationID][1] * percentage / 100;
			monster.mp = bossStats[locationID][2] * percentage / 100;
			monster.exp = bossStats[locationID][3] * xpPercentage / 100;
			monster.gill = bossStats[locationID][4] * xpPercentage / 100;
			monster.attack_count = bossStats[locationID][5];
			monster.agility = bossStats[locationID][6];
			monster.intelligence = bossStats[locationID][7];
			monster.spirit = bossStats[locationID][8];
			monster.magic = bossStats[locationID][9];
			monster.attack = bossStats[locationID][10];
			// Do not change defenses in case a jelly is encountered.  (mwhahahaaaaaa)
			//monster.defense = bossStats[locationID][11];
			//monster.ability_defense = bossStats[locationID][12];
			monster.accuracy_rate = bossStats[locationID][13];
			// Want to make sure the monster dies when HP reaches zero.
			if (monster.monster_flag_group_id == 5)
				monster.monster_flag_group_id = 1;

			List<int> locationIDs = new List<int> { 10, 15, 19, 29 };
			// Baron Soldier, Baigan, Left/Right Arm, Sandy, Cindy, Mindy, Calco, Brina, Skullnant, Shadow Dragon, Doctor, Barnabas, King/Queen Eblan, Defense Node, Attack Node, Barnabas-Z, Bomb, Grey Bomb (both from Mom Bomb)
			List<int> monsterAvoidIDs = new List<int> { 30, 168, 169, 170, 174, 175, 176, 179, 211, 212, 182, 183, 184, 186, 187, 199, 214, 213, 301, 302 };

			if (monsterAvoidIDs.Contains(monster.id) || normalDisappear)
				monster.disappear_type_id = 1;
			else if (locationIDs.Contains(locationID))
				monster.disappear_type_id = 2;
			else
				monster.disappear_type_id = 1;

			return monster;
		}

		private static void adjustCagnazzo(int hp, string directory)
		{
			string json = File.ReadAllText(Path.Combine("Res", "Battle", "MonsterAI", "sc_ai_171_Cagnazzo.json"));
			MonsterAiJSON elementalLord = JsonConvert.DeserializeObject<MonsterAiJSON>(json);
			foreach (var ev in elementalLord.Mnemonics)
			{
				if (ev.mnemonic == "IsParameterValue")
					ev.operands.iValues[3] = hp * 13 / 100;
			}

			JsonSerializer serializer = new();

			using StreamWriter sw = new(Path.Combine(directory, "Res", "Battle", "MonsterAI", "sc_ai_171_Cagnazzo.json"));
			using JsonWriter writer = new JsonTextWriter(sw);
			serializer.Serialize(writer, elementalLord);
		}

		private static void adjustElementalLords(int hp, string directory)
		{
			string json = File.ReadAllText(Path.Combine("Res", "Battle", "MonsterAI", "sc_ai_194_ElementalLord.json"));
			MonsterAiJSON elementalLord = JsonConvert.DeserializeObject<MonsterAiJSON>(json);
			foreach (var ev in elementalLord.Mnemonics)
			{
				if (ev.mnemonic == "IsParameterValue")
					ev.operands.iValues[3] = hp * 40 / 100;
			}

			JsonSerializer serializer = new();

			using StreamWriter sw = new(Path.Combine(directory, "Res", "Battle", "MonsterAI", "sc_ai_194_ElementalLord.json"));
			using JsonWriter writer = new JsonTextWriter(sw);
			serializer.Serialize(writer, elementalLord);

			////////////////////////////////////////////////////////////////////

			json = File.ReadAllText(Path.Combine("Res", "Battle", "MonsterAI", "sc_ai_195_ElementalLord.json"));
			elementalLord = JsonConvert.DeserializeObject<MonsterAiJSON>(json);
			foreach (var ev in elementalLord.Mnemonics)
			{
				if (ev.mnemonic == "IsParameterValue")
					ev.operands.iValues[3] = hp * 11 / 100;
			}

			serializer = new();

			using StreamWriter sw2 = new(Path.Combine(directory, "Res", "Battle", "MonsterAI", "sc_ai_195_ElementalLord.json"));
			using JsonWriter writer2 = new JsonTextWriter(sw2);
			serializer.Serialize(writer2, elementalLord);

			////////////////////////////////////////////////////////////////////

			json = File.ReadAllText(Path.Combine("Res", "Battle", "MonsterAI", "sc_ai_228_ElementalLord.json"));
			elementalLord = JsonConvert.DeserializeObject<MonsterAiJSON>(json);
			foreach (var ev in elementalLord.Mnemonics)
			{
				if (ev.mnemonic == "IsParameterValue")
					ev.operands.iValues[3] = hp * 30 / 100;
			}

			serializer = new();

			using StreamWriter sw3 = new(Path.Combine(directory, "Res", "Battle", "MonsterAI", "sc_ai_228_ElementalLord.json"));
			using JsonWriter writer3 = new JsonTextWriter(sw3);
			serializer.Serialize(writer3, elementalLord);
		}

		private static int SummonerAdjust(int monsterID, Random r1)
		{
			switch (monsterID)
			{
				case 99: return (r1.Next() % 6 != 0 ? 259 + (r1.Next() % 5) : monsterID);
				case 122: return (r1.Next() % 5 != 0 ? 264 + (r1.Next() % 4) : monsterID);
				case 136: return (r1.Next() % 4 != 0 ? 268 + (r1.Next() % 3) : monsterID);
				case 151: return (r1.Next() % 4 != 0 ? 271 + (r1.Next() % 3) : monsterID);
				default: return monsterID;
			}
		}

		private static List<singleGroup> FabulGauntlet(int locationID, List<singleGroup> groups, Random r1, int background, int bgm, int flag)
		{
			List<int> monsterBattles = new List<int>();
			switch (locationID)
			{
				case 0: monsterBattles = new List<int>() { 521, 522, 523, 8, 6, 524, 525, 210, 1, 2, 3, 4, 5, 7, 209, 10, 11, 7, 9, 12, 19, 23, 20, 25, 24, 26, 27, 28 }; break; // Mist Cave
				case 1: monsterBattles = new List<int>() { 103, 101, 104, 105, 106, 107, 102, 137, 140, 211, 146, 138, 139 }; break; // Mist Town - Use Magnetic Cavern and Valvalis
				case 2: monsterBattles = new List<int>() { 103, 101, 104, 105, 106, 107, 102, 88, 89, 90, 91, 92 }; break; // Kaipo Inn - Use Magnetic Cavern and Baron King
				case 3: monsterBattles = new List<int>() { 137, 140, 211, 146, 138, 139, 108, 110, 114, 112, 117, 118, 113, 116 }; break; // Kaipo Sandruby - Use Valvalis and Magus Sisters gauntlets
				case 4: monsterBattles = new List<int>() { 17, 18, 19, 21, 22, 23, 20 }; break; // Waterfall
				case 5: monsterBattles = new List<int>() { 12, 29, 30, 31, 32 }; break; // Damcyan
				case 6: monsterBattles = new List<int>() { 521, 33, 34, 35, 36, 37, 38, 39, 40, 527, 528 }; break; // Antlion Cave
				case 7: monsterBattles = new List<int>() { 41, 52, 529, 44, 530, 46, 531, 42, 48, 52 }; break; // Mt. Hobs
				case 8: monsterBattles = new List<int>() { 54, 55, 45, 43, 47, 56, 248, 249, 250 }; break; // Fabul
				case 9: monsterBattles = new List<int>() { 57, 30, 53, 55, 59, 58 }; break; // Ordeals-Milon
				case 10: monsterBattles = new List<int>() { 48, 50, 52, 61, 62, 63, 64 }; break; // Ordeals-Milon Z
				case 11: monsterBattles = new List<int>() { 68, 69, 70, 72, 64, 71, 73, 74 }; break; // Ordeals-DK Cecil
				case 12: monsterBattles = new List<int>() { 75, 76, 77, 78, 79, 80, 81 }; break; // Baron Inn Guards
				case 13: monsterBattles = new List<int>() { 82, 78, 80, 83, 81, 84, 85 }; break; // Baron Inn Yang
				case 14: monsterBattles = new List<int>() { 86, 87, 82, 85, 88 }; break; // Baron Castle Baigan
				case 15: monsterBattles = new List<int>() { 88, 89, 90, 91, 92 }; break; // Baron Castle King
				case 16: monsterBattles = new List<int>() { 333, 345, 338, 340, 341, 343, 344, 347, 354, 356, 360, 361, 357, 362, 353, 355 }; break; // Baron Superboss - use Feymarch monsters
				case 17: monsterBattles = new List<int>() { 103, 101, 104, 105, 106, 107, 102 }; break; // Magnetic Cavern
				case 18: monsterBattles = new List<int>() { 108, 110, 114, 112, 117, 118, 113, 116 }; break; // Tower Of Zot - Magus
				case 19: monsterBattles = new List<int>() { 137, 140, 211, 146, 138, 139 }; break; // Tower Of Zot - Valvalis
				case 20: monsterBattles = new List<int>() { 258, 259, 260, 261, 262, 263, 264 }; break; // Dwarf Castle - Dolls
				case 21: monsterBattles = new List<int>() { 473, 474, 475, 476, 477, 478, 479, 480 }; break; // Dwarf Castle - Golbez
				case 22: monsterBattles = new List<int>() { 265, 266, 514, 513, 267, 268, 273 }; break; // Lower Babil
				case 23: monsterBattles = new List<int>() { 279, 280, 281, 282, 283, 284, 273 }; break; // Lower Babil - Super Cannon
				case 24: monsterBattles = new List<int>() { 333, 345, 338, 340, 341, 343, 344, 347 }; break; // Feymarch-Asura
				case 25: monsterBattles = new List<int>() { 334, 346, 339, 337, 342, 343, 344, 348 }; break; // Feymarch-Leviathan
				case 26: monsterBattles = new List<int>() { 354, 356, 360, 361, 357, 362, 353, 355 }; break; // Sealed Cave
				case 27: monsterBattles = new List<int>() { 321, 326, 329, 323, 327, 486, 331, 324 }; break; // Sylvan Cave-PostPan
				case 28: monsterBattles = new List<int>() { 141, 142, 144, 145, 147, 534, 148, 143, 151, 150 }; break; // Upper Babil-Eblan
				case 29: monsterBattles = new List<int>() { 181, 182, 168, 159, 160, 535, 169, 154, 157, 183, 179 }; break; // Upper Babil-Rubicante
				case 30: monsterBattles = new List<int>() { 385, 386, 388, 387, 389, 492, 409 }; break; // Cave Bahamut
				case 31: monsterBattles = new List<int>() { 185, 186, 187, 188, 189, 190, 191 }; break; // Giant Of Babil-Elements
				case 32: monsterBattles = new List<int>() { 189, 188, 193, 194, 195, 191, 196 }; break; // Giant Of Babil-CPU
				case 33: monsterBattles = new List<int>() { 375, 448, 516, 517, 515, 518, 520, 519, 444, 445 }; break; // Lunar Subterranne-Murasame
				case 34: monsterBattles = new List<int>() { 391, 392, 393, 394, 492, 395 }; break; // Lunar Subterranne-Crystal Sword
				case 35: monsterBattles = new List<int>() { 396, 397, 398, 400, 399, 401, 395 }; break; // Lunar Subterranne-White Spear
				case 36: monsterBattles = new List<int>() { 413, 420, 410, 411, 395 }; break; // Lunar Subterranne-Ribbons
				case 37: monsterBattles = new List<int>() { 420, 416, 412, 413, 414, 415, 418, 419 }; break; // Lunar Subterranne-Masamune
			}

			monsterBattles.Shuffle(r1);
			for (int i = 0; i < 5; i++)
			{
				singleGroup fgGroup = groups.Where(c => c.id == monsterBattles[i]).Single().clone(600 + (5 * locationID) + i);
				if (locationID == 17 && i == 0) // Set to Dark Dragon party if first battle.
					fgGroup.id = 537;
				fgGroup.battle_background_asset_id = background;
				fgGroup.battle_bgm_asset_id = bgm;
				if (i != 4)
					fgGroup.battle_flag_group_id = 4; // Continue music after defeat of first four battles.
				else
					fgGroup.battle_flag_group_id = flag;
				fgGroup.script_name = 0;
				fgGroup.not_escape = 1;
				if (locationID == 10) {	fgGroup.battle_pattern1 = 0; fgGroup.battle_pattern2 = 1; }
					else { fgGroup.battle_pattern1 = 1; fgGroup.battle_pattern2 = 0; }
				groups.Add(fgGroup);
			}

			return groups;
		}
	}
}
