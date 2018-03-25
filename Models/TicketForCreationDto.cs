using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerServiceAPI.Models
{
    public class TicketForCreationDto
    {
        [Required(ErrorMessage = "Ticket Status is required")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Ticket Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Ticket Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "DeviceID is required")]
        public string DeviceId { get; set; }
    }
}
