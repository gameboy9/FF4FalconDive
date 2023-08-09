using FF4FreeEnterprisePR.Randomize;
using FF4FreeEnterprisePR.Inventory;
using System;
using System.IO;
using System.Windows.Forms;
using System.IO.Compression;
using System.Security.Cryptography;
using FF4FalconDive.Inventory;

namespace FF4FreeEnterprisePR
{
	public partial class FF4FalconDive : Form
	{
		const string defaultFlags = "0VQ9K0100000";
		string dataMainDirectory;
		string mainDirectory;
		string dataDirectory;
		string mapDirectory;
		bool loading = true;
		Random r1;
		const int flagLength = 12;

		public FF4FalconDive()
		{
			InitializeComponent();
		}

		public void DetermineFlags(object sender, EventArgs e)
		{
			if (loading) return;

			string flags = "";
			flags += convertIntToChar(checkboxesToNumber(new CheckBox[] { shopNoJ, shopNoSuper, treasureNoJ, treasureNoSuper, dupCharactersAllowed }));
			//// Combo boxes time...
			flags += convertIntToChar(encounterRate.SelectedIndex + (8 * requiredShards.SelectedIndex));
			flags += convertIntToChar(shopItemQty.SelectedIndex + (8 * shopBuyPrice.SelectedIndex));
			flags += convertIntToChar(shopItemTypes.SelectedIndex + (8 * treasureTypes.SelectedIndex));
			flags += convertIntToChar(xpMultiplier.SelectedIndex + (8 * zeromusDifficulty.SelectedIndex));
			flags += convertIntToChar(gpMultiplier.SelectedIndex + (8 * shardsBeforeSirens.SelectedIndex));
			flags += convertIntToChar(monsterDifficulty.SelectedIndex + (8 * numHeroes.SelectedIndex));
			flags += convertIntToChar(checkboxesToNumber(new CheckBox[] { removeBonusItems, exCecil, exCid, exEdge, exEdward, exFusoya }));
			flags += convertIntToChar(firstHero.SelectedIndex); // Maxes out at 13.
			flags += convertIntToChar(checkboxesToNumber(new CheckBox[] { exKain, exPalom, exPorom, exRosa, exRydia, exTellah }));
			flags += convertIntToChar(checkboxesToNumber(new CheckBox[] { exYang, removeFGExclusiveItems, exPaladinCecil }));
			flags += convertIntToChar(startingXP.SelectedIndex);
			RandoFlags.Text = flags;

			//flags = "";
			//flags += convertIntToChar(checkboxesToNumber(new CheckBox[] { CuteHats }));
			//VisualFlags.Text = flags;
		}

