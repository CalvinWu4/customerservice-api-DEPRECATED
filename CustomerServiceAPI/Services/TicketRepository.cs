using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using CustomerServiceAPI.Entities;
using CustomerServiceAPI.Models;

namespace CustomerServiceAPI.Services
{
    public class TicketRepository<T> : IRepository<Ticket>
    {
        private readonly Context _context;

        public TicketRepository(Context context)
        {
            _context = context;
        }

        public void Add(Ticket ticket)
        {
            _context.Add(ticket);
        }

        public void Update(Ticket ticket)
        {
            _context.Update(ticket);
        }

        public Ticket Query(int id)
        {
            return _context.Tickets.FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Ticket> FetchAll()
        {
            return _context.Tickets.OrderBy(t => t.ClientId).ToList();
        }

        public void Delete(Ticket ticket)
        {
            _context.Remove(ticket);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
