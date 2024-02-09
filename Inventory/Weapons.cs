using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static FF4FreeEnterprisePR.Common.Common;

namespace FF4FreeEnterprisePR.Inventory
{
	class Weapons
	{
		private class singleWeapon
		{
			public int id { get; set; }
			public string sort_id { get; set; }
			public int type_id { get; set; }
			public int category_type { get; set; }
			public int attribute_id { get; set; }
			public int attribute_group_id { get; set; }
			public int equip_job_group_id { get; set; }
			public int parts_group_id { get; set; }
			public int attack { get; set; }
			public int weight { get; set; }
			public int accuracy_rate { get; set; }
			public int evasion_rate { get; set; }
			public int ability_disturbed_rate { get; set; }
			public int destroy_rate { get; set; }
			public int magnetic_force { get; set; }
			public int throw_flag { get; set; }
			public int back_flag { get; set; }
			public int two_handed_flag { get; set; }
			public int invalid_reflection { get; set; }
			public int trigger_ability_id { get; set; }
			public int wear_function_group_id { get; set; }
			public int wear_condition_group_id { get; set; }
			public int weak_attribute { get; set; }
			public int effective_species { get; set; }
			public int additional_condition_group_id { get; set; }
			public int strength { get; set; }
			public int vitality  { get; set; }
			public int agility { get; set; }
			public int intelligence { get; set; }
			public int spirit { get; set; }
			public int magic { get; set; }
			public int critical_rate { get; set; }
			public int max_hp_up { get; set; }
			public int max_mp_up { get; set; }
			public int battle_equipment_asset_id { get; set; }
			public int battle_effect_asset_id { get; set; }
			public int guard_icon { get; set; }
			public int buy { get; set; }
			public int sell { get; set; }
			public int sales_not_possible { get; set; }
			public string process_prog { get; set; }
		}

		public const int darkSword = 128; // t1
		public const int shadowblade = 129; // t1
		public const int deathbringer = 130; // t2
		public const int lightSword = 132; // t6
		public const int excalibur = 133; // t7
		public const int ragnarok = 134; // t8
		public const int ancientSword = 135; // t3
		public const int bloodSword = 136; // t3
		public const int mythrilSword = 137; // t4
		public const int sleepBlade = 138; // t4
		public const int flameSword = 139; // t5
		public const int icebrand = 140; // t5
		public const int gorgonBlade = 141; // t5
		public const int avenger = 142; // t6
		public const int defender = 143; // t6
		public const int spear = 148; // t1
		public const int windSpear = 149; // t3
		public const int fireLance = 150; // t5
		public const int iceLance = 151; // t5
		public const int bloodLance = 152; // t4
		public const int gungnir = 153; // t6
		public const int wyvernLance = 154; // t7
		public const int holyLance = 155; // t7
		public const int mythrilKnife = 156; // t3
		public const int dancingDagger = 157; // t4
		public const int mageMasher = 158; // t5
		public const int knife = 159; // t9
		public const int dreamerHarp = 160; // t1
		public const int lamiaHarp = 161; // t2
		public const int fireClaw = 162; // t2
		public const int iceClaw = 163; // t2
		public const int thunderClaw = 164; // t2
		public const int fairyClaw = 165; // t4
		public const int hellClaw = 166; // t5
		public const int catClaw = 167; // t6
		public const int woodenHammer = 168; // t2
		public const int mythrilHammer = 169; // t3
		public const int gaiaHammer = 170; // t5
		public const int dwarfAxe = 171; // t5
		public const int ogreKiller = 172; // t6
		public const int poisonAxe = 173; // t6
		public const int runeAxe = 174; // t6
		public const int kunai = 175; // t4
		public const int ashura = 176; // t5
		public const int kotetsu = 177; // t5
		public const int kikuichimonji = 178; // t6
		public const int murasame = 179; // t8
		public const int masamune = 180; // t8
		public const int rod = 181; // t1
		public const int flameRod = 182; // t2
		public const int iceRod = 183; // t2
		public const int thunderRod = 184; // t2
		public const int lilithRod = 185; // t4
		public const int changeRod = 186; // t5
		public const int fairyRod = 187; // t6
		public const int stardustRod = 188; // t8
		public const int staff = 189; // t1
		public const int healingStaff = 190; // t2
		public const int mythrilStaff = 191; // t3
		public const int powerStaff = 192; // t5
		public const int kinesisStaff = 193; // t5
		public const int sageStaff = 194; // t7
		public const int runeStaff = 195; // t8
		public const int bow = 196; // t1
		public const int crossBow = 521; // t2
		public const int greatBow = 198; // t3
		public const int killerBow = 199; // t4
		public const int elvenBow = 200; // t5
		public const int yoichiBow = 201; // t6
		public const int artemisBow = 202; // t7
		public const int medusaArrow = 203; // t3
		public const int ironArrow = 204; // t1
		public const int holyArrow = 205; // t2
		public const int fireArrow = 206; // t4
		public const int iceArrow = 207; // t4
		public const int thunderArrow = 208; // t4
		public const int blindingArrow = 209; // t5
		public const int poisonArrow = 210; // t5
		public const int muteArrow = 211; // t6
		public const int angelArrow = 212; // t7
		public const int yoichiArrow = 213; // t7
		public const int artemisArrow = 214; // t8
		public const int whip = 215; // t4
		public const int chainWhip = 216; // t5
		public const int blitzWhip = 217; // t6
		public const int flameWhip = 218; // t7
		public const int dragonWhisker = 219; // t8
		public const int boomerang = 220; // t5
		public const int moonringBlade = 221; // t6
		public const int shuriken = 222; // t5
		public const int fumaShuriken = 223; // t7

