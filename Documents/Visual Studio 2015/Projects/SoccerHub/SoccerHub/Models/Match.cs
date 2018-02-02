using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoccerHub.Models
{
    public class Match
    {

        #region Fields
        // -- FIELDS --
        private byte outteamid;
        private byte hometeamid;
        #endregion

        #region Properties
        // -- PROPERTIES --

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>

        public ApplicationUser User { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        [Required]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the date time.
        /// </summary>
        /// <value>
        /// The date time.
        /// </value>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Gets or sets the stadium.
        /// </summary>
        /// <value>
        /// The stadium.
        /// </value>
        [Required]
        [StringLength(255)]
        public string Stadium { get; set; }

        /// <summary>
        /// Gets or sets the home team identifier.
        /// </summary>
        /// <value>
        /// The home team identifier.
        /// </value>
        [Required]
        public byte HomeTeamId
        {
            get { return hometeamid; }
            set
            {
                hometeamid = value;
                SearchClubs();
            }
        }

        /// <summary>
        /// Gets or sets the home team.
        /// </summary>
        /// <value>
        /// The home team.
        /// </value>
        [StringLength(255)]
        public Club HomeTeam { get; private set; }

        /// <summary>
        /// Gets or sets the out team identifier.
        /// </summary>
        /// <value>
        /// The out team identifier.
        /// </value>
        [Required]
        public byte OutTeamId
        {
            get { return outteamid; }
            set
            {
                outteamid = value;
                SearchClubs();
            }
        }

        /// <summary>
        /// Gets or sets the out team.
        /// </summary>
        /// <value>
        /// The out team.
        /// </value>
        [StringLength(255)]
        public Club OutTeam { get; private set; }


        /// <summary>
        /// Gets the game match ids.
        /// </summary>
        /// <value>
        /// The game match ids.
        /// </value>
        public string GameMatchIds
        {
            get
            {
                return HomeTeamId.ToString() + " - " + OutTeamId.ToString();
            }

        }

        /// <summary>
        /// Gets the game match.
        /// </summary>
        /// <value>
        /// The game match.
        /// </value>
        public string GameMatch
        {
            get
            {
                return HomeTeam.Name + " - " + OutTeam.Name;
            }

        }

        /// <summary>
        /// Gets or sets the clubs.
        /// </summary>
        /// <value>
        /// The clubs.
        /// </value>
        public IEnumerable<Club> Clubs { get; set; }

        #endregion

        #region Constructor
        // -- CONSTRUCTOR --


        /// <summary>
        /// Initializes a new instance of the <see cref="Match"/> class.
        /// </summary>
        public Match()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Clubs = db.Clubs;
        }
        #endregion

        #region Methods
        // -- METHODS --

        /// <summary>
        /// Search for the clubs.
        /// </summary>
        private void SearchClubs()
        {
            if (HomeTeam != null)
            {
                foreach (var club in Clubs)
                {
                    if (club.Id == HomeTeamId)
                    {
                        HomeTeam = club;
                    }
                }
            }
            if (OutTeam != null)
            {
                foreach (var club in Clubs)
                {
                    if (club.Id == OutTeamId)
                    {
                        OutTeam = club;
                    }
                }
            }
        }
        #endregion
    }
}