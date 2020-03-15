using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static TheTallTankardTavern.Configuration.Constants;
using static TheTallTankardTavern.Configuration.ApplicationSettings;

namespace TheTallTankardTavern.Controllers
{
    public class AppendixController : Controller
    {
        public IActionResult Index() {  return View(); }
        public IActionResult Calendar() { return View(); }
        public IActionResult CharacterAdvancement() { return View(ConfigurationSettings.CharacterAdvancement); }
        public IActionResult Homebrews() { return View(); }
        public IActionResult Maps() { return View(); }
        public IActionResult SpellSlots() { return View(); }
    }
}