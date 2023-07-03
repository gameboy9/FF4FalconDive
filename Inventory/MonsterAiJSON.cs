using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF4FreeEnterprisePR.Inventory
{
    public class MonsterAiJSON
    {
        public string Name { get; set; }
        public Title Title1 { get; set; }
        public int EntryPoint { get; set; }
        public int Count { get; set; }
        public Mnemonic[] Mnemonics { get; set; }

        public class Title
        {
            public string main { get; set; }
        }

        public class Mnemonic
        {
            public string label { get; set; }
            public string mnemonic { get; set; }
            public string comment { get; set; }
            public Operands operands { get; set; }
        }

        public class Operands
        {
            public int[] iValues { get; set; }
            public int[] rValues { get; set; }
        }
    }
}
