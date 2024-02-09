using FF4FreeEnterprisePR.Inventory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static FF4FreeEnterprisePR.Common.Common;

namespace FF4FreeEnterprisePR.Randomize
{
	public class Shops
	{
		// Let's const the stores
		const int baronWeapon = 1; // Baron Key
		const int baronArmor = 2; // Baron Key
		const int baronItem = 3;
		const int mistWeapon = 4;
		const int mistArmor = 5;
		const int kaipoWeapon = 6;
		const int kaipoArmor = 7;
		const int kaipoItem = 8;
		const int fabulArmorWeapon = 9;
		const int fabulItem = 10;
		const int mysidiaWeapon = 11;
		const int mysidiaArmor = 12;
		const int mysidiaItem = 13;
		const int agartWeapon = 14;
		const int agartArmor = 15;
		const int agartItem = 16;
		const int mythrilWeapon = 17;
		const int mythrilArmor = 18;
		const int mythrilItem = 19;
		const int troiaWeapon = 20;
		const int troiaArmor = 21;
		const int troiaItem = 22;
		const int troiaPub = 23;
		const int eblanWeapons = 24;
		const int eblanArmor = 25;
		const int eblanItem = 26;
		const int dwarfWeapons = 27;
		const int dwarfArmor = 28;
		const int dwarfItem1 = 29;
		const int dwarfItem2 = 30;
		const int tomraWeapons = 31;
		const int tomraArmor = 32;
		const int tomraItem1 = 33;
		const int tomraItem2 = 34;
		const int feymarchWeapon = 35;
		const int feymarchArmor = 36;
		const int feymarchItem1 = 37;
		const int feymarchItem2 = 38;
		const int smithyWeaponArmor = 39;
		const int hummingwayItem = 40;
		const int giantItem = 41;

		const int storeWeapon = 1;
		const int storeArmor = 0;
		const int storeItem = 2;
		const int storeWeaponArmor = 3;

		private class store
		{
			public int id { get; set; }
			public int type { get; set; }
			public int stdMinTier { get; set; }
			public int stdMaxTier { get; set; }
		}


		List<store> allStores = new List<store>()
		{
			new store { id = baronWeapon, stdMinTier = 3, stdMaxTier = 6, type = storeWeapon },
			new store { id = baronArmor, stdMinTier = 3, stdMaxTier = 6, type = storeArmor },
			new store { id = baronItem, stdMinTier = 1, stdMaxTier = 4, type = storeItem },
			new store { id = mistWeapon, stdMinTier = 3, stdMaxTier = 5, type = storeWeapon },
			new store { id = mistArmor, stdMinTier = 3, stdMaxTier = 5, type = storeArmor },
			new store { id = kaipoWeapon, stdMinTier = 1, stdMaxTier = 4, type = storeWeapon },
			new store { id = kaipoArmor, stdMinTier = 1, stdMaxTier = 4, type = storeArmor },
			new store { id = kaipoItem, stdMinTier = 1, stdMaxTier = 4, type = storeItem },
			new store { id = fabulArmorWeapon, stdMinTier = 1, stdMaxTier = 4, type = storeWeaponArmor },
			new store { id = fabulItem, stdMinTier = 1, stdMaxTier = 4, type = storeItem },
			new store { id = mysidiaWeapon, stdMinTier = 1, stdMaxTier = 4, type = storeWeapon },
			new store { id = mysidiaArmor, stdMinTier = 1, stdMaxTier = 4, type = storeArmor },
			new store { id = mysidiaItem, stdMinTier = 1, stdMaxTier = 4, type = storeItem },
			new store { id = agartWeapon, stdMinTier = 1, stdMaxTier = 4, type = storeWeapon },
			new store { id = agartArmor, stdMinTier = 1, stdMaxTier = 4, type = storeArmor },
			new store { id = agartItem, stdMinTier = 1, stdMaxTier = 4, type = storeItem },
			new store { id = mythrilWeapon, stdMinTier = 1, stdMaxTier = 5, type = storeWeapon },
			new store { id = mythrilArmor, stdMinTier = 1, stdMaxTier = 5, type = storeArmor },
			new store { id = mythrilItem, stdMinTier = 1, stdMaxTier = 5, type = storeItem },
			new store { id = troiaWeapon, stdMinTier = 1, stdMaxTier = 4, type = storeWeapon },
			new store { id = troiaArmor, stdMinTier = 1, stdMaxTier = 4, type = storeArmor },
			new store { id = troiaItem, stdMinTier = 1, stdMaxTier = 4, type = storeItem },
			new store { id = troiaPub, stdMinTier = 1, stdMaxTier = 4, type = storeItem },
			new store { id = eblanWeapons, stdMinTier = 4, stdMaxTier = 6, type = storeWeapon },
			new store { id = eblanArmor, stdMinTier = 4, stdMaxTier = 6, type = storeArmor },
			new store { id = eblanItem, stdMinTier = 4, stdMaxTier = 6, type = storeItem },
			new store { id = dwarfWeapons, stdMinTier = 4, stdMaxTier = 6, type = storeWeapon },
			new store { id = dwarfArmor, stdMinTier = 4, stdMaxTier = 6, type = storeArmor },
			new store { id = dwarfItem1, stdMinTier = 4, stdMaxTier = 6, type = storeItem },
			new store { id = dwarfItem2, stdMinTier = 4, stdMaxTier = 6, type = storeItem },
			new store { id = tomraWeapons, stdMinTier = 4, stdMaxTier = 6, type = storeWeapon },
			new store { id = tomraArmor, stdMinTier = 4, stdMaxTier = 6, type = storeArmor },
			new store { id = tomraItem1, stdMinTier = 4, stdMaxTier = 6, type = storeItem },
			new store { id = tomraItem2, stdMinTier = 4, stdMaxTier = 6, type = storeItem },
			new store { id = feymarchWeapon, stdMinTier = 4, stdMaxTier = 7, type = storeWeapon },
			new store { id = feymarchArmor, stdMinTier = 4, stdMaxTier = 7, type = storeArmor },
			new store { id = feymarchItem1, stdMinTier = 4, stdMaxTier = 7, type = storeItem },
			new store { id = feymarchItem2, stdMinTier = 4, stdMaxTier = 7, type = storeItem },
			new store { id = smithyWeaponArmor, stdMinTier = 6, stdMaxTier = 8, type = storeWeaponArmor },
			new store { id = hummingwayItem, stdMinTier = 4, stdMaxTier = 7, type = storeItem },
			new store { id = giantItem, stdMinTier = 5, stdMaxTier = 8, type = storeItem }
		};

