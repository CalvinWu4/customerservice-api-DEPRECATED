using System;
using System.Linq;
using System.Collections.Generic;
using CustomerServiceAPI.Entities;

namespace CustomerServiceAPI.Services
{
    public class ClientRepository : IClientRepository
    {
        private Context _context;

        public ClientRepository(Context context)
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
            return _context.Clients.FirstOrDefault(c => c.Id == clientId);
        }

        public IEnumerable<Client> GetClients()
        {
            return _context.Clients.OrderBy(c => c.Id).ToList();
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
