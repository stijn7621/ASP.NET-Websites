using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineAgenda.Models;

namespace OnlineAgenda.Controllers
{
    public class DocentController : Controller
    {
        private OnlineAgendaEntities db = new OnlineAgendaEntities();

        // GET: Docent
        public ActionResult Index()
        {
            return View(db.TblDocent.ToList());
        }

        // GET: Docent/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblDocent tblDocent = db.TblDocent.Find(id);
            if (tblDocent == null)
            {
                return HttpNotFound();
            }
            return View(tblDocent);
        }

        // GET: Docent/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Docent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Afkorting,Aanhef,Voorletters,GebruikersId")] TblDocent tblDocent)
        {
            if (ModelState.IsValid)
            {
                db.TblDocent.Add(tblDocent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblDocent);
        }

        // GET: Docent/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblDocent tblDocent = db.TblDocent.Find(id);
            if (tblDocent == null)
            {
                return HttpNotFound();
            }
            return View(tblDocent);
        }

        // POST: Docent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Afkorting,Aanhef,Voorletters,GebruikersId")] TblDocent tblDocent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblDocent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblDocent);
        }

        // GET: Docent/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblDocent tblDocent = db.TblDocent.Find(id);
            if (tblDocent == null)
            {
                return HttpNotFound();
            }
            return View(tblDocent);
        }

        // POST: Docent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TblDocent tblDocent = db.TblDocent.Find(id);
            db.TblDocent.Remove(tblDocent);
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
