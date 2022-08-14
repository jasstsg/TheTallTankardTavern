using Microsoft.AspNetCore.Mvc;
using TheTallTankardTavern.Attributes;
using TheTallTankardTavern.Configuration;
using TheTallTankardTavern.Helpers;
using TheTallTankardTavern.Models;
using static TheTallTankardTavern.Configuration.ApplicationSettings;
using static TheTallTankardTavern.Configuration.Constants;

namespace TheTallTankardTavern.Controllers
{
    [Authenticated]
    public class CharacterController : BaseController<CharacterModel>
    {
        public class SpellSlotsInfo
        {
            public string ID { get; set; }
            public int SpellSlotLevel { get; set; }
            public int SpellSlotsRemaining { get; set; }
        }

        public CharacterController() : base(CharacterDataContext, FOLDER.Characters) { }

        public override IActionResult Create()
        {
            CharacterModel Character = new CharacterModel
            {
                ID = NewGUID,
                Player_Name = ContextUser.Current.Username,
                Languages = CheckboxEnumListModel<LANGUAGES>.Empty()
            };
            Character.TakeLongRest();
            return View("Create", Character);
        }

        [HttpPost]
        public IActionResult Save(CharacterModel Model, string submit)
        {
            return SaveModel(Model, submit);
        }

        public IActionResult AddModel(string cid, string id, MODEL_TYPES type, int quantity = 1)
        {
            CharacterModel Character = DataContext.GetModelFromID(cid);

            //Call the appropriate add method, save the character, return to where we came from
            switch (type)
            {
                case MODEL_TYPES.SPELL: 
                    Character.Spells.AddSingle(id);
                    DataContext.Save(Character, Folder);
                    return RedirectToAction("Index", "Spell");
                case MODEL_TYPES.ITEM: 
                    Character.Inventory.AddNewItemInstance(id, quantity);
                    DataContext.Save(Character, Folder);
                    return RedirectToAction("Index", "Item");
                case MODEL_TYPES.FEATURE: 
                    Character.Features.AddSingle(id);
                    DataContext.Save(Character, Folder);
                    return RedirectToAction("Index", "Feature");
                case MODEL_TYPES.BACKGROUND:
                    BackgroundModel Background = BackgroundDataContext.GetModelFromID(id);
                    if (!string.IsNullOrEmpty(Character.BackgroundID))
                    {
                        Character.Features.Remove(BackgroundDataContext.GetModelFromID(Character.BackgroundID).FeatureID);
                    }
                    Character.BackgroundID = Background.ID;
                    Character.Features.AddSingle(Background.FeatureID);
                    DataContext.Save(Character, Folder);
                    return RedirectToAction("Index", "Background");
                default: break;
            }

            //Redirect to Character (old functionality, still here in case of default case)
            DataContext.Save(Character, Folder);
            return RedirectToAction("Details", new { id = Character.ID });
        }

        #region Manage Info
        public IActionResult TakeLongRest(string id)
        {
            CharacterModel Character = DataContext.GetModelFromID(id);
            Character.TakeLongRest();
            DataContext.Save(Character, Folder);
            ViewData["long-rest-message"] = Character.Name + " has fully rested!";
            return View("Details", Character);
        }

        public IActionResult UseHitDie(string id)
        {
            CharacterModel Character = DataContext.GetModelFromID(id);
            Character.UseHitDie();
            DataContext.Save(Character, Folder);
            return View("Details", Character);
        }

        //For class specific points
        public IActionResult RestorePoints(string id)
        {
            CharacterModel Character = DataContext.GetModelFromID(id);
            Character.ResetUniquePoints();
            DataContext.Save(Character, Folder);
            return View("Details", Character);
        }

        [HttpPost]
        public JsonResult QuickSaveHitPoints(string cid, int hitPointsRemaining)
        {
            try
            {
                CharacterModel Character = DataContext.GetModelFromID(cid);
                Character.Hit_Points_Remaining = hitPointsRemaining;
                DataContext.Save(Character, Folder);
                return this.JsonSuccessTrue();
            }
            catch
            {
                return this.JsonSuccessFalse();
            }
        }

        [HttpPost]
        public JsonResult QuickSaveHitDice(string cid, int hitDiceRemaining)
        {
            try
            {
                CharacterModel Character = CharacterDataContext.GetModelFromID(cid);
                Character.Hit_Dice_Remaining = hitDiceRemaining;
                CharacterDataContext.Save(Character, Folder);
                return this.JsonSuccessTrue();
            }
            catch
            {
                return this.JsonSuccessFalse();
            }
        }

