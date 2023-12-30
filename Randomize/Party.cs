using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using FF4FreeEnterprisePR.Common;

namespace FF4FreeEnterprisePR.Randomize
{
	public static class Party
	{
		private class character : ICloneable
		{
			public int id { get; set; }
			public int gender { get; set; }
			public int dominant_arm { get; set; }
			public int lv { get; set; }
			public int exp { get; set; }
			public int growth_curve_group_id { get; set; }
			public int job_id { get; set; }
			public string mes_id_name { get; set; }
			public int in_type_id { get; set; }
			public int hp { get; set; }
			public int mp { get; set; }
			public int magical_times1 { get; set; }
			public int magical_times2 { get; set; }
			public int magical_times3 { get; set; }
			public int magical_times4 { get; set; }
			public int magical_times5 { get; set; }
			public int magical_times6 { get; set; }
			public int magical_times7 { get; set; }
			public int magical_times8 { get; set; }
			public int strength { get; set; }
			public int vitality { get; set; }
			public int agility { get; set; }
			public int intelligence { get; set; }
			public int spirit { get; set; }
			public int magic { get; set; }
			public int luck { get; set; }
			public int attack { get; set; }
			public int defense { get; set; }
			public int accuracy_rate { get; set; }
			public int dodge_times { get; set; }
			public int evasion_rate { get; set; }
			public int ability_defense { get; set; }
			public int magic_evasion_rate { get; set; }
			public int corps { get; set; }
			public int command_id1 { get; set; } // Fight
			public int command_id2 { get; set; }
			public int command_id3 { get; set; }
			public int command_id4 { get; set; }
			public int command_id5 { get; set; }
			public int command_id6 { get; set; }
			public int content_id1 { get; set; }
			public int content_id2 { get; set; }
			public int content_id3 { get; set; }
			public int content_id4 { get; set; }
			public int content_id5 { get; set; }
			public int content_id6 { get; set; }
			public int ability_random_group_id { get; set; }
			public int initial_condition_group { get; set; }
			public int character_asset_id { get; set; }

			public object Clone()
			{
				return this.MemberwiseClone();
			}
		}

		private class init
        {
			public int id { get; set; }
			public string key_name { get; set; }
			public int value1 { get; set; }
			public int value2 { get; set; }
        }

		private class growth
		{
			public int id { get; set; }
			public int group_id { get; set; }
			public int lv { get; set; }
			public int insert_type_1 { get; set; } // = 2
			public int hp_value1 { get; set; } // Raise?
			public int hp_value2 { get; set; } // Raise?
			public int mp_value1 { get; set; } // Raise?
			public int mp_value2 { get; set; } // Raise?
			public int insert_type_2 { get; set; } // = 1
			public int strength { get; set; } // Raise?
			public int vitality { get; set; } // Raise?
			public int agility { get; set; } // Raise?
			public int intelligence { get; set; } // Raise?
			public int spirit { get; set; } // Raise?
			public int magic { get; set; }
			public int luck { get; set; }
			public int insert_type_3 { get; set; }
			public int accuracy_rate { get; set; }
			public int magic_evasion_rate { get; set; }
			public int magical_times1 { get; set; }
			public int magical_times2 { get; set; }
			public int magical_times3 { get; set; }
			public int magical_times4 { get; set; }
			public int magical_times5 { get; set; }
			public int magical_times6 { get; set; }
			public int magical_times7 { get; set; }
			public int magical_times8 { get; set; }
		}

		private class xpGrowth
		{
			public int id { get; set; }
			public int type_id { get; set; }
			public int group_id { get; set; }
			public int lv { get; set; }
			public int total_exp { get; set; }
		}

		private class abilityGroup
		{
			public int id { get; set; }
			public int group_id { get; set; }
			public int ability_id { get; set; }
			public int invocation_rate { get; set; }
			public int condition_group_id { get; set; }
		}

		private class learning
		{
			public int id { get; set; }
			public int type_id { get; set; }
			public int value1 { get; set; }
			public int value2 { get; set; }
			public int job_id { get; set; }
			public int content_id { get; set; }
		}

