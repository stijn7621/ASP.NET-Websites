using SoccerHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoccerHub.ViewModels
{
    public class MatchFormViewModel
    {

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the stadium.
        /// </summary>
        /// <value>
        /// The stadium.
        /// </value>
        [Required]
        public string Stadium { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        [Required]
        [FutureDate]
        public string Date { get; set; }
        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>
        /// The time.
        /// </value>
        [Required]
        [ValidTime]
        public string Time { get; set; }

        /// <summary>
        /// Gets or sets the home team.
        /// </summary>
        /// <value>
        /// The home team.
        /// </value>
        [Required]
        public byte HomeTeam { get; set; }

        /// <summary>
        /// Gets or sets the out team.
        /// </summary>
        /// <value>
        /// The out team.
        /// </value>
        [Required]
        public byte OutTeam { get; set; }

        /// <summary>
        /// Gets or sets the clubs.
        /// </summary>
        /// <value>
        /// The clubs.
        /// </value>
        public IEnumerable<Club> Clubs { get; set; }

        /// <summary>
        /// Gets the date time.
        /// </summary>
        /// <value>
        /// The date time.
        /// </value>
        public DateTime GetDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }

    }
}