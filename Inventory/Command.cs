using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF4FreeEnterprisePR.Inventory
{
    public class Command
    {
        private class singleCommand
        {
            public int id { get; set; }
            public int command_lv { get; set; }
            public string mes_id_name { get; set; }
            public string mes_id_name2 { get; set; }
            public string mes_id_description { get; set; }
            public int consume_type { get; set; }
            public int command_type { get; set; }
            public int execution_type { get; set; }
            public int list_type { get; set; }
            public int number_select { get; set; }
            public int ability_group_id { get; set; }
            public int ability_id { get; set; }
            public int job_group_id { get; set; }
            public int condition_group_id { get; set; }
            public int release_type { get; set; }
            public int release_value1 { get; set; }
            public int release_value2 { get; set; }
            public int command_wait { get; set; }
            public int adjust_strength { get; set; }
            public int adjust_vitality { get; set; }
            public int adjust_agility { get; set; }
            public int adjust_magic { get; set; }
            public int auto_execution_ability_id { get; set; }
        }

        public void adjustTwinCast(int position1, int position2, string directory)
        {
            List<singleCommand> records = new();
            using (StreamReader reader = new StreamReader(Path.Combine("csv", "command.csv")))
            using (CsvReader csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
                records = csv.GetRecords<singleCommand>().ToList();

            singleCommand twinCast = records.Where(c => c.id == 18).Single();
            twinCast.release_value1 = position1;
            twinCast.release_value2 = position2;

            using (StreamWriter writer = new StreamWriter(Path.Combine(directory, "command.csv")))
            using (CsvWriter csv = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(records);
            }
        }
    }
}
