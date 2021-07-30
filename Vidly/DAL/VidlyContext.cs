using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

using Vidly.Models;

namespace Vidly.DAL
{
    public class VidlyContext : IdentityDbContext<AppUser>
    {
        public VidlyContext() : base("VidlyContext")
        { 
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }

        internal object Include(Func<object, object> p)
        {
            throw new NotImplementedException();
        }
    }
}