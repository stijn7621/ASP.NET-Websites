using Microsoft.AspNet.Identity;
using SoccerHub.Dtos;
using SoccerHub.Models;
using System.Linq;
using System.Web.Http;

namespace SoccerHub.Controllers
{
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Attendances.Any(a => a.AttendeeId == userId && a.MatchId == dto.MatchId))
                return BadRequest("The attendance already exists.");

            var attendance = new Attendance
            {
                MatchId = dto.MatchId,
                AttendeeId = userId
            };
            _context.Attendances.Add(attendance);
            _context.SaveChanges();

            return Ok();
        }
    }
}