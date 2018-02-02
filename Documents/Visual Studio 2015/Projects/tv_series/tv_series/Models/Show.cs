using System.ComponentModel.DataAnnotations;

namespace tv_series.Models
{
    public class Show
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Storyline { get; set; }

        [Required]
        [StringLength(255)]
        public string Country { get; set; }

        public int year { get; set; }

        public bool netflix { get; set; }

    }
}