using System.Collections.Generic;
using System.IO;
using TTT.Json;

namespace TheTallTankardTavern.Providers
{
	public class JsonDataProvider
	{
		public string AppDataRoot
		{
			get;
			set;
		}

		public void ModelToJsonFile<T>(T Model, string sub_path)
		{
			DataSerializer.ObjectToJsonFile(Model, AppDataRoot + "\\" + sub_path);
		}

		public List<T> JsonFolderToDataContext<T>(string sub_path)
		{
			string dir = AppDataRoot + "\\" + sub_path;
			if (!Directory.Exists(dir))
			{
				Directory.CreateDirectory(dir);
			}
			List<T> ModelList = new List<T>();
			string[] JsonFiles = Directory.GetFiles(dir, "*.json");
			string[] array = JsonFiles;
			foreach (string filename in array)
			{
				ModelList.Add(DataSerializer.JsonFileToObject<T>(filename));
			}
			return ModelList;
		}

		public void DeleteJsonFile(string sub_path)
		{
			DataSerializer.DeleteFile(AppDataRoot + "\\" + sub_path);
		}
	}
}
