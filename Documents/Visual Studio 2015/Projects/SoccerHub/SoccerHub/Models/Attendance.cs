using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerHub.Models
{
    public class Attendance
    {
        public Match Match { get; set; }
        public ApplicationUser Attendee { get; set; }
        [Key]
        [Column(Order = 1)]
        public int MatchId { get; set; }
        [Key]
        [Column(Order = 2)]
        public string AttendeeId { get; set; }


    }
}