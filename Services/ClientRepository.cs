using System;
using System.Collections.Generic;
using System.Linq;
using CustomerServiceAPI.Entities;
using CustomerServiceAPI.Models;

namespace CustomerServiceAPI.Services
{
    public class ClientRepository : IClientRepository
    {
        private ClientContext _context;

        public ClientRepository(ClientContext context)
        {
            _context = context;
        }

        public void AddClient(Client client)
        {
            _context.Add(client);
        }

        public void UpdateClient(Client client)
        {
            _context.Update(client);
        }

        public Client GetClient(int clientId)
        {
            return _context.Clients.FirstOrDefault(t => t.clientId == clientId);
        }

        public IEnumerable<Client> GetClients()
        {
            return _context.Clients.OrderBy(t => t.FirstName).ToList();
        }

        public void DeleteClient(Client client)
        {
            _context.Remove(client);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
