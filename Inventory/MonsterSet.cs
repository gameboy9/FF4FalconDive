using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF4FreeEnterprisePR.Inventory
{
    public class MonsterSet
    {
        private class singleSet
        {
            public int id { get; set; }
            public int monster_set1 { get; set; }
            public int monster_set1_rate { get; set; }
            public int monster_set2 { get; set; }
            public int monster_set2_rate { get; set; }
            public int monster_set3 { get; set; }
            public int monster_set3_rate { get; set; }
            public int monster_set4 { get; set; }
            public int monster_set4_rate { get; set; }
            public int monster_set5 { get; set; }
            public int monster_set5_rate { get; set; }
            public int monster_set6 { get; set; }
            public int monster_set6_rate { get; set; }
            public int monster_set7 { get; set; }
            public int monster_set7_rate { get; set; }
            public int monster_set8 { get; set; }
            public int monster_set8_rate { get; set; }
            public int monster_set9 { get; set; }
            public int monster_set9_rate { get; set; }
            public int monster_set10 { get; set; }
            public int monster_set10_rate { get; set; }
            public int monster_set11 { get; set; }
            public int monster_set11_rate { get; set; }
            public int monster_set12 { get; set; }
            public int monster_set12_rate { get; set; }
            public int monster_set13 { get; set; }
            public int monster_set13_rate { get; set; }
            public int monster_set14 { get; set; }
            public int monster_set14_rate { get; set; }
            public int monster_set15 { get; set; }
            public int monster_set15_rate { get; set; }
            public int monster_set16 { get; set; }
            public int monster_set16_rate { get; set; }
        }

        public void escapeAdjust(bool allowRun, string directory)
        {
            List<singleSet> records = new();
            using (StreamReader reader = new StreamReader(Path.Combine("csv", "monster_set.csv")))
            using (CsvReader csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
                records = csv.GetRecords<singleSet>().ToList();

            foreach(singleSet record in records)
            {
                record.monster_set1 += allowRun ? 800 : 0;
                record.monster_set2 += allowRun ? 800 : 0;
                record.monster_set3 += allowRun ? 800 : 0;
                record.monster_set4 += allowRun ? 800 : 0;
                record.monster_set5 += allowRun ? 800 : 0;
                record.monster_set6 += allowRun ? 800 : 0;
                record.monster_set7 += allowRun ? 800 : 0;
                record.monster_set8 += allowRun ? 800 : 0;
            }

            using (StreamWriter writer = new StreamWriter(Path.Combine(directory, "monster_set.csv")))
            using (CsvWriter csv = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(records);
            }
        }
    }
}
