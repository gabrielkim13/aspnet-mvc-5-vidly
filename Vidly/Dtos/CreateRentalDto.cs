using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Dtos
{
    public class CreateRentalDto
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public List<int> MoviesIds { get; set; }
    }
}