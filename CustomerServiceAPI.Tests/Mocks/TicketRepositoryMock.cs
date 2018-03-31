using System.Collections.Generic;
using CustomerServiceAPI.Entities;
using CustomerServiceAPI.Services;
using System.Linq;

namespace CustomerServiceAPI.Tests.Mocks
{
    public class TicketRepositoryMock : ITicketRepository
    {
        List<Ticket> _tickets;

        public TicketRepositoryMock()
        {
            _tickets = new List<Ticket>();
        }

        public void AddTicket(Ticket ticket)
        {
            _tickets.Add(ticket);
        }

        public void DeleteTicket(Ticket ticket)
        {
            var ticketId = GetTicketIndex(ticket.Id);
            if (ticketId < 0) return;

            _tickets.RemoveAt(ticketId);
        }

        public Ticket GetTicket(int ticketId)
        {
            return _tickets.FirstOrDefault<Ticket>(t => t.Id == ticketId);
        }

        public IEnumerable<Ticket> GetTickets()
        {
            return _tickets;
        }

        public bool Save()
        {
            return true;
        }

        public void UpdateTicket(Ticket ticket)
        {
            var ticketId = GetTicketIndex(ticket.Id);
            if (ticketId < 0) return;

            _tickets[ticketId] = ticket;
        }

        int GetTicketIndex(int ticketId)
        {
            return _tickets.FindIndex(t => t.Id == ticketId);
        }
    }
}
