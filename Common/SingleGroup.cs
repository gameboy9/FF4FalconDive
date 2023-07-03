using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF4FreeEnterprisePR.Common
{
	public class singleGroup
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

		public singleGroup clone(int newID, bool neutralBattle = true, bool noEscape = true)
		{
			singleGroup newGroup = new singleGroup
			{
				id = newID,
				battle_background_asset_id = battle_background_asset_id,
				battle_bgm_asset_id = battle_bgm_asset_id,
				appearance_production = appearance_production,
				script_name = 0,
				battle_pattern1 = neutralBattle ? 1 : 0,
				battle_pattern2 = neutralBattle ? 0 : 1,
				battle_pattern3 = neutralBattle ? 0 : 1,
				battle_pattern4 = neutralBattle ? 0 : 1,
				battle_pattern5 = neutralBattle ? 0 : 1,
				battle_pattern6 = neutralBattle ? 0 : 1,
				not_escape = noEscape ? 1 : 0,
				battle_flag_group_id = battle_flag_group_id,
				get_value = get_value,
				get_ap = get_ap,
				monster1 = monster1,
				monster1_x_position = monster1_x_position,
				monster1_y_position = monster1_y_position,
				monster1_group = monster1_group,
				monster2 = monster2,
				monster2_x_position = monster2_x_position,
				monster2_y_position = monster2_y_position,
				monster2_group = monster2_group,
				monster3 = monster3,
				monster3_x_position = monster3_x_position,
				monster3_y_position = monster3_y_position,
				monster3_group = monster3_group,
				monster4 = monster4,
				monster4_x_position = monster4_x_position,
				monster4_y_position = monster4_y_position,
				monster4_group = monster4_group,
				monster5 = monster5,
				monster5_x_position = monster5_x_position,
				monster5_y_position = monster5_y_position,
				monster5_group = monster5_group,
				monster6 = monster6,
				monster6_x_position = monster6_x_position,
				monster6_y_position = monster6_y_position,
				monster6_group = monster6_group,
				monster7 = monster7,
				monster7_x_position = monster7_x_position,
				monster7_y_position = monster7_y_position,
				monster7_group = monster7_group,
				monster8 = monster8,
				monster8_x_position = monster8_x_position,
				monster8_y_position = monster8_y_position,
				monster8_group = monster8_group,
				monster9 = monster9,
				monster9_x_position = monster9_x_position,
				monster9_y_position = monster9_y_position,
				monster9_group = monster9_group
			};

			return newGroup;
		}
	}
}
