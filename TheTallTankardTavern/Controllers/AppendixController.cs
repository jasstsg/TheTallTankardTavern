using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheTallTankardTavern.Configuration;
using System.IO;
using TheTallTankardTavern.Models;

namespace TheTallTankardTavern.Controllers
{
    public class AppendixController : Controller
    {
        public IActionResult Index() { return View(); }
        public IActionResult Calendar() { return View(); }
        public IActionResult CharacterAdvancement() { return View(ApplicationSettings.ConfigurationSettings.CharacterAdvancement); }
        public IActionResult Combat() { return View(); }
        public IActionResult Homebrews() { return View(); }
        public IActionResult HomebrewConditions() { return View(); }

        public IActionResult Maps() {  return View(ApplicationSettings.MapTree); }
        public IActionResult ReloadMap(string imgsrc)
        {
            string mapPath = Path.ChangeExtension(imgsrc.Replace("\\images\\custom\\maps\\", ""), null);
            return View("Maps", MapTree.GetMap(ApplicationSettings.MapTree, new List<string>(mapPath.Split('\\'))));
        }

        public IActionResult MassiveDamage() { return View(); }
        public IActionResult SpellSlots() { return View(); }
    }
}