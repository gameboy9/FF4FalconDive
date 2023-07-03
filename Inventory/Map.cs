using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF4FreeEnterprisePR.Inventory
{
    public class Map
    {
        private class singleMap
        {
            public int id { get; set; }
            public string map_name { get; set; }
            public string asset_name { get; set; }
            public string map_title { get; set; }
            public int script_id { get; set; }
            public int bgm_id { get; set; }
            public int reverb_flag { get; set; }
            public int environmental_se_id { get; set; }
            public int required_steps_min { get; set; }
            public int required_steps_max { get; set; }
            public int subtract_steps { get; set; }
            public int monster_set_id { get; set; }
            public int encount_area_grid_id { get; set; }
            public int encount_effect_id { get; set; }
            public int area_id { get; set; }
            public int loop_type { get; set; }
            public int moving_availability { get; set; }
            public int sleeping_availability { get; set; }
            public int save_availability { get; set; }
            public int floor { get; set; }
        }

        public Map(Random r1, string directory, int encNumerator, int encDenominator, bool noEncounters)
        {
            List<singleMap> allMaps;

            using (StreamReader reader = new(Path.Combine("csv", "map.csv")))
            using (CsvReader csv = new(reader, System.Globalization.CultureInfo.InvariantCulture))
                allMaps = csv.GetRecords<singleMap>().ToList();

            foreach (singleMap map in allMaps)
            {
                if (noEncounters)
                {
                    map.required_steps_max = 9999;
                    map.required_steps_min = 9999;
                    map.subtract_steps = 1;
                } else
                {
                    map.required_steps_min *= encNumerator;
                    if (map.required_steps_min != 0)
                        map.required_steps_min /= encDenominator;

                    map.required_steps_max *= encNumerator;
                    if (map.required_steps_max != 0)
                        map.required_steps_max /= encDenominator;
                }
            }

            using (StreamWriter writer = new(Path.Combine(directory, "map.csv")))
            using (CsvWriter csv = new(writer, System.Globalization.CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(allMaps);
            }
        }
    }
}
