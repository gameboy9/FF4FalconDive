using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF4FreeEnterprisePR.Common
{
	public class singleMonster
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

		public singleMonster clone(int newID)
		{
			singleMonster newMonster = new singleMonster();
			newMonster.id = newID;
			newMonster.mes_id_name = mes_id_name;
			newMonster.cursor_x_position = cursor_x_position;
			newMonster.cursor_y_position = cursor_y_position;
			newMonster.in_type_id = in_type_id;
			newMonster.disappear_type_id = disappear_type_id;
			newMonster.species = species;
			newMonster.resistance_attribute = resistance_attribute;
			newMonster.resistance_condition = resistance_condition;
			newMonster.initial_condition = initial_condition;
			newMonster.lv = lv;
			newMonster.hp = hp;
			newMonster.mp = mp;
			newMonster.exp = exp;
			newMonster.gill = gill;
			newMonster.attack_count = attack_count;
			newMonster.attack_plus = attack_plus;
			newMonster.attack_plus_group = attack_plus_group;
			newMonster.attack_attribute = attack_attribute;
			newMonster.strength = strength;
			newMonster.vitality = vitality;
			newMonster.agility = agility;
			newMonster.intelligence = intelligence;
			newMonster.spirit = spirit;
			newMonster.magic = magic;
			newMonster.attack = attack;
			newMonster.ability_attack = ability_attack;
			newMonster.defense = defense;
			newMonster.ability_defense = ability_defense;
			newMonster.ability_defense_rate = ability_defense_rate;
			newMonster.accuracy_rate = accuracy_rate;
			newMonster.dodge_times = dodge_times;
			newMonster.evasion_rate = evasion_rate;
			newMonster.magic_evasion_rate = magic_evasion_rate;
			newMonster.ability_disturbed_rate = ability_disturbed_rate;
			newMonster.critical_rate = critical_rate;
			newMonster.luck = luck;
			newMonster.weight = weight;
			newMonster.boss = boss;
			newMonster.monster_flag_group_id = monster_flag_group_id;
			newMonster.drop_rate = drop_rate;
			newMonster.drop_content_id1 = drop_content_id1;
			newMonster.drop_content_id1_value = drop_content_id1_value;
			newMonster.drop_content_id2 = drop_content_id2;
			newMonster.drop_content_id2_value = drop_content_id2_value;
			newMonster.drop_content_id3 = drop_content_id3;
			newMonster.drop_content_id3_value = drop_content_id3_value;
			newMonster.drop_content_id4 = drop_content_id4;
			newMonster.drop_content_id4_value = drop_content_id4_value;
			newMonster.drop_content_id5 = drop_content_id5;
			newMonster.drop_content_id5_value = drop_content_id5_value;
			newMonster.drop_content_id6 = drop_content_id6;
			newMonster.drop_content_id6_value = drop_content_id6_value;
			newMonster.drop_content_id7 = drop_content_id7;
			newMonster.drop_content_id7_value = drop_content_id7_value;
			newMonster.drop_content_id8 = drop_content_id8;
			newMonster.drop_content_id8_value = drop_content_id8_value;
			newMonster.steal_content_id1 = steal_content_id1;
			newMonster.steal_content_id2 = steal_content_id2;
			newMonster.steal_content_id3 = steal_content_id3;
			newMonster.steal_content_id4 = steal_content_id4;
			newMonster.script_id = script_id;
			newMonster.monster_asset_id = monster_asset_id;
			newMonster.battle_effect_asset_id = battle_effect_asset_id;
			newMonster.p_use_ability_random_group_id = p_use_ability_random_group_id;
			newMonster.command_group_type = command_group_type;
			newMonster.release_ability_random_group_id = release_ability_random_group_id;
			newMonster.rage_ability_random_group_id = rage_ability_random_group_id;

			return newMonster;
		}
	}
}
