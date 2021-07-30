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
    public class CustomersController : ApiController
    {
        private readonly IMapper _mapper;

        private VidlyContext _context;

        public CustomersController()
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

        public IEnumerable<CustomerDto> GetCustomers()
        {
            return _context.Customers.ToList().Select(_mapper.Map<Customer, CustomerDto>);
        }

        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();
            
            return Ok(_mapper.Map<Customer, CustomerDto>(customer));
        }

        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = _mapper.Map<CustomerDto, Customer>(customerDto);
            
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            return Created(new Uri($"{Request.RequestUri}/{customer.Id}"), customerDto);
        }

        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var updateCustomer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (updateCustomer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _mapper.Map(customerDto, updateCustomer);
            _context.SaveChanges();
        }

        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var deleteCustomer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (deleteCustomer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Customers.Remove(deleteCustomer);
            _context.SaveChanges();
        }
    }
}
