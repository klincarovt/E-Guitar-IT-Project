using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using E_Guitar_IT_Project.Models;

namespace E_Guitar_IT_Project.Controllers
{
    [Authorize]
    public class CraftsmenController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Craftsmen
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.Craftsmen.ToList());
        }

        [Authorize(Roles = "ADMIN,EDITOR")]
        // GET: Craftsmen/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Craftsman craftsman = db.Craftsmen.Find(id);
            if (craftsman == null)
            {
                return HttpNotFound();
            }
            var result = from g in db.Guitars
                         select new
                         {
                             g.id,
                             g.Name,
                             Checked = ((from gTOc in db.GuitarToCraftsmen
                                         where (gTOc.CraftsmanID == id) && (gTOc.GuitarID == g.id)
                                         select gTOc).Count() > 0

                                        )
                         };

            var myViewModel = new CraftsmenViewModel();

            myViewModel.CrafrsmanID = id.Value;
            myViewModel.Name = craftsman.Name;
            myViewModel.Surname = craftsman.Surname;
            myViewModel.Description = craftsman.Description;
            myViewModel.Address = craftsman.Address;


            var CheckBoxList = new List<CheckboxViewModel>();

            foreach (var item in result)
            {
                CheckBoxList.Add(new CheckboxViewModel { id = item.id, Name = item.Name, Checked = item.Checked });
            }

            myViewModel.Guitars = CheckBoxList;

            return View(myViewModel);
        }

        // GET: Craftsmen/Create
        [Authorize(Roles = "ADMIN,EDITOR")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Craftsmen/Create

        [Authorize(Roles = "ADMIN,EDITOR")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Name,Surname,Description,Address")] Craftsman craftsman)
        {
            if (ModelState.IsValid)
            {
                db.Craftsmen.Add(craftsman);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(craftsman);
        }

        // GET: Craftsmen/Edit/5
        [Authorize(Roles = "ADMIN,EDITOR")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Craftsman craftsman = db.Craftsmen.Find(id);
            if (craftsman == null)
            {
                return HttpNotFound();
            }
            var result = from g in db.Guitars
                         select new
                         {
                             g.id,
                             g.Name,
                             Checked = ((from gTOc in db.GuitarToCraftsmen
                                         where (gTOc.CraftsmanID == id) && (gTOc.GuitarID == g.id)
                                         select gTOc).Count() > 0

                                        )
                         };

            var myViewModel = new CraftsmenViewModel();

            myViewModel.CrafrsmanID = id.Value;
            myViewModel.Name = craftsman.Name;
            myViewModel.Surname = craftsman.Surname;
            myViewModel.Description = craftsman.Description;
            myViewModel.Address = craftsman.Address;


            var CheckBoxList = new List<CheckboxViewModel>();

            foreach (var item in result)
            {
                CheckBoxList.Add(new CheckboxViewModel { id = item.id, Name = item.Name, Checked = item.Checked });
            }

            myViewModel.Guitars = CheckBoxList;

            return View(myViewModel);
        }

        // POST: Craftsmen/Edit/5
        [Authorize(Roles = "ADMIN,EDITOR")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CraftsmenViewModel craftsman)
        {
            if (ModelState.IsValid)
            {
                var myCraftsman = db.Craftsmen.Find(craftsman.CrafrsmanID);

                myCraftsman.Name = craftsman.Name;
                myCraftsman.Surname = craftsman.Surname;
                myCraftsman.Description = craftsman.Description;
                myCraftsman.Address = craftsman.Address;

                foreach (var item in db.GuitarToCraftsmen)
                {
                    if (item.CraftsmanID == craftsman.CrafrsmanID)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }
                }

                foreach (var item in craftsman.Guitars)
                {
                    if (item.Checked)
                    {
                        db.GuitarToCraftsmen.Add(new GuitarToCraftsman() { CraftsmanID = craftsman.CrafrsmanID, GuitarID = item.id });
                    }
                }


                //db.Entry(craftsman).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }


            return View(craftsman);
        }

        // GET: Craftsmen/Delete/5
        [Authorize(Roles = "ADMIN,EDITOR")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Craftsman craftsman = db.Craftsmen.Find(id);
            if (craftsman == null)
            {
                return HttpNotFound();
            }
            return View(craftsman);
        }

        // POST: Craftsmen/Delete/5
        [Authorize(Roles = "ADMIN,EDITOR")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Craftsman craftsman = db.Craftsmen.Find(id);
            db.Craftsmen.Remove(craftsman);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
