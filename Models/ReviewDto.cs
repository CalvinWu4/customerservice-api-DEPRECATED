using System;
namespace CustomerServiceAPI.Models
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int agentID { get; set; }
        public int clientID { get; set; }
        public string content { get; set; }
        public string dateCreated { get; set; }
    }
}
