using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using Vidly.Models;

namespace Vidly.Dtos
{
    public class MembershipTypeDto
    {
        public byte Id { get; set; }
        
        public string Name { get; set; }
    }
}