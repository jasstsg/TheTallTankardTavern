using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TheTallTankardTavern.Helpers;
using TheTallTankardTavern.Models;
using static TheTallTankardTavern.Configuration.ApplicationSettings;
using static TheTallTankardTavern.Configuration.Constants;

namespace TheTallTankardTavern.Controllers
{
    public class CargoController : Controller
    {
        public IActionResult Index()
        {
            if (CargoDataContext.Count <= 0)
            {
                CargoDataContext.Add(new CargoModel() { ID = Guid.NewGuid().ToString() });
            }
            return View(CargoDataContext.Single());
        }

        public IActionResult Deposit(string inventoryID, string cid)
        {
            CargoModel Cargo = CargoDataContext.Single();
            CharacterModel Character = CharacterDataContext.GetModelFromID(cid);

            Cargo.Add(inventoryID);
            Character.Inventory.Remove(inventoryID);

            CargoDataContext.Save(Cargo, FOLDER.Cargo);
            CharacterDataContext.Save(Character, FOLDER.Cargo);

            return PartialView("Cargo/_CargoItemsPartial", Cargo);
        }

        public IActionResult Withdraw(string inventoryID, string cid)
        {
            CargoModel Cargo = CargoDataContext.Single();
            CharacterModel Character = CharacterDataContext.GetModelFromID(cid);

            Cargo.Remove(inventoryID);
            Character.Inventory.Add(inventoryID);

            CargoDataContext.Save(Cargo, FOLDER.Cargo);
            CharacterDataContext.Save(Character, FOLDER.Cargo);

            return PartialView("Cargo/_CargoItemsPartial", Cargo);
        }
    }
}
