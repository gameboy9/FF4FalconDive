using CsvHelper;
using CsvHelper.Configuration.Attributes;
using FF4FreeEnterprisePR.Inventory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using static FF4FalconDive.Inventory.Messages;
using static FF4FreeEnterprisePR.Common.Scenarios;

namespace FF4FreeEnterprisePR.Randomize
{
	public static class Rewards
	{
		public class content
		{
			[Index(0)]
			public int id { get; set; }
			[Index(1)]
			public string mes_id_name { get; set; }
			[Index(2)]
			public string mes_id_battle { get; set; }
			[Index(3)]
			public string mes_id_description { get; set; }
			[Index(4)]
			public int icon_id { get; set; }
			[Index(5)]
			public int type_id { get; set; }
			[Index(6)]
			public int type_value { get; set; }
		}

		private static List<message> itemStrings;
		private static List<message> msgStrings;
		private static List<content> contentCSV = new List<content>();

		//static List<string> locations = new List<string> { // 31 locations total
		//	"Mist Cave",
		//	"Mist Town", // Bomb Ring
		//	"Kaipo Inn", // Kaipo Inn Pass (new item!)
		//	"Kaipo Sandruby", // Sandruby
		//	"Waterfall",
		//	"Damcyan", // 5
		//	"Antlion Cave",
		//	"Mt. Hobs",
		//	"Fabul",
		//	"Ordeals",
		//	"Baron Inn", // 10
		//	"Baron Castle", // Baron Key
		//	"Magnetic Cavern", // Twin Harp
		//	"Tower Of Zot", // Earth Crystal
		//	"Dwarf Castle", // Magma Key or Hook
		//	"Lower Babil", // Magma Key or Hook - 15
		//	"Babil-Super Cannon", // Magma Key or Hook + Tower Key
		//	"Tail Cave", // Hook + Rat Tail
		//	"Feymarch-Asura", // Magma Key or Hook
		//	"Feymarch-Leviathan", // Magma Key or Hook
		//	"Sealed Cave", // Magma Key or Hook + Luca Key - 20
		//	"Sylvan Cave-PostPan", // Magma Key or Hook + Pan
		//	"Baron Superboss", // Baron key
		//	"Cave Bahamut", // Darkness Crystal
		//	"Giant Of Babil 1", // Darkness Crystal
		//	"Lunar Subterranne-Murasame", // Darkness Crystal - 25
		//	"Lunar Subterranne-Crystal Sword", // Darkness Crystal
		//	"Lunar Subterranne-White Spear", // Darkness Crystal
		//	"Lunar Subterranne-Ribbons", // Darkness Crystal
		//	"Lunar Subterranne-Masamune" // Darkness Crystal
		//	"Giant Of Babil 2", // Darkness Crystal - 30
		//  "Nothing Vending Machine", // 3 Nothings - 31
		//	// Upper Babil ALWAYS rewards with the Falcon with the drill installed
		//	};

		//static List<string> rewards = new List<string> // 31 rewards
		//{
		//	"Character 2",
		//	"Character 3",
		//	"Character 4",
		//	"Character 5",
		//	"Bomb Ring", - 71
		//	"Kaipo Inn Pass", - 80 NEW // 5
		//	"Legend Sword", - 131
		//	"Sand Ruby", - 64
		//	"Baron Key", - 68
		//	"Twin Harp", - 69
		//	"Earth Crystal", - 66 // 10
		//	"Magma Key", - 72
		//	"Tower Key", - 75
		//	"Hovercraft", - 81 NEW
		//	"Luca Key", - 74
		//	"Darkness Crystal", - 73 // 15
		//	"Rat Tail", - 67
		//	"Yellow Tail", - 82 NEW
		//	"Green Tail", - 83 NEW
		//	"Blue Tail", - 84 NEW
		//	"Red Tail", - 85 NEW // 20
		//	"Black Tail", - 87 NEW
		//	"Pink Tail", - 70
		//	"Trash Can", - 88 NEW
		//	"Pan", - 62
		//	"Adamant", - 63 // 25
		//	"Crystal Shard 1", - 89 NEW
		//	"Crystal Shard 2", - 89 NEW
		//	"Crystal Shard 3", - 89 NEW
		//	"Crystal Shard 4"  - 89 NEW // 29
		//	"Orange Tail", - 91 NEW
		//	"White Tail", - 92 NEW
		//};

