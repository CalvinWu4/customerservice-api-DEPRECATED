using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerServiceAPI.Entities
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int agentId { get; set; }

        [Required]
        public int clientId { get; set; }

        [Required]
        public string content { get; set; }

        [Required]
        public string dateCreated { get; set; }

    }
}