		private class shopItem 
		{
			public int id;
			public int content_id; // Item
			public int group_id; // Store #
			public int coefficient = 0; // Inn/House of Healing cost
			public int purchase_limit = 0; // 0 = unlimited
		}

		public Shops(Random r1, int randoLevel, int freqLevel, bool noJ, bool noSuper, string directory, bool includeBonus, bool includeFGExclusive, int multiplier, int divisor, int[] party)
		{
			List<shopItem> shopDB = new List<shopItem>();
			List<shopItem> shopWorking = new List<shopItem>();
			int id = 1;

			foreach (store indStore in allStores)
			{
				shopWorking = new List<shopItem>();
				// Alternate between weapons, armor, and item stores, so each place has at least one of each.
				int freq = (freqLevel == 0 ? 4 : freqLevel == 1 ? 8 : freqLevel == 2 ? 12 : freqLevel == 3 ? 16 : 20);
				for (int j = 0; j <= freq; j++)
				{
					shopItem newItem = new shopItem();
					newItem.group_id = indStore.id;
					newItem.id = id;
					int minTier = randoLevel == 0 ? indStore.stdMinTier + 1 : randoLevel == 1 ? indStore.stdMinTier : randoLevel == 2 ? indStore.stdMinTier - 1 : 1;
					int maxTier = randoLevel == 0 ? indStore.stdMaxTier + 1 : randoLevel == 1 ? indStore.stdMaxTier : randoLevel == 2 ? indStore.stdMaxTier - 1 : noSuper ? 8 : 9;
					minTier = Math.Max(minTier, 1);
					minTier = Math.Min(minTier, 9);
					maxTier = Math.Max(maxTier, 1);
					maxTier = Math.Min(maxTier, 9);


					// Alternate between weapons, armor, and item stores, so each place has at least one of each.
					switch (indStore.type)
					{
						case storeArmor:
							newItem.content_id = new Inventory.Armor().selectItem(r1, minTier, maxTier, true, includeBonus, includeFGExclusive, party);
							break;
						case storeWeapon:
							newItem.content_id = new Inventory.Weapons().selectItem(r1, minTier, maxTier, true, includeBonus, includeFGExclusive, party);
							break;
						case storeItem:
							newItem.content_id = new Inventory.Items().selectItem(r1, minTier, maxTier, true, noJ);
							break;
						case storeWeaponArmor:
							// Alternate between weapons and armor
							if (j % 2 == 0)
								newItem.content_id = new Inventory.Weapons().selectItem(r1, minTier, maxTier, true, includeBonus, includeFGExclusive, party);
							else
								newItem.content_id = new Inventory.Armor().selectItem(r1, minTier, maxTier, true, includeBonus, includeFGExclusive, party);
							break;
					}

					// Do not add if an item couldn't be found or if it's a duplicate.
					if (newItem.content_id != -1 && shopWorking.Where(c => c.content_id == newItem.content_id).Count() == 0)
					{
						shopWorking.Add(newItem);
						id++;
					}
				}

				shopDB.AddRange(shopWorking.OrderBy(c => c.content_id));
			}

			using (StreamWriter sw = new StreamWriter(Updater.MemoriaToMagiciteFile(directory, "MainData", "master", "product.csv")))
			{
				sw.WriteLine("id,content_id,group_id,coefficient,purchase_limit");
				int finalID = 0;
				foreach (shopItem si in shopDB)
				{
					finalID++;
					sw.WriteLine(finalID + "," + si.content_id + "," + si.group_id + "," + si.coefficient + "," + si.purchase_limit);
				}

				// Add inn prices
				int[] prices = new int[] { 50, 50, 50, 100, 200, 50, 500, 400, 700, 600, 300, 1200 };
				for (int i = 0; i < 12; i++)
				{
					finalID++;
					sw.WriteLine(finalID + ",0," + (i + 101).ToString() + "," + (prices[i] * multiplier / divisor).ToString() + ",0"); finalID++;
				}
			}
		}
	}
}
