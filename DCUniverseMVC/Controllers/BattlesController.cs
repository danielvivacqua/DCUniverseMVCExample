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
    public class BattlesController : Controller
    {
        private DCUniverseEntities db = new DCUniverseEntities();

        // GET: Battles
        public ActionResult Index()
        {
            var battles = db.Battles.Include(b => b.Hero).Include(b => b.Villain);
            return View(battles.ToList());
        }

        // GET: Battles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Battle battle = db.Battles.Find(id);
            if (battle == null)
            {
                return HttpNotFound();
            }
            return View(battle);
        }

        // GET: Battles/Create
        public ActionResult Create()
        {
            ViewBag.HeroID = new SelectList(db.Heroes, "HeroID", "HeroName");
            ViewBag.VillainID = new SelectList(db.Villains, "VillainID", "VillainName");
            return View();
        }

        // POST: Battles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BattleID,HeroID,VillainID")] Battle battle)
        {
            if (ModelState.IsValid)
            {
                db.Battles.Add(battle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HeroID = new SelectList(db.Heroes, "HeroID", "HeroName", battle.HeroID);
            ViewBag.VillainID = new SelectList(db.Villains, "VillainID", "VillainName", battle.VillainID);
            return View(battle);
        }

        // GET: Battles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Battle battle = db.Battles.Find(id);
            if (battle == null)
            {
                return HttpNotFound();
            }
            ViewBag.HeroID = new SelectList(db.Heroes, "HeroID", "HeroName", battle.HeroID);
            ViewBag.VillainID = new SelectList(db.Villains, "VillainID", "VillainName", battle.VillainID);
            return View(battle);
        }

        // POST: Battles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BattleID,HeroID,VillainID")] Battle battle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(battle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HeroID = new SelectList(db.Heroes, "HeroID", "HeroName", battle.HeroID);
            ViewBag.VillainID = new SelectList(db.Villains, "VillainID", "VillainName", battle.VillainID);
            return View(battle);
        }

        // GET: Battles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Battle battle = db.Battles.Find(id);
            if (battle == null)
            {
                return HttpNotFound();
            }
            return View(battle);
        }

        // POST: Battles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Battle battle = db.Battles.Find(id);
            db.Battles.Remove(battle);
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
