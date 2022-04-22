using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TheTallTankardTavern.Attributes;
using TheTallTankardTavern.Configuration;
using TheTallTankardTavern.Helpers;
using TheTallTankardTavern.Models;
using static TheTallTankardTavern.Configuration.ApplicationSettings;
using static TheTallTankardTavern.Configuration.Constants;

namespace TheTallTankardTavern.Controllers
{
    [Authenticated]
    public class StorageController : Controller
    {
        public IActionResult Index()
        {
            if (StorageDataContext.Count <= 0)
            {
                StorageDataContext.Add(new StorageModel() { ID = Guid.NewGuid().ToString() });
            }

            StorageModel Storage = StorageDataContext.Single();
            if (!Storage.IsLocked || ContextUser.IsAdminOrDM)
            {
                return View(Storage);
            }

            return RedirectToAction("Locked");

        }

        public IActionResult Locked()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveSettings(StorageModel Storage, string submit)
        {
            StorageDataContext.Save(Storage, FOLDER.Storage);
            return View("Index", Storage);
        }

        public IActionResult DepositItem(string cid, string inventoryID)
        {
            StorageModel Storage = StorageDataContext.Single();
            CharacterModel Character = CharacterDataContext.GetModelFromID(cid);

            Storage.Inventory.Add(inventoryID);
            Character.Inventory.Remove(inventoryID);

            StorageDataContext.Save(Storage, FOLDER.Storage);
            CharacterDataContext.Save(Character, FOLDER.Characters);

            return PartialView("_StorageItemsPartial", Storage);
        }

        public IActionResult WithdrawItem(string cid, string inventoryID)
        {
            StorageModel Storage = StorageDataContext.Single();
            CharacterModel Character = CharacterDataContext.GetModelFromID(cid);

            Storage.Inventory.Remove(inventoryID);
            Character.Inventory.Add(inventoryID);

            StorageDataContext.Save(Storage, FOLDER.Storage);
            CharacterDataContext.Save(Character, FOLDER.Characters);

            return PartialView("_StorageItemsPartial", Storage);
        }
    }
}
