using System;
namespace CustomerServiceAPI.Models
{
    public class TicketDtoForUpdate
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string DeviceId { get; set; }
    }
}
