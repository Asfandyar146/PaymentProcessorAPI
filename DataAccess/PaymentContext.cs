using Microsoft.EntityFrameworkCore;
using PaymentProcessorAPI.Models;

namespace PaymentProcessorAPI.DataAccess
{
    public class PaymentContext : DbContext
    {
        public PaymentContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<PaymentState> States { get; set; }
        public DbSet<Request> Requests { get; set; }
    }

}
