using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;

using Vidly.Models;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();

            base.Dispose(disposing);
        }

        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.CanManageMovies))
                return View("List");

            return View("ReadOnlyList");
        }

        public ActionResult Details(int id)
        {
            var movie= _context.Movies.Include(c => c.Genre).SingleOrDefault(x => x.Id == id);

            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var movieFormViewModel = new MovieFormViewModel()
            {
                Movie = new Movie(),
                Genres = GetGenres(),
            };

            return View("MovieForm", movieFormViewModel);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.Include(c => c.Genre).SingleOrDefault(x => x.Id == id);

            if (movie == null)
                return HttpNotFound();

            var movieFormViewModel = new MovieFormViewModel()
            {
                Movie = movie,
                Genres = GetGenres(),
            };

            return View("MovieForm", movieFormViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Save(Movie movie)
        {
            if (movie.NumberAvailable > movie.NumberInStock)
                throw new Exception("Number Available can't be larger than Number In Stock.");

            if (movie.Id > 0)
            {
                var updateMovie = _context.Movies.Single(c => c.Id == movie.Id);

                updateMovie.Name = movie.Name;
                updateMovie.ReleaseDate = movie.ReleaseDate;
                updateMovie.Genre = movie.Genre;
                updateMovie.NumberInStock = movie.NumberInStock;
                updateMovie.NumberAvailable = movie.NumberAvailable;
            }
            else
            {
                movie.NumberAvailable = movie.NumberInStock;
                movie.DateAdded = DateTime.Now;

                _context.Movies.Add(movie);
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        private IEnumerable<Genre> GetGenres()
        {
            if (MemoryCache.Default["Genres"] == null)
            {
                MemoryCache.Default["Genres"] = _context.Genres.ToList();
            }

            var genres = (IEnumerable<Genre>)MemoryCache.Default["Genres"];

            return genres;
        }
    }
}