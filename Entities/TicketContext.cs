using Microsoft.EntityFrameworkCore;
using System;
namespace CustomerServiceAPI.Entities
{
    public class TicketContext : DbContext
    {
        public TicketContext(DbContextOptions<TicketContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }

        DbSet<Ticket> Tickets { get; set; }
    }
}
