using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TheTallTankardTavern.Models;
using TheTallTankardTavern.Helpers;
using static TheTallTankardTavern.Configuration.ApplicationSettings;
using static TheTallTankardTavern.Configuration.Constants;
using System;
using TheTallTankardTavern.Attributes;

namespace TheTallTankardTavern.Controllers
{
    [Authenticated]
    public class PartyController : Controller
    {
        public IActionResult Index()
        {
            return View(PartyDataContext.SingleOrDefault() ?? new PartyModel());
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

            return View("Index", Party);
        }

        public IActionResult Remove(string cid)
        {
            PartyModel Party = PartyDataContext.Single();
            Party.Members.RemoveAll(m => m.CharacterId.Equals(cid));
            PartyDataContext.Save(Party, FOLDER.Party);

            return View("Index", Party);
        }

        [HttpPost]
        public JsonResult QuickSaveMemberInitiative(string cid, int initiative)
        {
            try
            {
                PartyModel Party = PartyDataContext.Single();
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
        public JsonResult QuickSaveDate(string date)
        {
            try
            {
                PartyModel Party = PartyDataContext.Single();
                Party.Date = date;
                PartyDataContext.Save(Party, FOLDER.Party);
                return this.JsonSuccessTrue();
            }
            catch
            {
                return this.JsonSuccessFalse();
            }
        }

        public IActionResult ResetInitiative()
        {
            PartyModel Party = PartyDataContext.Single();
            foreach (MemberModel Member in Party.Members)
            {
                Member.Initiative = 0;
            }
            PartyDataContext.Save(Party, FOLDER.Party);

            return View("Index", Party);
        }
    }
}