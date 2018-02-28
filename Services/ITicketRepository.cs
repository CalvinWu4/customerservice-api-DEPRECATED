using System;
using System.Collections.Generic;
using CustomerServiceAPI.Entities;

namespace CustomerServiceAPI.Services
{
    public interface ITicketRepository
    {
        IEnumerable<Ticket> GetTickets();
        Ticket GetTicket(int ticketId);
        void AddTicket(Ticket ticket);
        bool Save();
    }
}
