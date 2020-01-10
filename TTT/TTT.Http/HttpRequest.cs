using System.IO;
using System.Net;

namespace TTT.Http
{
	public class HttpRequest
	{
		public HttpResponse Get(string url)
		{
			HttpWebRequest obj = (HttpWebRequest)WebRequest.Create(url);
			obj.AutomaticDecompression = (DecompressionMethods.GZip | DecompressionMethods.Deflate);
			using (HttpWebResponse response = (HttpWebResponse)obj.GetResponse())
			{
				using (Stream stream = response.GetResponseStream())
				{
					using (StreamReader reader = new StreamReader(stream))
					{
						return new HttpResponse
						{
							Response = reader.ReadToEnd()
						};
					}
				}
			}
		}
	}
}
