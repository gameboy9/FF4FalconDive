using CsvHelper;
using FF4FreeEnterprisePR.Common;
using FF4FreeEnterprisePR.Inventory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FF4FreeEnterprisePR.Inventory.Magic;
using static FF4FreeEnterprisePR.Randomize.Rewards;

namespace FF4FalconDive.Inventory
{
	static internal class Zeromus
	{
		static public void ZeromusSetup(Random r1, int ZShards, int difficulty, int sirenShards, int keyNothings, int itemNothings, string directory)
		{
			// Setup the required number of shared for the warps to Z
			List<string> scripts = new List<string> {
				@"Map_20101\Map_20101_5\sc_e_0139.json",
				@"Map_20151\Map_20151_15\sc_e_0129.json",
				@"Map_30251\Map_30251_22\sc_e_0074.json"
			};

			foreach (string script in scripts) {
				string json = File.ReadAllText(Path.Combine("Res", "Map", script));
				EventJSON jEvents = JsonConvert.DeserializeObject<EventJSON>(json);

				JsonSerializer serializer = new JsonSerializer();

				var rewardItem = jEvents.Mnemonics.Where(c => c.comment == "FEShardRequired").Single();
				rewardItem.operands.iValues[1] = ZShards - 1; // Script looks for greater than, not greater or equal to.

				using (StreamWriter sw = new StreamWriter(Updater.MemoriaToMagiciteFile(directory, Path.Combine("Map", script))))
				using (JsonWriter writer = new JsonTextWriter(sw))
				{
					serializer.Serialize(writer, jEvents);
				}
			}

			// We'll set up the number of required shards for siren usage as well.
			scripts = new List<string>
			{
				@"Map_Script\Resident\sc_alarm_0001.json"
			};

			foreach (string script in scripts)
			{
				string json = File.ReadAllText(Path.Combine("Res", "Map", script));
				EventJSON jEvents = JsonConvert.DeserializeObject<EventJSON>(json);

				JsonSerializer serializer = new JsonSerializer();

				var rewardItem = jEvents.Mnemonics.Where(c => c.comment == "NothingCheck1").Single();
				// Do not fire notification that you can get a key item if the required shards is 0.
				rewardItem.operands.iValues[1] = keyNothings == 0 ? 99 : keyNothings - 1; // Script looks for greater than, not greater or equal to.

				rewardItem = jEvents.Mnemonics.Where(c => c.comment == "NothingCheck2").Single();
				// Do not award tier 9 item if there are 0 or 1 Nothings available.
				rewardItem.operands.iValues[1] = itemNothings <= 1 ? 99 : itemNothings - 1; // Script looks for greater than, not greater or equal to.

				rewardItem = jEvents.Mnemonics.Where(c => c.comment == "ZShardCheck").Single();
				// Do not fire notification that you can beat Zeromus if the required shards is 0.
				rewardItem.operands.iValues[1] = ZShards == 0 ? 99 : ZShards - 1; // Script looks for greater than, not greater or equal to.
				rewardItem = jEvents.Mnemonics.Where(c => c.comment == "SirenShardCheck1").Single();
				rewardItem.operands.iValues[1] = sirenShards - 1; // Script looks for greater than, not greater or equal to.
				rewardItem = jEvents.Mnemonics.Where(c => c.comment == "SirenShardCheck2").Single();
				// Do not fire notification that you can use shards if the required siren shards is 0.
				rewardItem.operands.iValues[1] = sirenShards == 0 ? 99 : sirenShards - 1; // Script looks for greater than, not greater or equal to.

				// Establish weak siren battles
				// Level 0:  Baron overworld, Mist Cave
				List<int> weakSirenBattles = new List<int> { 1, 2, 3, 4, 5, 7, 8, 6, 521, 522, 523, 524, 525, 210 };
				rewardItem = jEvents.Mnemonics.Where(c => c.comment == "WeakSirenBattle0").Single();
				rewardItem.operands.iValues[0] = weakSirenBattles[r1.Next() % weakSirenBattles.Count]; // Script looks for greater than, not greater or equal to.

				// Level 1:  Mist/Kaipo overworld, Underground waterway, Antlion Cave
				weakSirenBattles = new List<int> { 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40 };
				rewardItem = jEvents.Mnemonics.Where(c => c.comment == "WeakSirenBattle1").Single();
				rewardItem.operands.iValues[0] = weakSirenBattles[r1.Next() % weakSirenBattles.Count]; // Script looks for greater than, not greater or equal to.

				// Level 2:  Mt. Hobs, Fabul overworld
				weakSirenBattles = new List<int> { 41, 42, 43, 44, 45, 46, 47, 53, 54, 55, 56, 529, 530, 531 };
				rewardItem = jEvents.Mnemonics.Where(c => c.comment == "WeakSirenBattle2").Single();
				rewardItem.operands.iValues[0] = weakSirenBattles[r1.Next() % weakSirenBattles.Count]; // Script looks for greater than, not greater or equal to.

				// Level 3:  Mysidia overworld, Mt. Ordeals
				weakSirenBattles = new List<int> { 41, 42, 46, 48, 50, 51, 52, 53, 55, 57, 58, 59, 61, 62, 63, 64, 68, 69, 70, 71, 72, 73, 74, 529, 531, 530 };
				rewardItem = jEvents.Mnemonics.Where(c => c.comment == "WeakSirenBattle3").Single();
				rewardItem.operands.iValues[0] = weakSirenBattles[r1.Next() % weakSirenBattles.Count]; // Script looks for greater than, not greater or equal to.

				// Level 4:  Old Waterway
				weakSirenBattles = new List<int> { 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88 };
				rewardItem = jEvents.Mnemonics.Where(c => c.comment == "WeakSirenBattle4").Single();
				rewardItem.operands.iValues[0] = weakSirenBattles[r1.Next() % weakSirenBattles.Count]; // Script looks for greater than, not greater or equal to.

				using (StreamWriter sw = new StreamWriter(Updater.MemoriaToMagiciteFile(directory, Path.Combine("Map", script))))
				using (JsonWriter writer = new JsonTextWriter(sw))
				{
					serializer.Serialize(writer, jEvents);
				}
			}

			List<singleMonster> allMonsters;

			// Do not load from initial CSV file.  We need to load from the already saved CSV file because the monsters were already randomized.
			using (StreamReader reader = new(Updater.MemoriaToMagiciteFile(directory, "MainData\\monster.csv")))
			using (CsvReader csv = new(reader, System.Globalization.CultureInfo.InvariantCulture))
				allMonsters = csv.GetRecords<singleMonster>().ToList();

			singleMonster zeroMonster = allMonsters.Where(c => c.id == 202).Single();

			if (difficulty == 0) { zeroMonster.hp = 10000; }
			if (difficulty == 1) { zeroMonster.hp = 40000; }
			// Very hard difficulty = Much higher HP and double agility
			if (difficulty == 4) { zeroMonster.hp = 120000; zeroMonster.agility = 202; }

			using (StreamWriter writer = new(Updater.MemoriaToMagiciteFile(directory, "MainData\\monster.csv")))
			using (CsvWriter csv = new(writer, System.Globalization.CultureInfo.InvariantCulture))
			{
				csv.WriteRecords(allMonsters);
			}


			List<ability> allAbility;
			using (StreamReader reader = new(Updater.MemoriaToMagiciteFile(directory, "MainData\\ability.csv")))
			using (CsvReader csv = new(reader, System.Globalization.CultureInfo.InvariantCulture))
				allAbility = csv.GetRecords<ability>().ToList();

			ability bigBangAbility = allAbility.Where(c => c.id == 150).Single();
			// Alter Big Bang power based on difficulty
			bigBangAbility.standard_value = difficulty switch
			{
				0 => 9,
				1 => 18,
				2 => 40,
				3 => 50,
				4 => 50,
				_ => 40,
			};
			ability nukeLevel2 = new ability { id = 501, sort_id = 501, ability_lv = 0, ability_group_id = 2, type_id = 2, attribute_id = 0, attribute_group_id = 0, system_id = 3, use_value = 50, standard_value = 40, adding_hit_rate = 0, valid_hit_rate = 0, weak_hit_rate = 0, attack_count = 0, accuracy_rate = 100, Impact_status = 0, use_job_group_id = 39, condition_group_id = 93, renge_id = 7, menu_renge_id = 7, battle_renge_id = 7, content_flag_group_id = 0, invalid_reflection = 0, invalid_boss = 0, resistance_attribute = 0, battle_effect_asset_id = 36, menu_se_asset_id = 0, reaction_type = 0, menu_function_group_id = 0, battle_function_group_id = 10, buy = 0, sell = 0, sales_not_possible = 0, ability_wait = 0, process_prog = "None", data_a = 0, data_b = 0, data_c = 0 };
			allAbility.Add(nukeLevel2);
			ability nukeLevel1 = new ability { id = 502, sort_id = 502, ability_lv = 0, ability_group_id = 2, type_id = 2, attribute_id = 0, attribute_group_id = 0, system_id = 3, use_value = 50, standard_value = 20, adding_hit_rate = 0, valid_hit_rate = 0, weak_hit_rate = 0, attack_count = 0, accuracy_rate = 100, Impact_status = 0, use_job_group_id = 39, condition_group_id = 93, renge_id = 7, menu_renge_id = 7, battle_renge_id = 7, content_flag_group_id = 0, invalid_reflection = 0, invalid_boss = 0, resistance_attribute = 0, battle_effect_asset_id = 36, menu_se_asset_id = 0, reaction_type = 0, menu_function_group_id = 0, battle_function_group_id = 10, buy = 0, sell = 0, sales_not_possible = 0, ability_wait = 0, process_prog = "None", data_a = 0, data_b = 0, data_c = 0 };
			allAbility.Add(nukeLevel1);

			ability bioLevel2 = new ability { id = 503, sort_id = 503, ability_lv = 0, ability_group_id = 2, type_id = 2, attribute_id = 0, attribute_group_id = 0, system_id = 3, use_value = 20, standard_value = 12, adding_hit_rate = 0, valid_hit_rate = 0, weak_hit_rate = 0, attack_count = 0, accuracy_rate = 100, Impact_status = 0, use_job_group_id = 0, condition_group_id = 93, renge_id = 4, menu_renge_id = 4, battle_renge_id = 4, content_flag_group_id = 0, invalid_reflection = 0, invalid_boss = 0, resistance_attribute = 0, battle_effect_asset_id = 183, menu_se_asset_id = 0, reaction_type = 0, menu_function_group_id = 0, battle_function_group_id = 91, buy = 0, sell = 0, sales_not_possible = 0, ability_wait = 0, process_prog = "None", data_a = 0, data_b = 0, data_c = 0 };
			allAbility.Add(bioLevel2);
			ability bioLevel1 = new ability { id = 504, sort_id = 504, ability_lv = 0, ability_group_id = 2, type_id = 2, attribute_id = 0, attribute_group_id = 0, system_id = 3, use_value = 20, standard_value = 6, adding_hit_rate = 0, valid_hit_rate = 0, weak_hit_rate = 0, attack_count = 0, accuracy_rate = 100, Impact_status = 0, use_job_group_id = 0, condition_group_id = 93, renge_id = 4, menu_renge_id = 4, battle_renge_id = 4, content_flag_group_id = 0, invalid_reflection = 0, invalid_boss = 0, resistance_attribute = 0, battle_effect_asset_id = 183, menu_se_asset_id = 0, reaction_type = 0, menu_function_group_id = 0, battle_function_group_id = 91, buy = 0, sell = 0, sales_not_possible = 0, ability_wait = 0, process_prog = "None", data_a = 0, data_b = 0, data_c = 0 };
			allAbility.Add(bioLevel1);

			ability meteoLevel2 = new ability { id = 505, sort_id = 505, ability_lv = 0, ability_group_id = 2, type_id = 2, attribute_id = 5, attribute_group_id = 105, system_id = 3, use_value = 99, standard_value = 80, adding_hit_rate = 0, valid_hit_rate = 0, weak_hit_rate = 0, attack_count = 0, accuracy_rate = 100, Impact_status = 0, use_job_group_id = 31, condition_group_id = 93, renge_id = 4, menu_renge_id = 4, battle_renge_id = 4, content_flag_group_id = 0, invalid_reflection = 1, invalid_boss = 0, resistance_attribute = 0, battle_effect_asset_id = 185, menu_se_asset_id = 0, reaction_type = 0, menu_function_group_id = 0, battle_function_group_id = 10, buy = 0, sell = 0, sales_not_possible = 0, ability_wait = 10, process_prog = "None", data_a = 0, data_b = 0, data_c = 0 };
			allAbility.Add(meteoLevel2);
			ability meteoLevel1 = new ability { id = 506, sort_id = 506, ability_lv = 0, ability_group_id = 2, type_id = 2, attribute_id = 5, attribute_group_id = 105, system_id = 3, use_value = 99, standard_value = 40, adding_hit_rate = 0, valid_hit_rate = 0, weak_hit_rate = 0, attack_count = 0, accuracy_rate = 100, Impact_status = 0, use_job_group_id = 31, condition_group_id = 93, renge_id = 4, menu_renge_id = 4, battle_renge_id = 4, content_flag_group_id = 0, invalid_reflection = 1, invalid_boss = 0, resistance_attribute = 0, battle_effect_asset_id = 185, menu_se_asset_id = 0, reaction_type = 0, menu_function_group_id = 0, battle_function_group_id = 10, buy = 0, sell = 0, sales_not_possible = 0, ability_wait = 10, process_prog = "None", data_a = 0, data_b = 0, data_c = 0 };
			allAbility.Add(meteoLevel2);

			using (StreamWriter writer = new(Updater.MemoriaToMagiciteFile(directory, "MainData\\ability.csv")))
			using (CsvWriter csv = new(writer, System.Globalization.CultureInfo.InvariantCulture))
			{
				csv.WriteRecords(allAbility);
			}

			List<Content> allContent;

			using (StreamReader reader = new(Updater.MemoriaToMagiciteFile(directory, "MainData\\content.csv")))
			using (CsvReader csv = new(reader, System.Globalization.CultureInfo.InvariantCulture))
				allContent = csv.GetRecords<Content>().ToList();
			Content newContent = new Content { id = 851, mes_id_name = "MSG_MAGIC_NAME_48", mes_id_battle = "None", mes_id_description = "None", icon_id = 0, type_id = 4, type_value = 501 };
			allContent.Add(newContent);
			newContent = new Content { id = 852, mes_id_name = "MSG_MAGIC_NAME_48", mes_id_battle = "None", mes_id_description = "None", icon_id = 0, type_id = 4, type_value = 502 };
			allContent.Add(newContent);
			newContent = new Content { id = 853, mes_id_name = "MSG_MAGIC_NAME_38", mes_id_battle = "None", mes_id_description = "None", icon_id = 0, type_id = 4, type_value = 503 };
			allContent.Add(newContent);
			newContent = new Content { id = 854, mes_id_name = "MSG_MAGIC_NAME_38", mes_id_battle = "None", mes_id_description = "None", icon_id = 0, type_id = 4, type_value = 504 };
			allContent.Add(newContent);
			newContent = new Content { id = 855, mes_id_name = "MSG_MAGIC_NAME_47", mes_id_battle = "None", mes_id_description = "None", icon_id = 0, type_id = 4, type_value = 505 };
			allContent.Add(newContent);
			newContent = new Content { id = 856, mes_id_name = "MSG_MAGIC_NAME_47", mes_id_battle = "None", mes_id_description = "None", icon_id = 0, type_id = 4, type_value = 506 };
			allContent.Add(newContent);

			using (StreamWriter writer = new(Updater.MemoriaToMagiciteFile(directory, "MainData\\content.csv")))
			using (CsvWriter csv = new(writer, System.Globalization.CultureInfo.InvariantCulture))
			{
				csv.WriteRecords(allContent);
			}

			// Rewrite Zeromus script

			string zeroAIJson = File.ReadAllText(Path.Combine("Res", "Battle", "MonsterAI", "sc_ai_202_Zeromus.json"));
			MonsterAiJSON zeroJson = JsonConvert.DeserializeObject<MonsterAiJSON>(zeroAIJson);
			if (difficulty <= 1)
			{
				// Jump straight to Meteo phase after Z's initial HP falls to 10000 or 4000, depending on difficulty.
				MonsterAiJSON.Mnemonic hpMnemonic = zeroJson.Mnemonics.Where(c => c.label == "0018").Single();
				hpMnemonic.operands.iValues[3] = difficulty == 1 ? 10000 : 4000;
				hpMnemonic.operands.iValues[4] = 63;

				// Weaker bio
				hpMnemonic = zeroJson.Mnemonics.Where(c => c.label == "0037").Single();
				hpMnemonic.operands.iValues[1] = difficulty == 1 ? 503 : 504;
				hpMnemonic = zeroJson.Mnemonics.Where(c => c.label == "0087").Single();
				hpMnemonic.operands.iValues[0] = difficulty == 1 ? 503 : 504;

				// Weaker flare/nuke
				hpMnemonic = zeroJson.Mnemonics.Where(c => c.label == "0060").Single();
				hpMnemonic.operands.iValues[1] = difficulty == 1 ? 501 : 502;
				hpMnemonic = zeroJson.Mnemonics.Where(c => c.label == "0080").Single();
				hpMnemonic.operands.iValues[0] = difficulty == 1 ? 501 : 502;

				// Weaker meteo
				hpMnemonic = zeroJson.Mnemonics.Where(c => c.label == "0074").Single();
				hpMnemonic.operands.iValues[1] = difficulty == 1 ? 505 : 506;
			}

			JsonSerializer zeroAISerialize = new();

			using StreamWriter zeroAISW = new(Updater.MemoriaToMagiciteFile(directory, "MonsterAI\\sc_ai_202_Zeromus.json"));
			using JsonWriter zeroAIWriter = new JsonTextWriter(zeroAISW);
			zeroAISerialize.Serialize(zeroAIWriter, zeroJson);
		}
	}
}
