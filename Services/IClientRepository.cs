using System;
using System.Collections.Generic;
using CustomerServiceAPI.Entities;
using CustomerServiceAPI.Models;

namespace CustomerServiceAPI.Services
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetClients();
        Client GetClient(int clientId);
        void AddClient(Client client);
        void UpdateClient(Client client);
        void DeleteClient(Client client);
        bool Save();
    }
}
