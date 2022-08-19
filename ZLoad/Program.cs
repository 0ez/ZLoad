using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace ZLoad
{
	// Token: 0x02000003 RID: 3
	internal class Program
	{
		private static void Main(string[] args)
		{
			MessageBox.Show("Any crashes are mostly due to the tag not existing.\nI will maybe fix it sometime...", "ZLoad", MessageBoxButtons.OK, MessageBoxIcon.Information);
			Console.Title = "";
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.Write("ZLoad (very secret name), made by parkie (github 0ez)\nHow much photos needed today? ");
			string photoCount_STR = Console.ReadLine();
			Console.Clear();
			Console.Write("ZLoad (very secret name), made by parkie (github 0ez)\nWhat tag you want? Maybe nekos today?\n");
			string tag = Console.ReadLine();
			Console.Clear();
			Console.Write("ZLoad (very secret name), made by parkie (github 0ez)\nSelect the service:\n1 - Rule34\n2 - konachan\n");
			string option = Console.ReadLine();
			int option_int = int.Parse(option);

			switch(option_int)
            {
				case 1:
					Rule34.Download(int.Parse(photoCount_STR), tag);
					break;
				case 2:
					Konachan.Download(int.Parse(photoCount_STR), tag);
					break;
            }

			Console.WriteLine("\n");

			Console.Clear();
			Console.Write("ZLoad (very secret name), made by artii\n");
			Console.WriteLine("All photos are done downloading! Happy fapp... I MEAN WHAT?!\nWant me to zip them? (y/n)");
			string zip = Console.ReadLine();
			bool flag5 = zip == "y";
			if (flag5)
			{
				string date = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
				Console.WriteLine("\n\nZipping photos...\n");
				bool flag6 = tag != null;
				if (flag6)
				{
					ZipFile.CreateFromDirectory("photos", tag + "-" + date + "-zLoad.zip");
				}
				else
				{
					ZipFile.CreateFromDirectory("photos", date + "-zLoad.zip");
				}
				Directory.Delete("photos");
				Console.WriteLine("Zipping done!\n");

			}
			else
			{
				Console.WriteLine("Ok, no zip for you!\nPress any button to exit the program...");
			}
			Console.ReadKey();
		}
	}
}
