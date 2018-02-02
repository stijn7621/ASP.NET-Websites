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
    public class TblBijlagesController : Controller
    {
        private OnlineAgendaEntities db = new OnlineAgendaEntities();

        // GET: TblBijlages
        public ActionResult Index()
        {
            return View(db.TblBijlage.ToList());
        }

        // GET: TblBijlages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblBijlage tblBijlage = db.TblBijlage.Find(id);
            if (tblBijlage == null)
            {
                return HttpNotFound();
            }
            return View(tblBijlage);
        }

        // GET: TblBijlages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TblBijlages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naam,Locatie")] TblBijlage tblBijlage)
        {
            if (ModelState.IsValid)
            {
                db.TblBijlage.Add(tblBijlage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblBijlage);
        }

        // GET: TblBijlages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblBijlage tblBijlage = db.TblBijlage.Find(id);
            if (tblBijlage == null)
            {
                return HttpNotFound();
            }
            return View(tblBijlage);
        }

        // POST: TblBijlages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naam,Locatie")] TblBijlage tblBijlage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblBijlage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblBijlage);
        }

        // GET: TblBijlages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblBijlage tblBijlage = db.TblBijlage.Find(id);
            if (tblBijlage == null)
            {
                return HttpNotFound();
            }
            return View(tblBijlage);
        }

        // POST: TblBijlages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TblBijlage tblBijlage = db.TblBijlage.Find(id);
            db.TblBijlage.Remove(tblBijlage);
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
