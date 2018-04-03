using Microsoft.EntityFrameworkCore;
using System;
namespace CustomerServiceAPI.Entities
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            :base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Client> Clients { get; set; }
    }
}