		static List<int> initialLocations = new List<int> { 0, 4, 5, 6, 7, 8, 9, 10 };
		static List<List<int>> itemLocations = new List<List<int>> { 
			new List<int> { },
			new List<int> { },
			new List<int> { },
			new List<int> { },
			new List<int> { 1 }, // Bomb Ring
			new List<int> { 2 }, // Kaipo Inn Pass - 5
			new List<int> { },
			new List<int> { 3 }, // Sand Ruby
			new List<int> { 11, 22 }, // Baron Key
			new List<int> { 12 }, // Twin Harp
			new List<int> { 13 }, // Earth Crystal - 10
			new List<int> { 14, 15, 18, 19 }, // Magma Key -> CHECK FOR PAN, LUCA KEY, AND TOWER KEY
			new List<int> { 16 }, // Tower Key -> CHECK FOR HOOK OR MAGMA KEY
			new List<int> { 14, 15, 18, 19 }, // Hook -> CHECK FOR PAN, LUCA KEY, TOWER KEY, AND RAT TAIL
			new List<int> { 20 }, // Luca Key -> CHECK FOR HOOK OR MAGMA KEY
			new List<int> { 23, 24, 25, 26, 27, 28, 29, 30 }, // Darkness Crystal - 15
			new List<int> { 17 }, // Rat Tail - CHECK FOR HOOK
			new List<int> { }, 
			new List<int> { }, 
			new List<int> { }, 
			new List<int> { }, // 20
			new List<int> { }, 
			new List<int> { }, 
			new List<int> { }, 
			new List<int> { 21 }, // Pan - CHECK FOR HOOK OR MAGMA KEY
			new List<int> { }, // 25
			new List<int> { },
			new List<int> { },
			new List<int> { },
			new List<int> { },
			new List<int> { }, // 30
			new List<int> { } // 31
		};

		static List<int> allRewards = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 };
		static List<int> keyRewards = new List<int> { 4, 5, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 24 };
		static List<int> nothingRewards = new List<int> { 17, 18, 19, 20, 21, 26, 27 };

		class pairing
		{
			public int locationID { get; set; }
			public int rewardID { get; set; }
			public string rewardText { get; set; }

			public pairing(int pLocID, int pRewardID) 
			{
				locationID = pLocID;
				rewardID = pRewardID;
			}
		}

