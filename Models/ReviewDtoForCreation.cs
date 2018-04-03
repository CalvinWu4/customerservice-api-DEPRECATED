using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerServiceAPI.Models
{
    public class ReviewDtoForCreation
    {
        [Required(ErrorMessage = "Agent Id is required")]
        public int AgentId { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Date created is required")]
        public string DateCreated { get; set; }

    }
}
