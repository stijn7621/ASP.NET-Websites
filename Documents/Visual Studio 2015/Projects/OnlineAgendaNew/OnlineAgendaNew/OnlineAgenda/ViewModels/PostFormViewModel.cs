using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineAgenda.ViewModels
{
    public class PostFormViewModel
    {
        private static DateTime date;

        public string GebruikerId { get; set; }
        [Required]
        public int AgendaItemId { get; set; }

        public static string Datum
        {
            get
            {
                date = DateTime.Now;
                return date.ToString("dd-MM-yyyy");
            }
        }

        public static string Tijd
        {
            get
            {
                date = DateTime.Now;
                return date.ToString("HH:mm");
            }
        }
        [Required]
        public string Post { get; set; }

        //public virtual AspNetUsers AspNetUsers { get; set; }
        // public virtual TblAgendaItem TblAgendaItem { get; set; }


        /// <summary>
        /// Gets the date time.
        /// </summary>
        /// <value>
        /// The date time.
        /// </value>
        public DateTime GetDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", Datum, Tijd));
        }

        public PostFormViewModel()
        {
        }

    }
}