		public static void establishRewards(Random r1, int[] characters, string directory, string dataDirectory, bool includeBonus, bool includeFGExclusive, int[] party, double xpMultiplier)
		{
			List<pairing> pairings = new List<pairing>();
			List<pairing> tempPairings = new List<pairing>();
			List<int> validLocations = initialLocations.ToList();
			List<int> validRewards = allRewards.ToList();
			List<int> rerollLocations = new List<int> { 9, 12, 13, 14, 20, 31 };
			List<int> rerollRewards = new List<int> { 6, 16, 17, 18, 19, 20, 21, 22, 23, 25, 26, 27 };
			bool underground = false;
			bool moon = false;
			bool nothingCheck = false;

			while (validRewards.Count > 0)
			{
				int locationID = validLocations[r1.Next() % validLocations.Count];
				int rerolls = rerollLocations.Contains(locationID) ? 1 : 0;
				int rewardID = validRewards[r1.Next() % validRewards.Count];
				// Reroll if a non-progression item is rolled in Ordeals, Magnetic Cavern, Tower of Zot, Dwarf Castle, or Sealed Cave
				if (rerolls == 1 && rerollRewards.Contains(rewardID))
					rewardID = validRewards[r1.Next() % validRewards.Count];

				// Debug
				//if (tempPairings.Count == 0 && pairings.Count == 0)
				//{
				//	locationID = 0; rewardID = 17;
				//}

				tempPairings.Add(new pairing(locationID, rewardID));
				validLocations.Remove(locationID);
				validRewards.Remove(rewardID);

				if (validLocations.Count == 0 && validRewards.Count == 0)
					break;

				bool locationAdded = false;

				if (pairings.Where(c => nothingRewards.Contains(c.rewardID)).Count() + tempPairings.Where(c => nothingRewards.Contains(c.rewardID)).Count() >= 3 && !nothingCheck)
				{
					validLocations.Add(31);
					pairings.AddRange(tempPairings);
					tempPairings.Clear();
					nothingCheck = true;
				}

				if (itemLocations[rewardID].Count() > 0)
				{
					// NEXT 2 LINES:  Debug
					if (rewardID == 14) rewardID = 14;
					if (locationID == 20) locationID = 20;
					List<int> newLocations = itemLocations[rewardID].ToList();

					bool doNotProcess = false;

					// If the Tower Key, Luca Key, or Pan are awarded, but the Magma Key or Hook has not, then don't process additional locations yet.
					if ((rewardID == 12 || rewardID == 14 || rewardID == 24) && !pairings.Exists(c => c.rewardID == 11) && !pairings.Exists(c => c.rewardID == 13) && !tempPairings.Exists(c => c.rewardID == 11) && !tempPairings.Exists(c => c.rewardID == 13))
						doNotProcess = true;
					// Same for the Rat Tail, except just the Hook only.
					if ((rewardID == 16) && !pairings.Exists(c => c.rewardID == 13) && !tempPairings.Exists(c => c.rewardID == 13))
						doNotProcess = true;

					if (!doNotProcess)
					{
						// If the Magma Key or the Hook has been awarded, and the Tower Key has previously been awarded, then add the Super Cannon.
						if ((rewardID == 11 || rewardID == 13) && (pairings.Exists(c => c.rewardID == 12) || tempPairings.Exists(c => c.rewardID == 12))) // Tower Key
							newLocations.AddRange(itemLocations[12]);
						if ((rewardID == 11 || rewardID == 13) && (pairings.Exists(c => c.rewardID == 14) || tempPairings.Exists(c => c.rewardID == 14))) // Luca Key
							newLocations.AddRange(itemLocations[14]);
						if ((rewardID == 11 || rewardID == 13) && (pairings.Exists(c => c.rewardID == 24) || tempPairings.Exists(c => c.rewardID == 24))) // Pan
							newLocations.AddRange(itemLocations[24]);
						if (rewardID == 13 && (pairings.Exists(c => c.rewardID == 16) || tempPairings.Exists(c => c.rewardID == 16))) // Rat tail
							newLocations.AddRange(itemLocations[16]);

						foreach (int location in newLocations)
						{
							if (!validLocations.Exists(c => c == location) && !pairings.Exists(c => c.locationID == location) && !tempPairings.Exists(c => c.locationID == location))
							{
								validLocations.Add(location);
								locationAdded = true;
							}
						}

						if (locationAdded)
						{
							if (tempPairings.Where(c => c.rewardID == 15 || c.rewardID == 13).Count() > 0)
								moon = true;
							if (tempPairings.Where(c => c.rewardID == 11).Count() > 0)
								underground = true;
							pairings.AddRange(tempPairings);
							tempPairings.Clear();

							int charsRequired = -1;

							if (moon)             charsRequired = 3;
							else if (underground) charsRequired = 2;

							// Enforce at least 3 characters when moon access is found or you have to go through the hovercraft route.
							// Enforce at least 2 characters when underground access is found.
							if (charsRequired > 1)
							{
								int chars = 1 + pairings.Where(c => c.rewardID <= 3).Count();
								while (chars < charsRequired)
								{
									// Replace non-progression items with characters.
									pairing pair = pairings.Where(c => c.rewardID == 6 || c.rewardID == 12 || c.rewardID == 14 || c.rewardID >= 16).FirstOrDefault();
									if (pair != null)
									{
										validRewards.Add(pair.rewardID);
										List<int> validChars = validRewards.Where(c => c <= 3).ToList();
										pair.rewardID = validChars[r1.Next() % validChars.Count()];
										validRewards.Remove(pair.rewardID);
										chars++;
									}
									else
									{
										// If a non-progression item can't be found, reset everything and start over.
										pairings = new List<pairing>();
										tempPairings = new List<pairing>();
										validLocations = initialLocations.ToList();
										validRewards = allRewards.ToList();
										break;
									}
								}
							}
						}
					}
				} 

				if (validLocations.Count == 0)
				{
					foreach(var pairing in tempPairings)
					{
						validLocations.Add(pairing.locationID);
						validRewards.Add(pairing.rewardID);
					}
					tempPairings.Clear();
				}
			}

			if (tempPairings.Count > 0)
			{
				pairings.AddRange(tempPairings);
			}

			pairings = pairingSort(pairings);

			// Now fill in the rewards!
			// Initialization

			using (StreamReader reader = new StreamReader(Path.Combine("Data", "Master", "content.csv")))
			{
				CsvHelper.Configuration.CsvConfiguration config = new CsvHelper.Configuration.CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture);
				config.Delimiter = ",";
				config.HasHeaderRecord = true;
				config.BadDataFound = null;

				using (CsvReader csv = new CsvReader(reader, config))
					contentCSV = csv.GetRecords<content>().ToList();
			}

