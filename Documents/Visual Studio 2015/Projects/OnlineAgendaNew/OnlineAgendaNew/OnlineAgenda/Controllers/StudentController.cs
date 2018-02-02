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
    public class StudentController : Controller
    {
        private OnlineAgendaEntities db = new OnlineAgendaEntities();

        // GET: Student
        public ActionResult Index()
        {
            var tblStudents = db.TblStudent.Include(t => t.TblKlas);
            return View(tblStudents.ToList());
        }

        // GET: Student/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblStudent tblStudent = db.TblStudent.Find(id);
            if (tblStudent == null)
            {
                return HttpNotFound();
            }
            return View(tblStudent);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            ViewBag.Klas = new SelectList(db.TblKlas, "KlasCode", "Mentor");
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentNummer,Geboortedatum,Adres,Postcode,Plaats,Klas,GebruikersId")] TblStudent tblStudent)
        {
            if (ModelState.IsValid)
            {
                db.TblStudent.Add(tblStudent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Klas = new SelectList(db.TblKlas, "KlasCode", "Mentor", tblStudent.Klas);
            return View(tblStudent);
        }

        // GET: Student/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblStudent tblStudent = db.TblStudent.Find(id);
            if (tblStudent == null)
            {
                return HttpNotFound();
            }
            ViewBag.Klas = new SelectList(db.TblKlas, "KlasCode", "Mentor", tblStudent.Klas);
            return View(tblStudent);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentNummer,Geboortedatum,Adres,Postcode,Plaats,Klas,GebruikersId")] TblStudent tblStudent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblStudent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Klas = new SelectList(db.TblKlas, "KlasCode", "Mentor", tblStudent.Klas);
            return View(tblStudent);
        }

        // GET: Student/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblStudent tblStudent = db.TblStudent.Find(id);
            if (tblStudent == null)
            {
                return HttpNotFound();
            }
            return View(tblStudent);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TblStudent tblStudent = db.TblStudent.Find(id);
            db.TblStudent.Remove(tblStudent);
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
