using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        [Display(Name = "Genre")]
        public byte GenreId { get; set; }
        
        [Display(Name = "Release Date")]
        public DateTime? ReleaseDate { get; set; }
        
        [Display(Name = "Number in Stock")]
        public int NumberInStock { get; set; }
        
        public DateTime DateAdded { get; set; }

        public Genre Genre { get; set; }
    }
}