using System;
namespace CustomerServiceAPI.Models
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int AgentId { get; set; }
        public string Content { get; set; }
        public string DateCreated { get; set; }
    }
}
