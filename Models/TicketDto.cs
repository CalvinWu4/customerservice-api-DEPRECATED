using System;
namespace CustomerServiceAPI.Models
{
    public class TicketDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        public string Status { get; set; }
        public int ClientId { get; set; }
        public int AgentId { get; set; }
        public int DeviceId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class Address {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
    }
}
