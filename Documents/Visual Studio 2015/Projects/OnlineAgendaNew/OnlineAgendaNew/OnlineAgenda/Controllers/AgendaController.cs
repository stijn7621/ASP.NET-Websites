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
    public class AgendaController : Controller
    {
        private OnlineAgendaEntities db = new OnlineAgendaEntities();

        // GET: Agenda
        public ActionResult Index()
        {
            if (User.IsInRole("Student"))
            {
                string klas = getKlasCode(User.Identity.Name);
                var tblAgendaItems = db.TblAgendaItem.Include(t => t.TblDocent).Include(t => t.TblKlas).Where(s => s.Klas == klas).Include(t => t.TblVak);
                return View(tblAgendaItems.ToList());
            } else
            {
                var tblAgendaItems = db.TblAgendaItem.Include(t => t.TblDocent).Include(t => t.TblKlas).Include(t => t.TblVak);
                return View(tblAgendaItems.ToList());
            }
        }

        // GET: Agenda/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblAgendaItem tblAgendaItem = db.TblAgendaItem.Find(id);
            if (tblAgendaItem == null)
            {
                return HttpNotFound();
            }
            return View(tblAgendaItem);
        }



        // GET: Agenda/Create for Docent only
        [Authorize(Roles = "Docent")]
        public ActionResult Create()
        {
            string[] soorten = { "H", "T", "D" };

            ViewBag.Soort = new SelectList(soorten, "Soort");
            ViewBag.Docentcode = new SelectList(db.TblDocent, "Afkorting", "Afkorting");
            ViewBag.Klas = new SelectList(db.TblKlas, "KlasCode", "KlasCode");
            ViewBag.VakCode = new SelectList(db.TblVaks, "VakCode", "Vaknaam");
            return View();
        }

        // POST: Agenda/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Docent")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DatumTijd,Omschrijving,Soort,VakCode,Klas,Docentcode,ForumStatus")] TblAgendaItem tblAgendaItem)
        {
            if (ModelState.IsValid)
            {
                var userName = User.Identity.Name;
                
                // Set data which is unlogic for the Docent to fill in
                tblAgendaItem.Docentcode = GetDocentCode(userName);
                tblAgendaItem.ForumStatus = "O";

                db.TblAgendaItem.Add(tblAgendaItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            string[] soorten = { "H", "T", "D" };

            ViewBag.Soort = new SelectList(soorten, "Soort");
            ViewBag.Docentcode = new SelectList(db.TblDocent, "Afkorting", "Afkorting", tblAgendaItem.Docentcode);
            ViewBag.Klas = new SelectList(db.TblKlas, "KlasCode", "KlasCode", tblAgendaItem.Klas);
            ViewBag.VakCode = new SelectList(db.TblVaks, "VakCode", "Vaknaam", tblAgendaItem.VakCode);

            return View(tblAgendaItem);
        }

        // GET: Agenda/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblAgendaItem tblAgendaItem = db.TblAgendaItem.Find(id);
            if (tblAgendaItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.Docentcode = new SelectList(db.TblDocent, "Afkorting", "Aanhef", tblAgendaItem.Docentcode);
            ViewBag.Klas = new SelectList(db.TblKlas, "KlasCode", "Mentor", tblAgendaItem.Klas);
            ViewBag.VakCode = new SelectList(db.TblVaks, "VakCode", "Vaknaam", tblAgendaItem.VakCode);
            return View(tblAgendaItem);
        }

        // POST: Agenda/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DatumTijd,Omschrijving,Soort,VakCode,Klas,Docentcode,ForumStatus")] TblAgendaItem tblAgendaItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblAgendaItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Docentcode = new SelectList(db.TblDocent, "Afkorting", "Aanhef", tblAgendaItem.Docentcode);
            ViewBag.Klas = new SelectList(db.TblKlas, "KlasCode", "Mentor", tblAgendaItem.Klas);
            ViewBag.VakCode = new SelectList(db.TblVaks, "VakCode", "Vaknaam", tblAgendaItem.VakCode);
            return View(tblAgendaItem);
        }

        // GET: Agenda/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblAgendaItem tblAgendaItem = db.TblAgendaItem.Find(id);
            if (tblAgendaItem == null)
            {
                return HttpNotFound();
            }
            return View(tblAgendaItem);
        }

        // POST: Agenda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TblAgendaItem tblAgendaItem = db.TblAgendaItem.Find(id);
            db.TblAgendaItem.Remove(tblAgendaItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Docentcode from the username of the logged in user (if is docent)
        private string GetDocentCode(string username)
        {
            string selectCommand = "SELECT D.Afkorting FROM TblDocent D WHERE d.GebruikersId = (SELECT A.Id FROM AspNetUsers A WHERE A.UserName = '"+username+"')";
            var code = db.Database.SqlQuery<string>(selectCommand).FirstOrDefault<string>();

            return code.ToString();
        }

        // GET: Klascode from give username of the logged in user (if is student)
        private string getKlasCode(string username)
        {
            string selectCommand = "SELECT S.Klas FROM TblStudent S WHERE S.GebruikersId = (SELECT A.Id FROM AspNetUsers A WHERE A.UserName = '" + username + "')";
            var code = db.Database.SqlQuery<string>(selectCommand).FirstOrDefault<string>();

            return code.ToString();
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
