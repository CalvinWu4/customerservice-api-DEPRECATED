﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerServiceAPI.Models
{
    public class TicketForCreationDto
    {
        [Required(ErrorMessage = "Customer's FirstName is required")]
        public String FirstName { get; set; }

        [Required(ErrorMessage = "Customer's LastName is required")]
        public String LastName { get; set; }

        [Required(ErrorMessage = "Customer's Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Ticket Description is required")]
        public string Description { get; set; }
    }
}