		private void determineChecks(object sender, EventArgs e)
		{
			if (loading && RandoFlags.Text.Length < flagLength)
				RandoFlags.Text = defaultFlags; // Default flags here
			else if (RandoFlags.Text.Length < flagLength)
				return;

			//if (loading && VisualFlags.Text.Length < 1)
			//	VisualFlags.Text = "0";
			//else if (VisualFlags.Text.Length < 1)
			//	return;

			loading = true;

			string flags = RandoFlags.Text;
			numberToCheckboxes(convertChartoInt(Convert.ToChar(flags.Substring(0, 1))), new CheckBox[] { shopNoJ, shopNoSuper, treasureNoJ, treasureNoSuper, dupCharactersAllowed });
			encounterRate.SelectedIndex = convertChartoInt(Convert.ToChar(flags.Substring(1, 1))) % 8;
			requiredShards.SelectedIndex = convertChartoInt(Convert.ToChar(flags.Substring(1, 1))) / 8;
			shopItemQty.SelectedIndex = convertChartoInt(Convert.ToChar(flags.Substring(2, 1))) % 8;
			shopBuyPrice.SelectedIndex = convertChartoInt(Convert.ToChar(flags.Substring(2, 1))) / 8;
			shopItemTypes.SelectedIndex = convertChartoInt(Convert.ToChar(flags.Substring(3, 1))) % 8;
			treasureTypes.SelectedIndex = convertChartoInt(Convert.ToChar(flags.Substring(3, 1))) / 8;
			xpMultiplier.SelectedIndex = convertChartoInt(Convert.ToChar(flags.Substring(4, 1))) % 8;
			zeromusDifficulty.SelectedIndex = convertChartoInt(Convert.ToChar(flags.Substring(4, 1))) / 8;
			gpMultiplier.SelectedIndex = convertChartoInt(Convert.ToChar(flags.Substring(5, 1))) % 8;
			shardsBeforeSirens.SelectedIndex = convertChartoInt(Convert.ToChar(flags.Substring(5, 1))) / 8;
			monsterDifficulty.SelectedIndex = convertChartoInt(Convert.ToChar(flags.Substring(6, 1))) % 8;
			numHeroes.SelectedIndex = convertChartoInt(Convert.ToChar(flags.Substring(6, 1))) / 8;
			numberToCheckboxes(convertChartoInt(Convert.ToChar(flags.Substring(7, 1))), new CheckBox[] { removeBonusItems, exCecil, exCid, exEdge, exEdward, exFusoya });
			firstHero.SelectedIndex = convertChartoInt(Convert.ToChar(flags.Substring(8, 1))) % 16;
			numberToCheckboxes(convertChartoInt(Convert.ToChar(flags.Substring(9, 1))), new CheckBox[] { exKain, exPalom, exPorom, exRosa, exRydia, exTellah });
			numberToCheckboxes(convertChartoInt(Convert.ToChar(flags.Substring(10, 1))), new CheckBox[] { exYang, removeFGExclusiveItems, exPaladinCecil });
			startingXP.SelectedIndex = convertChartoInt(Convert.ToChar(flags.Substring(11, 1))) % 8;

			//flags = VisualFlags.Text;
			//numberToCheckboxes(convertChartoInt(Convert.ToChar(flags.Substring(0, 1))), new CheckBox[] { CuteHats });

			loading = false;
		}

		private int checkboxesToNumber(CheckBox[] boxes)
		{
			int number = 0;
			for (int lnI = 0; lnI < Math.Min(boxes.Length, 6); lnI++)
				number += boxes[lnI].Checked ? (int)Math.Pow(2, lnI) : 0;

			return number;
		}

		private int numberToCheckboxes(int number, CheckBox[] boxes)
		{
			for (int lnI = 0; lnI < Math.Min(boxes.Length, 6); lnI++)
				boxes[lnI].Checked = number % ((int)Math.Pow(2, lnI + 1)) >= (int)Math.Pow(2, lnI);

			return number;
		}

		private string convertIntToChar(int number)
		{
			if (number >= 0 && number <= 9)
				return number.ToString();
			if (number >= 10 && number <= 35)
				return Convert.ToChar(55 + number).ToString();
			if (number >= 36 && number <= 61)
				return Convert.ToChar(61 + number).ToString();
			if (number == 62) return "!";
			if (number == 63) return "@";
			return "";
		}

		private int convertChartoInt(char character)
		{
			if (character >= Convert.ToChar("0") && character <= Convert.ToChar("9"))
				return character - 48;
			if (character >= Convert.ToChar("A") && character <= Convert.ToChar("Z"))
				return character - 55;
			if (character >= Convert.ToChar("a") && character <= Convert.ToChar("z"))
				return character - 61;
			if (character == Convert.ToChar("!")) return 62;
			if (character == Convert.ToChar("@")) return 63;
			return 0;
		}

		private void FF4FabulGauntlet_Load(object sender, EventArgs e)
		{
			RandoSeed.Text = (DateTime.Now.Ticks % 2147483647).ToString();

			try
			{
				using (TextReader reader = File.OpenText("lastFF4FG.txt"))
				{
					FF4PRFolder.Text = reader.ReadLine();
					RandoSeed.Text = reader.ReadLine();
					RandoFlags.Text = reader.ReadLine();
					gameAssetsFile.Text = reader.ReadLine();
					//VisualFlags.Text = reader.ReadLine();
					determineChecks(null, null);

					loading = false;
				}
			}
			catch
			{
				// Default flags here
				RandoFlags.Text = defaultFlags;
				//VisualFlags.Text = "0";
				// ignore error
				loading = false;
				determineChecks(null, null);
			}
		}

