using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF4FreeEnterprisePR.Inventory
{
	public static class Updater
	{
		public static void update(string directory, bool zAtFalcon, bool zAtOrdeals)
		{
			// Iterate through the map directory and copy the files into the other map directory...
			foreach (string jsonFile in Directory.GetFiles(Path.Combine("Res"), "*.*", SearchOption.AllDirectories))
				File.Copy(jsonFile, Path.Combine(directory, jsonFile), true);
			foreach (string jsonFile in Directory.GetFiles(Path.Combine("Data"), "*.*", SearchOption.AllDirectories))
				File.Copy(jsonFile, Path.Combine(directory, jsonFile), true);

			if (zAtFalcon)
			{
				foreach (string jsonFile in Directory.GetFiles(Path.Combine("AltScenarios", "Z At Falcon"), "*.*", SearchOption.AllDirectories))
					File.Copy(jsonFile, Path.Combine(directory, "Res", "Map", jsonFile.Replace(Path.Combine("AltScenarios", "Z At Falcon") + "\\", "")), true);
			}

			if (zAtOrdeals)
			{
				foreach (string jsonFile in Directory.GetFiles(Path.Combine("AltScenarios", "Z At Ordeals"), "*.*", SearchOption.AllDirectories))
					File.Copy(jsonFile, Path.Combine(directory, "Res", "Map", jsonFile.Replace(Path.Combine("AltScenarios", "Z At Ordeals") + "\\", "")), true);
			}
		}
	}
}