			// Retrieve english item strings so we can look up new key items.
			using (StreamReader reader = new StreamReader(Path.Combine("Data", "Message", "system_en.txt")))
			{
				CsvHelper.Configuration.CsvConfiguration config = new CsvHelper.Configuration.CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture);
				config.Delimiter = "\t";
				config.HasHeaderRecord = false;
				config.BadDataFound = null;

				using (CsvReader csv = new CsvReader(reader, config))
					itemStrings = csv.GetRecords<message>().ToList();
			}

			List<int> tailSelection = new List<int>();

			int shardID = 301;
			int nothingID = 321;

			List<scenario> scenarios = getScripts();
			// Retrieve win JSON file, then change reward text
			foreach (pairing rewardPair in pairings)
			{
				scenario currentReward = scenarios.Where(c => c.rewardId == rewardPair.locationID).SingleOrDefault();
				// Skip if the scenario entry hasn't been recorded yet; that part is either still under construction.
				if (currentReward == null) { continue; }

				// Open the winning JSON to set up rewards.
				string json = File.ReadAllText(currentReward.winScript);
				EventJSON jEvents = JsonConvert.DeserializeObject<EventJSON>(json);

				JsonSerializer serializer = new JsonSerializer();

				int finalItem = 0;

				if (rewardPair.rewardID > 3)
				{
					switch (rewardPair.rewardID)
					{
						case 4:	finalItem = 71; break;
						case 5:	finalItem = 80; break;
						case 6:	finalItem = 131; break;
						case 7:	finalItem = 64; break;
						case 8:	finalItem = 68; break;
						case 9:	finalItem = 69; break;
						case 10: finalItem = 66; break;
						case 11: finalItem = 72; break;
						case 12: finalItem = 75; break;
						case 13: finalItem = 81; break;
						case 14: finalItem = 74; break;
						case 15: finalItem = 73; break;
						case 16: finalItem = 67; break;
						case 17: finalItem = 82; break; // 82
						case 18: finalItem = 82; break; // 83
						case 19: finalItem = 82; break; // 84
						case 20: finalItem = 82; break; // 85
						case 21: finalItem = 82; break; // 87
						case 22: finalItem = 70; break;
						case 23: finalItem = 88; break;
						case 24: finalItem = 62; break;
						case 25: finalItem = 63; break;
						case 26: finalItem = 82; break; // 91
						case 27: finalItem = 82; break;
						case >= 28: finalItem = 89; break;
					}

					rewardPair.rewardText = "You got " + itemLookup(itemIDLookup(finalItem)) + "!\\n";

					var rewardItem = jEvents.Mnemonics.Where(c => c.comment == "FERewardItem").Single();
					
					if (rewardPair.rewardID == 13) // Gain hovership
					{
						rewardItem.mnemonic = "SysCall";
						rewardItem.operands.iValues[0] = 0;
						rewardItem.operands.iValues[1] = 0;
						rewardItem.operands.sValues[0] = "ホバー船入手";
					} 
					else
					{
						rewardItem.mnemonic = "GetItem";
						rewardItem.operands.iValues[0] = finalItem;
						rewardItem.operands.iValues[1] = 1;
						rewardItem.operands.sValues[0] = "";
					}

					var nothingShardFlag = jEvents.Mnemonics.Where(c => c.comment == "FENothingShardFlag").SingleOrDefault();
					if (nothingShardFlag != null)
						nothingShardFlag.operands.iValues[0] = finalItem == 82 ? 351 : finalItem == 89 ? 350 : 352;
				}
				else
				{
					string character = "";
					switch (characters[rewardPair.rewardID + 1])
					{
						case 1: case 13: character = "Cecil"; break;
						case 2: character = "Kain"; break;
						case 3: character = "Rosa"; break;
						case 4: character = "Rydia"; break;
						case 5: character = "Cid"; break;
						case 6: character = "Tellah"; break;
						case 7: character = "Edward"; break;
						case 8: character = "Yang"; break;
						case 9: character = "Palom"; break;
						case 10: character = "Porom"; break;
						case 11: character = "Edge"; break;
						case 12: character = "FuSoYa"; break;
					}
					rewardPair.rewardText = character + " has joined the party!";
					var rewardItem = jEvents.Mnemonics.Where(c => c.comment == "FERewardItem").Single();
					rewardItem.mnemonic = "SysCall";
					rewardItem.operands.iValues[0] = 0;
					rewardItem.operands.iValues[1] = 0;

					int xp = currentReward.characterXP - (currentReward.characterXP / 5) + (r1.Next() % currentReward.characterXP * 2 / 5);
					switch (rewardPair.rewardID)
					{
						case 0: rewardItem.operands.sValues[0] = "ローザ加入"; Party.adjustParty(2, xp, Path.Combine(directory, "Data", "Master"), r1, xpMultiplier); break; // Rosa (entry 3 in character_status)
						case 1: rewardItem.operands.sValues[0] = "シド加入"; Party.adjustParty(4, xp, Path.Combine(directory, "Data", "Master"), r1, xpMultiplier); break; // Cid (entry 5 in character_status)
						case 2: rewardItem.operands.sValues[0] = "テラ加入"; Party.adjustParty(5, xp, Path.Combine(directory, "Data", "Master"), r1, xpMultiplier); break; // Tellah (entry 6 in character_status)
						case 3: rewardItem.operands.sValues[0] = "ギルバート加入"; Party.adjustParty(6, xp, Path.Combine(directory, "Data", "Master"), r1, xpMultiplier); break; // Edward (entry 7 in character_status)
					}					
				}

				var rewardFlag = jEvents.Mnemonics.Where(c => c.comment == "FERewardFlag").Single();
				rewardFlag.mnemonic = "SetFlag";
				if (finalItem == 82) { rewardFlag.operands.iValues[0] = nothingID; nothingID++; }
				else if (finalItem == 89) { rewardFlag.operands.iValues[0] = shardID; shardID++; }
				else rewardFlag.operands.iValues[0] = 200 + rewardPair.rewardID;
				rewardFlag.operands.sValues[0] = "ScenarioFlag1";

				if (rewardPair.locationID == 17) // Adamant Grotto / Tail turn-in cave
				{
					int minTier = 9;
					int maxTier = 9;
					string commentToUse = "FE_PinkTail";
					int itemSelected;
					if (r1.Next() % 2 == 0)
						itemSelected = new Weapons().selectItem(r1, minTier, maxTier, false, includeBonus, includeFGExclusive, party);
					else
						itemSelected = new Armor().selectItem(r1, minTier, maxTier, false, includeBonus, includeFGExclusive, party);

					var rewardItem = jEvents.Mnemonics.Where(c => c.comment == commentToUse).Single();
					rewardItem.operands.iValues[0] = itemSelected;
					tailSelection.Add(itemSelected);
				}

				if (rewardPair.locationID == 31) // Nothing Vending Machine
				{
					int minTier = 9;
					int maxTier = 9;
					string commentToUse = "FE_NothingBonus";
					int itemSelected;
					if (r1.Next() % 2 == 0)
						itemSelected = new Weapons().selectItem(r1, minTier, maxTier, false, includeBonus, includeFGExclusive, party);
					else
						itemSelected = new Armor().selectItem(r1, minTier, maxTier, false, includeBonus, includeFGExclusive, party);

					var rewardItem = jEvents.Mnemonics.Where(c => c.comment == commentToUse).Single();
					rewardItem.operands.iValues[0] = itemSelected;
					tailSelection.Add(itemSelected);
				}

				using (StreamWriter sw = new StreamWriter(Path.Combine(directory, currentReward.winScript)))
				using (JsonWriter writer = new JsonTextWriter(sw))
				{
					serializer.Serialize(writer, jEvents);
				}
			}

			// Set up the reward for the legend sword/adamantite turn-in.
			string jsonXcal = File.ReadAllText(@"Res\Map\Map_20181\Map_20181_1\sc_e_0107.json");
			EventJSON jEventsXCal = JsonConvert.DeserializeObject<EventJSON>(jsonXcal);

			JsonSerializer serializerXCal = new JsonSerializer();

			int swordItem;
			if (r1.Next() % 2 == 0)
				swordItem = new Weapons().selectItem(r1, 9, 9, false, includeBonus, includeFGExclusive, party);
			else
				swordItem = new Armor().selectItem(r1, 9, 9, false, includeBonus, includeFGExclusive, party);

			var swordJSON = jEventsXCal.Mnemonics.Where(c => c.comment == "XcalReward").Single();
			swordJSON.operands.iValues[0] = swordItem;

			using (StreamWriter sw = new StreamWriter(Path.Combine(directory, @"Res\Map\Map_20181\Map_20181_1\sc_e_0107.json")))
			using (JsonWriter writer = new JsonTextWriter(sw))
			{
				serializerXCal.Serialize(writer, jEventsXCal);
			}

			List<string> languages = new List<string>() { "de", "en", "es", "fr", "it", "ja", "ko", "pt", "ru", "th", "zhc", "zht" };

			foreach (string language in languages)
			{
				// Get mes_id_name from content.csv, then get accordingly name from whatever language you're using. (system_xx)
				using (StreamReader reader = new StreamReader(Path.Combine("Data", "Message", "story_mes_" + language + ".txt")))
				{
					CsvHelper.Configuration.CsvConfiguration config = new CsvHelper.Configuration.CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture);
					config.Delimiter = "\t";
					config.HasHeaderRecord = false;
					config.BadDataFound = null;

					using (CsvReader csv = new CsvReader(reader, config))
						msgStrings = csv.GetRecords<message>().ToList();

					foreach (pairing rewardPair in pairings)
					{
						msgStrings.Add(new message { id = "FE_REWARD_" + rewardPair.locationID.ToString("0#"), msgString = rewardPair.rewardText });
					}

					int i = 0;
					foreach (int tail in tailSelection)
					{
						string msgId = i == 0 ? "FE_TAIL_PINK" : "FE_NOTHING_BONUS";
						msgStrings.Add(new message { id = msgId, msgString = "You got " + itemLookup(itemIDLookup(tail)) + "!" });
						i++;
					}

					msgStrings.Add(new message { id = "XCAL_REWARD", msgString = "Received " + itemLookup(itemIDLookup(swordItem)) + "!" });

					using (StreamWriter writer = new StreamWriter(Path.Combine(dataDirectory, "story_mes_" + language + ".txt")))
					using (CsvWriter csv = new CsvWriter(writer, config))
					{
						csv.WriteRecords(msgStrings);
					}
				}
			}
		}

		private static string itemIDLookup(int finalItem)
		{
			return contentCSV.Where(c => c.id == finalItem).Single().mes_id_name;
		}

		private static string itemLookup(string mesName)
		{
			return itemStrings.Where(c => c.id == mesName).Single().msgString;
		}


		static private List<pairing> pairingSort(List<pairing> pairings)
		{
			int min = pairings.Min(c => c.locationID);
			int max = pairings.Max(c => c.locationID);
			List<pairing> newPairing = new List<pairing>();

			int i = min;
			while (i <= max)
			{
				newPairing.Add(pairings.Where(c => c.locationID == i).Single());
				i++;
			}

			return newPairing;
		}
	}
}
