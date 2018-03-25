using System;
namespace CustomerServiceAPI.Models
{
    public class ClientDtoForUpdate
    {
        public int clientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string street { get; set; }
    }
}