		private void NewSeed_Click(object sender, EventArgs e)
		{
			RandoSeed.Text = (DateTime.Now.Ticks % 2147483647).ToString();
		}

		private void Randomize_Click(object sender, EventArgs e)
		{
			if (!File.Exists(Path.Combine(FF4PRFolder.Text, "BepInEx", "plugins", "Memoria.FF4.dll")) || !File.Exists(Path.Combine(FF4PRFolder.Text, "BepInEx", "plugins", "Memoria.FF4.pdb"))
				|| !File.Exists(Path.Combine(FF4PRFolder.Text, "BepInEx", "config", "Memoria.ffpr.cfg")) || !Directory.Exists(Path.Combine(FF4PRFolder.Text, "FINAL FANTASY IV_Data", "StreamingAssets", "Assets", "GameAssets")))
			{
				MessageBox.Show("Randomizer assets have not been extracted.  Please extract, then try randomization again.");
				return;
			}

			int included = (exCecil.Checked ? 0 : 1) +
				(exCid.Checked ? 0 : 1) +
				(exEdge.Checked ? 0 : 1) +
				(exEdward.Checked ? 0 : 1) +
				(exFusoya.Checked ? 0 : 1) +
				(exKain.Checked ? 0 : 1) +
				(exPalom.Checked ? 0 : 1) +
				(exPorom.Checked ? 0 : 1) +
				(exRosa.Checked ? 0 : 1) +
				(exRydia.Checked ? 0 : 1) +
				(exTellah.Checked ? 0 : 1) +
				(exYang.Checked ? 0 : 1) + 
				(exPaladinCecil.Checked ? 0 : 1);

			if (included < Convert.ToInt32(numHeroes.SelectedItem) && !dupCharactersAllowed.Checked)
            {
				MessageBox.Show("Included heroes exceed number of heroes and duplicate heroes is not checked.  Please try again.");
				return;
			}
			
			mainDirectory = Path.Combine(FF4PRFolder.Text, "FINAL FANTASY IV_Data", "StreamingAssets", "Assets", "GameAssets", "Serial");
			dataDirectory = Path.Combine(FF4PRFolder.Text, "FINAL FANTASY IV_Data", "StreamingAssets", "Assets", "GameAssets", "Serial", "Data");
			dataMainDirectory = Path.Combine(FF4PRFolder.Text, "FINAL FANTASY IV_Data", "StreamingAssets", "Assets", "GameAssets", "Serial", "Data", "Master");
			mapDirectory = Path.Combine(FF4PRFolder.Text, "FINAL FANTASY IV_Data", "StreamingAssets", "Assets", "GameAssets", "Serial", "Res", "Map");

			double xpMulti = xpMultiplier.SelectedIndex == 0 ? 1 :
				xpMultiplier.SelectedIndex == 1 ? 1.5 :
				xpMultiplier.SelectedIndex == 2 ? 2 :
				xpMultiplier.SelectedIndex == 3 ? 2.5 :
				xpMultiplier.SelectedIndex == 4 ? 3 :
				xpMultiplier.SelectedIndex == 5 ? 4 :
				xpMultiplier.SelectedIndex == 6 ? 5 : 10;

			double xpStart = startingXP.SelectedIndex == 0 ? 1 :
				startingXP.SelectedIndex == 1 ? -1 :
				startingXP.SelectedIndex == 2 ? 0.5 :
				startingXP.SelectedIndex == 3 ? 2 :
				startingXP.SelectedIndex == 4 ? 3 :
				startingXP.SelectedIndex == 5 ? 4 :
				startingXP.SelectedIndex == 6 ? 5 : 10;

			update();
			long seedNumber;
			try
			{
				seedNumber = Convert.ToInt64(RandoSeed.Text);
			} catch
			{
				MessageBox.Show("Invalid seed number");
				return;
			}
			r1 = new Random((int)(seedNumber % 2147483648));
			int[] party = randomizeParty(xpMulti * xpStart);
			randomizeShops(party);
			randomizeTreasures(party);
			priceAdjustment();
			if (seedNumber > 2147483647)
				r1 = new Random((int)(seedNumber / 2147483648));

			randomizeMonstersWithBoost(xpMulti);
			Zeromus.ZeromusSetup(r1, Convert.ToInt32(requiredShards.Text), zeromusDifficulty.SelectedIndex, shardsBeforeSirens.SelectedIndex, mainDirectory);
			Rewards.establishRewards(r1, party, mainDirectory, 
				Path.Combine(dataDirectory, "Message"), !removeBonusItems.Checked, !removeFGExclusiveItems.Checked, party, xpMulti * xpStart);
			new Map(r1, dataMainDirectory,
					encounterRate.SelectedIndex == 1 || encounterRate.SelectedIndex == 4 ? 2 :
					encounterRate.SelectedIndex == 3 || encounterRate.SelectedIndex == 5 ? 4 :
					encounterRate.SelectedIndex == 6 ? 8 : 1,
					encounterRate.SelectedIndex == 0 ? 2 :
					encounterRate.SelectedIndex == 1 || encounterRate.SelectedIndex == 3 ? 3 : 1,
				encounterRate.SelectedIndex == 7);

			try
			{
				string checkSum = "";
				using (SHA1 sha1Crypto = SHA1.Create())
				{
					using (FileStream stream = File.OpenRead(Path.Combine(dataMainDirectory, "monster_party.csv")))
					{
						checkSum = BitConverter.ToString(sha1Crypto.ComputeHash(stream)).ToLower().Replace("-", "").Substring(0, 16);
					}
				}

				Clipboard.SetText(checkSum);
				Messages.updateMessages(Path.Combine(dataDirectory, "Message"), RandoSeed.Text, RandoFlags.Text, checkSum, shardsBeforeSirens.SelectedIndex != 5);
				NewChecksum.Text = "COMPLETE - checksum " + checkSum + " (copied to clipboard)";
			}
			catch
			{

			}
		}

