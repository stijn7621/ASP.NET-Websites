using Microsoft.AspNet.Identity;
using SoccerHub.Models;
using SoccerHub.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace SoccerHub.Controllers
{
    public class MatchesController : Controller
    {
        #region Fields

        // -- FIELDS --

        /// <summary>
        /// The context
        /// </summary>
        private MatchesViewModel matchesViewModel;
        private ApplicationDbContext _context;
        #endregion

        #region Constructor

        // -- CONSTRUCTOR --

        /// <summary>
        /// Initializes a new instance of the <see cref="MatchesController" /> class.
        /// </summary>
        public MatchesController()
        {
            _context = new ApplicationDbContext();
        }

        #endregion

        #region ActionResults

        // -- ACTION RESULTS --

        // GET: Matches
        /// <summary>
        /// ActionResult Index.
        /// </summary>
        /// <returns>View Index</returns>
        public ActionResult Index()
        {
            var matches = _context.Matches
                .Include(m => m.User)
                .Include(m => m.HomeTeam)
                .Include(m => m.OutTeam)
                .Where(m => m.DateTime > DateTime.Now);
            matchesViewModel = new MatchesViewModel
            {
                UpcomingMatches = matches,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Matches"
            };

            return View("Index", matchesViewModel);
        }

        // GET: Shows/Details/5
        /// <summary>
        /// Detailspage about the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>View Details</returns>
        public ActionResult Details([Bind(Include = "User,HomeTeam, OutTeam")] int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Match match = _context.Matches.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            return View(match);
        }

        /// <summary>
        /// Attending matches.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var matches = _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Match)
                .Include(m => m.User)
                .Include(m => m.HomeTeam)
                .Include(m => m.OutTeam)
                .ToList();

            var viewModel = new MatchesViewModel()
            {
                UpcomingMatches = matches,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Matches I'm Attending"
            };

            return View("Matches", viewModel);
        }


        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>View Create</returns>
        [Authorize]
        // GET: Matches
        public ActionResult Create()
        {


            var viewModel = new MatchFormViewModel
            {
                Clubs = _context.Clubs.OrderBy(c => c.Name).ToList()
            };

            return View(viewModel);
        }

        /// <summary>
        /// Creates the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MatchFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", viewModel);
            }
            var match = new Match
            {
                UserId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                HomeTeamId = viewModel.HomeTeam,
                OutTeamId = viewModel.OutTeam,
                Stadium = viewModel.Stadium,
            };

            _context.Matches.Add(match);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        // GET: Matches/Edit
        /// <summary>
        /// Edits the specified match.
        /// </summary>
        /// <param name="id">The id of the match.</param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Match match = _context.Matches.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            return View(match);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Edits the specified match.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Match match)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(match).State = EntityState.Modified;
                _context.Entry(match).CurrentValues["DateTime"] = match.DateTime;
                _context.Entry(match).CurrentValues["UserId"] = User.Identity.GetUserId();
                _context.Entry(match).CurrentValues["HomeTeamId"] = match.HomeTeamId;
                _context.Entry(match).CurrentValues["OutTeamId"] = match.OutTeamId;
                _context.Entry(match).CurrentValues["Stadium"] = match.Stadium;

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(match);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Delete([Bind(Include = "User,HomeTeam, OutTeam")] int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Match match = _context.Matches.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            return View(match);
        }


        /// <summary>
        /// Confirms if deleted
        /// 
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Match match = _context.Matches.Find(id);
            if (match != null) _context.Matches.Remove(match);
            _context.SaveChanges();
            return RedirectToAction("Index", "Matches");
        }

        /// <summary>
        /// Releases unmanaged resources and optionally releases managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}
