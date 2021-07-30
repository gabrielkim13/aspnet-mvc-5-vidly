using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Vidly.App_Start;
using Vidly.DAL;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private readonly IMapper _mapper;

        private VidlyContext _context;

        public MoviesController()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = config.CreateMapper();
            _context = new VidlyContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();

            base.Dispose(disposing);
        }

        public IEnumerable<MovieDto> GetMovies()
        {
            return _context.Movies.ToList().Select(_mapper.Map<Movie, MovieDto>);
        }

        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return NotFound();
            
            return Ok(_mapper.Map<Movie, MovieDto>(movie));
        }

        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            movieDto.DateAdded = DateTime.Now;

            var movie = _mapper.Map<MovieDto, Movie>(movieDto);
            
            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;

            return Created(new Uri($"{Request.RequestUri}/{movie.Id}"), movieDto);
        }

        [HttpPut]
        public void UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var updateMovie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (updateMovie == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _mapper.Map(movieDto, updateMovie);
            _context.SaveChanges();
        }

        [HttpDelete]
        public void DeleteMovie(int id)
        {
            var deleteMovie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (deleteMovie == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Movies.Remove(deleteMovie);
            _context.SaveChanges();
        }
    }
}
