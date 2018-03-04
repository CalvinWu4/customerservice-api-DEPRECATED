using System;
namespace CustomerServiceAPI.Models
{
    public class TicketDtoForUpdate
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        public string Description { get; set; }
    }
}