		public const int excalipoor = 899; // t2
		public const int flandango = 900; // t4
		public const int lightbringer = 901; // t9
		public const int piggyStick = 902; // t8
		public const int abelsLance = 903; // t9
		public const int gigantAxe = 904; // t9
		public const int perseusBow = 905; // t9
		public const int perseusArrow = 906; // t9
		public const int mysticWhip = 907; // t8
		public const int tritonDagger = 908; // t8
		public const int assassinDagger = 909; // t9
		public const int sasukeKatana = 910; // t9
		public const int mutsunokami = 911; // t9
		public const int scrapMetal = 912; // t2
		public const int risingSun = 913; // t8
		public const int tigerFangs = 914; // t7
		public const int dragonClaws = 915; // t8
		public const int godhand = 916; // t9
		public const int thorHammer = 917; // t8
		public const int fieryHammer = 918; // t9
		public const int asuraRod = 919; // t9
		public const int seraphimMace = 920; // t8
		public const int nirvana = 921; // t9
		public const int apolloHarp = 922; // t6
		public const int requiemHarp = 923; // t8
		public const int lokiHarp = 924; // t9
		public const int bloodDarkSword = 925; // t5
		public const int golbezSword = 926; // t6
		public const int vampireSword = 927; // t7
		public const int megicoSword = 928; // t9
		public const int erdrickSword = 929; // t7
		public const int vampireSpear = 965; // t8
		public const int superAngerRod = 966; // t9
		public const int mop = 967; // t9

		public List<List<int>> tiers = new List<List<int>>
			{ new List<int> { darkSword, shadowblade, spear, dreamerHarp, rod, staff, bow, ironArrow },
			  new List<int> { deathbringer, lamiaHarp, fireClaw, iceClaw, thunderClaw, woodenHammer, flameRod, iceRod, thunderRod, healingStaff, crossBow, holyArrow, excalipoor, scrapMetal },
			  new List<int> { ancientSword, bloodSword, windSpear, mythrilKnife, mythrilHammer, mythrilStaff, greatBow, medusaArrow },
			  new List<int> { mythrilSword, sleepBlade, bloodLance, dancingDagger, fairyClaw, kunai, lilithRod, killerBow, fireArrow, iceArrow, thunderArrow, whip, flandango },
			  new List<int> { flameSword, icebrand, gorgonBlade, fireLance, iceLance, mageMasher, hellClaw, gaiaHammer, dwarfAxe, ashura, kotetsu, elvenBow,
				changeRod, powerStaff, kinesisStaff, blindingArrow, poisonArrow, chainWhip, boomerang, shuriken, bloodDarkSword, erdrickSword },
			  new List<int> { lightSword, avenger, defender, gungnir, catClaw, ogreKiller, poisonAxe, runeAxe, kikuichimonji, fairyRod, yoichiBow, muteArrow, blitzWhip, moonringBlade, apolloHarp, golbezSword },
			  new List<int> { excalibur, wyvernLance, holyLance, sageStaff, artemisBow, angelArrow, yoichiArrow, flameWhip, fumaShuriken, tigerFangs },
			  new List<int> { piggyStick, ragnarok, murasame, masamune, stardustRod, runeStaff, artemisArrow, dragonWhisker, mysticWhip, tritonDagger, risingSun, dragonClaws, thorHammer, seraphimMace, vampireSword },
			  new List<int> { knife, lightbringer, abelsLance, gigantAxe, perseusBow, perseusArrow, assassinDagger, sasukeKatana, mutsunokami, godhand, fieryHammer, asuraRod, nirvana, requiemHarp, lokiHarp, megicoSword, vampireSpear, superAngerRod, mop }
		};

