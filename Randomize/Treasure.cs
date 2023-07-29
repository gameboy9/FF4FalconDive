using FF4FreeEnterprisePR.Inventory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;
using System.Threading;

namespace FF4FreeEnterprisePR.Randomize
{
	public class Treasure
	{
		private class message
		{
			[Index(0)]
			public string id { get; set; }
			[Index(1)]
			public string msgString { get; set; }
		}

		private class treasureDirectory
		{
			public string directory { get; set; }
			public int stdMinTier { get; set; }
			public int stdMaxTier { get; set; }
			public int monsterBooster { get; set; }
			public List<string> booster { get; set; }
		}

		public Treasure(Random r1, int randoLevel, string directory, bool noJItems, bool noSuper, bool includeBonus, bool includeFGExclusive, int divisor, int[] party)
		{
			List<treasureDirectory> treasureDirectories = new()
			{
				new treasureDirectory { directory = "Map_20010", stdMinTier = 3, stdMaxTier = 6, monsterBooster = 1, booster = new List<string>() { } }, // Baron Castle - 2 boss blocks
				new treasureDirectory { directory = "Map_20011", stdMinTier = 3, stdMaxTier = 6, monsterBooster = 1, booster = new List<string>() { } }, // Baron Castle - 2 boss blocks
				new treasureDirectory { directory = "Map_20020", stdMinTier = 1, stdMaxTier = 4, monsterBooster = 1, booster = new List<string>() { } }, // Baron Town
				new treasureDirectory { directory = "Map_20021", stdMinTier = 1, stdMaxTier = 4, monsterBooster = 1, booster = new List<string>() { "Map_20021_7**", "Map_20021_8**" } }, // Waterway and Weapon/Armor store behind Baron Key
				new treasureDirectory { directory = "Map_20030", stdMinTier = 1, stdMaxTier = 4, monsterBooster = 1, booster = new List<string>() { } }, // Kaipo
				new treasureDirectory { directory = "Map_20041", stdMinTier = 1, stdMaxTier = 4, monsterBooster = 1, booster = new List<string>() { } }, // Damcyan Castle
				new treasureDirectory { directory = "Map_20051", stdMinTier = 1, stdMaxTier = 4, monsterBooster = 1, booster = new List<string>() { "Map_20051_11*", "Map_20051_12*", "Map_20051_3*" } }, // Fabul - Boost for King's Tower, Throne Room
				new treasureDirectory { directory = "Map_20070", stdMinTier = 2, stdMaxTier = 6, monsterBooster = 1, booster = new List<string>() { } }, // Mist - Blocked by Bomb Ring / boss fight
				new treasureDirectory { directory = "Map_20071", stdMinTier = 2, stdMaxTier = 6, monsterBooster = 1, booster = new List<string>() { } },
				new treasureDirectory { directory = "Map_20080", stdMinTier = 2, stdMaxTier = 5, monsterBooster = 1, booster = new List<string>() { } }, // Mythril - Not commonly explored
				new treasureDirectory { directory = "Map_20091", stdMinTier = 2, stdMaxTier = 5, monsterBooster = 1, booster = new List<string>() { } }, // Eblan Castle
				new treasureDirectory { directory = "Map_20100", stdMinTier = 2, stdMaxTier = 5, monsterBooster = 1, booster = new List<string>() { } }, // Troia Town - Not commonly explored
				new treasureDirectory { directory = "Map_20111", stdMinTier = 2, stdMaxTier = 6, monsterBooster = 1, booster = new List<string>() { "Map_20111_9***" } }, // Troia Castle - Rate that treasury (Earth Crystal blocked)
				new treasureDirectory { directory = "Map_20120", stdMinTier = 2, stdMaxTier = 5, monsterBooster = 1, booster = new List<string>() { } }, // Agart
				new treasureDirectory { directory = "Map_20131", stdMinTier = 3, stdMaxTier = 6, monsterBooster = 1, booster = new List<string>() { } }, // Eblan Settlement (Hovercraft blocked)
				new treasureDirectory { directory = "Map_20151", stdMinTier = 3, stdMaxTier = 6, monsterBooster = 1, booster = new List<string>() { "Map_20151_5*", "Map_20151_6*", "Map_20151_7*", "Map_20151_12*", "Map_20151_13*", "Map_20151_14*" } }, // Dwarf Castle
				new treasureDirectory { directory = "Map_20160", stdMinTier = 4, stdMaxTier = 6, monsterBooster = 1, booster = new List<string>() { } }, // Land of Summons
				new treasureDirectory { directory = "Map_20161", stdMinTier = 4, stdMaxTier = 6, monsterBooster = 1, booster = new List<string>() { "Map_20161_1*" } }, // Land of Summons - hidden chamber boosted
				new treasureDirectory { directory = "Map_20171", stdMinTier = 4, stdMaxTier = 6, monsterBooster = 1, booster = new List<string>() { } }, // Tomra
				new treasureDirectory { directory = "Map_20181", stdMinTier = 4, stdMaxTier = 6, monsterBooster = 1, booster = new List<string>() { } }, // Kokkol's Smithy
				new treasureDirectory { directory = "Map_20210", stdMinTier = 2, stdMaxTier = 4, monsterBooster = 1, booster = new List<string>() { "Map_20210_2*", "Map_20210_4**", "Map_20210_5**" } }, // Chocobo Forest
				new treasureDirectory { directory = "Map_20220", stdMinTier = 3, stdMaxTier = 6, monsterBooster = 1, booster = new List<string>() { } }, // Chocobo Village
				new treasureDirectory { directory = "Map_30011", stdMinTier = 2, stdMaxTier = 5, monsterBooster = 1, booster = new List<string>() { } }, // Mist Cave (OK with boosting a bit)
				new treasureDirectory { directory = "Map_30021", stdMinTier = 2, stdMaxTier = 5, monsterBooster = 1, booster = new List<string>() { "Map_30021_2*", "Map_30021_9*", "Map_30021_10*" } }, // Underground Waterway
				new treasureDirectory { directory = "Map_30031", stdMinTier = 2, stdMaxTier = 5, monsterBooster = 1, booster = new List<string>() { "Map_30031_2*", "Map_30031_4*" } }, // Antlion Cave
				new treasureDirectory { directory = "Map_30050", stdMinTier = 3, stdMaxTier = 6, monsterBooster = 1, booster = new List<string>() { } }, // Mt. Hobs - blocked by boss
				new treasureDirectory { directory = "Map_30060", stdMinTier = 3, stdMaxTier = 6, monsterBooster = 1, booster = new List<string>() { } }, // Mt. Hobs - blocked by boss
				new treasureDirectory { directory = "Map_30080", stdMinTier = 3, stdMaxTier = 5, monsterBooster = 1, booster = new List<string>() { } }, // Mt. Ordeals
				new treasureDirectory { directory = "Map_30100", stdMinTier = 3, stdMaxTier = 5, monsterBooster = 1, booster = new List<string>() { } },
				new treasureDirectory { directory = "Map_30121", stdMinTier = 3, stdMaxTier = 6, monsterBooster = 1, booster = new List<string>() { "Map_30121_5*" } }, // Old Waterway
				new treasureDirectory { directory = "Map_30131", stdMinTier = 3, stdMaxTier = 6, monsterBooster = 1, booster = new List<string>() { } }, // Magnetic Cavern
				new treasureDirectory { directory = "Map_30141", stdMinTier = 3, stdMaxTier = 6, monsterBooster = 1, booster = new List<string>() { } }, // Tower of Zot (blocked by Earth Crystal)
				new treasureDirectory { directory = "Map_30151", stdMinTier = 4, stdMaxTier = 7, monsterBooster = 1, booster = new List<string>() { } }, // Lower Babel
				new treasureDirectory { directory = "Map_30161", stdMinTier = 4, stdMaxTier = 6, monsterBooster = 2, booster = new List<string>() { } }, // Cave of Eblan
				new treasureDirectory { directory = "Map_30171", stdMinTier = 4, stdMaxTier = 7, monsterBooster = 2, booster = new List<string>() { } }, // Upper Babel
				new treasureDirectory { directory = "Map_30181", stdMinTier = 4, stdMaxTier = 7, monsterBooster = 1, booster = new List<string>() { "Map_30181_4*" } }, // Sylvan Cave
				new treasureDirectory { directory = "Map_30191", stdMinTier = 4, stdMaxTier = 7, monsterBooster = 1, booster = new List<string>() { } }, // Cave of Summons
				new treasureDirectory { directory = "Map_30201", stdMinTier = 5, stdMaxTier = 8, monsterBooster = 1, booster = new List<string>() { } }, // Sealed Cavern (blocked by Luca Key)
				new treasureDirectory { directory = "Map_30211", stdMinTier = 6, stdMaxTier = 8, monsterBooster = 1, booster = new List<string>() { } }, // Lunar Path
				new treasureDirectory { directory = "Map_30221", stdMinTier = 6, stdMaxTier = 8, monsterBooster = 1, booster = new List<string>() { } }, // Cave of Bahamut
				new treasureDirectory { directory = "Map_30231", stdMinTier = 5, stdMaxTier = 8, monsterBooster = 1, booster = new List<string>() { } }, // Giant of Babel
				new treasureDirectory { directory = "Map_30251", stdMinTier = 6, stdMaxTier = 8, monsterBooster = 1, booster = new List<string>() { "Map_30251_19*", "Map_30251_20*", "Map_30251_21*" } }, // Lunar Subterrane - Boost the core.
			};

			int i = 0;

			foreach (treasureDirectory tDir in treasureDirectories)
			{
				foreach (string fileName in Directory.EnumerateFiles(Path.Combine("Res", "Map", tDir.directory), "entity_default.json", SearchOption.AllDirectories))
				{
					int trInitMaxTier = (randoLevel == 0 ? tDir.stdMaxTier + 1 :
									randoLevel == 1 ? tDir.stdMaxTier :
									randoLevel == 2 ? tDir.stdMaxTier - 1 :
					noSuper ? 8 : 9);

					int trInitMinTier = (randoLevel == 0 ? tDir.stdMinTier + 1 :
						randoLevel == 1 ? tDir.stdMinTier :
						randoLevel == 2 ? tDir.stdMinTier - 1 :
						1);

					// Boost the max tier on the Lunar Subterranne to 8 in pro mode.  This opens up tier 9 possibilities for monster chests.
					if (tDir.directory == "Map_30251" && randoLevel == 2) trInitMaxTier++;

					string booster = tDir.booster.Where(c => c.Contains(Path.GetFileName(Path.GetFullPath(Path.Combine(fileName, @".."))))).FirstOrDefault();
					if (booster != null)
					{
						trInitMinTier += booster.Contains("***") ? 3 : booster.Contains("**") ? 2 : 1;
						trInitMaxTier += booster.Contains("***") ? 3 : booster.Contains("**") ? 2 : 1;
					}

					trInitMinTier = Math.Max(1, trInitMinTier);
					trInitMinTier = Math.Min(!noSuper ? 9 : 8, trInitMinTier);
					trInitMaxTier = Math.Max(1, trInitMaxTier);
					trInitMaxTier = Math.Min(!noSuper ? 9 : 8, trInitMaxTier);

					string json = File.ReadAllText(fileName);
					EntityJSON jEvents = JsonConvert.DeserializeObject<EntityJSON>(json);
					foreach (var layer in jEvents.layers)
						foreach (var sObject in layer.objects)
						{
							int trMinTier = trInitMinTier;
							int trMaxTier = trInitMaxTier;

							bool process = sObject.properties.Where(c => c.name == "action_id" && (long)c.value == 6).Any();
							bool monster = sObject.properties.Where(c => c.name == "script_id" && (long)c.value != 0).Any();
							bool gold = false;

							// Boost at least one tier for monsters in a box
							if (monster)
							{
								//trMinTier = trInitMinTier + tDir.monsterBooster; 
								trMaxTier = trInitMaxTier + tDir.monsterBooster;
								// In Pro mode in the Lunar Subterranne, make the monster tier 7-9.  All others can be 8-9 or 9-9.
								if (tDir.directory == "Map_30251" && randoLevel == 2)
									trMinTier = trInitMaxTier - 2;
								else
									trMinTier = trInitMaxTier - 1;
								trMinTier = Math.Min(!noSuper ? 9 : 8, trMinTier);
								trMaxTier = Math.Min(!noSuper ? 9 : 8, trMaxTier);
							}

							if (process)
							{
								int trType = gold ? 0 : r1.Next() % 12;
								int finalType = 0;
								if (monster) // Only weapons and armor in monster chests
									finalType = (r1.Next() % 2) + 2;
								else
									finalType = trType == 0 ? 0 : trType >= 1 && trType <= 5 ? 1 : trType >= 6 && trType <= 8 ? 2 : 3;

								if (finalType == 0)
								{
									int goldMin = (trMinTier == 0 ? 0 : trMinTier == 1 ? 100 : trMinTier == 2 ? 300 : trMinTier == 3 ? 500 :
										trMinTier == 4 ? 1000 : trMinTier == 5 ? 2000 : trMinTier == 6 ? 10000 : trMinTier == 7 ? 20000 : trMinTier == 8 ? 50000 : 100000) / divisor;
									int goldMax = (trMaxTier == 0 ? 200 : trMaxTier == 1 ? 400 : trMaxTier == 2 ? 700 : trMaxTier == 3 ? 1000 :
										trMaxTier == 4 ? 2000 : trMaxTier == 5 ? 5000 : trMaxTier == 6 ? 20000 : trMaxTier == 7 ? 40000 : trMaxTier == 8 ? 100000 : 250000) / divisor;
									int goldAmount = (r1.Next() % (goldMax - goldMin + 1)) + goldMin;
									if (goldAmount > 100000) goldAmount = goldAmount / 10000 * 10000;
									else if (goldAmount > 10000) goldAmount = goldAmount / 1000 * 1000;
									else if (goldAmount > 1000) goldAmount = goldAmount / 100 * 100;
									else if (goldAmount > 10) goldAmount = goldAmount / 10 * 10;

									foreach (var prop in sObject.properties.Where(c => c.name == "content_id"))
										prop.value = 1;
									foreach (var prop in sObject.properties.Where(c => c.name == "content_num"))
										prop.value = goldAmount;
									foreach (var prop in sObject.properties.Where(c => c.name == "message_key"))
										prop.value = "S0005_999_a_03";
								}
								else if (finalType == 1)
								{
									foreach (var prop in sObject.properties.Where(c => c.name == "content_id"))
										prop.value = new Items().selectItem(r1, trMinTier, trMaxTier, false, noJItems);
									foreach (var prop in sObject.properties.Where(c => c.name == "content_num"))
										prop.value = 1;
									foreach (var prop in sObject.properties.Where(c => c.name == "message_key"))
										prop.value = "S0005_999_a_02";
								}
								else if (finalType == 2)
								{
									foreach (var prop in sObject.properties.Where(c => c.name == "content_id"))
										prop.value = new Weapons().selectItem(r1, trMinTier, trMaxTier, false, includeBonus, includeFGExclusive, party);
									foreach (var prop in sObject.properties.Where(c => c.name == "content_num"))
										prop.value = 1;
									foreach (var prop in sObject.properties.Where(c => c.name == "message_key"))
										prop.value = "S0005_999_a_02";
								}
								else
								{
									foreach (var prop in sObject.properties.Where(c => c.name == "content_id"))
										prop.value = new Armor().selectItem(r1, trMinTier, trMaxTier, false, includeBonus, includeFGExclusive, party);
									foreach (var prop in sObject.properties.Where(c => c.name == "content_num"))
										prop.value = 1;
									foreach (var prop in sObject.properties.Where(c => c.name == "message_key"))
										prop.value = "S0005_999_a_02";
								}
							}
						}

					JsonSerializer serializer = new();

					using StreamWriter sw = new(Path.Combine(directory, fileName));
					using JsonWriter writer = new JsonTextWriter(sw);
					serializer.Serialize(writer, jEvents);
				}
				i++;
			}
		}
	}
}