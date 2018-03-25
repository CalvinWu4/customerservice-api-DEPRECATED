using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerServiceAPI.Models
{
    public class ClientDtoForCreation
    {
        [Required(ErrorMessage = "Customer's Id is required")]
        public int clientId { get; set; }

        [Required(ErrorMessage = "Customer's FirstName is required")]
        public String FirstName { get; set; }

        [Required(ErrorMessage = "Customer's LastName is required")]
        public String LastName { get; set; }

        [Required(ErrorMessage = "Customer's Address is required")]
        public string street { get; set; }
    }
}
