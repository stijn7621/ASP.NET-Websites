using SoccerHub.Models;
using System.Collections.Generic;

namespace SoccerHub.ViewModels
{
    public class MatchesViewModel
    {
        public IEnumerable<Match> UpcomingMatches { get; set; }
        public bool ShowActions { get; set; }
        public string Heading { get; set; }
    }
}