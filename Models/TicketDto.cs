using System;
namespace CustomerServiceAPI.Models
{
    public class TicketDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Status { get; set; }
        public int ClientId { get; set; }
        public int AgentId { get; set; }
        public int DeviceId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Opened { get; set; }
        public DateTime Closed { get; set; }
    }
}