		private class intGrowCurve
		{
			public int id { get; set; }
			public int character_id { get; set; }
			public int job_id { get; set; }
			public int growth_curve_group_id { get; set; }
			public int exp_table_group_id { get; set; }
		}

		const int dkCecil = 1;
		const int cecil = 13;
		const int kain = 2;
		const int rosa = 3;
		const int rydia = 4;
		const int cid = 5;
		const int tellah = 6;
		const int edward = 7;
		const int yang = 8;
		const int palom = 9;
		const int porom = 10;
		const int edge = 11;
		const int fusoya = 12;

		private static List<int> characters = new List<int>();

		public static int[] establishParty(Random r1, string directory, int firstHero, bool duplicates, int numHeroes, bool noPromote, bool[] exclude, double xpMultiplier)
		{
			List<character> records;

			using (StreamReader reader = new StreamReader(Path.Combine("csv", "character_status.csv")))
			using (CsvReader csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
				records = csv.GetRecords<character>().ToList();

			if (duplicates)
			{
				characters = new List<int>();
				for (int i = 0; i < 12; i++)
				{
					if (i == 0 && firstHero != 0) characters.Add(firstHero);
					int charID = r1.Next() % 13 + 1;
					if ((charID == dkCecil && !exclude[0]) ||
						(charID == kain && !exclude[1]) ||
						(charID == rydia && !exclude[2]) ||
						(charID == tellah && !exclude[3]) ||
						(charID == edward && !exclude[4]) ||
						(charID == rosa && !exclude[5]) ||
						(charID == yang && !exclude[6]) ||
						(charID == palom && !exclude[7]) ||
						(charID == porom && !exclude[8]) ||
						(charID == cid && !exclude[9]) ||
						(charID == edge && !exclude[10]) ||
						(charID == fusoya && !exclude[11]) ||
						(charID == cecil && !exclude[12]))
							characters.Add(charID);
					else 
						i--;  // redraw if a character is checked as excluded
				}
			} else
			{
				characters = new List<int> { dkCecil, kain, rydia, tellah, edward, rosa, yang, palom, porom, cid, edge, fusoya, cecil };
				if (exclude[0])	characters.Remove(dkCecil);
				if (exclude[1])	characters.Remove(kain);
				if (exclude[2])	characters.Remove(rydia);
				if (exclude[3])	characters.Remove(tellah);
				if (exclude[4])	characters.Remove(edward);
				if (exclude[5])	characters.Remove(rosa);
				if (exclude[6])	characters.Remove(yang);
				if (exclude[7])	characters.Remove(palom);
				if (exclude[8])	characters.Remove(porom);
				if (exclude[9])	characters.Remove(cid);
				if (exclude[10]) characters.Remove(edge);
				if (exclude[11]) characters.Remove(fusoya);
				if (exclude[12]) characters.Remove(cecil);
				characters.Shuffle(r1);
				if (firstHero != 0)
				{
					if (characters.Contains(firstHero))
						characters.Remove(firstHero);
					characters.Add(firstHero);
					int tempChar = characters[0];
					characters[0] = firstHero;
					characters[characters.Count - 1] = tempChar;
				}
				while (characters.Count < 12)
					characters.Add(cecil);
			}
			// Need to move characters around because we have to skip character slots due to promotions based on ScenarioFlag setting.
			characters[6] = characters[4];
			characters[5] = characters[3];
			characters[4] = characters[2];
			characters[2] = characters[1];
			// In case of Cecil promotions
			characters.Add(cecil); // Add this to make it the "13th character" - otherwise the "Cecil Career Change" SysCall doesn't work.

			int id = 1;
			List<character> newRecords = new List<character>();
			foreach (int singleChar in characters)
			{
				// Next two lines:  Prevents changes done to the single object as well as the list.
				var oldRecord = records.Where(c => c.id == singleChar).ToList()[0];
				var newRecord = (character)oldRecord.Clone();
				newRecord.id = id;
				newRecord.growth_curve_group_id = id;
				newRecord.mes_id_name = "MSG_CHAR_NAME_" + id.ToString("D2");
				newRecords.Add(newRecord);
				id++;
			}

			using (StreamWriter writer = new StreamWriter(Path.Combine(directory, "..", "..", "Data", "Master", "character_status.csv")))
			using (CsvWriter csv = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
				csv.WriteRecords(newRecords);

			adjustParty(0, 0 + r1.Next() % 3000, directory, r1, xpMultiplier);

			List<init> newInits = new List<init>();

			using (StreamReader reader = new StreamReader(Path.Combine("csv", "initialize_data.csv")))
			using (CsvReader csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
				newInits = csv.GetRecords<init>().ToList();

			newInits.RemoveAll(c => c.id == 5);
			newInits.RemoveAll(c => c.id == 4);
			newInits.RemoveAll(c => c.id == 3);
			newInits.RemoveAll(c => c.id == 2);

			int startingGold = 0 + (r1.Next() % 11 * 10);
			newInits.Where(c => c.id == 6).Single().value1 = startingGold;

			using (StreamWriter writer = new StreamWriter(Path.Combine(directory, "..", "..", "Data", "Master", "initialize_data.csv")))
			using (CsvWriter csv = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
			{
				csv.WriteRecords(newInits);
			}

			using (StreamWriter writer = new StreamWriter(Path.Combine(directory, "..", "..", "Data", "Master", "intermediate_growth_curve.csv")))
			{
				writer.WriteLine("id,character_id,job_id,growth_curve_group_id,exp_table_group_id");
				id = 1;
				int growthID;
				foreach (character record in newRecords)
				{
					growthID = record.job_id == 6 ? 1 : record.job_id == 3 ? 2 : record.job_id == 2 ? 3 : record.job_id == 7 ? 4 : record.job_id == 10 ? 5 :
						record.job_id == 8 ? 6 : record.job_id == 9 ? 7 : record.job_id == 11 ? 8 : record.job_id == 12 ? 9 : record.job_id == 13 ? 10 :
						record.job_id == 5 ? 11 : record.job_id == 14 ? 12 : record.job_id == 1 ? 13 : 4;
					writer.WriteLine(id.ToString().Trim() + "," + id.ToString().Trim() + "," + record.job_id.ToString().Trim() + "," + growthID.ToString().Trim() + "," + growthID.ToString().Trim());
					id++;
				}
			}

			// If Palom and Porom are in the group, we need to adjust the Twincast command.
			while (characters.Count > 7)
				characters.RemoveAt(characters.Count - 1);
			if (characters.Where(c => c == palom).Count() == 1 && characters.Where(c => c == porom).Count() == 1)
				new Inventory.Command().adjustTwinCast(characters.IndexOf(palom) + 1, characters.IndexOf(porom) + 1, Path.Combine(directory, "..", "..", "Data", "Master"));
			else
				new Inventory.Command().adjustTwinCast(9, 10, Path.Combine(directory, "..", "..", "Data", "Master")); // Setting Palom and Porom back to their original values will disable Twincast.

			return numHeroes switch
			{
				1 => new int[] { characters[0] },
				2 => new int[] { characters[0], characters[2] },
				3 => new int[] { characters[0], characters[2], characters[4] },
				4 => new int[] { characters[0], characters[2], characters[4], characters[5] },
				_ => new int[] { characters[0], characters[2], characters[4], characters[5], characters[6] },
			};
		}

		public static void adjustParty(int i, int xp, string directory, Random r1, double xpMultiplier)
		{
			xp = (int)(xp * xpMultiplier);
			// Must be a value between 0 and 9,000,000
			xp = Math.Min(xp, 9000000);
			xp = Math.Max(xp, 0);

			List<character> records;
			using (StreamReader reader = new StreamReader(Path.Combine(directory, "..", "..", "Data", "Master", "character_status.csv")))
			using (CsvReader csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
				records = csv.GetRecords<character>().ToList();

			List<growth> growthRecords;
			using (StreamReader reader = new StreamReader(Path.Combine("csv", "growth_curve.csv")))
			using (CsvReader csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
				growthRecords = csv.GetRecords<growth>().ToList();

			List<xpGrowth> xpRecords;
			using (StreamReader reader = new StreamReader(Path.Combine("csv", "exp_table.csv")))
			using (CsvReader csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
				xpRecords = csv.GetRecords<xpGrowth>().ToList();

			List<learning> learnings;
			using (StreamReader reader = new StreamReader(Path.Combine("csv", "learning.csv")))
			using (CsvReader csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
				learnings = csv.GetRecords<learning>().ToList();

			List<intGrowCurve> intGrowthCurve;
			using (StreamReader reader = new StreamReader(Path.Combine(directory, "..", "..", "Data", "Master", "intermediate_growth_curve.csv")))
			using (CsvReader csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
				intGrowthCurve = csv.GetRecords<intGrowCurve>().ToList();

			List<abilityGroup> startGroups;
			using (StreamReader reader = new StreamReader(Path.Combine(directory, "..", "..", "Data", "Master", "ability_random_group.csv")))
			using (CsvReader csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
				startGroups = csv.GetRecords<abilityGroup>().ToList();

			int startGroupID = startGroups.Max(c => c.id) + 1;

			List<abilityGroup> beginningStartGroups = startGroups.Where(c => c.group_id == records[i].ability_random_group_id).ToList();
			foreach (abilityGroup singleBeginGroup in beginningStartGroups)
			{
				abilityGroup newStartGroup = new abilityGroup();
				newStartGroup.id = startGroupID;
				newStartGroup.group_id = 20 + i;
				newStartGroup.ability_id = singleBeginGroup.ability_id;

				newStartGroup.invocation_rate = 0;
				newStartGroup.condition_group_id = 0;
				startGroups.Add(newStartGroup);
				startGroupID++;
			}

			int hp = records[i].hp;
			int mp = records[i].mp;
			int strength = records[i].strength;
			int vitality = records[i].vitality;
			int agility = records[i].agility;
			int intelligence = records[i].intelligence;
			int spirit = records[i].spirit;
			int startGroupCharID = 20 + i;

			int xpTable = intGrowthCurve.Where(c => c.id == records[i].id).Single().exp_table_group_id;
			List<growth> charGrowth = growthRecords.Where(c => c.group_id == characters[i]).ToList();
			int finalLevel = xpRecords.Where(c => c.group_id == xpTable && c.total_exp <= xp).Max(c => c.lv);
			int level = 1;

			while (level < finalLevel)
			{
				level++;
				growth levelGrowth = charGrowth.Where(c => c.lv == level).Single();
				hp += levelGrowth.hp_value1 + (r1.Next() % (levelGrowth.hp_value2 - levelGrowth.hp_value1 + 1));
				mp += levelGrowth.mp_value1 + (r1.Next() % (levelGrowth.mp_value2 - levelGrowth.mp_value1 + 1));
				strength += levelGrowth.strength;
				vitality += levelGrowth.vitality;
				agility += levelGrowth.agility;
				intelligence += levelGrowth.intelligence;
				spirit += levelGrowth.spirit;

				List<learning> levelLearnings = learnings.Where(c => c.job_id == records[i].job_id && c.value1 == level).ToList();
				foreach (learning lvLearn in levelLearnings)
				{
					abilityGroup newStartGroup = new abilityGroup();
					newStartGroup.id = startGroupID;
					newStartGroup.group_id = startGroupCharID;
					newStartGroup.ability_id = lvLearn.content_id - 310;

					newStartGroup.invocation_rate = 0;
					newStartGroup.condition_group_id = 0;
					startGroups.Add(newStartGroup);
					startGroupID++;
				}
			}

			records[i].hp = hp;
			records[i].mp = mp;
			records[i].strength = strength;
			records[i].vitality = vitality;
			records[i].agility = agility;
			records[i].intelligence = intelligence;
			records[i].spirit = spirit;
			records[i].lv = level;
			records[i].exp = xp;
			records[i].ability_random_group_id = startGroupCharID;

			using (StreamWriter writer = new StreamWriter(Path.Combine(directory, "..", "..", "Data", "Master", "character_status.csv")))
			using (CsvWriter csv = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
				csv.WriteRecords(records);

			using (StreamWriter writer = new StreamWriter(Path.Combine(directory, "..", "..", "Data", "Master", "ability_random_group.csv")))
			using (CsvWriter csv = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
				csv.WriteRecords(startGroups);
		}
	}
}
