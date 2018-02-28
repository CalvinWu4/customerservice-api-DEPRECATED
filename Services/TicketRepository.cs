﻿using System;
using System.Collections.Generic;
using System.Linq;
using CustomerServiceAPI.Entities;

namespace CustomerServiceAPI.Services
{
    public class TicketRepository : ITicketRepository
    {
        private TicketContext _context;

        public TicketRepository(TicketContext context)
        {
            _context = context;
        }

        public void AddTicket(Ticket ticket)
        {
            _context.Add(ticket);
        }

        public Ticket GetTicket(int ticketId)
        {
            return _context.Tickets.FirstOrDefault(t => t.Id == ticketId);
        }

        public IEnumerable<Ticket> GetTickets()
        {
            return _context.Tickets.OrderBy(t => t.FirstName).ToList();
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}