		private void update()
		{
			new Updater(mainDirectory);
		}

		private int[] randomizeParty(double xpMulti)
		{
			return Party.establishParty(r1, mapDirectory, firstHero.SelectedIndex, dupCharactersAllowed.Checked, Convert.ToInt32(numHeroes.SelectedItem), exPaladinCecil.Checked,
				new bool[] { exCecil.Checked, exKain.Checked, exRydia.Checked, exTellah.Checked, exEdward.Checked, exRosa.Checked, exYang.Checked, exPalom.Checked, exPorom.Checked, exCid.Checked, exEdge.Checked, exFusoya.Checked, exPaladinCecil.Checked }, xpMulti);
		}

		private void randomizeShops(int[] party)
		{
			int buyMultiplier = shopBuyPrice.SelectedIndex == 0 ? 0 : shopBuyPrice.SelectedIndex == 1 ? 1 : shopBuyPrice.SelectedIndex == 2 ? 2 : shopBuyPrice.SelectedIndex == 3 ? 4 : shopBuyPrice.SelectedIndex == 4 ? 8 : 20;
			new Shops(r1, shopItemTypes.SelectedIndex, shopItemQty.SelectedIndex, shopNoJ.Checked, shopNoSuper.Checked, 
				Path.Combine(dataMainDirectory, "product.csv"), !removeBonusItems.Checked, !removeFGExclusiveItems.Checked, buyMultiplier, 20, party);
		}

		private void randomizeTreasures(int[] party)
		{
			new Treasure(r1, treasureTypes.SelectedIndex, mainDirectory,
				treasureNoJ.Checked, treasureNoSuper.Checked, !removeBonusItems.Checked, !removeFGExclusiveItems.Checked, 5, party);
		}

