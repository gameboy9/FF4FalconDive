using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FF4FreeEnterprisePR.Inventory
{
	public static class Updater
	{
		public static void update(string mainDirectory, string directory, bool zAtFalcon, bool zAtOrdeals, bool showMonsterChests)
		{
			if (!Path.Exists(Path.Combine(mainDirectory, "BepInEx"))) 
			{
				ZipFile.ExtractToDirectory("install.zip", mainDirectory, true);
			}

			// Iterate through the map directory and copy the files into the other map directory...
			foreach (string jsonFile in Directory.GetDirectories(Path.Combine("Res","Map"), "*.*", SearchOption.AllDirectories))
			{
				if (jsonFile.Count(f => f == '\\') != 2) continue;

				MemoriaToMagiciteCopy(directory, jsonFile, "Map", Path.GetFileName(jsonFile));
			}
			MemoriaToMagiciteCopy(directory, Path.Combine("Res","Battle","MonsterAI"), "MonsterAI", "monster_ai");
			// Likewise for the Battle resources
			foreach (string jsonFile in Directory.GetDirectories(Path.Combine("Res","Battle","BattleWeapon"), "*.*", SearchOption.AllDirectories))
			{
				if (jsonFile.Count(f => f == '\\') != 3) continue;

				MemoriaToMagiciteCopy(directory, jsonFile, "BattleWeapon", Path.GetFileName(jsonFile));
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

			if (showMonsterChests)
			{
				foreach (string jsonFile in Directory.GetDirectories(Path.Combine("AltScenarios","ShowMonsterChests"), "*.*", SearchOption.AllDirectories))
				{
					if (jsonFile.Count(f => f == '\\') != 2) continue;

					MemoriaToMagiciteCopy(directory, jsonFile, "Map", Path.GetFileName(jsonFile), true);
				}			
			}
		}

		public static void MemoriaToMagiciteCopy(string mainDirectory, string origDirectory, string type, string topKey, bool merge=false)
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
				case "BattleWeapon":
					topValue = Path.Combine("Assets", "GameAssets", "Serial", "Res", "Battle", "BattleWeapon");
					break;
				case "Map":
					topValue = Path.Combine("Assets", "GameAssets", "Serial", "Res", "Map", topKey);
					break;
				default:
					throw new Exception("Invalid type parameter in MemoriaToMagicite");
			}

			topKey = topKey.ToLower();
			topDirectory = Path.Combine(mainDirectory, "Magicite", "FalconDive", topKey, topValue);
			Directory.CreateDirectory(topDirectory); // <-- We'll be creating an Export.json soon
			Directory.CreateDirectory(Path.Combine(mainDirectory, "Magicite", "FalconDive", topKey, "keys")); // <-- We'll be creating an Export.json soon
			ImportData importJson = new ImportData();
			if (merge && File.Exists(Path.Combine(mainDirectory, "Magicite", "FalconDive", topKey, "keys", "Export.json")))
			{
				using (StreamReader sr = new StreamReader(Path.Combine(mainDirectory, "Magicite", "FalconDive", topKey, "keys", "Export.json")))
				using (JsonTextReader reader = new JsonTextReader(sr))
				{
					JsonSerializer deserializer = new JsonSerializer();
					importJson = deserializer.Deserialize<ImportData>(reader);
				}
			}

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

				if (finalFile.EndsWith("spritedata")) continue; // FIXME this is correct for tilemap pngs which aren't supposed to have a spritedata but may fail if multiple sprites look at one png
				if (type == "Map" && (file.Count(f => f == '\\') == 3))
					finalFile = finalFile.Substring(finalFile.IndexOf('\\') + 1);
				string keyName = (type == "BattleWeapon") ? finalFile.Substring(finalFile.IndexOf('\\') + 1) : finalFile;
				importJson.keys.Add(keyName.Substring(0, keyName.IndexOf('.')).Replace('\\', '/'));
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
					topValue = Path.Combine("Assets", "GameAssets", "Serial", "Res", "Map", topKey);
					break;
				case "BattleWeapon":
					if (topKey == null) throw new Exception("BattleWeapon type has no topKey parameter value");
					topValue = Path.Combine("Assets", "GameAssets", "Serial", "Res", "Battle", "BattleWeapon");
					break;
				default:
					throw new Exception("Invalid type parameter in MemoriaToMagicite");
			}

			return Path.Combine(mainDirectory, "Magicite", "FalconDive", topKey, topValue, fileToUse);
		}

		public static string MemoriaToMagiciteFile(string mainDirectory, string fileToUse)
		{
			//string finalFile = fileToUse.ToLower();
			string finalFile = fileToUse;
			// TODO:  Establish types
			while (finalFile.StartsWith(@"\"))
				finalFile = fileToUse[1..];
			if (finalFile.StartsWith(@"altscenarios\",StringComparison.OrdinalIgnoreCase))
			{
				// Treat AltScenarios as consisting only of Map_XXXXX dirs
				finalFile = finalFile[(finalFile.IndexOf('\\') + 1)..];
				finalFile = finalFile[(finalFile.IndexOf('\\') + 1)..];
				string topKey = finalFile.Substring(0, finalFile.IndexOf('\\'));
				finalFile = finalFile[(finalFile.IndexOf('\\') + 1)..];
				return MemoriaToMagiciteFile(mainDirectory, "Map", finalFile, topKey);
			}
			while (finalFile.StartsWith(@"res\",StringComparison.OrdinalIgnoreCase) || finalFile.StartsWith(@"data\",StringComparison.OrdinalIgnoreCase))
				finalFile = finalFile[(finalFile.IndexOf('\\') + 1)..];
			if (finalFile.StartsWith(@"battle\",StringComparison.OrdinalIgnoreCase))
				finalFile = finalFile[(finalFile.IndexOf('\\') + 1)..];
			if (finalFile.StartsWith(@"map\",StringComparison.OrdinalIgnoreCase))
			{
				finalFile = finalFile[(finalFile.IndexOf('\\') + 1)..];
				string topKey = finalFile.Substring(0, finalFile.IndexOf('\\'));
				finalFile = finalFile[(finalFile.IndexOf('\\') + 1)..];
				return MemoriaToMagiciteFile(mainDirectory, "Map", finalFile, topKey);
			}
			else if (finalFile.StartsWith(@"monsterai",StringComparison.OrdinalIgnoreCase))
			{
				finalFile = finalFile[(finalFile.IndexOf('\\') + 1)..];
				return MemoriaToMagiciteFile(mainDirectory, "MonsterAI", finalFile);
			}
			else if (finalFile.StartsWith(@"battleweapon",StringComparison.OrdinalIgnoreCase))
			{
				finalFile = finalFile[(finalFile.IndexOf('\\') + 1)..];
				string topKey = finalFile.Substring(0, finalFile.IndexOf('\\'));
				return MemoriaToMagiciteFile(mainDirectory, "BattleWeapon", finalFile, topKey);
			}
			else if (finalFile.StartsWith(@"message",StringComparison.OrdinalIgnoreCase))
			{
				finalFile = finalFile[(finalFile.IndexOf('\\') + 1)..];
				return MemoriaToMagiciteFile(mainDirectory, "Message", finalFile);
			}
			else if (finalFile.StartsWith(@"master",StringComparison.OrdinalIgnoreCase) || finalFile.StartsWith(@"maindata",StringComparison.OrdinalIgnoreCase))
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
