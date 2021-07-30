using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Vidly.DAL;
using Vidly.Models;
using Vidly.ViewModels;

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

        public ActionResult New()
        {
            var movieFormViewModel = new MovieFormViewModel()
            {
                Movie = new Movie(),
                Genres = _context.Genres.ToList(),
            };

            return View("MovieForm", movieFormViewModel);
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.Include(c => c.Genre).SingleOrDefault(x => x.Id == id);

            if (movie == null)
                return HttpNotFound();

            var movieFormViewModel = new MovieFormViewModel()
            {
                Movie = movie,
                Genres = _context.Genres.ToList(),
            };

            return View("MovieForm", movieFormViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (movie.Id > 0)
            {
                var updateMovie = _context.Movies.Single(c => c.Id == movie.Id);

                updateMovie.Name = movie.Name;
                updateMovie.ReleaseDate = movie.ReleaseDate;
                updateMovie.Genre = movie.Genre;
                updateMovie.NumberInStock = movie.NumberInStock;
            }
            else
            {
                movie.DateAdded = DateTime.Now;

                _context.Movies.Add(movie);
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
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