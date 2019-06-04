using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Theme
    {
        [Key]
        public int Id { get; set; }
        public string Nom { get; set; }

        public ICollection<Film> films { get; set; }
    }
}
