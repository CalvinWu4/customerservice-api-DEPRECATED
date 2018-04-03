using System;
using System.Collections.Generic;
using CustomerServiceAPI.Entities;
using CustomerServiceAPI.Models;

namespace CustomerServiceAPI.Services
{
    public interface ITicketRepository
    {
        IEnumerable<Ticket> GetTickets();
        Ticket GetTicket(int ticketId);
        void AddTicket(Ticket ticket);
        void UpdateTicket(Ticket ticket);
        void DeleteTicket(Ticket ticket);
        bool Save();
    }
}
