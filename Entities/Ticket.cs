using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerServiceAPI.Entities
{
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string AddressLine1 { get; set; }


        public string AddressLine2 { get; set; }

        [Required]
        public string AddressCity { get; set; }

        [Required]
        public string AddressState { get; set; }

        [Required]
        public string AddressZipcode { get; set; }

        [Required]
        public string AddressCountry { get; set; }
    }
}
