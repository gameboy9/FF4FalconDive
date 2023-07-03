using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FF4FreeEnterprisePR.Inventory.EntityJSON;

namespace FF4FreeEnterprisePR.Inventory
{
	class Areas
	{
		class TransitionPoints
		{
			public string path;
			public int gotoMapId;
			public int orderId;
			public int defaultMapId;
			public int defaultPointId;
		}

		public Areas(string directory, int numAreas)
		{
			List<List<TransitionPoints>> areaEntryScripts = new List<List<TransitionPoints>>
			{
				// Baron shop and start of Mist Cave are constant
				// Area 1 -> 2
				new List<TransitionPoints> {
					new TransitionPoints { path = Path.Combine(directory, "Map_30011", "Map_30011_1", "entity_default.json"), orderId = 1, gotoMapId = 2, defaultMapId = 67, defaultPointId = 1 }, // Mist Cave
					new TransitionPoints { path = Path.Combine(directory, "Map_20070", "Map_20070_1", "entity_default.json"), orderId = 2, gotoMapId = 1, defaultMapId = 162, defaultPointId = 1 }, // Mist
					new TransitionPoints { path = Path.Combine(directory, "Map_20070", "Map_20070_1", "entity_default.json"), orderId = 3, gotoMapId = 2, defaultMapId = 163, defaultPointId = 1 }, // Mist
					new TransitionPoints { path = Path.Combine(directory, "Map_30021", "Map_30021_1", "entity_default.json"), orderId = 4, gotoMapId = 1, defaultMapId = 67, defaultPointId = 2 }, // U. Waterway
				},

				// Area 2 -> 3
				new List<TransitionPoints> {
					new TransitionPoints { path = Path.Combine(directory, "Map_30021", "Map_30021_10", "entity_default.json"), orderId = 1, gotoMapId = 2, defaultMapId = 33, defaultPointId = 1 }, // U. Waterway
					new TransitionPoints { path = Path.Combine(directory, "Map_20031", "Map_20031_1", "entity_default.json"), orderId = 2, gotoMapId = 1, defaultMapId = 172, defaultPointId = 1 }, // Kaipo-1
					new TransitionPoints { path = Path.Combine(directory, "Map_20031", "Map_20031_1", "entity_default.json"), orderId = 3, gotoMapId = 2, defaultMapId = 178, defaultPointId = 1 }, // Kaipo-1
					new TransitionPoints { path = Path.Combine(directory, "Map_30040", "Map_30040_1", "entity_default.json"), orderId = 4, gotoMapId = 1, defaultMapId = 33, defaultPointId = 2 }, // Hobs
				},

				// Area 3 -> 4
				new List<TransitionPoints> {
					new TransitionPoints { path = Path.Combine(directory, "Map_30070", "Map_30070_1", "entity_default.json"), orderId = 1, gotoMapId = 2, defaultMapId = 35, defaultPointId = 1 }, // Hobs
					new TransitionPoints { path = Path.Combine(directory, "Map_20031", "Map_20031_3", "entity_default.json"), orderId = 2, gotoMapId = 1, defaultMapId = 181, defaultPointId = 1 }, // Kaipo-2
					new TransitionPoints { path = Path.Combine(directory, "Map_20031", "Map_20031_3", "entity_default.json"), orderId = 3, gotoMapId = 2, defaultMapId = 182, defaultPointId = 1 }, // Kaipo-2
					new TransitionPoints { path = Path.Combine(directory, "Map_30080", "Map_30080_1", "entity_default.json"), orderId = 4, gotoMapId = 1, defaultMapId = 35, defaultPointId = 2 }, // Ordeals
					new TransitionPoints { path = Path.Combine(directory, "Map_30080", "Map_30080_1", "entity_default.json"), orderId = 4, gotoMapId = 2, defaultMapId = 35, defaultPointId = 2 }, // Ordeals
				},

				// Ordeals-out is done by event.  Everybody has to go through Ordeals anyway...
				// Area 4 -> 5
				new List<TransitionPoints> {
					new TransitionPoints { path = Path.Combine(directory, "Map_20031", "Map_20031_4", "entity_default.json"), orderId = 2, gotoMapId = 1, defaultMapId = 185, defaultPointId = 1 }, // Kaipo-3
					new TransitionPoints { path = Path.Combine(directory, "Map_20031", "Map_20031_4", "entity_default.json"), orderId = 3, gotoMapId = 2, defaultMapId = 186, defaultPointId = 1 }, // Kaipo-3
					new TransitionPoints { path = Path.Combine(directory, "Map_30121", "Map_30121_1", "entity_default.json"), orderId = 4, gotoMapId = 1, defaultMapId = 36, defaultPointId = 2 }, // Baron Waterway
				},

				// Area 5 -> 6
				new List<TransitionPoints> {
					new TransitionPoints { path = Path.Combine(directory, "Map_20011", "Map_20011_5", "entity_default.json"), orderId = 1, gotoMapId = 2, defaultMapId = 58, defaultPointId = 1 }, // Baron Waterway
					new TransitionPoints { path = Path.Combine(directory, "Map_20061", "Map_20061_1", "entity_default.json"), orderId = 2, gotoMapId = 1, defaultMapId = 5, defaultPointId = 1 }, // Mysidia-1
					new TransitionPoints { path = Path.Combine(directory, "Map_20061", "Map_20061_1", "entity_default.json"), orderId = 3, gotoMapId = 1, defaultMapId = 199, defaultPointId = 2 }, // Mysidia-1
					new TransitionPoints { path = Path.Combine(directory, "Map_30131", "Map_30131_8", "entity_default.json"), orderId = 4, gotoMapId = 1, defaultMapId = 58, defaultPointId = 2 }, // Magnes
				},

				// Area 6 -> 7
				new List<TransitionPoints> {
					new TransitionPoints { path = Path.Combine(directory, "Map_30131", "Map_30131_1", "entity_default.json"), orderId = 1, gotoMapId = 1, defaultMapId = 59, defaultPointId = 1 }, // Magnes
					new TransitionPoints { path = Path.Combine(directory, "Map_20061", "Map_20061_2", "entity_default.json"), orderId = 2, gotoMapId = 1, defaultMapId = 192, defaultPointId = 1 }, // Mysidia-2
					new TransitionPoints { path = Path.Combine(directory, "Map_20061", "Map_20061_2", "entity_default.json"), orderId = 3, gotoMapId = 1, defaultMapId = 202, defaultPointId = 1 }, // Mysidia-2
					new TransitionPoints { path = Path.Combine(directory, "Map_30141", "Map_30141_1", "ev_e_0040_2.json"), orderId = 4, gotoMapId = 1, defaultMapId = 59, defaultPointId = 2 }, // Zot
				},

				// Area 7 -> 8
				new List<TransitionPoints> {
					new TransitionPoints { path = Path.Combine(directory, "Map_30141", "Map_30141_5", "entity_default.json"), orderId = 1, gotoMapId = 4, defaultMapId = 118, defaultPointId = 1 }, // Zot
					new TransitionPoints { path = Path.Combine(directory, "Map_20150", "Map_20150", "entity_default.json"), orderId = 2, gotoMapId = 1, defaultMapId = 206, defaultPointId = 1 }, // Dwarf Castle
					new TransitionPoints { path = Path.Combine(directory, "Map_20150", "Map_20150", "entity_default.json"), orderId = 2, gotoMapId = 2, defaultMapId = 206, defaultPointId = 1 }, // Dwarf Castle
					new TransitionPoints { path = Path.Combine(directory, "Map_20150", "Map_20150", "entity_default.json"), orderId = 2, gotoMapId = 3, defaultMapId = 206, defaultPointId = 1 }, // Dwarf Castle
					new TransitionPoints { path = Path.Combine(directory, "Map_20151", "Map_20151_10", "entity_default.json"), orderId = 3, gotoMapId = 1, defaultMapId = 210, defaultPointId = 1 }, // Dwarf Castle
					new TransitionPoints { path = Path.Combine(directory, "Map_30151", "Map_30151_1", "entity_default.json"), orderId = 4, gotoMapId = 1, defaultMapId = 128, defaultPointId = 1 }, // Lower Babil
				},

				// Area 8 -> 9
				new List<TransitionPoints> {
					new TransitionPoints { path = Path.Combine(directory, "Map_30151", "Map_30151_14", "entity_default.json"), orderId = 1, gotoMapId = 1, defaultMapId = 226, defaultPointId = 1 }, // Lower Babil
					new TransitionPoints { path = Path.Combine(directory, "Map_30171", "Map_30161_1", "entity_default.json"), orderId = 4, gotoMapId = 1, defaultMapId = 223, defaultPointId = 1 }, // Eblan Cave -> Upper Babil
				},

				// Area 9 -> 10
				new List<TransitionPoints> {
					new TransitionPoints { path = Path.Combine(directory, "Map_30171", "Map_30171_6", "entity_default.json"), orderId = 1, gotoMapId = 1, defaultMapId = 61, defaultPointId = 1 }, // Upper Babil
					new TransitionPoints { path = Path.Combine(directory, "Map_20061", "Map_20061_4", "entity_default.json"), orderId = 2, gotoMapId = 1, defaultMapId = 239, defaultPointId = 1 }, // Mysidia-3
					new TransitionPoints { path = Path.Combine(directory, "Map_20061", "Map_20061_4", "entity_default.json"), orderId = 3, gotoMapId = 2, defaultMapId = 265, defaultPointId = 1 }, // Mysidia-3
					new TransitionPoints { path = Path.Combine(directory, "Map_30191", "Map_30191_1", "entity_default.json"), orderId = 4, gotoMapId = 1, defaultMapId = 61, defaultPointId = 2 }, // Land Of Summoned Monsters
				},

				// Area 10 -> 11
				new List<TransitionPoints> {
					new TransitionPoints { path = Path.Combine(directory, "Map_30221", "Map_30221_2", "entity_default.json"), orderId = 1, gotoMapId = 1, defaultMapId = 153, defaultPointId = 1 }, // Bahamut
					new TransitionPoints { path = Path.Combine(directory, "Map_20201", "Map_20201_1", "entity_default.json"), orderId = 2, gotoMapId = 1, defaultMapId = 294, defaultPointId = 1 } // Crystal Palace
				}
				// Crystal Palace 2F and Lunar Subterranne are constant.
			};

			if (numAreas == 11)
			{
				reMap(areaEntryScripts[0], new List<newPoint> { new newPoint(67, 1), new newPoint(162, 1), new newPoint(163, 1), new newPoint(67, 2) });
				reMap(areaEntryScripts[1], new List<newPoint> { new newPoint(33, 1), new newPoint(172, 1), new newPoint(178, 1), new newPoint(33, 2) });
				reMap(areaEntryScripts[2], new List<newPoint> { new newPoint(35, 1), new newPoint(181, 1), new newPoint(182, 1), new newPoint(35, 2) });
				reMap(areaEntryScripts[3], new List<newPoint> { new newPoint(-1, 1), new newPoint(185, 1), new newPoint(186, 1), new newPoint(36, 2) });
				reMap(areaEntryScripts[4], new List<newPoint> { new newPoint(58, 1), new newPoint(9, 1), new newPoint(199, 3), new newPoint(58, 2) });
				reMap(areaEntryScripts[5], new List<newPoint> { new newPoint(59, 1), new newPoint(192, 1), new newPoint(202, 1), new newPoint(59, 2) });
				reMap(areaEntryScripts[6], new List<newPoint> { new newPoint(118, 1), new newPoint(206, 1), new newPoint(210, 1), new newPoint(128, 1) });
				reMap(areaEntryScripts[7], new List<newPoint> { new newPoint(226, 1), new newPoint(-1, 1), new newPoint(-1, 1), new newPoint(223, 1) });
				reMap(areaEntryScripts[8], new List<newPoint> { new newPoint(61, 1), new newPoint(239, 1), new newPoint(265, 1), new newPoint(61, 2) });
				reMap(areaEntryScripts[9], new List<newPoint> { new newPoint(153, 1), new newPoint(294, 1), new newPoint(-1, 1), new newPoint(-1, 2) });
			}
		}

		class newPoint
		{
			public int mapId;
			public int pointId;

			public newPoint(int pMap, int pPoint) { mapId = pMap; pointId = pPoint; }
		}

		private void reMap(List<TransitionPoints> scripts, List<newPoint> newPoints)
		{
			int gotoId = 0;
			foreach (TransitionPoints tp in scripts)
			{
				string json = File.ReadAllText(tp.path);
				EntityJSON jEvents = JsonConvert.DeserializeObject<EntityJSON>(json);
				foreach (var layer in jEvents.layers)
					foreach (var sObject in layer.objects)
					{
						if (sObject.name == "GotoMap")
						{
							gotoId++;
							if (gotoId == tp.gotoMapId)
							{
								Property1 mapProp = sObject.properties.Where(c => c.name == "map_id").Single();
								Property1 pointProp = sObject.properties.Where(c => c.name == "point_id").Single();
								mapProp.value = newPoints[tp.orderId - 1];
								pointProp.value = newPoints[tp.orderId - 1];
							}
						}
					}
			}
		}
	}
}
