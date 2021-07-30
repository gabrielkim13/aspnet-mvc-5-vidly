using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Vidly.DAL;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private VidlyContext _context;

        public MoviesController()
        {
            _context = new VidlyContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();

            base.Dispose(disposing);
        }

        public ActionResult Index()
        {
            var movies = _context.Movies.Include(m => m.Genre).ToList();

            return View(movies);
        }

        public ActionResult Details(int id)
        {
            var movie= _context.Movies.Include(c => c.Genre).SingleOrDefault(x => x.Id == id);

            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

        //public ActionResult Random()
        //{
        //    var random = new Random();
        //    var randomIndex = random.Next(0, Movies.Count);

        //    var movie = Movies[randomIndex];
        //    var customers = new List<Customer>
        //    {
        //        new Customer { Name = "Alice" },
        //        new Customer { Name = "Bob" },
        //        new Customer { Name = "Carol" },
        //    };

        //    var randomMovieViewModel = new RandomMovieViewModel
        //    {
        //        Movie = movie,
        //        Customers = customers
        //    };

        //    //ViewData["Movie"] = movie;
        //    //ViewBag.Movie = movie;

        //    return View(randomMovieViewModel);
        //}

        //[Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1, 12)}")]
        //public ActionResult ByReleaseDate(int year, int month)
        //{
        //    return Content(string.Format("Release date: {0}/{1}", month, year));
        //}
    }
}