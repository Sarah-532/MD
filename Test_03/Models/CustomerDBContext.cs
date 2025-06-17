using Microsoft.EntityFrameworkCore;

namespace Test_03.Models
{
    public class CustomerDBContext:DbContext
    {
        public CustomerDBContext(DbContextOptions<CustomerDBContext>options):base(options)
        {

            
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<DeliveryDetail> DeliveryDetails { get; set; }

    }
}
