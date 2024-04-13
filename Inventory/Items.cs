using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static FF4FreeEnterprisePR.Common.Common;

namespace FF4FreeEnterprisePR.Inventory
{
	public class Items
	{
		private class singleItem
		{
			public int id { get; set; }
			public int sort_id { get; set; }
			public int type_id { get; set; }
			public int system_id { get; set; }
			public int item_lv { get; set; }
			public int attribute_id { get; set; }
			public int accuracy_rate { get; set; }
			public int destroy_rate { get; set; }
			public int standard_value { get; set; }
			public int renge_id { get; set; }
			public int menu_renge_id { get; set; }
			public int battle_renge_id { get; set; }
			public int invalid_reflection { get; set; }
			public int period_id { get; set; }
			public int throw_flag { get; set; }
			public int preparation_flag { get; set; }
			public int drink_flag { get; set; }
			public int machine_flag { get; set; }
			public int condition_group_id { get; set; }
			public int battle_effect_asset_id { get; set; }
			public int menu_se_asset_id { get; set; }
			public int menu_function_group_id { get; set; }
			public int battle_function_group_id { get; set; }
			public int buy { get; set; }
			public int sell { get; set; }
			public int sales_not_possible { get; set; }
		}

		public const int potion = 2; // t1
		public const int hiPotion = 3; // t3
		public const int xPotion = 4; // t6
		public const int ether = 5; // t3
		public const int dryEther = 6; // t6
		public const int elixir = 7; // t8
		public const int phoenixDown = 9; // t1
		public const int goldNeedle = 10; // t3
		public const int maidensKiss = 11; // t1
		public const int mallet = 12; // t1
		public const int dietFood = 13; // t1
		public const int echoHerbs = 14; // t1
		public const int eyeDrops = 15; // t1
		public const int antidote = 16; // t1
		public const int cross = 17; // t3
		public const int remedy = 18; // t5
		public const int alarmClock = 19; // t3
		public const int unicornHorn = 20; // t5
		public const int tent = 21; // t3
		public const int cottage = 22; // t6
		public const int emergencyExit = 23; // t4
		// This whistle, ID 26, is skipped.
		public const int goldenApple = 27; // t8
		public const int silverApple = 28; // t7
		public const int somaDrop = 29; // t7
		public const int redFang = 33; // t5
		public const int whiteFang = 34; // t5
		public const int blueFang = 35; // t5
		public const int bombFragment = 36; // t1
		public const int bombArm = 37; // t4
		public const int antarcticWind = 38; // t1
		public const int arcticWind = 39; // t4
		public const int zeusWrath = 40; // t1
		public const int rageOfTheGods = 41; // t4
		public const int gaiaDrum = 42; // t6
		public const int bombCore = 43; // t3
		public const int stardust = 44; // t5
		public const int lilithKiss = 45; // t3
		public const int vampireFang = 46; // t4
		public const int spiderSilk = 47; // t3
		public const int bellOfSilence = 48; // t2
		public const int coeurlWhisker = 49; // t6
		public const int bestiary = 50; // t1
		public const int hourglass = 51; // t6
		public const int silverHourglass = 52; // t7
		public const int goldenHourglass = 53; // t8
		public const int bacchusCider = 54; // t4
		public const int hermesShoes = 55; // t4
		public const int decoy = 56; // t3
		public const int lightCurtain = 57; // t6
		public const int lunarCurtain = 58; // t8
		public const int pinkTail = 70; // t8
		public const int megalixir = 969; // t8

		public List<List<int>> tiers = new List<List<int>>
			{ new List<int> { potion, phoenixDown, maidensKiss, mallet, dietFood, echoHerbs, eyeDrops, antidote,
					bombFragment, antarcticWind, zeusWrath, bestiary },
			  new List<int> { vampireFang, goldNeedle, bellOfSilence },
			  new List<int> { hiPotion, ether, cross, alarmClock, tent, bombCore, lilithKiss, spiderSilk, decoy },
			  new List<int> { bombArm, arcticWind, rageOfTheGods, bacchusCider, hermesShoes, emergencyExit },
			  new List<int> { remedy, unicornHorn, redFang, whiteFang, stardust, blueFang },
			  new List<int> { xPotion, dryEther, cottage, coeurlWhisker, lightCurtain, hourglass, gaiaDrum },
			  new List<int> { xPotion, silverApple, somaDrop, silverHourglass },
			  // Duplicating the first part to reduce the chance of acquiring a Pink Tail.
			  new List<int> { elixir, goldenApple, goldenHourglass, lunarCurtain, elixir, goldenApple, goldenHourglass, lunarCurtain, pinkTail },
			  new List<int> { megalixir, goldenApple, pinkTail }
		};

		List<int> jItems = new List<int>
		{
			maidensKiss, mallet, dietFood, echoHerbs, eyeDrops, antidote, bombFragment, antarcticWind, zeusWrath, bestiary, bellOfSilence,
				cross, alarmClock, bombCore, lilithKiss, spiderSilk, decoy, bombArm, arcticWind, rageOfTheGods, gaiaDrum, stardust,
			vampireFang, bacchusCider, hermesShoes, unicornHorn, redFang, whiteFang, blueFang, hourglass, coeurlWhisker, lightCurtain,
			silverApple, somaDrop, silverHourglass, goldenApple, goldenHourglass, lunarCurtain, emergencyExit, megalixir
		};

		public void adjustPrices(string directory, int multiplier, int divisor)
		{
			List<singleItem> records;

			using (StreamReader reader = new StreamReader(Path.Combine("csv", "item.csv")))
			using (CsvReader csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
				records = csv.GetRecords<singleItem>().ToList();

			foreach (singleItem item in records)
			{
				item.buy *= multiplier;
				item.buy /= divisor;
				item.sell *= Math.Min(multiplier, 4);
				item.sell /= divisor;

				item.buy = item.buy > 99999 ? 99999 : item.buy < 1 ? 1 : item.buy;
				item.sell = item.sell > 99999 ? 99999 : item.sell < 1 ? 1 : item.sell;
			}

			using (StreamWriter writer = new StreamWriter(Updater.MemoriaToMagiciteFile(directory, "MainData\\item.csv")))
			using (CsvWriter csv = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
			{
				csv.WriteRecords(records);
			}
		}

		public int selectItem(Random r1, int minTier, int maxTier, bool highTierReduction, bool noJItems)
		{
			List<int> selection = new List<int>();
			for (int i = minTier - 1; i <= maxTier - 1; i++)
			{
				List<int> tiersToAdd = tiers[i];
				if (noJItems)
					tiersToAdd = tiersToAdd.Where(c => !jItems.Contains(c)).ToList();
				int repetition = highTierReduction ? maxTier - i : 1;
				for (int j = 0; j < repetition; j++)
					selection.AddRange(tiersToAdd);
			}
			return selection[r1.Next() % selection.Count];
		}
	}
}
