using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerServiceAPI.Models
{
    public class ReviewDtoForCreation
    {
        [Required(ErrorMessage = "Content is required")]
        public string content { get; set; }

    }
}
