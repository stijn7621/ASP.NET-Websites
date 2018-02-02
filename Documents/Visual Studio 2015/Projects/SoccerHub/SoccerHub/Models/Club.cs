using System.ComponentModel.DataAnnotations;

namespace SoccerHub.Models
{
    /// <summary>
    /// Class for the Clubs
    /// </summary>
    public class Club
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public byte Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        [Required]
        [StringLength(255)]
        public string Country { get; set; }

    }
}