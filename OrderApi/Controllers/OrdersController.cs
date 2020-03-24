using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Data;
using OrderApi.Infrastructure;
using OrderApi.Models;
using RestSharp;

namespace OrderApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IRepository<HiddenOrder> repository;
        private readonly IMessagePublisher messagePublisher;

        public OrdersController(IRepository<HiddenOrder> repos, IMessagePublisher publisher)
        {
            repository = repos;
            messagePublisher = publisher;
        }

        // GET: orders
        [HttpGet]
        public IEnumerable<HiddenOrder> Get()
        {
            return repository.GetAll();
        }

        // GET orders/5
        [HttpGet("{id}", Name = "GetOrder")]
        public IActionResult Get(int id)
        {
            var item = repository.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // POST orders
        [HttpPost]
        public IActionResult Post([FromBody]HiddenOrder order)
        {
            if (order == null)
            {
                return BadRequest();
            }
            else
            {
                CustomerExists(order);
            }

        }

        private void CustomerExists(HiddenOrder order)
        {
            try
            {
                messagePublisher.PublishCustomerExists(order);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
