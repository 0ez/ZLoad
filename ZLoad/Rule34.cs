using System;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace ZLoad
{
    internal class Rule34
    {
        public static void Download(int photoCount, string tag)
        {
			WebClient webClient = new WebClient();
			for (int i = 0; i < photoCount; i++)
			{
				bool flag2 = tag == null;
				string url;
				if (flag2)
				{
					url = "https://api.rule34.xxx/index.php?page=dapi&s=post&q=index&json=1&limit=1" + "&pid=" + i;
				}
				else
				{
					url = "https://api.rule34.xxx/index.php?page=dapi&s=post&q=index&json=1&limit=1&tags=" + tag + "&pid=" + i;
				}
				HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
				HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();
				StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream());
				string result = streamReader.ReadToEnd();
				string resultString = result.ToString();
				string resultNo = resultString.Remove(0, 1);
				string resultNo2 = resultNo.Remove(resultNo.Length - 1);
				bool flag3 = httpResponse.StatusCode == HttpStatusCode.Forbidden || httpResponse.StatusCode == HttpStatusCode.NotFound || httpResponse.StatusCode == HttpStatusCode.Unauthorized;
				if (flag3)
				{
					Console.Clear();
					Console.Write("ZLoad (very secret name), made by parkie (github 0ez)\nR34 is down or your fibre died!\nPress any button to exit the program...");
					Console.ReadKey();
					Environment.Exit(0);
				}
				bool flag4 = result == "[]";
				if (flag4)
				{
					Console.Clear();
					Console.Write("ZLoad (very secret name), made by parkie (github 0ez)\nTag not found\nPress any button to exit the program...");
					Console.ReadKey();
					Environment.Exit(0);
				}
				var definition = new
				{
					file_url = "",
					hash = ""
				};
				var hJson = JsonConvert.DeserializeAnonymousType(resultNo2, definition);
				Directory.CreateDirectory("photos");
				string path = "photos\\" + hJson.hash + ".png";

				if (File.Exists(path) || hJson.hash == "")
				{
					i++;
					continue;
				}

				webClient.DownloadFile(hJson.file_url, path);
				int fileCount = Directory.GetFiles("photos\\").Length;
				Console.WriteLine("Photo number " + fileCount + " is on yo disk!");
			}
		}
    }
}