		public List<int> bonusWeapons = new List<int>
		{
			excalipoor, flandango, lightbringer, piggyStick, abelsLance, gigantAxe, perseusArrow, perseusBow, mysticWhip, tritonDagger, assassinDagger, sasukeKatana, mutsunokami,
			scrapMetal, risingSun, tigerFangs, dragonClaws, godhand, thorHammer, fieryHammer, asuraRod, seraphimMace, nirvana, apolloHarp, requiemHarp, lokiHarp
		};

		public List<int> fgExclusiveWeapons = new List<int> { bloodDarkSword, golbezSword, vampireSword, vampireSpear, megicoSword, erdrickSword, superAngerRod, mop };

		public List<int> dkCecilOnly = new List<int> { darkSword, shadowblade, deathbringer, bloodDarkSword, golbezSword, vampireSword, megicoSword };
		public List<int> pallyCecilOnly = new List<int> { excalibur, excalipoor, flandango, lightSword, ragnarok, lightbringer };
		public List<int> kainOnly = new List<int> { vampireSpear, spear, windSpear, fireLance, iceLance, bloodLance, gungnir, wyvernLance, holyLance, abelsLance };
		public List<int> cecilKain = new List<int> { ancientSword, bloodSword, mythrilSword, sleepBlade, flameSword, icebrand, gorgonBlade, avenger, defender, piggyStick };
		public List<int> cecilKainCid = new List<int> { dwarfAxe, ogreKiller, poisonAxe, runeAxe, gigantAxe };
		public List<int> bowUsers = new List<int> { bow, crossBow, greatBow, killerBow, elvenBow, yoichiBow, artemisBow, medusaArrow, ironArrow, holyArrow, fireArrow, iceArrow, thunderArrow, blindingArrow, poisonArrow, muteArrow, angelArrow, yoichiArrow, artemisArrow };
		public List<int> knifeUsers = new List<int> { mythrilKnife, dancingDagger, mageMasher, assassinDagger };
		public List<int> clawUsers = new List<int> { fireClaw, iceClaw, thunderClaw, fairyClaw, hellClaw, catClaw };
		public List<int> rodUsers = new List<int> { rod, iceRod, flameRod, thunderRod, lilithRod, changeRod, fairyRod, stardustRod };
		public List<int> staffUsers = new List<int> { staff, healingStaff, mythrilStaff, powerStaff, kinesisStaff, sageStaff, runeStaff };
		public List<int> rosaOnly = new List<int> { perseusArrow, perseusBow };
		public List<int> rydiaOnly = new List<int> { whip, chainWhip, blitzWhip, flameWhip, dragonWhisker, mysticWhip };
		public List<int> cidOnly = new List<int> { woodenHammer, mythrilHammer, gaiaHammer, thorHammer, fieryHammer };
		public List<int> edwardOnly = new List<int> { dreamerHarp, lamiaHarp, apolloHarp, requiemHarp, lokiHarp };
		public List<int> yangOnly = new List<int> { tigerFangs, dragonClaws, godhand };
		public List<int> palomOnly = new List<int> { tritonDagger, asuraRod };
		public List<int> poromOnly = new List<int> { seraphimMace, nirvana };
		public List<int> edgeOnly = new List<int> { kunai, ashura, kotetsu, kikuichimonji, murasame, masamune, sasukeKatana, mutsunokami, scrapMetal, shuriken, fumaShuriken, boomerang, moonringBlade, risingSun };
		public List<int> tellahOnly = new List<int> { superAngerRod };
		public List<int> edgeEdward = new List<int> { knife };
		public List<int> fusoyaOnly = new List<int> { mop };

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

