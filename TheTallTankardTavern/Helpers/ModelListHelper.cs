using System;
using System.Collections.Generic;
using System.Linq;
using TheTallTankardTavern.Configuration;
using TheTallTankardTavern.Enumerators;
using TheTallTankardTavern.Models;
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
		public static T GetModelFromID<T>(this IEnumerable<T> ModelList, string ID) where T : BaseModel
		{
			return ModelList?.First(m => m.ID.Equals(ID)) ?? NewTModel<T>();
		}

		public static string GetNameFromID<T>(this IEnumerable<T> ModelList, string ID) where T : BaseModel
		{
			return ModelList.GetModelFromID(ID).Name;
		}

		public static bool Exists<T>(this List<T> ModelList, string ID) where T : BaseModel
		{
			return ModelList.Exists((T m) => m.ID == ID);
		}

		public static bool RemoveFirst<T>(this List<T> ModelList, string ID) where T : BaseModel
		{
			return ModelList.Remove(ModelList.GetModelFromID(ID));
		}

		public static void Save<T>(this List<T> ModelList, T Model, FOLDER folder) where T : BaseModel
		{
			try
			{
				if (ModelList.Exists(Model.ID))
				{
					ModelList.Delete(Model.ID, folder);
				}
				ModelList.Add(Model);
			}
			catch (Exception ex)
			{
				Model.ID = null;
				throw new Exception("Error while adding Model to ModelList on Save Action.\nModelList = " + ModelList.ToString() + "\nModel = " + Model.ToString(), ex);
			}
			ApplicationSettings.JsonDataProvider.ModelToJsonFile(Model, folder.ToString() + "\\" + Model.ID + ".json");
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

		private static T NewTModel<T>()
		{
			return (T)Activator.CreateInstance(typeof(T));
		}
	}
}
