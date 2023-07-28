using CsvHelper;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FF4FalconDive.Inventory
{
	public static class Messages
	{
		public class message
		{
			[Index(0)]
			public string id { get; set; }
			[Index(1)]
			public string msgString { get; set; }
		}

		public static void updateMessages(string directory, string seed, string flags, string checksum, bool sirensAllowed)
		{
			List<message> msgStrings;

			List<string> languages = new List<string>() { "de", "en", "es", "fr", "it", "ja", "ko", "pt", "ru", "th", "zhc", "zht" };

			foreach (string language in languages)
			{
				// Get mes_id_name from content.csv, then get accordingly name from whatever language you're using. (system_xx)
				using (StreamReader reader = new StreamReader(Path.Combine(directory, "story_mes_" + language + ".txt")))
				{
					CsvHelper.Configuration.CsvConfiguration config = new CsvHelper.Configuration.CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture);
					config.Delimiter = "\t";
					config.HasHeaderRecord = false;
					config.BadDataFound = null;

					using (CsvReader csv = new CsvReader(reader, config))
						msgStrings = csv.GetRecords<message>().ToList();

					msgStrings.Add(new message { id = "LUNAR_WHERE", msgString = "Where do you want to go?" });
					msgStrings.Add(new message { id = "LUNAR_GIANT", msgString = "Go To The Giant of Babel" });
					msgStrings.Add(new message { id = "LUNAR_MOON", msgString = "Go To The Moon" });
					msgStrings.Add(new message { id = "LUNAR_CANCEL", msgString = "Cancel" });
					msgStrings.Add(new message { id = "LUNAR_BLUE", msgString = "Go To The Blue Planet" });
					msgStrings.Add(new message { id = "LEGEND_REQ", msgString = "My smithing days are over! I won't forge another sword until I find the legendary ore, adamantite... and I need the Sword Of Legend too..." });
					msgStrings.Add(new message { id = "FE_NOT_ENOUGH_SHARDS", msgString = "You do not have the required number of shards to face Zeromus." });
					msgStrings.Add(new message { id = "FE_NOTHING_G1", msgString = "You now have found 3 Nothings!  Check out the Nothing Vending Machine in the Agora Laboratory (the telescope) to receive a possible key item!" });
					msgStrings.Add(new message { id = "FE_NOTHING_G2", msgString = "You now have found 5 Nothings!  Check out the Nothing Vending Machine in the Agora Laboratory (the telescope) for a special reward!" });
					msgStrings.Add(new message { id = "FE_ZEROMUS_GOOD", msgString = "You now have enough shards to face Zeromus!" });
					msgStrings.Add(new message { id = "FE_SIREN_GOOD", msgString = "You now have enough shards to use sirens!" });
					msgStrings.Add(new message { id = "FE_SIREN_BAD", msgString = sirensAllowed ? "You do not have the required number of shards to use sirens." : "Sirens are not allowed in this seed." });
					msgStrings.Add(new message { id = "FE_NM_L1_GOOD", msgString = "This is the Nothing Vending Machine.  You have received at least 3 Nothings!  Dispensing prize..." });
					msgStrings.Add(new message { id = "FE_NM_L2_GOOD", msgString = "This is the Nothing Vending Machine.  You have received at least 5 Nothings!  Dispensing your special prize..." });
					msgStrings.Add(new message { id = "FE_NM_L1_BAD", msgString = "This is the Nothing Vending Machine.  You need at least 3 nothings to receive a prize." });
					msgStrings.Add(new message { id = "FE_NM_L2_BAD", msgString = "This is the Nothing Vending Machine.  You need at least 5 nothings to receive a special prize." });
					msgStrings.Add(new message { id = "FE_NM_FINISH", msgString = "KABOOM!" });
					msgStrings.Add(new message { id = "FE_NM_DONE", msgString = "You see a note:  The Nothing Vending Machine is out of order." });
					msgStrings.Add(new message { id = "N105_C00_027_90_10", msgString = "Zzz... you get a Free Enterprise here... but the REAL Free Enterprise is at www.ff4fe.com! Zzz..." });

					msgStrings.Where(c => c.id == "N101_C02_027_01_01").Single().msgString = @"Welcome to Final Fantasy IV:  Falcon Dive!\nThis was based off of and inspired by Final Fantasy IV:  Free Enterprise by b0ardface!<P>" +
						@"\nRULES (including races):\nSet your configuration and initial party setup now.<P>" +
						@"\nBegin your timer as soon as you leave this room.  If you are in a race, leave this room when GO is called.<P>" +
						@"\nYou will see the Enterprise as soon as you leave town.<P>" +
						@"\nGood luck!";
					msgStrings.Where(c => c.id == "E0021_00_047_a_14").Single().msgString = "Will you help us to defend Fabul?";
					msgStrings.Where(c => c.id == "E0008_00_067_a_01").Single().msgString = "You may not enter until you acquire the Bomb Ring...";
					msgStrings.Where(c => c.id == "E0039_00_098_a_01").Single().msgString = "Go to the Tower Of Zot?";
					msgStrings.Where(c => c.id == "E0039_00_098_a_02").Single().msgString = "You may not enter until you have the Earth Crystal.";
					msgStrings.Where(c => c.id == "E0039_00_098_a_03").Single().msgString = "I can take you to the Tower of Zot... if you have the Earth Crystal...";
					msgStrings.Where(c => c.id == "E0036_01_192_a_03").Single().msgString = "Magnetic field is in effect.";
					msgStrings.Where(c => c.id == "E0129_00_133_a_01").Single().msgString = "I can take you to the Zeromus fight!  Are you interested?";
					msgStrings.Where(c => c.id == "E0120_01_00_143_a_04").Single().msgString = "Do you possess the strength and courage to challenge me?";
					msgStrings.Where(c => c.id == "E0120_03_00_143_a_01").Single().msgString = "A strong spirit is required to steer one's powers toward righteousness.<P>\\nWill you test your spirit against mine?";
					msgStrings.Where(c => c.id == "P013_C00_ 274_02_01").Single().msgString = "Trapdoor?";

					// Credits
					msgStrings.Where(c => c.id == "E0030_00_353_a_01").Single().msgString = @"Congratulations!\nYou have completed a seed of Final Fantasy IV:  Falcon Dive!\n";
					msgStrings.Where(c => c.id == "E0030_00_353_a_02").Single().msgString = @"";
					msgStrings.Where(c => c.id == "E0030_00_353_a_03").Single().msgString = @"Creator, Design, and Programming\ngameboyf9\n\n";
					msgStrings.Where(c => c.id == "E0030_00_353_a_04").Single().msgString = @"Thanks to the Beta Helpers\n";
					msgStrings.Where(c => c.id == "E0030_00_353_a_05").Single().msgString = @"Antidale\nEngidave\nFleury14\nInfinious\nInven\nTybalt\nSyconawt\nwarlink05\n\n";
					msgStrings.Where(c => c.id == "E0030_00_353_a_06").Single().msgString = @"Special Thanks\n";
					msgStrings.Where(c => c.id == "E0030_00_353_a_07").Single().msgString = @"mcgrew\nAlbeoris\nSchalaKitty\nSilvris\n\n";
					msgStrings.Where(c => c.id == "E0074_06_354_a_01").Single().msgString = @"Based off of and inspired by\nFinal Fantasy IV:  Free Enterprise\nwww.ff4fe.com\n";
					msgStrings.Where(c => c.id == "E0074_06_354_a_02").Single().msgString = @"Created by b0ardface\n";
					msgStrings.Where(c => c.id == "E0074_06_354_a_03").Single().msgString = @"Additional Special Thanks to\nThe Free Enterprise Community\n";
					msgStrings.Where(c => c.id == "E0074_06_354_a_04").Single().msgString = @"Thank you for playing!\n";
					msgStrings.Where(c => c.id == "E0074_06_354_a_05").Single().msgString = @"";
					msgStrings.Where(c => c.id == "E0074_06_354_a_06").Single().msgString = @"";


					using (StreamWriter writer = new StreamWriter(Path.Combine(directory, "story_mes_" + language + ".txt")))
					using (CsvWriter csv = new CsvWriter(writer, config))
					{
						csv.WriteRecords(msgStrings);
					}
				}

				using (StreamReader reader = new StreamReader(Path.Combine(directory, "system_" + language + ".txt")))
				{
					CsvHelper.Configuration.CsvConfiguration config = new CsvHelper.Configuration.CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture);
					config.Delimiter = "\t";
					config.HasHeaderRecord = false;
					config.BadDataFound = null;

					using (CsvReader csv = new CsvReader(reader, config))
						msgStrings = csv.GetRecords<message>().ToList();

					msgStrings.Where(c => c.id == "MSG_SYSTEM_253").Single().msgString = @"©SQUARE ENIX CO., LTD. All Rights Reserved.  Final Fantasy IV:  Falcon Dive by gameboyf9\nSeed - " + seed + @"\nFlags - " + flags + @"\nChecksum - " + checksum;

					using (StreamWriter writer = new StreamWriter(Path.Combine(directory, "system_" + language + ".txt")))
					using (CsvWriter csv = new CsvWriter(writer, config))
					{
						csv.WriteRecords(msgStrings);
					}
				}
			}
		}
	}
}
