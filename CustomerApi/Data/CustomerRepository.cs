using CustomerApi.HiddenModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CustomerApi.Data
{
    public class CustomerRepository : IRepository<HiddenCustomer>
    {
        private readonly CustomerApiContext db;
        public CustomerRepository(CustomerApiContext context)
        {
            db = context;
        }
        public HiddenCustomer Add(HiddenCustomer customer)
        {
            var newCustomer = db.Customers.Add(customer).Entity;
            db.SaveChanges();
            return newCustomer;
        }

        public void Edit(HiddenCustomer modifiedCustomer)
        {
            db.Entry(modifiedCustomer).State = EntityState.Modified;
            db.SaveChanges();
        }

        public HiddenCustomer Get(int id)
        {
            return db.Customers.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<HiddenCustomer> GetAll()
        {
            return db.Customers.ToList();
        }

        public void Remove(int id)
        {
            var customer = db.Customers.FirstOrDefault(c => c.Id == id);
            db.Customers.Remove(customer);
            db.SaveChanges();
        }
    }
}
