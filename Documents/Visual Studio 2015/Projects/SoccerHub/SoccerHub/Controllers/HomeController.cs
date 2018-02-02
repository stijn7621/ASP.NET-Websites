using SoccerHub.Models;
using SoccerHub.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace SoccerHub.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// The context
        /// </summary>
        private ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        /// <summary>
        /// ActionResult Index.
        /// </summary>
        /// <returns>View Index</returns>
        public ActionResult Index()
        {
            var upcomingMatches = _context.Matches
                .Include(m => m.User)
                .Include(m => m.HomeTeam)
                .Include(m => m.OutTeam)
                .Where(m => m.DateTime > DateTime.Now);

            var viewModel = new MatchesViewModel
            {
                UpcomingMatches = upcomingMatches,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Matches"
            };
            return View("Matches", viewModel);
        }

        /// <summary>
        /// ActionResult About.
        /// </summary>
        /// <returns>View About</returns>
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        /// <summary>
        /// ActionResult About.
        /// </summary>
        /// <returns>View Contact</returns>
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}