using System;
namespace CustomerServiceAPI.Models
{
    public class TicketDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
    }
}
