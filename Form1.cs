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
		const string defaultFlags = "05Q9KGX0000075";
		string updateDirectory;

		bool loading = true;
		Random r1;
		const int flagLength = 14;
		TextBox[,] heroNames = new TextBox[12, 5];
		string checkSum;

		public FF4FalconDive()
		{
			InitializeComponent();
		}

		public void DetermineFlags(object sender, EventArgs e)
		{
			if (loading) return;

			if (requiredShards.SelectedIndex > numberOfShards.SelectedIndex) 
				requiredShards.SelectedIndex = numberOfShards.SelectedIndex;
			if (shardsBeforeSirens.SelectedIndex > numberOfShards.SelectedIndex)
				shardsBeforeSirens.SelectedIndex = numberOfShards.SelectedIndex;
			if (minHeroes.SelectedIndex > numHeroes.SelectedIndex && minHeroes.SelectedIndex != 5 && numHeroes.SelectedIndex != 5)
				minHeroes.SelectedIndex = numHeroes.SelectedIndex;

			nothingAmount.Text = "# of Nothings:  " + (13 - numberOfShards.SelectedIndex);
			nothingKeyItem.Text = "Req for Key Item:  " + ((13 - numberOfShards.SelectedIndex) / 2);
			// No special item if nothings equal 0 or 1.
			nothingTier9Item.Text = "Req For Special:  " + (numberOfShards.SelectedIndex >= 11 ? "N/A" : (int)Math.Ceiling((double)(13 - numberOfShards.SelectedIndex) * 3 / 4));

			string flags = "";
			flags += convertIntToChar(checkboxesToNumber([shopNoJ, shopNoSuper, treasureNoJ, treasureNoSuper, dupCharactersAllowed, replaceNothings]));
			//// Combo boxes time...
			flags += convertIntToChar(requiredShards.SelectedIndex);
			flags += convertIntToChar(shopItemQty.SelectedIndex + (8 * shopBuyPrice.SelectedIndex));
			flags += convertIntToChar(shopItemTypes.SelectedIndex + (8 * treasureTypes.SelectedIndex));
			flags += convertIntToChar(xpMultiplier.SelectedIndex + (8 * zeromusDifficulty.SelectedIndex));
			flags += convertIntToChar(gpMultiplier.SelectedIndex + (8 * minHeroes.SelectedIndex));
			flags += convertIntToChar(monsterDifficulty.SelectedIndex + (8 * numHeroes.SelectedIndex));
			flags += convertIntToChar(checkboxesToNumber([removeBonusItems, exCecil, exCid, exEdge, exEdward, exFusoya]));
			flags += convertIntToChar(firstHero.SelectedIndex); // Maxes out at 13.
			flags += convertIntToChar(checkboxesToNumber([exKain, exPalom, exPorom, exRosa, exRydia, exTellah]));
			flags += convertIntToChar(checkboxesToNumber([exYang, removeFGExclusiveItems, exPaladinCecil, zFalcon, zOrdeals]));
			flags += convertIntToChar(startingXP.SelectedIndex);
			flags += convertIntToChar(numberOfShards.SelectedIndex);
			flags += convertIntToChar(shardsBeforeSirens.SelectedIndex);
			RandoFlags.Text = flags;
		}

		private void determineChecks(object sender, EventArgs e)
		{
			if (loading && RandoFlags.Text.Length < flagLength)
				RandoFlags.Text = defaultFlags; // Default flags here
			else if (RandoFlags.Text.Length < flagLength)
				return;

			loading = true;

			string flags = RandoFlags.Text;
			numberToCheckboxes(convertChartoInt(Convert.ToChar(flags.Substring(0, 1))), [shopNoJ, shopNoSuper, treasureNoJ, treasureNoSuper, dupCharactersAllowed, replaceNothings]);
			requiredShards.SelectedIndex = convertChartoInt(Convert.ToChar(flags.Substring(1, 1)));
			shopItemQty.SelectedIndex = convertChartoInt(Convert.ToChar(flags.Substring(2, 1))) % 8;
			shopBuyPrice.SelectedIndex = convertChartoInt(Convert.ToChar(flags.Substring(2, 1))) / 8;
			shopItemTypes.SelectedIndex = convertChartoInt(Convert.ToChar(flags.Substring(3, 1))) % 8;
			treasureTypes.SelectedIndex = convertChartoInt(Convert.ToChar(flags.Substring(3, 1))) / 8;
			xpMultiplier.SelectedIndex = convertChartoInt(Convert.ToChar(flags.Substring(4, 1))) % 8;
			zeromusDifficulty.SelectedIndex = convertChartoInt(Convert.ToChar(flags.Substring(4, 1))) / 8;
			gpMultiplier.SelectedIndex = convertChartoInt(Convert.ToChar(flags.Substring(5, 1))) % 8;
			minHeroes.SelectedIndex = convertChartoInt(Convert.ToChar(flags.Substring(5, 1))) / 8;
			monsterDifficulty.SelectedIndex = convertChartoInt(Convert.ToChar(flags.Substring(6, 1))) % 8;
			numHeroes.SelectedIndex = convertChartoInt(Convert.ToChar(flags.Substring(6, 1))) / 8;
			numberToCheckboxes(convertChartoInt(Convert.ToChar(flags.Substring(7, 1))), [removeBonusItems, exCecil, exCid, exEdge, exEdward, exFusoya]);
			firstHero.SelectedIndex = convertChartoInt(Convert.ToChar(flags.Substring(8, 1))) % 16;
			numberToCheckboxes(convertChartoInt(Convert.ToChar(flags.Substring(9, 1))), [exKain, exPalom, exPorom, exRosa, exRydia, exTellah]);
			numberToCheckboxes(convertChartoInt(Convert.ToChar(flags.Substring(10, 1))), [exYang, removeFGExclusiveItems, exPaladinCecil, zFalcon, zOrdeals]);
			startingXP.SelectedIndex = convertChartoInt(Convert.ToChar(flags.Substring(11, 1))) % 8;
			numberOfShards.SelectedIndex = convertChartoInt(Convert.ToChar(flags.Substring(12, 1))) % 16;
			shardsBeforeSirens.SelectedIndex = convertChartoInt(Convert.ToChar(flags.Substring(13, 1))) % 16;
			nothingAmount.Text = "# of Nothings:  " + (13 - numberOfShards.SelectedIndex);
			nothingKeyItem.Text = "Req for Key Item:  " + ((13 - numberOfShards.SelectedIndex) / 2);
			// No special item if nothings equal 0 or 1.
			nothingTier9Item.Text = "Req For Special:  " + (numberOfShards.SelectedIndex >= 11 ? "N/A" : (int)Math.Ceiling((double)(13 - numberOfShards.SelectedIndex) * 3 / 4));

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
			for (int i = 0; i < 12; i++)
				for (int j = 0; j < 5; j++)
				{
					heroNames[i, j] = new TextBox
					{
						Location = new System.Drawing.Point(86 + (j * 131), 12 + (i * 28)),
						Size = new System.Drawing.Size(125, 27),
						MaxLength = 8
					};
					heroNameTab.Controls.Add(heroNames[i, j]);
				}

			RandoSeed.Text = (DateTime.Now.Ticks % 2147483647).ToString();

			try
			{
				using (TextReader reader = File.OpenText("lastFF4FG.txt"))
				{
					FF4PRFolder.Text = reader.ReadLine();
					RandoSeed.Text = reader.ReadLine();
					RandoFlags.Text = reader.ReadLine();
					//VisualFlags.Text = reader.ReadLine();
					determineChecks(null, null);

					for (int i = 0; i < 12; i++)
						for (int j = 0; j < 5; j++)
						{
							string heroName = reader.ReadLine();
							if (heroName == null)
								heroNames[i, j].Text =
									i == 0 ? "Cecil" :
									i == 1 ? "Kain" :
									i == 2 ? "Rosa" :
									i == 3 ? "Rydia" :
									i == 4 ? "Cid" :
									i == 5 ? "Tellah" :
									i == 6 ? "Edward" :
									i == 7 ? "Yang" :
									i == 8 ? "Palom" :
									i == 9 ? "Porom" :
									i == 10 ? "Edge" :
									"Fusoya"; // i == 11
							else
								heroNames[i, j].Text = heroName;
						}

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

				for (int i = 0; i < 12; i++)
					for (int j = 0; j < 5; j++)
					{
						heroNames[i, j].Text =
							i == 0 ? "Cecil" :
							i == 1 ? "Kain" :
							i == 2 ? "Rosa" :
							i == 3 ? "Rydia" :
							i == 4 ? "Cid" :
							i == 5 ? "Tellah" :
							i == 6 ? "Edward" :
							i == 7 ? "Yang" :
							i == 8 ? "Palom" :
							i == 9 ? "Porom" :
							i == 10 ? "Edge" :
							"Fusoya"; // i == 11
					}
			}

			if (!string.IsNullOrWhiteSpace(FF4PRFolder.Text))
			{
				if (Path.Exists(Path.Combine(FF4PRFolder.Text, "FINAL FANTASY IV_Data", "StreamingAssets", "Assets")) || File.Exists(Path.Combine(FF4PRFolder.Text, "BepInEx", "plugins", "Memoria.FF4.dll")))
				{
					if(MessageBox.Show("You have files from pre-version 5.0 Falcon Dive installations.  It is STRONGLY RECOMMENDED that those files are removed.  Would you like to remove those files?", "FF4 Falcon Dive", MessageBoxButtons.YesNo) == DialogResult.Yes) {
						try
						{
							if (Directory.Exists(Path.Combine(FF4PRFolder.Text, "BepInEx")))
								Directory.Delete(Path.Combine(FF4PRFolder.Text, "BepInEx"), true);
							if (Directory.Exists(Path.Combine(FF4PRFolder.Text, "mono")))
								Directory.Delete(Path.Combine(FF4PRFolder.Text, "mono"), true);
							if (Directory.Exists(Path.Combine(FF4PRFolder.Text, "FINAL FANTASY IV_Data", "StreamingAssets", "Assets")))
								Directory.Delete(Path.Combine(FF4PRFolder.Text, "FINAL FANTASY IV_Data", "StreamingAssets", "Assets"), true);
							MessageBox.Show("Pre-version 5.0 files deleted.");
						}
						catch (Exception ex)
						{
							MessageBox.Show("Unable to delete - " + ex.Message);
						}
					}
				}
			}
		}

		private void NewSeed_Click(object sender, EventArgs e)
		{
			RandoSeed.Text = (DateTime.Now.Ticks % 2147483647).ToString();
		}

		private void Randomize_Click(object sender, EventArgs e)
		{
			try
			{
				if (!Path.Exists(FF4PRFolder.Text))
				{
					MessageBox.Show("FF4 PR folder does not exist.  Please pick a new folder and try again.");
					return;
				}
			} 
			catch (Exception ex)
			{
				MessageBox.Show("There was an issue trying to find the FF4 PR folder:  " + ex.Message);
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

			updateDirectory = Path.Combine(FF4PRFolder.Text, "FINAL FANTASY IV_Data", "StreamingAssets");

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

			long seedNumber;
			try
			{
				seedNumber = Convert.ToInt64(RandoSeed.Text);
			}
			catch
			{
				MessageBox.Show("Invalid seed number");
				return;
			}

			try
			{
				Updater.update(FF4PRFolder.Text, updateDirectory, zFalcon.Checked, zOrdeals.Checked, showMonsterChests.Checked);

				int heroCount = numHeroes.SelectedIndex == 5 ? r1.Next() % 5 + 1 : numHeroes.SelectedIndex + 1;

				int heroMin = minHeroes.SelectedIndex == 5 ? r1.Next() % heroCount + 1 : Math.Min(minHeroes.SelectedIndex + 1, heroCount);

				r1 = new Random((int)(seedNumber % 2147483648));
				int[] party = randomizeParty(xpMulti * xpStart, heroCount);
				priceAdjustment();
				randomizeShops(party);
				randomizeTreasures(party);
				if (seedNumber > 2147483647)
					r1 = new Random((int)(seedNumber / 2147483648));

				randomizeMonstersWithBoost(xpMulti);
				Zeromus.ZeromusSetup(r1, Convert.ToInt32(requiredShards.Text), zeromusDifficulty.SelectedIndex, shardsBeforeSirens.SelectedIndex,
					(13 - numberOfShards.SelectedIndex) / 2, (int)Math.Ceiling((double)(13 - numberOfShards.SelectedIndex) * 3 / 4),
					updateDirectory);
				Rewards.establishRewards(r1, party, updateDirectory,
					!removeBonusItems.Checked, !removeFGExclusiveItems.Checked, party, xpMulti * xpStart, zOrdeals.Checked, zFalcon.Checked, 
					numberOfShards.SelectedIndex, replaceNothings.Checked, (13 - numberOfShards.SelectedIndex) / 2, (int)Math.Ceiling((double)(13 - numberOfShards.SelectedIndex) * 3 / 4), heroCount, heroMin);

				using (SHA1 sha1Crypto = SHA1.Create())
				{
					using (FileStream stream = File.OpenRead(Updater.MemoriaToMagiciteFile(updateDirectory, @"MainData\monster_party.csv")))
					{
						checkSum = BitConverter.ToString(sha1Crypto.ComputeHash(stream)).ToLower().Replace("-", "").Substring(0, 16);
					}
				}

				Clipboard.SetText("FF4FD_" + RandoFlags.Text + "_" + RandoSeed.Text + "_" + checkSum);
				Messages.updateMessages(updateDirectory, RandoSeed.Text, RandoFlags.Text, checkSum, shardsBeforeSirens.SelectedIndex != 13, 13 - numberOfShards.SelectedIndex, party, heroNames, r1);
				NewChecksum.Text = "COMPLETE - checksum " + checkSum + " - copied to clipboard with seed and flags";
			}
			catch (Exception ex)
			{
				using (StreamWriter sw = new StreamWriter("FDError.txt", true))
				{
					sw.WriteLine(ex.ToString());
					sw.WriteLine(RandoFlags.Text + "/" + RandoSeed.Text);
					sw.WriteLine("---------------------");
				}
				NewChecksum.Text = "Error generating seed - see FDError.txt for details";
			}
		}

		private int[] randomizeParty(double xpMulti, int heroCount)
		{
			return Party.establishParty(r1, updateDirectory, firstHero.SelectedIndex, dupCharactersAllowed.Checked, heroCount, exPaladinCecil.Checked,
				new bool[] { exCecil.Checked, exKain.Checked, exRydia.Checked, exTellah.Checked, exEdward.Checked, exRosa.Checked, exYang.Checked, exPalom.Checked, exPorom.Checked, exCid.Checked, exEdge.Checked, exFusoya.Checked, exPaladinCecil.Checked }, xpMulti);
		}

		private void randomizeShops(int[] party)
		{
			int buyMultiplier = shopBuyPrice.SelectedIndex == 0 ? 0 : shopBuyPrice.SelectedIndex == 1 ? 1 : shopBuyPrice.SelectedIndex == 2 ? 2 : shopBuyPrice.SelectedIndex == 3 ? 4 : shopBuyPrice.SelectedIndex == 4 ? 8 : 20;
			new Shops(r1, shopItemTypes.SelectedIndex, shopItemQty.SelectedIndex, shopNoJ.Checked, shopNoSuper.Checked,
				updateDirectory, !removeBonusItems.Checked, !removeFGExclusiveItems.Checked, buyMultiplier, 20, party);
		}

		private void randomizeTreasures(int[] party)
		{
			new Treasure(r1, treasureTypes.SelectedIndex, updateDirectory,
				treasureNoJ.Checked, treasureNoSuper.Checked, !removeBonusItems.Checked, !removeFGExclusiveItems.Checked, 5, party);
		}

		private void randomizeMonstersWithBoost(double xpMulti)
		{
			Bosses.establishBosses(updateDirectory, r1, zOrdeals.Checked);

			double gpMulti = gpMultiplier.SelectedIndex == 0 ? 1 :
				gpMultiplier.SelectedIndex == 1 ? 1.5 :
				gpMultiplier.SelectedIndex == 2 ? 2 :
				gpMultiplier.SelectedIndex == 3 ? 2.5 :
				gpMultiplier.SelectedIndex == 4 ? 3 :
				gpMultiplier.SelectedIndex == 5 ? 4 :
				gpMultiplier.SelectedIndex == 6 ? 5 : 10;
			int xpBoostInt = 0;
			int gpBoostInt = 0;
			Monster.MonsterBoost(updateDirectory, xpMulti, xpBoostInt, gpMulti, gpBoostInt, 5);
			Monster.AdjustMonsterDifficulty(updateDirectory, monsterDifficulty.SelectedIndex);
		}

		private void priceAdjustment()
		{
			int buyMultiplier = shopBuyPrice.SelectedIndex == 0 ? 0 : shopBuyPrice.SelectedIndex == 1 ? 1 : shopBuyPrice.SelectedIndex == 2 ? 2 : shopBuyPrice.SelectedIndex == 3 ? 4 : shopBuyPrice.SelectedIndex == 4 ? 8 : 20;
			new Weapons().adjustPrices(updateDirectory, buyMultiplier, 20);
			new Items().adjustPrices(updateDirectory, buyMultiplier, 20);
			new Armor().adjustPrices(updateDirectory, buyMultiplier, 20);
		}

		private void FF4FabulGauntlet_FormClosing(object sender, FormClosingEventArgs e)
		{
			using (StreamWriter writer = File.CreateText("lastFF4FG.txt"))
			{
				writer.WriteLine(FF4PRFolder.Text);
				writer.WriteLine(RandoSeed.Text);
				writer.WriteLine(RandoFlags.Text);
				for (int i = 0; i < 12; i++)
					for (int j = 0; j < 5; j++)
						writer.WriteLine(heroNames[i, j].Text);

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

		private void revertToDefault_click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to revert Final Fantasy IV back to vanilla?", "Final Fantasy IV: Fabul Gauntlet", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				try
				{
					NewChecksum.Text = "Reverting...";
					if (Directory.Exists(Path.Combine(FF4PRFolder.Text, "BepInEx")))
						Directory.Delete(Path.Combine(FF4PRFolder.Text, "BepInEx"), true);
					if (Directory.Exists(Path.Combine(FF4PRFolder.Text, "mono")))
						Directory.Delete(Path.Combine(FF4PRFolder.Text, "mono"), true);
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

		private void flagDefault_Click(object sender, EventArgs e)
		{
			Button btn = (Button)sender;
			if (btn.Name == "flagDefault")
			{
				RandoFlags.Text = defaultFlags;
			}
			if (btn.Name == "flagCustom1") { if (flagCustom1.Text.Length == flagLength) RandoFlags.Text = flagCustom1Text.Text; else MessageBox.Show("Invalid flag string"); }
			if (btn.Name == "flagCustom2") { if (flagCustom2.Text.Length == flagLength) RandoFlags.Text = flagCustom2Text.Text; else MessageBox.Show("Invalid flag string"); }
			if (btn.Name == "flagCustom3") { if (flagCustom3.Text.Length == flagLength) RandoFlags.Text = flagCustom3Text.Text; else MessageBox.Show("Invalid flag string"); }
			if (btn.Name == "flagCustom4") { if (flagCustom4.Text.Length == flagLength) RandoFlags.Text = flagCustom4Text.Text; else MessageBox.Show("Invalid flag string"); }
			if (btn.Name == "flagCustom5") { if (flagCustom5.Text.Length == flagLength) RandoFlags.Text = flagCustom5Text.Text; else MessageBox.Show("Invalid flag string"); }

			determineChecks(null, null);
		}

		private void FDItemLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("https://docs.google.com/spreadsheets/d/1lULu47K_qzDfWUxVXHlzmDaYO1RL7CbF3A2cvBa1OXk/edit#gid=843831314");
		}

		private void copyRaceBot_Click(object sender, EventArgs e)
		{
			Clipboard.SetText("!setmetadata seed " + RandoSeed.Text + " flags " + RandoFlags.Text + " checksum " + checkSum);
			NewChecksum.Text = "COMPLETE - checksum " + checkSum + " - copied as !setmetadata command";
		}
	}
}
