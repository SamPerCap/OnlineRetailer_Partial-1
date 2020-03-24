using CustomerApi.Data;
using CustomerApi.HiddenModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CustomerApi.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IRepository<HiddenCustomer> repository;

        public CustomerController(IRepository<HiddenCustomer> repo)
        {
            repository = repo;
        }

        // GET: api/Customer
        [HttpGet]
        public IEnumerable<HiddenCustomer> Get()
        {
            return repository.GetAll();
        }
        // GET: api/Customer/5
        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult Get(int id)
        {
            var item = repository.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
        // POST: api/Customer
        [HttpPost]
        public IActionResult Post([FromBody] HiddenCustomer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }
            var newCustomer = repository.Add(customer);
            return CreatedAtRoute("GetCustomer", new
            {
                id = newCustomer.Id,
                name = newCustomer.Name
            },
                newCustomer);
        }
    }
}