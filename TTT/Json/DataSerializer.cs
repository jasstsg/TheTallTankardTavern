using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace TTT.Json
{
	public static class DataSerializer
	{
		private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
		{
			NullValueHandling = NullValueHandling.Include,
			MissingMemberHandling = MissingMemberHandling.Ignore,
			Formatting = Formatting.Indented,
			ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			
		};

		public static T JsonStringToObject<T>(string json_string)
		{
			return JsonConvert.DeserializeObject<T>(json_string, JsonSettings);
		}

		public static T JsonFileToObject<T>(string filepath)
		{
			try
			{
				using (StreamReader reader = new StreamReader(filepath))
				{
					return JsonStringToObject<T>(reader.ReadToEnd());
				}
			}
			catch (JsonSerializationException jex)
            {
				throw new JsonSerializationException($"Error while deserializing the json file: {filepath}", jex);
            }
			catch (Exception ex)
            {
				throw new Exception($"Unexpected error while trying to read and deserialize the json file: {filepath}", ex);
            }
		}

		public static IEnumerable<T> JsonDirectoryToIEnumerable<T>(string directory)
		{
			List<T> ListObject = new List<T>();
			string[] files = Directory.GetFiles(directory, "*.json");
			foreach (string file in files)
			{
				ListObject.Add(JsonFileToObject<T>(file));
			}
			return ListObject;
		}

		public static string ObjectToJsonString<T>(T obj)
		{
			return JsonConvert.SerializeObject(obj, JsonSettings);
		}

		public static void ObjectToJsonFile<T>(T obj, string filepath)
		{
			using (StreamWriter writer = new StreamWriter(filepath, append: false))
			{
				writer.Write(ObjectToJsonString(obj));
			}
		}

		public static void DeleteFile(string filename)
		{
			if (File.Exists(filename))
			{
				File.Delete(filename);
			}
		}

		public static T CopyJsonObject<T>(T model)
		{
			return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(model));
		}
	}
}
