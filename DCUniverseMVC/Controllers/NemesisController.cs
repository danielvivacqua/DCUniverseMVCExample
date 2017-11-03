using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DCUniverseMVC.Models;

namespace DCUniverseMVC.Controllers
{
    public class NemesisController : Controller
    {
        private DCUniverseEntities db = new DCUniverseEntities();

        // GET: Nemesis
        public ActionResult Index()
        {
            var nemesis = db.Nemesis.Include(n => n.Hero);
            return View(nemesis.ToList());
        }

        // GET: Nemesis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nemesi nemesi = db.Nemesis.Find(id);
            if (nemesi == null)
            {
                return HttpNotFound();
            }
            return View(nemesi);
        }

        // GET: Nemesis/Create
        public ActionResult Create()
        {
            ViewBag.HeroID = new SelectList(db.Heroes, "HeroID", "HeroName");
            return View();
        }

        // POST: Nemesis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NemesisID,NemesisName,InJail,NumberOfHenchmen,HeroID")] Nemesi nemesi)
        {
            if (ModelState.IsValid)
            {
                db.Nemesis.Add(nemesi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HeroID = new SelectList(db.Heroes, "HeroID", "HeroName", nemesi.HeroID);
            return View(nemesi);
        }

        // GET: Nemesis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nemesi nemesi = db.Nemesis.Find(id);
            if (nemesi == null)
            {
                return HttpNotFound();
            }
            ViewBag.HeroID = new SelectList(db.Heroes, "HeroID", "HeroName", nemesi.HeroID);
            return View(nemesi);
        }

        // POST: Nemesis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NemesisID,NemesisName,InJail,NumberOfHenchmen,HeroID")] Nemesi nemesi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nemesi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HeroID = new SelectList(db.Heroes, "HeroID", "HeroName", nemesi.HeroID);
            return View(nemesi);
        }

        // GET: Nemesis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nemesi nemesi = db.Nemesis.Find(id);
            if (nemesi == null)
            {
                return HttpNotFound();
            }
            return View(nemesi);
        }

        // POST: Nemesis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nemesi nemesi = db.Nemesis.Find(id);
            db.Nemesis.Remove(nemesi);
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
