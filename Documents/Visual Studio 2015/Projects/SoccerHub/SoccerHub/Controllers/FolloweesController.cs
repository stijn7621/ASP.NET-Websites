using Microsoft.AspNet.Identity;
using SoccerHub.Models;
using System.Linq;
using System.Web.Mvc;

namespace SoccerHub.Controllers
{
    public class FolloweesController : Controller
    {
        private ApplicationDbContext _context;

        public FolloweesController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var users = _context.Followings
                .Where(f => f.FollowerId == userId)
                .Select(f => f.Followee)
                .ToList();

            return View(users);
        }
    }
}