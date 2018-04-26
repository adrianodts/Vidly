using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Policy;
using System.Web.Http;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public CustomersController()
        {
            this._context = new ApplicationDbContext();
        }

        [HttpGet]
        public IHttpActionResult GetCustomers()
        {
            var customersList = this._context.Customers
                .Include(c => c.MembershipType)
                .ToList()
                .Select(Mapper.Map<Customer, CustomersDto>);

            return Ok(customersList);
        }

        [HttpGet]
        public IHttpActionResult GetMovieById(int id)
        {
            var customer = _context.Customers
                .SingleOrDefault(m => m.Id == id);

            if (customer == null)
                return NotFound();

            return Ok(Mapper.Map<Customer, CustomersDto>(customer));
        }

        [HttpPost]
        public IHttpActionResult CreateMovie(CustomersDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();    

            var customer = Mapper.Map<CustomersDto, Customer>(customerDto);

            this._context.Customers.Add(customer);
            this._context.SaveChanges();

            customerDto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }

        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, CustomersDto customersDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customerInDb = _context.Customers
                .SingleOrDefault(m => m.Id == id);

            if (customerInDb == null)
                return NotFound();

            Mapper.Map(customersDto, customerInDb);

            //this because autommaper (customerDto) changes the value of Id field (customerInDb)
            customerInDb.Id = id;

            this._context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult RemoveMovie(int id)
        {
            var customer = this._context.Customers.SingleOrDefault(m => m.Id == id);

            if (customer == null)
                return NotFound();

            this._context.Customers.Remove(customer);
            this._context.SaveChanges();

            return Ok();
        }
    }

 
}