        [HttpPost]
        public JsonResult QuickSaveTemporaryHitPoints(string cid, int tempHitPoints)
        {
            try
            {
                CharacterModel Character = CharacterDataContext.GetModelFromID(cid);
                Character.Temp_Hit_Points = tempHitPoints;
                CharacterDataContext.Save(Character, Folder);
                return this.JsonSuccessTrue();
            }
            catch
            {
                return this.JsonSuccessFalse();
            }
        }

        [HttpPost]
        public JsonResult QuickSaveLayOnHandsPool(string cid, int layOnHandsPool)
        {
            try
            {
                CharacterModel Character = CharacterDataContext.GetModelFromID(cid);
                Character.Lay_On_Hands_Pool = layOnHandsPool;
                CharacterDataContext.Save(Character, Folder);
                return this.JsonSuccessTrue();
            }
            catch
            {
                return this.JsonSuccessFalse();
            }
        }

        public JsonResult QuickSaveKiPoints(string cid, int kiPoints)
        {
            try
            {
                CharacterModel Character = CharacterDataContext.GetModelFromID(cid);
                Character.Ki_Points = kiPoints;
                CharacterDataContext.Save(Character, Folder);
                return this.JsonSuccessTrue();
            }
            catch
            {
                return this.JsonSuccessFalse();
            }
        }

        public JsonResult QuickSaveSorceryPoints(string cid, int sorceryPoints)
        {
            try
            {
                CharacterModel Character = CharacterDataContext.GetModelFromID(cid);
                Character.Sorcery_Points = sorceryPoints;
                CharacterDataContext.Save(Character, Folder);
                return this.JsonSuccessTrue();
            }
            catch
            {
                return this.JsonSuccessFalse();
            }
        }

        public JsonResult QuickSaveWildShapes(string cid, int wildShapes)
        {
            try
            {
                CharacterModel Character = CharacterDataContext.GetModelFromID(cid);
                Character.Wild_Shapes = wildShapes;
                CharacterDataContext.Save(Character, Folder);
                return this.JsonSuccessTrue();
            }
            catch
            {
                return this.JsonSuccessFalse();
            }
        }

        public JsonResult QuickSaveCoins(string cid, string coin, int pieces)
        {
            try
            {
                CharacterModel Character = CharacterDataContext.GetModelFromID(cid);
                switch (coin)
                {
                    case "Copper": Character.Coin_Purse.Copper_Pieces = pieces; break;
                    case "Silver": Character.Coin_Purse.Silver_Pieces = pieces; break;
                    case "Gold": Character.Coin_Purse.Gold_Pieces = pieces; break;
                    case "Platinum": Character.Coin_Purse.Platinum_Pieces = pieces; break;
                }
                CharacterDataContext.Save(Character, Folder);
                return this.JsonSuccessTrue();
            }
            catch
            {
                return this.JsonSuccessFalse();
            }
        }

        public IActionResult UpdateCoinPurse(string cid, string coin, int pieces)
        {
            CharacterModel Character = CharacterDataContext.GetModelFromID(cid);
            switch (coin)
            {
                case "Copper": Character.Coin_Purse.Copper_Pieces = pieces; break;
                case "Silver": Character.Coin_Purse.Silver_Pieces = pieces; break;
                case "Gold": Character.Coin_Purse.Gold_Pieces = pieces; break;
                case "Platinum": Character.Coin_Purse.Platinum_Pieces = pieces; break;
            }
            CharacterDataContext.Save(Character, Folder);
            return PartialView("Details/_StatsAndInfo/_CoinsPartial", Character);
        }
        #endregion

        #region Manage Items
        [HttpGet]
        public IActionResult GiveItem(string cid, string inventoryId)
        {
            ViewData["inventoryId"] = inventoryId;
            return View("GiveItem", CharacterDataContext.GetModelFromID(cid));
        }

        [HttpPost]
        public IActionResult GiveItem(string giverId, string receiverId, string inventoryId)
        {
            //Get Characters
            CharacterModel Giver = CharacterDataContext.GetModelFromID(giverId);
            CharacterModel Receiver = CharacterDataContext.GetModelFromID(receiverId);

            //Give Item
            Giver.Inventory.Remove(inventoryId);
            Receiver.Inventory.Add(inventoryId);

            //Save Characters
            CharacterDataContext.Save(Giver, FOLDER.Characters);
            CharacterDataContext.Save(Receiver, FOLDER.Characters);

            //Go back to Giver's page
            return RedirectToAction("Details", new { id = Giver.ID });
        }

