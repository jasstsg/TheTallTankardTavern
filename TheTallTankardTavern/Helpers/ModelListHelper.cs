using System;
using System.Collections.Generic;
using System.Linq;
using TheTallTankardTavern.Configuration;
using TTT;
using TTT.Common.Abstractions;
using TTT.Enumerator;
using static TheTallTankardTavern.Configuration.Constants;

namespace TheTallTankardTavern.Helpers
{
	public static class ModelListHelper
	{
		public static T GetModelFromName<T>(this IEnumerable<T> ModelList, string name) where T : BaseModel
		{
			return ModelList.First((T m) => m.Name == name);
		}

		/// <summary>
		/// If ID is null or empty, or if the ID is not found, this returns a new instance of T.
		/// </summary>
		public static T GetModelFromID<T>(this IEnumerable<T> ModelList, string ID) where T : IFileDataModel
		{
			return ModelList.FirstOrDefault(m => m.ID.Equals(ID)) ?? NewTModel<T>();
		}

		public static string GetNameFromID<T>(this IEnumerable<T> ModelList, string ID) where T : BaseModel
		{
			return ModelList.GetModelFromID(ID).Name;
		}

		public static bool Exists<T>(this List<T> ModelList, string ID) where T : IFileDataModel
		{
			return ModelList.Exists((T m) => m.ID == ID);
		}

		public static bool RemoveFirst<T>(this List<T> ModelList, string ID) where T : IFileDataModel
		{
			return ModelList.Remove(ModelList.GetModelFromID(ID));
		}

		/// <summary>
		/// Tries to merge the NewModel with an existing model of the same ID, if not it adds it to the list.
		/// </summary>
		public static T Save<T>(this List<T> ModelList, T NewModel, FOLDER folder) where T : IFileDataModel
		{
			try
			{
				if (ModelList.Exists(m => m.ID.Equals(NewModel.ID)))
				{
					T Model = ModelList.GetModelFromID(NewModel.ID);
					Model.Merge(NewModel);
					ApplicationSettings.JsonDataProvider.ModelToJsonFile(Model, $"{folder.ToString()}\\{Model.ID}.json");
					return Model;
				}
                else
                {
					ModelList.Add(NewModel);
					ApplicationSettings.JsonDataProvider.ModelToJsonFile(NewModel, $"{folder.ToString()}\\{NewModel.ID}.json");
					return NewModel;
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error while saving Model.\nModelList = " + ModelList.ToString() + "\nModel = " + NewModel.ToString(), ex);
			}
		}

		public static void Delete<T>(this List<T> ModelList, string ID, FOLDER folder) where T : BaseModel
		{
			ModelList.RemoveAll((T m) => m.ID == ID);
			ApplicationSettings.JsonDataProvider.DeleteJsonFile(folder.ToString() + "\\" + ID + ".json");
		}

		public static TEnum[] AllValues<TEnum>()
		{
			return typeof(TEnum).EnumToEnumArray<TEnum>();
		}

		private static T NewTModel<T>() where T : IFileDataModel
		{
			return (T)Activator.CreateInstance(typeof(T));
		}
	}
}