		private void randomizeMonstersWithBoost(double xpMulti)
		{
			Bosses.establishBosses(mainDirectory, dataMainDirectory, r1);

			double gpMulti = gpMultiplier.SelectedIndex == 0 ? 1 :
				gpMultiplier.SelectedIndex == 1 ? 1.5 :
				gpMultiplier.SelectedIndex == 2 ? 2 :
				gpMultiplier.SelectedIndex == 3 ? 2.5 :
				gpMultiplier.SelectedIndex == 4 ? 3 :
				gpMultiplier.SelectedIndex == 5 ? 4 :
				gpMultiplier.SelectedIndex == 6 ? 5 : 10;
			int xpBoostInt = 0;
			int gpBoostInt = 0;
			Monster.MonsterBoost(dataMainDirectory, xpMulti, xpBoostInt, gpMulti, gpBoostInt, 5);
			Monster.AdjustMonsterDifficulty(dataMainDirectory, monsterDifficulty.SelectedIndex);
		}

		private void priceAdjustment()
		{
			int buyMultiplier = shopBuyPrice.SelectedIndex == 0 ? 0 : shopBuyPrice.SelectedIndex == 1 ? 1 : shopBuyPrice.SelectedIndex == 2 ? 2 : shopBuyPrice.SelectedIndex == 3 ? 4 : shopBuyPrice.SelectedIndex == 4 ? 8 : 20;
			new Weapons().adjustPrices(dataMainDirectory, buyMultiplier, 20);
			new Items().adjustPrices(dataMainDirectory, buyMultiplier, 20);
			new Armor().adjustPrices(dataMainDirectory, buyMultiplier, 20);
		}

		private void FF4FabulGauntlet_FormClosing(object sender, FormClosingEventArgs e)
		{
			using (StreamWriter writer = File.CreateText("lastFF4FG.txt"))
			{
				writer.WriteLine(FF4PRFolder.Text);
				writer.WriteLine(RandoSeed.Text);
				writer.WriteLine(RandoFlags.Text);
				writer.WriteLine(gameAssetsFile.Text);
				//writer.WriteLine(VisualFlags.Text);
			}
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			using (var fbd = new FolderBrowserDialog())
			{
				DialogResult result = fbd.ShowDialog();

				if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
					FF4PRFolder.Text = fbd.SelectedPath;
			}
		}

		private void extractGameAssets_Click(object sender, EventArgs e)
		{
			try
			{
				if (!File.Exists(gameAssetsFile.Text))
				{
					MessageBox.Show("Cannot extract - game assets file listed does not exist...");
					NewChecksum.Text = "Extraction failed...";
					return;
				}
				NewChecksum.Text = "Extracting...";
				if (!Directory.Exists(Path.Combine(FF4PRFolder.Text, "BepInEx")))
					ZipFile.ExtractToDirectory(Path.Combine("install", "BepInEx.zip"), Path.Combine(FF4PRFolder.Text), true);

				if (!File.Exists(Path.Combine(FF4PRFolder.Text, "BepInEx", "plugins", "Memoria.FF4.dll")))
					File.Copy(Path.Combine("install", "Memoria.FF4.dll"), Path.Combine(FF4PRFolder.Text, "BepInEx", "plugins", "Memoria.FF4.dll"));
				if (!File.Exists(Path.Combine(FF4PRFolder.Text, "BepInEx", "plugins", "Memoria.FF4.pdb")))
					File.Copy(Path.Combine("install", "Memoria.FF4.pdb"), Path.Combine(FF4PRFolder.Text, "BepInEx", "plugins", "Memoria.FF4.pdb"));
				if (!File.Exists(Path.Combine(FF4PRFolder.Text, "BepInEx", "config", "Memoria.ffpr.cfg")))
					File.Copy(Path.Combine("install", "Memoria.ffpr.cfg"), Path.Combine(FF4PRFolder.Text, "BepInEx", "config", "Memoria.ffpr.cfg"));
				if (!Directory.Exists(Path.Combine(FF4PRFolder.Text, "FINAL FANTASY IV_Data", "StreamingAssets", "Assets", "GameAssets")))
					ZipFile.ExtractToDirectory(gameAssetsFile.Text, Path.Combine(FF4PRFolder.Text, "FINAL FANTASY IV_Data", "StreamingAssets", "Assets"));
				NewChecksum.Text = "Extraction complete!";
			}
			catch (Exception ex)
			{
				MessageBox.Show("Unable to extract - " + ex.Message);
				NewChecksum.Text = "Extraction failed...";
			}
		}

