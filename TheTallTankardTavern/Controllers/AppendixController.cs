using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static TheTallTankardTavern.Configuration.Constants;
using static TheTallTankardTavern.Configuration.ApplicationSettings;
using System.IO;
using TheTallTankardTavern.Models;

namespace TheTallTankardTavern.Controllers
{
    public class AppendixController : Controller
    {
        public IActionResult Index() { return View(); }
        public IActionResult Calendar() { return View(); }
        public IActionResult CharacterAdvancement() { return View(ConfigurationSettings.CharacterAdvancement); }
        public IActionResult Combat() { return View(); }
        public IActionResult Homebrews() { return View(); }
        public IActionResult HomebrewConditions() { return View(); }

        public IActionResult Maps()
        {
            return LoadMap(ImagesFolder + "\\custom\\maps\\Esidar");
        }

        public IActionResult LoadMap(string folderPath)
        {
            MapModel Map = new MapModel(folderPath) { Parent = new MapModel(Directory.GetParent(folderPath).FullName) };
            BuildMapTree(Map);
            return View("Maps", Map);
        }

        private void BuildMapTree(MapModel Map)
        {
            string[] childMaps = Directory.GetDirectories(Map.ImageFolder, "*", SearchOption.TopDirectoryOnly);
            if (childMaps.Length <= 0)
            {
                return;
            }
            foreach (string childMap in childMaps)
            {
                MapModel ChildMap = new MapModel(childMap);
                BuildMapTree(ChildMap);
                Map.Children.Add(ChildMap);
            }
        }

        public IActionResult MassiveDamage() { return View(); }
        public IActionResult SpellSlots() { return View(); }
    }
}