using System;
using System.Linq;
using System.Collections.Generic;
using CustomerServiceAPI.Entities;

namespace CustomerServiceAPI.Services
{
    public class ClientRepository : IRepository<Client>
    {
        private readonly Context _context;

        public ClientRepository(Context context)
        {
            _context = context;
        }

        public void Add(Client client)
        {
            _context.Add(client);
        }

        public void Update(Client client)
        {
            _context.Update(client);
        }

        public Client Query(int clientId)
        {
            return _context.Clients.FirstOrDefault(c => c.Id == clientId);
        }

        public IEnumerable<Client> FetchAll()
        {
            return _context.Clients.OrderBy(c => c.Id).ToList();
        }

        public void Delete(Client client)
        {
            _context.Remove(client);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
