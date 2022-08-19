using System;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace ZLoad
{
    internal class Konachan
    {
        public static void Download(int photoCount, string tag)
        {
			for (int i = 0; i < photoCount; i++)
			{
				bool flag2 = tag == null;
				string url;
				if (flag2)
				{
					url = "http://konachan.com/post.json?limit=1&tags=+uncensored+-pool:309+order:random+rating:explict";
				}
				else
				{
					url = "http://konachan.com/post.json?limit=1&tags=+uncensored+" + tag + "+-pool:309+order:random+rating:explict";
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
					Console.Write("ZLoad (very secret name), made by artii\nKonachan is down or your fibre died!\nPress any button to exit the program...");
					Console.ReadKey();
					Environment.Exit(0);
				}
				bool flag4 = result == "[]";
				if (flag4)
				{
					Console.Clear();
					Console.Write("ZLoad (very secret name), made by artii\nTag not found\nPress any button to exit the program...");
					Console.ReadKey();
					Environment.Exit(0);
				}
				var definition = new
				{
					file_url = "",
					md5 = ""
				};
				var hJson = JsonConvert.DeserializeAnonymousType(resultNo2, definition);
				Directory.CreateDirectory("photos");
				string path = "photos\\" + hJson.md5 + ".png";

				if (File.Exists(path))
				{
					i--;
					continue;
				}

				WebClient webClient = new WebClient();
				webClient.DownloadFile(hJson.file_url, path);
				Console.WriteLine("Photo number " + (i + 1).ToString() + " is on yo disk!");
			}

		}
	}
}
