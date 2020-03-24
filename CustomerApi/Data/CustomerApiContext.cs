using CustomerApi.HiddenModel;
using Microsoft.EntityFrameworkCore;

namespace CustomerApi.Data
{
    public class CustomerApiContext : DbContext
    {
        public CustomerApiContext(DbContextOptions<CustomerApiContext> options)
                : base(options)
        { }

        public DbSet<HiddenCustomer> Customers { get; set; }
    }
}