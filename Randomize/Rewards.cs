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
		public class Content
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

		private static List<Message> itemStrings;
		private static List<Message> msgStrings;
		private static List<Content> contentCSV = new List<Content>();

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
		//	"Crystal Shard 6"  - 89 NEW //	"Legend Sword", - 131
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
		//	"Nothing", - 93 NEW //	"Yellow Tail", - 82 NEW
		//	"Nothing", - 93 NEW //	"Green Tail", - 83 NEW
		//	"Nothing", - 93 NEW //	"Blue Tail", - 84 NEW
		//	"Nothing", - 93 NEW //	"Red Tail", - 85 NEW // 20
		//	"Nothing", - 93 NEW //	"Black Tail", - 87 NEW
		//	"Crystal Shard 5"  - 89 NEW //	"Pink Tail", - 70
		//	"Trash Can", - 88 NEW
		//	"Pan", - 62
		//	"Adamant", - 63 // 25
		//	"Nothing", - 93 NEW //	"Orange Tail", - 91 NEW
		//	"Crystal Shard 7", - 93 NEW // "White Tail", - 92 NEW
		//	"Crystal Shard 1", - 89 NEW
		//	"Crystal Shard 2", - 89 NEW
		//	"Crystal Shard 3", - 89 NEW // 30
		//	"Crystal Shard 4"  - 89 NEW
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
		static List<int> nothingRewards = new List<int> { };
		static List<int> nothingCrystalRewards = new List<int> { 6, 17, 18, 19, 20, 21, 22, 26, 27, 28, 29, 30, 31 };

		class Pairing
		{
			public int locationID { get; set; }
			public int rewardID { get; set; }
			public bool underUnlocked { get; set; }
			public bool moonUnlocked { get; set; }
			public string rewardText { get; set; }

			public Pairing(int pLocID, int pRewardID) 
			{
				locationID = pLocID;
				rewardID = pRewardID;
			}
		}

		public static void establishRewards(Random r1, int[] characters, string directory, bool includeBonus, bool includeFGExclusive, int[] party, double xpMultiplier, bool zAtOrdeals, bool zAtFalcon, int crystalShards, bool convertNothings, int nothingKey, int nothingItem, int numHeroes, int charsRequired)
		{
			List<Pairing> pairings = new List<Pairing>();
			List<Pairing> tempPairings = new List<Pairing>();
			List<int> validLocations = initialLocations.ToList();
			List<int> validRewards = allRewards.ToList();
			// Reroll at Ordeals, Magnetic Cavern, Zot, Dwarf Castle, Sealed Cave, and the Nothing Vending Machine
			List<int> rerollLocations = new List<int> { 9, 12, 13, 14, 20, 31 };
			List<int> rerollRewards = new List<int> { 23, 25 }; // Currently Trash Can and Adamantite.  Add nothings in a moment
			if (crystalShards <= 12 ) { nothingRewards.Add(6); rerollRewards.Add(6); }
			if (crystalShards <= 11 ) { nothingRewards.Add(17); rerollRewards.Add(17); }
			if (crystalShards <= 10 ) { nothingRewards.Add(18); rerollRewards.Add(18); }
			if (crystalShards <= 9 ) { nothingRewards.Add(19); rerollRewards.Add(19); }
			if (crystalShards <= 8 ) { nothingRewards.Add(20); rerollRewards.Add(20); }
			if (crystalShards <= 7 ) { nothingRewards.Add(21); rerollRewards.Add(21); }
			if (crystalShards <= 6 ) { nothingRewards.Add(22); rerollRewards.Add(22); }
			if (crystalShards <= 5 ) { nothingRewards.Add(26); rerollRewards.Add(26); }
			if (crystalShards <= 4 ) { nothingRewards.Add(27); rerollRewards.Add(27); }
			if (crystalShards <= 3 ) { nothingRewards.Add(28); rerollRewards.Add(28); }
			if (crystalShards <= 2 ) { nothingRewards.Add(29); rerollRewards.Add(29); }
			if (crystalShards <= 1 ) { nothingRewards.Add(30); rerollRewards.Add(30); }
			if (crystalShards == 0 ) { nothingRewards.Add(31); rerollRewards.Add(31); }
			List<int> newLocations = new List<int>();
			bool underground = false;
			bool moon = false;
			bool nothingCheck = false;
			bool finalPass = false;

			// Remove the following if zAtFalcon or zAtOrdeals == true:
			// Legend Sword, Magma Key, Tower Key, Luca Key, Dark Crystal, Pan, Adamant, and all Crystal Shards
			// Keep the following rewards:  
			if (zAtFalcon || zAtOrdeals)
				validRewards = new List<int> { 0, 1, 2, 3, 4, 5, 7, 8, 9, 10, 13, 16, 17, 18, 19, 20, 21, 22, 23, 30, 31 };

			while (validRewards.Count > 0)
			{
				int locationID = validLocations[r1.Next() % validLocations.Count];
				int rerolls = rerollLocations.Contains(locationID) ? 1 : 0;
				int rewardID = validRewards[r1.Next() % validRewards.Count];
				// Reroll if a non-progression item is rolled in Ordeals, Magnetic Cavern, Tower of Zot, Dwarf Castle, or Sealed Cave
				// Non-progression items are the Legend Sword, the Rat Tail, the Pink Tail, the Trash Can, the Adamantite, or a Nothing
				if (rerolls == 1 && rerollRewards.Contains(rewardID))
					rewardID = validRewards[r1.Next() % validRewards.Count];

				// Debug
				//if (tempPairings.Count == 0 && pairings.Count == 0)
				//{
				//	locationID = 0; rewardID = 17;
				//}

				tempPairings.Add(new Pairing(locationID, rewardID));
				validLocations.Remove(locationID);
				validRewards.Remove(rewardID);

				if (validLocations.Count == 0 && validRewards.Count == 0)
					break;

				bool locationAdded = false;

				if (itemLocations[rewardID].Count() > 0)
				{
					bool doNotProcess = false;

					// If the Tower Key, Luca Key, or Pan are awarded, but the Magma Key or Hook has not, then don't process additional locations yet.
					if ((rewardID == 12 || rewardID == 14 || rewardID == 24) && !pairings.Exists(c => c.rewardID == 11) && !pairings.Exists(c => c.rewardID == 13) && !tempPairings.Exists(c => c.rewardID == 11) && !tempPairings.Exists(c => c.rewardID == 13))
						doNotProcess = true;
					// Same for the Rat Tail, except just the Hook only.
					if ((rewardID == 16) && !pairings.Exists(c => c.rewardID == 13) && !tempPairings.Exists(c => c.rewardID == 13))
						doNotProcess = true;

					if (!doNotProcess)
					{
						newLocations.AddRange(itemLocations[rewardID].ToList());
						if (pairings.Where(c => nothingRewards.Contains(c.rewardID)).Count() + tempPairings.Where(c => nothingRewards.Contains(c.rewardID)).Count() >= nothingKey && !nothingCheck)
						{
							newLocations.Add(31);
							nothingCheck = true;
						}

						// If the Magma Key or the Hook has been awarded, and the Tower Key has previously been awarded, then add the Super Cannon.
						if ((rewardID == 11 || rewardID == 13) && (pairings.Exists(c => c.rewardID == 12) || tempPairings.Exists(c => c.rewardID == 12))) // Tower Key
							newLocations.AddRange(itemLocations[12]);
						if ((rewardID == 11 || rewardID == 13) && (pairings.Exists(c => c.rewardID == 14) || tempPairings.Exists(c => c.rewardID == 14))) // Luca Key
							newLocations.AddRange(itemLocations[14]);
						if ((rewardID == 11 || rewardID == 13) && (pairings.Exists(c => c.rewardID == 24) || tempPairings.Exists(c => c.rewardID == 24))) // Pan
							newLocations.AddRange(itemLocations[24]);
						if (rewardID == 13 && (pairings.Exists(c => c.rewardID == 16) || tempPairings.Exists(c => c.rewardID == 16))) // Rat tail
							newLocations.AddRange(itemLocations[16]);
					}
				} 

				if (validLocations.Count == 0)
				{
					if (finalPass) break;

					// Need to remove duplicate newLocations first...
					List<Pairing> dupPairing = pairings.Where(c => newLocations.Contains(c.locationID)).ToList();
					foreach (Pairing dup in dupPairing) { newLocations.Remove(dup.locationID); }
					dupPairing = tempPairings.Where(c => newLocations.Contains(c.locationID)).ToList();
					foreach (Pairing dup in dupPairing) { newLocations.Remove(dup.locationID); }

					if (newLocations.Count == 0)
					{
						foreach (var pairing in tempPairings)
						{
							validLocations.Add(pairing.locationID);
							validRewards.Add(pairing.rewardID);
						}
						tempPairings.Clear();

						// If we have early Z, see if we have only nothings left for validRewards.  If there are, then we should have "one more pass", and then move on.
						if (zAtFalcon || zAtOrdeals)
						{
							int nothingRewards = validRewards.Where(c => c >= 16 || c <= 3).Count();
							if (nothingRewards == validRewards.Count())
								finalPass = true;
						}
					}
					else
					{
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

							if (charsRequired > 1)
							{
								int chars = 1 + pairings.Where(c => c.rewardID <= 3).Count();
								while (chars < charsRequired)
								{
									// Replace non-progression items with characters.
									List<int> nonProgression = new List<int> { 6, 12, 14, 16, 22, 23, 25, 28, 29, 30, 31 };
									Pairing pair = pairings.Where(c => nonProgression.Contains(c.rewardID)).FirstOrDefault();
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
										pairings = new List<Pairing>();
										tempPairings = new List<Pairing>();
										validLocations = initialLocations.ToList();
										validRewards = allRewards.ToList();
										nothingCheck = false;
										break;
									}
								}
							}
						}
						else
						{
							if (newLocations.Contains(31))
								nothingCheck = false;
						}

						newLocations = new List<int>();
					}
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
					contentCSV = csv.GetRecords<Content>().ToList();
			}

			// Retrieve english item strings so we can look up new key items.
			using (StreamReader reader = new StreamReader(Path.Combine("Data", "Message", "system_en.txt")))
			{
				CsvHelper.Configuration.CsvConfiguration config = new CsvHelper.Configuration.CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture);
				config.Delimiter = "\t";
				config.HasHeaderRecord = false;
				config.BadDataFound = null;

				using (CsvReader csv = new CsvReader(reader, config))
					itemStrings = csv.GetRecords<Message>().ToList();
			}

			List<int> tailSelection = new List<int>();

			int shardID = 301;
			int nothingID = 321;
			int shardCount = 0;

			List<scenario> scenarios = getScripts();
			// Retrieve win JSON file, then change reward text
			foreach (Pairing rewardPair in pairings)
			{
				scenario currentReward = scenarios.Where(c => c.rewardId == rewardPair.locationID).SingleOrDefault();
				// Skip if the scenario entry hasn't been recorded yet; that part is either still under construction.
				if (currentReward == null) { continue; }

				// Do not change the win script if zAtOrdeals is active and we're at DK Cecil spot.
				if (currentReward.rewardId == 9 && zAtOrdeals)
				{
					continue;
				}

				// Open the winning JSON to set up rewards.
				string json = File.ReadAllText(Path.Combine("Res", "Map", currentReward.winScript));
				EventJSON jEvents = JsonConvert.DeserializeObject<EventJSON>(json);

				JsonSerializer serializer = new JsonSerializer();

				if (currentReward.winScript == @"Map_20121\Map_20121_2\sc_e_0161.json")
				{
					var rewardItem = jEvents.Mnemonics.Where(c => c.comment == "NothingCheck1").Single();
					// Do not fire notification that you can get a key item if the required shards is 0.
					rewardItem.operands.iValues[1] = nothingKey - 1; // Script looks for greater than, not greater or equal to.

					rewardItem = jEvents.Mnemonics.Where(c => c.comment == "NothingCheck2").Single();
					// Do not award tier 9 item if there are 0 or 1 Nothings available.
					rewardItem.operands.iValues[1] = nothingItem <= 1 ? 99 : nothingItem - 1; // Script looks for greater than, not greater or equal to.
				}

				int finalItem = 0;
				bool nothing = false;

				// rewardID is 0-based.  So it's always true if there's only one hero.  (numHeroes - 2 == -1)
				if (rewardPair.rewardID > numHeroes - 2)
				{
					switch (rewardPair.rewardID)
					{
						case 0:
						case 1:
						case 2:
						case 3:
							finalItem = new Items().selectItem(r1, 1, 4, false, false);
							break;
						case 4:	finalItem = 71; break;
						case 5:	finalItem = 80; break;
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
						case 23: finalItem = 88; break;
						case 24: finalItem = 62; break;
						case 25: finalItem = 63; break;
						default:  // case 6, 22, 27, 28, 29, 30, 31 (all formerly crystal shards, pink tail, legend sword), 17, 18, 19, 20, 21, 26 (all formerly tails and nothings)
							if ((rewardPair.rewardID == 6 && crystalShards >= 13) ||
								(rewardPair.rewardID == 17 && crystalShards >= 12) ||
								(rewardPair.rewardID == 18 && crystalShards >= 11) ||
								(rewardPair.rewardID == 19 && crystalShards >= 10) ||
								(rewardPair.rewardID == 20 && crystalShards >= 9) ||
								(rewardPair.rewardID == 21 && crystalShards >= 8) ||
								(rewardPair.rewardID == 22 && crystalShards >= 7) ||
								(rewardPair.rewardID == 26 && crystalShards >= 6) ||
								(rewardPair.rewardID == 27 && crystalShards >= 5) ||
								(rewardPair.rewardID == 28 && crystalShards >= 4) ||
								(rewardPair.rewardID == 29 && crystalShards >= 3) ||
								(rewardPair.rewardID == 30 && crystalShards >= 2) ||
								(rewardPair.rewardID == 31 && crystalShards >= 1))
							{
								finalItem = 89; // Crystal Shard
							}
							else
							{
								if (convertNothings)
								{
									int minTier;
									int maxTier;
									switch (rewardPair.locationID)
									{
										case 0: minTier = 3; maxTier = 4; break;
										case 1: minTier = 4; maxTier = 7; break;
										case 2: minTier = 4; maxTier = 7; break;
										case 3: minTier = 4; maxTier = 7; break;
										case 4: minTier = 3; maxTier = 5; break;
										case 5: minTier = 3; maxTier = 5; break;
										case 6: minTier = 3; maxTier = 5; break;
										case 7: minTier = 3; maxTier = 5; break;
										case 8: minTier = 3; maxTier = 5; break;
										case 9: minTier = 4; maxTier = 6; break;
										case 10: minTier = 4; maxTier = 6; break;
										case 11: minTier = 5; maxTier = 7; break;
										case 12: minTier = 5; maxTier = 7; break;
										case 13: minTier = 5; maxTier = 7; break;
										case 14: minTier = 5; maxTier = 8; break;
										case 15: minTier = 6; maxTier = 8; break;
										case 16: minTier = 6; maxTier = 8; break;
										case 17: minTier = 8; maxTier = 9; break;
										case 18: minTier = 6; maxTier = 8; break;
										case 19: minTier = 6; maxTier = 8; break;
										case 20: minTier = 6; maxTier = 8; break;
										case 21: minTier = 6; maxTier = 8; break;
										case 22: minTier = 6; maxTier = 8; break;
										case 23: minTier = 7; maxTier = 9; break;
										case 24: minTier = 7; maxTier = 9; break;
										case 25: minTier = 8; maxTier = 9; break;
										case 26: minTier = 8; maxTier = 9; break;
										case 27: minTier = 8; maxTier = 9; break;
										case 28: minTier = 8; maxTier = 9; break;
										case 29: minTier = 9; maxTier = 9; break;
										case 30: minTier = 7; maxTier = 9; break;
										case 31: minTier = 7; maxTier = 9; break;
										default: minTier = 8; maxTier = 9; break;
									}
									if (r1.Next() % 2 == 0)
										finalItem = new Weapons().selectItem(r1, minTier, maxTier, false, includeBonus, includeFGExclusive, party);
									else
										finalItem = new Armor().selectItem(r1, minTier, maxTier, false, includeBonus, includeFGExclusive, party);
								}
								else
								{
									finalItem = 82; // Nothing
								}
								nothing = true;
							}
							shardCount++;
							break;
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
						nothingShardFlag.operands.iValues[0] = nothing ? 351 : finalItem == 89 ? 350 : 352; // formerly finalItem == 82
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
						case 0: rewardItem.operands.sValues[0] = "ローザ加入"; Party.adjustParty(2, xp, Path.Combine(directory), r1, xpMultiplier); break; // Rosa (entry 3 in character_status)
						case 1: rewardItem.operands.sValues[0] = "シド加入"; Party.adjustParty(4, xp, Path.Combine(directory), r1, xpMultiplier); break; // Cid (entry 5 in character_status)
						case 2: rewardItem.operands.sValues[0] = "テラ加入"; Party.adjustParty(5, xp, Path.Combine(directory), r1, xpMultiplier); break; // Tellah (entry 6 in character_status)
						case 3: rewardItem.operands.sValues[0] = "ギルバート加入"; Party.adjustParty(6, xp, Path.Combine(directory), r1, xpMultiplier); break; // Edward (entry 7 in character_status)
					}					
				}

				var rewardFlag = jEvents.Mnemonics.Where(c => c.comment == "FERewardFlag").Single();
				rewardFlag.mnemonic = "SetFlag";
				if (nothing) { rewardFlag.operands.iValues[0] = nothingID; nothingID++; } // formerly finalItem == 82
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

				using (StreamWriter sw = new StreamWriter(Updater.MemoriaToMagiciteFile(directory, Path.Combine("Map", currentReward.winScript))))
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

			using (StreamWriter sw = new StreamWriter(Updater.MemoriaToMagiciteFile(directory, @"Map\Map_20181\Map_20181_1\sc_e_0107.json")))
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
						msgStrings = csv.GetRecords<Message>().ToList();

					foreach (Pairing rewardPair in pairings)
					{
						msgStrings.Add(new Message { id = "FE_REWARD_" + rewardPair.locationID.ToString("0#"), msgString = rewardPair.rewardText });
					}

					int i = 0;
					foreach (int tail in tailSelection)
					{
						string msgId = i == 0 ? "FE_TAIL_PINK" : "FE_NOTHING_BONUS";
						msgStrings.Add(new Message { id = msgId, msgString = "You got " + itemLookup(itemIDLookup(tail)) + "!" });
						i++;
					}

					msgStrings.Add(new Message { id = "XCAL_REWARD", msgString = "Received " + itemLookup(itemIDLookup(swordItem)) + "!" });

					using (StreamWriter writer = new StreamWriter(Updater.MemoriaToMagiciteFile(directory, "Message\\story_mes_" + language + ".txt")))
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


		static private List<Pairing> pairingSort(List<Pairing> pairings)
		{
			int min = pairings.Min(c => c.locationID);
			int max = pairings.Max(c => c.locationID);
			List<Pairing> newPairing = new List<Pairing>();

			int i = min;
			while (i <= max)
			{
				Pairing toAdd = pairings.Where(c => c.locationID == i).SingleOrDefault();

				if (toAdd != null)
					newPairing.Add(toAdd);
				i++;
			}

			return newPairing;
		}
	}
}
