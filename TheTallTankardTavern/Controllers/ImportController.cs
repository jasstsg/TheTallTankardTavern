using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TheTallTankardTavern.Attributes;
using TheTallTankardTavern.Configuration;
using TheTallTankardTavern.Helpers;
using TheTallTankardTavern.Models;
using TTT.Json;
using static TheTallTankardTavern.Configuration.Constants;
using static TheTallTankardTavern.Configuration.ApplicationSettings;

namespace TheTallTankardTavern.Controllers
{
	[Authorized(ROLES.Administrator)]
	public class ImportController : Controller
	{
		private readonly string DND_5E_API_SPELLS_FILE_PATH = $"{AppConfigFolder}\\5e-SRD-Spells.json";
		private readonly string DND_5E_API_CLASS_FEATURES_FILE_PATH = $"{AppConfigFolder}\\5e-SRD-Class-Features.json";

		public IActionResult Index()
		{
			return View("Index");
		}

		private void InitializeImport<ImportModelType>(string importFilePath, string importDirectoryName, out List<ImportModelType> ImportedElementList)
		{
			if (!System.IO.File.Exists(importFilePath))
			{
				throw new Exception(importFilePath + "file does not exist");
			}
			ImportedElementList = DataSerializer.JsonFileToObject<List<ImportModelType>>(importFilePath);
			string importDirectoryPath = ApplicationSettings.AppDataFolder + "\\" + importDirectoryName;
			if (Directory.Exists(importDirectoryPath))
			{
				Directory.Delete(importDirectoryPath, recursive: true);
			}
			Directory.CreateDirectory(importDirectoryPath);
		}

		public IActionResult ImportDND5EAPISpells()
		{
			try
			{
				string ImportedSpellsFolder = FOLDER.Imported_Spells.ToString();
				InitializeImport(DND_5E_API_SPELLS_FILE_PATH, ImportedSpellsFolder, out List<Dnd5eApiSpellModel> ImportedSpells);
				foreach (Dnd5eApiSpellModel spell in ImportedSpells)
				{
					StringBuilder DescBuilder = new StringBuilder();
					StringBuilder HLDBuilder = new StringBuilder();
					string[] desc2 = spell.Desc;
					foreach (string desc in desc2)
					{
						DescBuilder.Append(desc);
						DescBuilder.Append("\n");
					}
					if (spell.Higher_Level != null && spell.Higher_Level.Length != 0)
					{
						string[] higher_Level = spell.Higher_Level;
						foreach (string hdl in higher_Level)
						{
							HLDBuilder.Append(hdl);
							HLDBuilder.Append("\n");
						}
					}
					else
					{
						HLDBuilder.Append("");
					}
					List<string> Classes = new List<string>();
					NameAndUrlModel[] classes = spell.Classes;
					foreach (NameAndUrlModel @class in classes)
					{
						Classes.Add(@class.Name);
					}
					List<string> Subclasses = new List<string>();
					NameAndUrlModel[] subclasses = spell.Subclasses;
					foreach (NameAndUrlModel subclass in subclasses)
					{
						Subclasses.Add(subclass.Name);
					}
					string components = string.Join(',', spell.Components).ToUpper();
					SpellModel Spell = new SpellModel
					{
						ID = Guid.NewGuid().ToString(),
						Name = spell.Name,
						Desc = DescBuilder.ToString(),
						Higher_Level_Desc = HLDBuilder.ToString(),
						Range = spell.Range,
						Verbal_Components = components.Contains("V"),
						Somatic_Components = components.Contains("S"),
						Material_Components = components.Contains("M"),
						Ritual = spell.Ritual,
						Duration = spell.Duration,
						Concentration = spell.Concentration,
						Casting_Time = spell.Casting_Time,
						Level = spell.Level,
						School = spell.School.Name,
						Classes = Classes,
						Subclasses = Subclasses
					};
					ApplicationSettings.JsonDataProvider.ModelToJsonFile(Spell, ImportedSpellsFolder + "\\" + Spell.ID + ".json");
				}
				base.ViewData["Error"] = "Successfully imported spells.";
			}
			catch (Exception ex)
			{
				base.ViewData["Error"] = "Error occured during spell importing: " + ex.Message;
			}
			ApplicationSettings.ReloadDataContext();
			return Index();
		}
		public IActionResult ImportDND5EAPIClassFeatures()
		{
			try
			{
				string ImportedFolder = FOLDER.Imported_Features.ToString();
				InitializeImport(DND_5E_API_CLASS_FEATURES_FILE_PATH, ImportedFolder, out List<Dnd5eApiFeatureModel> Imported);
				foreach (Dnd5eApiFeatureModel model in Imported)
				{
					StringBuilder DescBuilder = new StringBuilder();
					string[] desc2 = model.Desc;
					foreach (string desc in desc2)
					{
						DescBuilder.Append(desc);
						DescBuilder.Append("\n");
					}
					FeatureModel Feature = new FeatureModel
					{
						ID = Guid.NewGuid().ToString(),
						Name = model.Name,
						Desc = DescBuilder.ToString(),
						Class = model.Class.Name, 
						Subclass = model.Subclass.Name, 
						IsClassFeature = true
					};
					Feature.Prerequisite = $"{model.Class.Name} {model.Subclass.Name.AddBraces()}";
					ApplicationSettings.JsonDataProvider.ModelToJsonFile(Feature, ImportedFolder + "\\" + Feature.ID + ".json");
				}
				base.ViewData["Error"] = "Successfully imported.";
			}
			catch (Exception ex)
			{
				base.ViewData["Error"] = "Error occured during while importing: " + ex.Message;
			}
			ApplicationSettings.ReloadDataContext();
			return Index();
		}
	}
}
