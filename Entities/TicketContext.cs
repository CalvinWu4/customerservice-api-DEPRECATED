using Microsoft.EntityFrameworkCore;
using System;
using CustomerServiceAPI.Models;

namespace CustomerServiceAPI.Entities
{
    public class TicketContext : DbContext
    {
        public TicketContext(DbContextOptions<TicketContext> options)
            : base(options)
        {
        }

        public DbSet<TicketDto> Tickets { get; set; }
    }
}
