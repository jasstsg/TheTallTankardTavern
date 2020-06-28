using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TheTallTankardTavern.Helpers;
using TTT.Common.Abstractions;
using static TheTallTankardTavern.Configuration.Constants;

namespace TheTallTankardTavern.Controllers
{
    public abstract class BaseController<T> : Controller where T : BaseModel
    {
        protected static string NewGUID { get { return Guid.NewGuid().ToString(); } }
        protected List<T> DataContext { get; private set; }
        protected FOLDER Folder { get; private set; }

        protected BaseController(List<T> DataContext, FOLDER Folder)
        {
            this.DataContext = DataContext;
            this.Folder = Folder;
        }

        public abstract IActionResult Create();

        public virtual IActionResult Index()
        {
            return View("Index", DataContext.OrderBy(x => x.Name).ToList());
        }

		public virtual IActionResult Details(string id)
        {
            return ControllerHelper.ViewExists(this, "Details") ? View("Details", DataContext.GetModelFromID(id)) : Index();
        }

		public virtual IActionResult Edit(string id)
        {
            return View("Create", DataContext.GetModelFromID(id));
        }

        protected virtual IActionResult SaveModel(T Model, string submit)
        {
            DataContext.Save(Model, Folder);
            switch (submit)
            {
                case TAGHELPER.SUBMIT_TEXT.SAVE_AND_CONTINUE:
                    return View("Create", Model);
                case TAGHELPER.SUBMIT_TEXT.SAVE_AND_FINISH:
                default:
                    return ControllerHelper.ViewExists(this, "Details") ? View("Details", Model) : Index();
            }
        }

        public virtual IActionResult Delete(string id)
        {
            return View("Delete", new BaseModel() { ID = id });
        }

        [HttpPost]
        public virtual IActionResult Delete(BaseModel Model)
        {
            if (DataContext.GetModelFromID(Model.ID).Name.Equals(Model.Name))
            {
                DataContext.Delete(Model.ID, Folder);
                return Index();
            }
            else
            {
                ViewData["msg"] = "The typed name did not match.";
                return Delete(Model.ID);
            }
        }

        public virtual IActionResult Add(string id)
        {
            ViewData["id"] = id;
            return View("Add");
        }


    }
}