		public void adjustPrices(string directory, int multiplier, int divisor)
		{
			List<singleWeapon> records;

			using (StreamReader reader = new StreamReader(Path.Combine("csv", "weapon.csv")))
			using (CsvReader csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
				records = csv.GetRecords<singleWeapon>().ToList();

			foreach (singleWeapon item in records)
			{
				item.buy *= multiplier;
				item.buy /= divisor;
				item.sell *= Math.Min(multiplier, 4);
				item.sell /= divisor;

				item.buy = item.buy > 99999 ? 99999 : item.buy < 1 ? 1 : item.buy;
				item.sell = item.sell > 99999 ? 99999 : item.sell < 1 ? 1 : item.sell;
			}

			using (StreamWriter writer = new StreamWriter(Updater.MemoriaToMagiciteFile(directory, "MainData", "master", "weapon.csv")))
			using (CsvWriter csv = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
			{
				csv.WriteRecords(records);
			}
		}

		public int selectItem(Random r1, int minTier, int maxTier, bool highTierReduction, bool includeBonus, bool includeExclusive, int[] party)
		{
			List<int> selection = new List<int>();
			for (int i = minTier - 1; i <= maxTier - 1; i++)
			{
				int repetition = highTierReduction ? maxTier - i : 1;
				for (int j = 0; j < repetition; j++)
					selection.AddRange(tiers[i]);
			}

			if (!party.Contains(dkCecil)) selection = selection.Where(c => !dkCecilOnly.Contains(c)).ToList();
			if (!party.Contains(cecil)) selection = selection.Where(c => !pallyCecilOnly.Contains(c)).ToList();
			if (!party.Contains(kain)) selection = selection.Where(c => !kainOnly.Contains(c)).ToList();
			if (!party.Contains(rosa)) selection = selection.Where(c => !rosaOnly.Contains(c)).ToList();
			if (!party.Contains(rydia)) selection = selection.Where(c => !rydiaOnly.Contains(c)).ToList();
			if (!party.Contains(cid)) selection = selection.Where(c => !cidOnly.Contains(c)).ToList();
			if (!party.Contains(edward)) selection = selection.Where(c => !edwardOnly.Contains(c)).ToList();
			if (!party.Contains(yang)) selection = selection.Where(c => !yangOnly.Contains(c)).ToList();
			if (!party.Contains(palom)) selection = selection.Where(c => !palomOnly.Contains(c)).ToList();
			if (!party.Contains(porom)) selection = selection.Where(c => !poromOnly.Contains(c)).ToList();
			if (!party.Contains(edge)) selection = selection.Where(c => !edgeOnly.Contains(c)).ToList();
			if (!party.Contains(tellah)) selection = selection.Where(c => !tellahOnly.Contains(c)).ToList();
			if (!party.Contains(fusoya)) selection = selection.Where(c => !fusoyaOnly.Contains(c)).ToList();

			if (!party.Contains(cecil) && !party.Contains(kain)) selection = selection.Where(c => !cecilKain.Contains(c)).ToList();
			if (!party.Contains(cecil) && !party.Contains(kain) && !party.Contains(cid)) selection = selection.Where(c => !cecilKainCid.Contains(c)).ToList();
			if (!party.Contains(cecil) && !party.Contains(rydia) && !party.Contains(edward) && !party.Contains(rosa) && !party.Contains(palom) && !party.Contains(porom) && !party.Contains(cid)) selection = selection.Where(c => !bowUsers.Contains(c)).ToList();
			if (!party.Contains(cecil) && !party.Contains(kain) && !party.Contains(rydia) && !party.Contains(edward) && !party.Contains(palom)) selection = selection.Where(c => !knifeUsers.Contains(c)).ToList();
			if (!party.Contains(edge) && !party.Contains(yang)) selection = selection.Where(c => !clawUsers.Contains(c)).ToList();
			if (!party.Contains(rydia) && !party.Contains(tellah) && !party.Contains(palom) && !party.Contains(fusoya)) selection = selection.Where(c => !rodUsers.Contains(c)).ToList();
			if (!party.Contains(rosa) && !party.Contains(tellah) && !party.Contains(porom) && !party.Contains(fusoya)) selection = selection.Where(c => !staffUsers.Contains(c)).ToList();
			if (!party.Contains(edge) && !party.Contains(edward)) selection = selection.Where(c => !edgeEdward.Contains(c)).ToList();

			if (selection.Count == 0)
				return -1;

			bool bad = true;
			int finalSelection = -1;
			while (bad)
			{
				finalSelection = selection[r1.Next() % selection.Count];
				if (includeBonus || !bonusWeapons.Contains(finalSelection)) bad = false;
				if (includeExclusive || !fgExclusiveWeapons.Contains(finalSelection)) bad = false;
			}

			return finalSelection;
		}
	}
}
