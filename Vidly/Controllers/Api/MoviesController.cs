using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private readonly IMapper _mapper;

        private ApplicationDbContext _context;

        public MoviesController()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = config.CreateMapper();
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();

            base.Dispose(disposing);
        }

        public IEnumerable<MovieDto> GetMovies(string query = "")
        {
            var movies = _context.Movies.Include(m => m.Genre);

            if (!string.IsNullOrWhiteSpace(query))
                movies = movies.Where(m => m.Name.Contains(query) && m.NumberAvailable > 0);

            return movies.ToList().Select(_mapper.Map<Movie, MovieDto>);
        }

        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies
                .Include(m => m.Genre)
                .SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return NotFound();
            
            return Ok(_mapper.Map<Movie, MovieDto>(movie));
        }

        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (movieDto.NumberAvailable > movieDto.NumberInStock)
                return BadRequest("Number Available can't be larger than Number In Stock.");

            movieDto.DateAdded = DateTime.Now;

            var movie = _mapper.Map<MovieDto, Movie>(movieDto);
            
            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;

            return Created(new Uri($"{Request.RequestUri}/{movie.Id}"), movieDto);
        }

        [HttpPut]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var updateMovie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (updateMovie == null)
                return NotFound();

            if (movieDto.NumberAvailable > movieDto.NumberInStock)
                return BadRequest("Number Available can't be larger than Number In Stock.");

            _mapper.Map(movieDto, updateMovie);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult DeleteMovie(int id)
        {
            var deleteMovie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (deleteMovie == null)
                return NotFound();

            _context.Movies.Remove(deleteMovie);
            _context.SaveChanges();

            return Ok();
        }
    }
}
