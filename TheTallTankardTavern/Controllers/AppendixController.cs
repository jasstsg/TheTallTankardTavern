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
        /*
         private const string SHARED_ONENOTE_MAP_PAGE = @"https://onedrive.live.com/view.aspx?resid=73768C612996B132%211197&id=documents&wd=target%28World%20map%20of%20Esidar%2FEsidar.one%7C289B59EE-9DA9-4173-AE2E-F47FFED47772%2FEsidar%7C7AB531E7-CF1B-4A47-8B1C-5727DC946FD6%2F%29
onenote:https://d.docs.live.net/73768c612996b132/Documents/Shared%20Player%20Knowledge/World%20map%20of%20Esidar/Esidar.one#Esidar&section-id={289B59EE-9DA9-4173-AE2E-F47FFED47772}&page-id={7AB531E7-CF1B-4A47-8B1C-5727DC946FD6}&end";
         */
        public IActionResult Index() { return View(); }
        public IActionResult Calendar() { return View(); }
        public IActionResult CharacterAdvancement() { return View(ApplicationSettings.ConfigurationSettings.CharacterAdvancement); }
        public IActionResult Combat() { return View(); }
        public IActionResult Homebrews() { return View(); }
        public IActionResult HomebrewConditions() { return View(); }

        public IActionResult Maps() { return View(ApplicationSettings.MapTree); /*  return new RedirectResult(SHARED_ONENOTE_MAP_PAGE);  */ }
        public IActionResult ReloadMap(string imgsrc)
        {
            string mapPath = Path.ChangeExtension(imgsrc.Replace("\\images\\custom\\maps\\", ""), null);
            return View("Maps", MapTree.GetMap(ApplicationSettings.MapTree, new List<string>(mapPath.Split('\\'))));
        }

        public IActionResult MassiveDamage() { return View(); }
        public IActionResult SpellSlots() { return View(); }
    }
}