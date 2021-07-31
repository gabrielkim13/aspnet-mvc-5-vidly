using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class RentalsController : ApiController
    {
        private readonly IMapper _mapper;

        private ApplicationDbContext _context;

        public RentalsController()
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

        [HttpPost]
        public IHttpActionResult CreateRental(CreateRentalDto createRentalDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model state is invalid.");
            
            var customer = _context.Customers.SingleOrDefault(c => c.Id == createRentalDto.CustomerId);

            if (customer == null)
                return BadRequest("Customer not found.");

            var rentableMovies = _context.Movies
                .Where(m => createRentalDto.MoviesIds.Contains(m.Id) && m.NumberAvailable > 0)
                .ToList();

            var rentableMoviesIds = rentableMovies.Select(m => m.Id);
            var unrentableMoviesIds = rentableMoviesIds.Except(createRentalDto.MoviesIds);

            if (unrentableMoviesIds.Count() > 0)
            {
                var unrentableMoviesIdsString = string.Join(", ", unrentableMoviesIds.Select(x => x.ToString()).ToArray());
                
                return BadRequest($"The following movies cannot be rented: {unrentableMoviesIdsString}.");
            }

            foreach (var movie in rentableMovies)
            {
                --movie.NumberAvailable;

                var rental = new Rental()
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now,
                };
                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();

            return Ok();
        }
    }
}