		private void revertToDefault_click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to revert Final Fantasy IV back to vanilla?", "Final Fantasy IV: Fabul Gauntlet", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				try
				{
					NewChecksum.Text = "Reverting...";
					if (File.Exists(Path.Combine(FF4PRFolder.Text, "BepInEx", "plugins", "Memoria.FF4.dll")))
						File.Delete(Path.Combine(FF4PRFolder.Text, "BepInEx", "plugins", "Memoria.FF4.dll"));
					if (File.Exists(Path.Combine(FF4PRFolder.Text, "BepInEx", "plugins", "Memoria.FF4.pdb")))
						File.Delete(Path.Combine(FF4PRFolder.Text, "BepInEx", "plugins", "Memoria.FF4.pdb"));
					if (File.Exists(Path.Combine(FF4PRFolder.Text, "BepInEx", "config", "Memoria.ffpr.cfg")))
						File.Delete(Path.Combine(FF4PRFolder.Text, "BepInEx", "config", "Memoria.ffpr.cfg"));
					if (Directory.Exists(Path.Combine(FF4PRFolder.Text, "BepInEx")))
						Directory.Delete(Path.Combine(FF4PRFolder.Text, "BepInEx"), true);
					if (Directory.Exists(Path.Combine(FF4PRFolder.Text, "FINAL FANTASY IV_Data", "StreamingAssets", "Assets")))
						Directory.Delete(Path.Combine(FF4PRFolder.Text, "FINAL FANTASY IV_Data", "StreamingAssets", "Assets"), true);
					NewChecksum.Text = "Revert complete!";
				}
				catch (Exception ex)
				{
					MessageBox.Show("Unable to revert - " + ex.Message);
					NewChecksum.Text = "Revert failed...";
				}
			}
		}

		private void BrowseForGameAssets_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog1 = new OpenFileDialog();

			openFileDialog1.InitialDirectory = @"C:\";
			openFileDialog1.Filter = "zip files (*.zip)|*.zip|All files (*.*)|*.*";
			openFileDialog1.FilterIndex = 2;
			openFileDialog1.RestoreDirectory = true;

			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				gameAssetsFile.Text = openFileDialog1.FileName;
			}
		}

		private void flagDefault_Click(object sender, EventArgs e)
		{
			Button btn = (Button)sender;
			if (btn.Name == "flagDefault")
			{
				RandoFlags.Text = "0VQ9K0100000";
			}
			if (btn.Name == "flagCustom1") { if (flagCustom1.Text.Length == flagLength) RandoFlags.Text = flagCustom1Text.Text; else MessageBox.Show("Invalid flag string"); }
			if (btn.Name == "flagCustom2") { if (flagCustom2.Text.Length == flagLength) RandoFlags.Text = flagCustom2Text.Text; else MessageBox.Show("Invalid flag string"); }
			if (btn.Name == "flagCustom3") { if (flagCustom3.Text.Length == flagLength) RandoFlags.Text = flagCustom3Text.Text; else MessageBox.Show("Invalid flag string"); }
			if (btn.Name == "flagCustom4") { if (flagCustom4.Text.Length == flagLength) RandoFlags.Text = flagCustom4Text.Text; else MessageBox.Show("Invalid flag string"); }
			if (btn.Name == "flagCustom5") { if (flagCustom5.Text.Length == flagLength) RandoFlags.Text = flagCustom5Text.Text; else MessageBox.Show("Invalid flag string"); }
		}
	}
}
