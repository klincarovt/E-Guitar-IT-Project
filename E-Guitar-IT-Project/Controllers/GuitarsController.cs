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
    public class GuitarsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Guitars
        [AllowAnonymous]
        public ActionResult Index()
        {
            var guitars = db.Guitars.Include(g => g.Category);
            return View(guitars.ToList());
        }

        // GET: Guitars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guitar guitar = db.Guitars.Find(id);
            if (guitar == null)
            {
                return HttpNotFound();
            }
            return View(guitar);
        }

        // GET: Guitars/Create
        [Authorize(Roles = "ADMIN,EDITOR")]
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "id", "Name");
            return View();
        }

        // POST: Guitars/Create.
        [Authorize(Roles = "ADMIN,EDITOR")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,UrlImg,Name,Price,Samples,CategoryID")] Guitar guitar)
        {
            if (ModelState.IsValid)
            {
                db.Guitars.Add(guitar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "id", "Name", guitar.CategoryID);
            return View(guitar);
        }

        // GET: Guitars/Edit/5
        [Authorize(Roles = "ADMIN,EDITOR")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guitar guitar = db.Guitars.Find(id);
            if (guitar == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "id", "Name", guitar.CategoryID);
            return View(guitar);
        }

        // POST: Guitars/Edit/5

        [Authorize(Roles = "ADMIN,EDITOR")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,UrlImg,Name,Price,Samples,CategoryID")] Guitar guitar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(guitar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "id", "Name", guitar.CategoryID);
            return View(guitar);
        }

        // GET: Guitars/Delete/5
        [Authorize(Roles = "ADMIN,EDITOR")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guitar guitar = db.Guitars.Find(id);
            if (guitar == null)
            {
                return HttpNotFound();
            }
            return View(guitar);
        }

        // POST: Guitars/Delete/5
        [Authorize(Roles = "ADMIN,EDITOR")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Guitar guitar = db.Guitars.Find(id);
            db.Guitars.Remove(guitar);
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