        public IActionResult UnequipArmour(string cid, string inventoryID)
        {
            CharacterModel Character = DataContext.GetModelFromID(cid);
            Character.Equipment.UnequipArmour(inventoryID);
            return SaveAndReturnToDetailsPartialView(Character);
        }

        public IActionResult UnequipSpellCastingFocus(string cid, string inventoryID)
        {
            CharacterModel Character = DataContext.GetModelFromID(cid);
            Character.Equipment.UnequipSpellcastingFocus(inventoryID);
            return SaveAndReturnToDetailsPartialView(Character);
        }

        public IActionResult UnequipShield(string cid, string inventoryID)
        {
            CharacterModel Character = DataContext.GetModelFromID(cid);
            Character.Equipment.UnequipShield(inventoryID);
            return SaveAndReturnToDetailsPartialView(Character);
        }

        public IActionResult UnequipTwoHand(string cid, string inventoryID)
        {
            CharacterModel Character = DataContext.GetModelFromID(cid);
            Character.Equipment.UnequipTwoHand(inventoryID);
            return SaveAndReturnToDetailsPartialView(Character);
        }

        public IActionResult UnequipMainHand(string cid, string inventoryID)
        {
            CharacterModel Character = DataContext.GetModelFromID(cid);
            Character.Equipment.UnequipMainHand(inventoryID);
            return SaveAndReturnToDetailsPartialView(Character);
        }

        public IActionResult UnequipOffHand(string cid, string inventoryID)
        {
            CharacterModel Character = DataContext.GetModelFromID(cid);
            Character.Equipment.UnequipOffHand(inventoryID);
            return SaveAndReturnToDetailsPartialView(Character);
        }

        public IActionResult UnequipAttunableItem(string cid, string inventoryID)
        {
            CharacterModel Character = DataContext.GetModelFromID(cid);
            Character.Equipment.UnequipAttunableItem(inventoryID);
            return SaveAndReturnToDetailsPartialView(Character);
        }

        public IActionResult EquipItem(string cid, string inventoryID)
        {
            CharacterModel Character = DataContext.GetModelFromID(cid);
            Character.Equipment.TryEquip(inventoryID, Character.Inventory, Character.HasFeature(SpecialFeatures.DUAL_WIELDER), Character.Proficiency_Bonus);
            return SaveAndReturnToDetailsPartialView(Character);
        }

        public IActionResult RemoveItemFromInventory(string cid, string inventoryID)
        {
            CharacterModel Character = DataContext.GetModelFromID(cid);
            Character.Inventory.Remove(inventoryID);
            return SaveAndReturnToDetailsPartialView(Character);
        }

        private IActionResult SaveAndReturnToDetailsPartialView(CharacterModel Character)
        {
            DataContext.Save(Character, Folder);
            //return PartialView("Details/_ItemsPartial", Character);
            return PartialView("Details/_DetailsPartial", Character);
        }
        #endregion

        #region Manage Spells
        [HttpPost]
        public IActionResult RemoveSpell(string cid, string sid)
        {
            try
            {
                CharacterModel Character = DataContext.GetModelFromID(cid);
                Character.Spells.Remove(sid);
                DataContext.Save(Character, Folder);
                return this.JsonSuccessTrue();
            }
            catch
            {
                return this.JsonSuccessFalse();
            }
        }

        [HttpPost]
        public JsonResult QuickSaveSpellSlots(SpellSlotsInfo spellSlotsInfo)
        {
            try
            {
                CharacterModel Character = CharacterDataContext.GetModelFromID(spellSlotsInfo.ID);
                Character.SpellSlots[spellSlotsInfo.SpellSlotLevel - 1] = spellSlotsInfo.SpellSlotsRemaining;
                CharacterDataContext.Save(Character, Folder);
                return this.JsonSuccessTrue();
            }
            catch
            {
                return this.JsonSuccessFalse();
            }
        }
        #endregion

        #region Manage Features
        [HttpPost]
        public IActionResult RemoveFeature(string cid, string fid)
        {
            try
            {
                CharacterModel Character = DataContext.GetModelFromID(cid);
                Character.Features.Remove(fid);
                DataContext.Save(Character, Folder);
                return this.JsonSuccessTrue();
            }
            catch
            {
                return this.JsonSuccessFalse();
            }
        }
        #endregion

        #region Manage Notes
        public JsonResult SaveNotes(string cid, string notes)
        {
            try
            {
                CharacterModel Character = CharacterDataContext.GetModelFromID(cid);
                Character.Notes = notes;
                CharacterDataContext.Save(Character, Folder);
                return this.JsonSuccessTrue();
            }
            catch
            {
                return this.JsonSuccessFalse();
            }
        }
        #endregion
    }
}
