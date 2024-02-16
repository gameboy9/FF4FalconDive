using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF4FreeEnterprisePR.Inventory
{
	public static class Updater
	{
		public static void update(string mainDirectory, string directory, bool zAtFalcon, bool zAtOrdeals)
		{
			if (!Path.Exists(Path.Combine(mainDirectory, "BepInEx"))) 
			{
				ZipFile.ExtractToDirectory("install.zip", mainDirectory, true);
			}

			// Iterate through the map directory and copy the files into the other map directory...
			foreach (string jsonFile in Directory.GetDirectories(Path.Combine("Res"), "*.*", SearchOption.AllDirectories))
			{
				if (jsonFile.Count(f => f == '\\') != 2) continue;

				MemoriaToMagiciteCopy(directory, jsonFile, jsonFile.Contains("Map") ? "Map" : "MonsterAI", jsonFile.Contains("Map") ? Path.GetFileName(jsonFile) : "monster_ai");
			}

			foreach (string jsonFile in Directory.GetDirectories(Path.Combine("Data"), "*.*", SearchOption.AllDirectories))
				MemoriaToMagiciteCopy(directory, jsonFile, jsonFile.Contains("Message") ? "Message" : "MainData", jsonFile.Contains("Message") ? "message" : "master");

			if (zAtFalcon)
			{
				foreach (string jsonFile in Directory.GetFiles(Path.Combine("AltScenarios", "Z At Falcon"), "*.*", SearchOption.AllDirectories))
				{
					string topKey = jsonFile.Replace(Path.Combine("AltScenarios", "Z At Falcon") + "\\", "");
					string fileToUse = topKey.Substring(topKey.IndexOf('\\') + 1);
					topKey = topKey.Substring(0, topKey.IndexOf('\\'));
					File.Copy(jsonFile, MemoriaToMagiciteFile(directory, "Map", topKey, fileToUse), true);
				}
			}

			if (zAtOrdeals)
			{
				foreach (string jsonFile in Directory.GetFiles(Path.Combine("AltScenarios", "Z At Ordeals"), "*.*", SearchOption.AllDirectories))
				{
					string topKey = jsonFile.Replace(Path.Combine("AltScenarios", "Z At Ordeals") + "\\", "");
					string fileToUse = topKey.Substring(topKey.IndexOf('\\') + 1);
					topKey = topKey.Substring(0, topKey.IndexOf('\\'));
					File.Copy(jsonFile, MemoriaToMagiciteFile(directory, "Map", topKey, fileToUse), true);
				}
			}
		}

		public static void MemoriaToMagiciteCopy(string mainDirectory, string origDirectory, string type, string topKey)
		{
			string topDirectory;
			string topValue;

			switch (type)
			{
				case "Message":
					topValue = Path.Combine("Assets", "GameAssets", "Serial", "Data", "Message");
					break;
				case "MainData":
					topValue = Path.Combine("Assets", "GameAssets", "Serial", "Data", "Master");
					break;
				case "MonsterAI":
					topValue = Path.Combine("Assets", "GameAssets", "Serial", "Res", "Battle");
					break;
				case "Map":
					topValue = Path.Combine("Assets", "GameAssets", "Serial", "Res", "Map");
					break;
				default:
					throw new Exception("Invalid type parameter in MemoriaToMagicite");
			}

			topDirectory = Path.Combine(mainDirectory, "Magicite", "FalconDive", topKey, topValue);
			Directory.CreateDirectory(topDirectory); // <-- We'll be creating an Export.json soon
			Directory.CreateDirectory(Path.Combine(mainDirectory, "Magicite", "FalconDive", topKey, "keys")); // <-- We'll be creating an Export.json soon
			ImportData importJson = new ImportData();

			// Get files to export
			string[] filesToExport = Directory.GetFiles(origDirectory, "*.*", SearchOption.AllDirectories);

			foreach (string file in filesToExport)
			{
				// Strip first three directories from the file
				string finalFile = file.Substring(file.IndexOf('\\') + 1);
				finalFile = finalFile.Substring(finalFile.IndexOf('\\') + 1);
				if (file.Count(f => f == '\\') > 3)
					finalFile = finalFile.Substring(finalFile.IndexOf('\\') + 1);
				if (!Directory.Exists(Path.Combine(topDirectory, Path.GetDirectoryName(finalFile))))
					Directory.CreateDirectory(Path.Combine(topDirectory, Path.GetDirectoryName(finalFile)));

				File.Copy(file, MemoriaToMagiciteFile(mainDirectory, file), true);
				importJson.keys.Add(finalFile.Substring(0, finalFile.IndexOf('.')).Replace('\\', '/'));
				importJson.values.Add(topValue.Replace('\\', '/') + "/" + finalFile.Substring(0, finalFile.IndexOf('.')).Replace('\\', '/'));
			}

			JsonSerializer serializer = new JsonSerializer();

			using (StreamWriter sw = new StreamWriter(Path.Combine(mainDirectory, "Magicite", "FalconDive", topKey, "keys", "Export.json")))
			using (JsonWriter writer = new JsonTextWriter(sw))
			{
				serializer.Serialize(writer, importJson);
			}
		}

		public static string MemoriaToMagiciteFile(string mainDirectory, string type, string fileToUse, string topKey = null)
		{
            string topValue;

			switch (type)
			{
				case "Message":
					topKey = "message";
					topValue = Path.Combine("Assets", "GameAssets", "Serial", "Data", "Message");
					break;
				case "MainData":
					topKey = "master";
					topValue = Path.Combine("Assets", "GameAssets", "Serial", "Data", "Master");
					break;
				case "MonsterAI":
					topKey = "monster_ai";
					topValue = Path.Combine("Assets", "GameAssets", "Serial", "Res", "Battle", "MonsterAI");
					break;
				case "Map":
					if (topKey == null) throw new Exception("Map type has no topKey parameter value");
					topValue = Path.Combine("Assets", "GameAssets", "Serial", "Res", "Map");
					break;
				default:
					throw new Exception("Invalid type parameter in MemoriaToMagicite");
			}

			return Path.Combine(mainDirectory, "Magicite", "FalconDive", topKey, topValue, fileToUse);
		}

		public static string MemoriaToMagiciteFile(string mainDirectory, string fileToUse)
		{
			string finalFile = fileToUse.ToLower();
			// TODO:  Establish types
			while (finalFile.StartsWith(@"\"))
				finalFile = fileToUse[1..];
			while (finalFile.StartsWith(@"res\") || finalFile.StartsWith(@"data\"))
				finalFile = finalFile[(finalFile.IndexOf('\\') + 1)..];
			if (finalFile.StartsWith(@"battle\"))
				finalFile = finalFile[(finalFile.IndexOf('\\') + 1)..];
			if (finalFile.StartsWith(@"map\"))
			{
				finalFile = finalFile[(finalFile.IndexOf('\\') + 1)..];
				string topKey = finalFile.Substring(0, finalFile.IndexOf('\\'));
				finalFile = finalFile[(finalFile.IndexOf('\\') + 1)..];
				return MemoriaToMagiciteFile(mainDirectory, "Map", finalFile, topKey);
			}
			else if (finalFile.StartsWith(@"monsterai"))
			{
				finalFile = finalFile[(finalFile.IndexOf('\\') + 1)..];
				return MemoriaToMagiciteFile(mainDirectory, "MonsterAI", finalFile);
			}
			else if (finalFile.StartsWith(@"message"))
			{
				finalFile = finalFile[(finalFile.IndexOf('\\') + 1)..];
				return MemoriaToMagiciteFile(mainDirectory, "Message", finalFile);
			}
			else if (finalFile.StartsWith(@"master") || finalFile.StartsWith(@"maindata"))
			{
				finalFile = finalFile[(finalFile.IndexOf('\\') + 1)..];
				return MemoriaToMagiciteFile(mainDirectory, "MainData", finalFile);
			}

			throw new Exception("Invalid fileToUse parameter:  " + fileToUse);
		}

		private class ImportData
		{
			public List<string> keys { get; set; }
			public List<string> values { get; set; }

			public ImportData()
			{
				keys = new List<string>();
				values = new List<string>();
			}
		}
	}
}
