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
                Player_Name = ContextUser.GetContextUser.Username,
                Languages = CheckBoxListModel<LANGUAGES>.Empty()
            };
            Character.TakeLongRest();
            return View("Create", Character);
        }

        [HttpPost]
        public IActionResult Save(CharacterModel Model, string submit)
        {
            return SaveModel(Model, submit);
        }

        public IActionResult AddModel(string cid, string id, MODEL_TYPES type)
        {
            CharacterModel Character = DataContext.GetModelFromID(cid);

            switch (type)
            {
                case MODEL_TYPES.SPELL: Character.Spells.AddSingle(id); break;
                case MODEL_TYPES.ITEM: Character.Inventory.Add(id); break;
                case MODEL_TYPES.FEATURE: Character.Features.AddSingle(id); break;
                case MODEL_TYPES.BACKGROUND:
                    BackgroundModel Background = BackgroundDataContext.GetModelFromID(id);
                    if (!string.IsNullOrEmpty(Character.BackgroundID))
                    {
                        Character.Features.Remove(BackgroundDataContext.GetModelFromID(Character.BackgroundID).FeatureID);
                    }
                    Character.BackgroundID = Background.ID;
                    Character.Features.AddSingle(Background.FeatureID);
                    break;
                default: break;
            }
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

        //For Ki Points, Sorcery Points
        public IActionResult RestorePoints(string id)
        {
            CharacterModel Character = DataContext.GetModelFromID(id);
            Character.RestorePoints();
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
        #endregion

        #region Manage Items
        public IActionResult UnequipItem(string cid, string iid)
        {
            CharacterModel Character = DataContext.GetModelFromID(cid);
            Character.Inventory.Add(iid);
            Character.Equipment.Unequip(iid);
            return SaveAndReturnToItemsPartialView(Character);
        }

        public IActionResult EquipItem(string cid, string iid)
        {
            CharacterModel Character = DataContext.GetModelFromID(cid);
            Character.Equipment.TryEquip(iid);
            return SaveAndReturnToItemsPartialView(Character);
        }

        public IActionResult RemoveItemFromEquipment(string cid, string iid)
        {
            CharacterModel Character = DataContext.GetModelFromID(cid);
            Character.Equipment.Unequip(iid);
            return SaveAndReturnToItemsPartialView(Character);
        }

        public IActionResult RemoveItemFromInventory(string cid, string iid)
        {
            CharacterModel Character = DataContext.GetModelFromID(cid);
            Character.Inventory.Remove(iid);
            return SaveAndReturnToItemsPartialView(Character);
        }

        private IActionResult SaveAndReturnToItemsPartialView(CharacterModel Character)
        {
            DataContext.Save(Character, Folder);
            return PartialView("Details/_ItemsPartial", Character);
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
                    case "Gold":  Character.Coin_Purse.Gold_Pieces = pieces; break;
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
            return PartialView("Details/_Items/_CoinsPartial", Character);
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
