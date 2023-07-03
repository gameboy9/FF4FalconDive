using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF4FreeEnterprisePR.Inventory
{
	public class Updater
	{
		public Updater(string directory)
		{
			// Iterate through the map directory and copy the files into the other map directory...
			foreach (string jsonFile in Directory.GetFiles(Path.Combine("Res"), "*.*", SearchOption.AllDirectories))
				File.Copy(jsonFile, Path.Combine(directory, jsonFile), true);
			foreach (string jsonFile in Directory.GetFiles(Path.Combine("Data"), "*.*", SearchOption.AllDirectories))
				File.Copy(jsonFile, Path.Combine(directory, jsonFile), true);
		}
	}
}
