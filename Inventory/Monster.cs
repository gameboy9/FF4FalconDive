using CsvHelper;
using FF4FalconDive.Inventory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF4FreeEnterprisePR.Inventory
{
	public static class Monster
	{
		private class singleMonster
		{
			public int id { get; set; }
			public string mes_id_name { get; set; }
			public int cursor_x_position { get; set; }
			public int cursor_y_position { get; set; }
			public int in_type_id { get; set; }
			public int disappear_type_id { get; set; }
			public int species { get; set; }
			public int resistance_attribute { get; set; }
			public int resistance_condition { get; set; }
			public int initial_condition { get; set; }
			public int lv { get; set; }
			public int hp { get; set; }
			public int mp { get; set; }
			public int exp { get; set; }
			public int gill { get; set; }
			public int attack_count { get; set; }
			public int attack_plus { get; set; }
			public int attack_plus_group { get; set; }
			public int attack_attribute { get; set; }
			public int strength { get; set; }
			public int vitality { get; set; }
			public int agility { get; set; }
			public int intelligence { get; set; }
			public int spirit { get; set; }
			public int magic { get; set; }
			public int attack { get; set; }
			public int ability_attack { get; set; }
			public int defense { get; set; }
			public int ability_defense { get; set; }
			public int ability_defense_rate { get; set; }
			public int accuracy_rate { get; set; }
			public int dodge_times { get; set; }
			public int evasion_rate { get; set; }
			public int magic_evasion_rate { get; set; }
			public int ability_disturbed_rate { get; set; }
			public int critical_rate { get; set; }
			public int luck { get; set; }
			public int weight { get; set; }
			public int boss { get; set; }
			public int monster_flag_group_id { get; set; }
			public int drop_rate { get; set; }
			public int drop_content_id1 { get; set; }
			public int drop_content_id1_value { get; set; }
			public int drop_content_id2 { get; set; }
			public int drop_content_id2_value { get; set; }
			public int drop_content_id3 { get; set; }
			public int drop_content_id3_value { get; set; }
			public int drop_content_id4 { get; set; }
			public int drop_content_id4_value { get; set; }
			public int drop_content_id5 { get; set; }
			public int drop_content_id5_value { get; set; }
			public int drop_content_id6 { get; set; }
			public int drop_content_id6_value { get; set; }
			public int drop_content_id7 { get; set; }
			public int drop_content_id7_value { get; set; }
			public int drop_content_id8 { get; set; }
			public int drop_content_id8_value { get; set; }
			public int steal_content_id1 { get; set; }
			public int steal_content_id2 { get; set; }
			public int steal_content_id3 { get; set; }
			public int steal_content_id4 { get; set; }
			public int script_id { get; set; }
			public int monster_asset_id { get; set; }
			public int battle_effect_asset_id { get; set; }
			public int p_use_ability_random_group_id { get; set; }
			public int command_group_type { get; set; }
			public int release_ability_random_group_id { get; set; }
			public int rage_ability_random_group_id { get; set; }
		}

		private class singleGroup
		{
			public int id { get; set; }
			public int battle_background_asset_id { get; set; }
			public int battle_bgm_asset_id { get; set; }
			public int appearance_production { get; set; }
			public int script_name { get; set; }
			public int battle_pattern1 { get; set; }
			public int battle_pattern2 { get; set; }
			public int battle_pattern3 { get; set; }
			public int battle_pattern4 { get; set; }
			public int battle_pattern5 { get; set; }
			public int battle_pattern6 { get; set; }
			public int not_escape { get; set; }
			public int battle_flag_group_id { get; set; }
			public int get_value { get; set; }
			public int get_ap { get; set; }
			public int monster1 { get; set; }
			public int monster1_x_position { get; set; }
			public int monster1_y_position { get; set; }
			public int monster1_group { get; set; }
			public int monster2 { get; set; }
			public int monster2_x_position { get; set; }
			public int monster2_y_position { get; set; }
			public int monster2_group { get; set; }
			public int monster3 { get; set; }
			public int monster3_x_position { get; set; }
			public int monster3_y_position { get; set; }
			public int monster3_group { get; set; }
			public int monster4 { get; set; }
			public int monster4_x_position { get; set; }
			public int monster4_y_position { get; set; }
			public int monster4_group { get; set; }
			public int monster5 { get; set; }
			public int monster5_x_position { get; set; }
			public int monster5_y_position { get; set; }
			public int monster5_group { get; set; }
			public int monster6 { get; set; }
			public int monster6_x_position { get; set; }
			public int monster6_y_position { get; set; }
			public int monster6_group { get; set; }
			public int monster7 { get; set; }
			public int monster7_x_position { get; set; }
			public int monster7_y_position { get; set; }
			public int monster7_group { get; set; }
			public int monster8 { get; set; }
			public int monster8_x_position { get; set; }
			public int monster8_y_position { get; set; }
			public int monster8_group { get; set; }
			public int monster9 { get; set; }
			public int monster9_x_position { get; set; }
			public int monster9_y_position { get; set; }
			public int monster9_group { get; set; }
		}

		class limitedMonsters
		{
			public int id;
			public int hpPercentage = 0;
			public int monsterLimit = 9;
			public int followUp = 0;
		}

		public static void MonsterBoost(string directory, double xpMultiplier, int xpBoost, double gpMultiplier, int gpBoost, int gpDivisor)
		{
			List<singleMonster> allMonsters;

			// Do not load from initial CSV file.  We need to load from the already saved CSV file because the monsters were already randomized.
			using (StreamReader reader = new(Updater.MemoriaToMagiciteFile(directory, "MainData", "master", "monster.csv")))
			using (CsvReader csv = new(reader, System.Globalization.CultureInfo.InvariantCulture))
				allMonsters = csv.GetRecords<singleMonster>().ToList();

			foreach (singleMonster iMonster in allMonsters)
			{
				iMonster.exp = (int)(iMonster.exp * xpMultiplier);
				iMonster.exp += xpBoost;
				iMonster.gill = (int)(iMonster.gill * gpMultiplier / gpDivisor);
				iMonster.gill += gpBoost;
			}

			using (StreamWriter writer = new(Updater.MemoriaToMagiciteFile(directory, "MainData", "master", "monster.csv")))
			using (CsvWriter csv = new(writer, System.Globalization.CultureInfo.InvariantCulture))
			{
				csv.WriteRecords(allMonsters);
			}
		}

		public static void AdjustMonsterDifficulty(string directory, int monsterDifficulty)
		{
			List<Game_Constant> gameConst;

			using (StreamReader reader = new(Updater.MemoriaToMagiciteFile(directory, "MainData", "master", "game_constant_int.csv")))
			using (CsvReader csv = new(reader, System.Globalization.CultureInfo.InvariantCulture))
				gameConst = csv.GetRecords<Game_Constant>().ToList();

			// Keep commented.  We're going to directly adjust agility for each monster instead.
			Game_Constant enemySpeed = gameConst.Where(c => c.key_name == "ENEMY_ATP_COEFFICIENT").Single();
			enemySpeed.value1 = 2;
			//enemySpeed.value1 = monsterDifficulty == 0 ? 2 : 1;

			// Do adjust 
			Game_Constant starterHeroATB = gameConst.Where(c => c.key_name == "ACCUMULATION_ATB_NORMAL").Single();
			switch (monsterDifficulty)
			{
				case 0: starterHeroATB.value1 = 50; starterHeroATB.value2 = 100; break;
				case 1: starterHeroATB.value1 = 25; starterHeroATB.value2 = 50; break;
				case 2: starterHeroATB.value1 = 0; starterHeroATB.value2 = 50; break;
				case 3: starterHeroATB.value1 = 0; starterHeroATB.value2 = 25; break;
			}

			starterHeroATB.value1 = monsterDifficulty == 0 ? 0 : 25;

			Game_Constant starterEnemyATB = gameConst.Where(c => c.key_name == "ENEMY_ACCUMULATION_ATB_NORMAL").Single();
			switch (monsterDifficulty)
			{
				case 0: starterHeroATB.value1 = 0; starterHeroATB.value2 = 25; break;
				case 1: starterHeroATB.value1 = 0; starterHeroATB.value2 = 50; break;
				case 2: starterHeroATB.value1 = 15; starterHeroATB.value2 = 30; break;
				case 3: starterHeroATB.value1 = 50; starterHeroATB.value2 = 100; break;
			}

			using (StreamWriter writer = new(Updater.MemoriaToMagiciteFile(directory, "MainData", "master", "game_constant_int.csv")))
			using (CsvWriter csv = new(writer, System.Globalization.CultureInfo.InvariantCulture))
			{
				csv.WriteRecords(gameConst);
			}

			List<singleMonster> allMonsters;

			// Do not load from initial CSV file.  We need to load from the already saved CSV file because the monsters were already randomized.
			using (StreamReader reader = new(Updater.MemoriaToMagiciteFile(directory, "MainData", "master", "monster.csv")))
			using (CsvReader csv = new(reader, System.Globalization.CultureInfo.InvariantCulture))
				allMonsters = csv.GetRecords<singleMonster>().ToList();

			foreach (singleMonster iMonster in allMonsters)
			{
				// Do not adjust Zeromus difficulty.  That's a different flag.
				if (iMonster.id == 202)
					continue;
				switch (monsterDifficulty)
				{
					case 0: 
						iMonster.hp = (int)iMonster.hp * 3 / 4;
						//iMonster.attack_count = iMonster.attack_count;
						iMonster.agility = (int)iMonster.agility * 3 / 4;
						iMonster.attack = (int)iMonster.attack * 3 / 4;
						iMonster.intelligence = (int)iMonster.intelligence * 3 / 4;
						iMonster.spirit = (int)iMonster.spirit * 3 / 4;
						iMonster.magic = (int)iMonster.magic * 3 / 4;
						break;
					case 1: // Normal old patch rules.
						break;
					case 2:
						iMonster.lv = (int)iMonster.lv * 3 / 2;
						iMonster.hp = (int)iMonster.hp * 5 / 4;
						iMonster.attack_count = iMonster.attack_count * 3 / 2;
						iMonster.agility = (int)iMonster.agility * 3 / 2;
						iMonster.attack = (int)iMonster.attack * 5 / 4;
						iMonster.intelligence = (int)iMonster.intelligence * 5 / 4;
						iMonster.spirit = (int)iMonster.spirit * 5 / 4;
						iMonster.magic = (int)iMonster.magic * 5 / 4;
						iMonster.accuracy_rate = 99;
						break;
					case 3:
						iMonster.lv = (int)iMonster.lv * 2;
						iMonster.hp = (int)iMonster.hp * 3 / 2;
						iMonster.attack_count = iMonster.attack_count * 2;
						iMonster.agility = (int)iMonster.agility * 2;
						iMonster.attack = (int)iMonster.attack * 5 / 4;
						iMonster.intelligence = (int)iMonster.intelligence * 3 / 2;
						iMonster.spirit = (int)iMonster.spirit * 3 / 2;
						iMonster.magic = (int)iMonster.magic * 3 / 2;
						iMonster.accuracy_rate = 99;
						break;
				}
			}

			using (StreamWriter writer = new(Updater.MemoriaToMagiciteFile(directory, "MainData", "master", "monster.csv")))
			using (CsvWriter csv = new(writer, System.Globalization.CultureInfo.InvariantCulture))
			{
				csv.WriteRecords(allMonsters);
			}

		}
	}
}
