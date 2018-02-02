using Microsoft.AspNet.Identity;
using OnlineAgenda.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace OnlineAgenda.Controllers
{
    public class PostController : Controller
    {
        private OnlineAgendaEntities db = new OnlineAgendaEntities();

        [Authorize]
        // GET: Post
        public ActionResult Index()
        {
            var tblPosts = db.tblPost.Include(t => t.TblAgendaItem);
            return View(tblPosts.ToList());
        }

        [Authorize]
        // GET: Post/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPost tblPost = db.tblPost.Find(id);
            if (tblPost == null)
            {
                return HttpNotFound();
            }
            return View(tblPost);
        }
        [Authorize]
        // GET: Posts/Create
        public ActionResult Create()
        {
            ViewBag.AgendaItemId = new SelectList(db.TblAgendaItem, "Id", "Omschrijving", null);
            ViewBag.Datum = ViewModels.PostFormViewModel.Datum;
            ViewBag.Tijd = ViewModels.PostFormViewModel.Tijd;
            return View();
        }
        [Authorize]
        public ActionResult Create2(TblAgendaItem agendaitem)
        {
            ViewBag.AgendaItemId = agendaitem.Id;
            ViewBag.Datum = ViewModels.PostFormViewModel.Datum;
            ViewBag.Tijd = ViewModels.PostFormViewModel.Tijd;
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewModels.PostFormViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                return View("Create", viewModel);
            }
            var Post = new tblPost
            {
                AgendaItemId = viewModel.AgendaItemId,
                GebruikerId = User.Identity.GetUserId(),
                PostDatumTijd = viewModel.GetDateTime(),
                Post = viewModel.Post
            };
            object obj = db.TblAgendaItem.Find(viewModel.AgendaItemId);
            if (obj != null)
            {
                db.TblAgendaItem.Find(viewModel.AgendaItemId).ForumStatus = "O";
            }
            db.tblPost.Add(Post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        // GET: Post/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPost tblPost = db.tblPost.Find(id);
            if (tblPost == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgendaItemId = new SelectList(db.TblAgendaItem, "Id", "Omschrijving", tblPost.AgendaItemId);
            return View(tblPost);
        }

        // POST: Post/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GebruikerId,AgendaItemId,PostDatumTijd,Post")] tblPost tblPost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AgendaItemId = new SelectList(db.TblAgendaItem, "Id", "Omschrijving", tblPost.AgendaItemId);
            return View(tblPost);
        }

        [Authorize(Roles = "Docent")]
        // GET: Posts/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPost tblPost = db.tblPost.Find(id);
            if (tblPost == null)
            {
                return HttpNotFound();
            }
            return View(tblPost);
        }

        // POST: Posts/Delete/5
        [Authorize(Roles = "Docent")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tblPost tblPost = db.tblPost.Find(id);
            db.tblPost.Remove(tblPost);
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
