using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TheTallTankardTavern.Models;
using TheTallTankardTavern.Helpers;
using static TheTallTankardTavern.Configuration.ApplicationSettings;
using static TheTallTankardTavern.Configuration.Constants;
using System;
using TheTallTankardTavern.Attributes;
using TheTallTankardTavern.Configuration;
using System.Collections.Generic;

namespace TheTallTankardTavern.Controllers
{
    [Authenticated]
    public class PartyController : Controller
    {
        public IEnumerable<PartyModel> Parties
        {
            get
            {
                return ContextUser.IsAdminOrDM ? PartyDataContext : ContextUser.Current.Parties;
            }
        }

        public IActionResult Index()
        {
            if (!ContextUser.IsAdminOrDM && Parties.Count() == 1)
            {
                return View("Details", Parties.Single());
            }
            return View(Parties?.OrderBy(x => x.Name));
        }

        public IActionResult Create()
        {
            return View("Create", new PartyModel()
            {
                ID = Guid.NewGuid().ToString()
            });
        }

        public IActionResult Edit(string id)
        {
            return View("Create", PartyDataContext.GetModelFromID(id));
        }

        [HttpPost]
        public IActionResult Save(PartyModel Model, string submit)
        {
            PartyModel SavedModel = PartyDataContext.Save(Model, FOLDER.Party);
            switch (submit)
            {
                case TAGHELPER.SUBMIT_TEXT.SAVE_AND_CONTINUE:  
                    return RedirectToAction("Edit", new { id = SavedModel.ID });
                case TAGHELPER.SUBMIT_TEXT.SAVE_AND_FINISH:
                default:
                    return ControllerHelper.ViewExists(this, "Details") ?
                        RedirectToAction("Details", new { id = SavedModel.ID }) : RedirectToAction("Index");
            }
        }

        public IActionResult Details(string id)
        {
            return View(PartyDataContext.GetModelFromID(id));
        }

        public IActionResult Delete(string id)
        {
            return View("Delete", new PartyModel() { ID = id });
        }

        [HttpPost]
        public IActionResult Delete(PartyModel Model)
        {
            if (PartyDataContext.GetModelFromID(Model.ID).Name.Equals(Model.Name))
            {
                PartyDataContext.Delete(Model.ID, FOLDER.Party);
                return View("Index", Parties);
            }
            else
            {
                ViewData["msg"] = "The typed name did not match.";
                return Delete(Model.ID);
            }
        }

        [HttpPost]
        public IActionResult Add(PartyModel Party)
        {
            if (string.IsNullOrEmpty(Party.ID))
            {
                Party.ID = Guid.NewGuid().ToString();
            }

            Party.Members.Add(new MemberModel()
            {
                CharacterId = Party.NewMemberId,
                Initiative = 0
            });

            PartyDataContext.Save(Party, FOLDER.Party);

            return View("Details", Party);
        }

        // Not used
        public IActionResult Remove(string cid)
        {
            PartyModel Party = PartyDataContext.Single();
            Party.Members.RemoveAll(m => m.CharacterId.Equals(cid));
            PartyDataContext.Save(Party, FOLDER.Party);

            return View("Index", Party);
        }

        [HttpPost]
        public JsonResult QuickSaveMemberInitiative(string id, string cid, int initiative)
        {
            try
            {
                PartyModel Party = PartyDataContext.GetModelFromID(id);
                Party.Members.Find(m => m.CharacterId.Equals(cid)).Initiative = initiative;
                PartyDataContext.Save(Party, FOLDER.Party);
                return this.JsonSuccessTrue();
            }
            catch
            {
                return this.JsonSuccessFalse();
            }
        }

        [HttpPost]
        public JsonResult QuickSaveDate(string id, string date)
        {
            try
            {
                PartyModel Party = PartyDataContext.GetModelFromID(id);
                Party.Date = date;
                PartyDataContext.Save(Party, FOLDER.Party);
                return this.JsonSuccessTrue();
            }
            catch
            {
                return this.JsonSuccessFalse();
            }
        }

        public IActionResult ResetInitiative(string id)
        {
            PartyModel Party = PartyDataContext.GetModelFromID(id);
            foreach (MemberModel Member in Party.Members)
            {
                Member.Initiative = 0;
            }
            PartyDataContext.Save(Party, FOLDER.Party);

            return View("Details", Party);
        }

        [HttpPost]
        public IActionResult QuickSaveConditions(string id, string cid, string conditions)
        {
            try
            {
                PartyModel Party = PartyDataContext.GetModelFromID(id);
                MemberModel Member = Party.Members.Where(m => m.CharacterId == cid).Single();
                Member.Conditions = conditions;
                PartyDataContext.Save(Party, FOLDER.Party);
                return this.JsonSuccessTrue();
            }
            catch
            {
                return this.JsonSuccessFalse();
            }
        }

        public IActionResult ReloadPartyTable(string id)
        {
            return PartialView("_PartyTable", PartyDataContext.GetModelFromID(id));
        }
    }
}