using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Film
    {
        [Key]

        public int Id { get; set; }

        [Required]
        public string Titre { get; set; }
        
        public string Description { get; set; }
        [Required]
        public string Url { get; set; }

        [Required]
        public string Vignette { get; set; } = "http://localhost:52714/Benjamin.gif";

        public int ThemeId { get; set; }

        public Theme theme { get; set; }
    }
}
