using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using TheTallTankardTavern.Models;
using TheTallTankardTavern.Providers;
using TTT.Common.Abstractions;
using TTT.Items;
using TTT.Json;
using static TheTallTankardTavern.Configuration.Constants;

namespace TheTallTankardTavern.Configuration
{
	public static class ApplicationSettings
	{
		#region Properties
		public static IHttpContextAccessor HttpContextAccessor { get; private set; }
        public static IWebHostEnvironment WebHostEnvironment { get; private set; }

		public static string WebRoot { get; private set; }
        public static string AppConfigFolder { get; private set; }
        public static string AppDataFolder { get; private set; }
        public static string BackupDataFolder { get; private set; }
		public static string ImagesFolder { get; private set; }

		public static MapTree MapTree { get; private set; }

        public static JsonDataProvider JsonDataProvider { get; } = new JsonDataProvider();
		public static ConfigurationSettings ConfigurationSettings { get; } = new ConfigurationSettings();
		
        public static List<BackgroundModel> BackgroundDataContext { get; private set; } = new List<BackgroundModel>();
		public static List<CharacterModel> CharacterDataContext { get; private set; } = new List<CharacterModel>();
		public static List<FeatureModel> FeatureDataContext { get; private set; } = new List<FeatureModel>();
		public static List<ItemModel> ItemDataContext { get; private set; } = new List<ItemModel>();
		public static List<PartyModel> PartyDataContext { get; private set; } = new List<PartyModel>();
		public static List<SpellModel> SpellDataContext { get; private set; } = new List<SpellModel>();
		public static List<StorageModel> StorageDataContext { get; private set; } = new List<StorageModel>();
		public static List<UserModel> UserDataContext { get; private set; } = new List<UserModel>();
        #endregion

        public static void Initialize(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
		{
			HttpContextAccessor = httpContextAccessor;
            WebHostEnvironment = webHostEnvironment;

			WebRoot = WebHostEnvironment.WebRootPath;
			AppConfigFolder = Path.Combine(WebHostEnvironment.WebRootPath, "App_Config");
			AppDataFolder = Path.Combine(WebHostEnvironment.WebRootPath, "App_Data");
			BackupDataFolder = Path.Combine(WebHostEnvironment.WebRootPath, "Backup_Data");
			ImagesFolder = Path.Combine(WebHostEnvironment.WebRootPath, "images");

			MapTree = new MapTree(MAP_ROOT);

			JsonDataProvider.AppDataRoot = AppDataFolder;

			ReloadConfiguration();
			ReloadDataContext();
		}

		public static void ReloadConfiguration()
		{
			ConfigurationSettings.Classes.Clear();
			ConfigurationSettings.Races.Clear();
			ConfigurationSettings.Alignments.Clear();
			ConfigurationSettings.CharacterAdvancement.Clear();
			ConfigurationSettings.Item_Types.Clear();

			ConfigurationSettings.Classes = GetJsonConfig<string, string[]>("classes");
			ConfigurationSettings.Races = GetJsonConfig<string, string[]>("races");
			ConfigurationSettings.Alignments = GetJsonConfig<string, string[]>("alignment");
			ConfigurationSettings.CharacterAdvancement = GetJsonConfig<int, int>("character_advancement");
			ConfigurationSettings.Item_Types = GetJsonConfig<string, string[]>("item_types");
		}

		public static void ReloadDataContext()
		{
			BackgroundDataContext.Clear();
			CharacterDataContext.Clear();
			ItemDataContext.Clear();
			FeatureDataContext.Clear();
			PartyDataContext.Clear();
			SpellDataContext.Clear();
			StorageDataContext.Clear();
			UserDataContext.Clear();

			BackgroundDataContext.LoadDataContext(FOLDER.Backgrounds);
			StorageDataContext.LoadDataContext(FOLDER.Storage);
			CharacterDataContext.LoadDataContext(FOLDER.Characters);
			ItemDataContext.LoadDataContext(FOLDER.Items);
			FeatureDataContext.LoadDataContext(FOLDER.Features);
			FeatureDataContext.LoadDataContext(FOLDER.Imported_Features);
			PartyDataContext.LoadDataContext(FOLDER.Party);
			SpellDataContext.LoadDataContext(FOLDER.Spells);
			SpellDataContext.LoadDataContext(FOLDER.Imported_Spells);
			UserDataContext.LoadDataContext(FOLDER.User);

			BackupData();
		}

        #region Private Methods
        private static Dictionary<K, V> GetJsonConfig<K, V>(string jsonConfigFileName)
		{
			return DataSerializer.JsonFileToObject<Dictionary<K, V>>(AppConfigFolder + "\\" + jsonConfigFileName + ".json");
		}

		private static void LoadDataContext<T>(this List<T> DataContext, FOLDER folder) where T : IFileDataModel
		{
			DataContext.AddRange(JsonDataProvider.JsonFolderToDataContext<T>(folder.ToString()));
		}

		private static void BackupData()
		{
			if (!Directory.Exists(BackupDataFolder))
			{
				Directory.CreateDirectory(BackupDataFolder);
			}
			ZipFile.CreateFromDirectory(AppDataFolder, BackupDataFolder + "\\" + DateTime.Now.ToString("yyyyMMddThhmmss") + ".zip", CompressionLevel.Optimal, includeBaseDirectory: false);
		}
        #endregion
    }
}
