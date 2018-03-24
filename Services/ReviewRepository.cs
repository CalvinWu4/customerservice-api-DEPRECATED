using System;
using System.Collections.Generic;
using System.Linq;
using CustomerServiceAPI.Entities;
using CustomerServiceAPI.Models;

namespace CustomerServiceAPI.Services
{
    public class ReviewRepository : IReviewRepository
    {
        private ReviewContext _context;

        public ReviewRepository(ReviewContext context)
        {
            _context = context;
        }

        public void AddReview(Review review)
        {
            _context.Add(review);
        }

        public void UpdateReivew(Review review)
        {
            _context.Update(review);
        }

        public Review GetReview(int Id)
        {
            return _context.Reviews.FirstOrDefault(t => t.agentId == Id);
        }

        public IEnumerable<Review> GetReviews()
        {
            return _context.Reviews.OrderBy(t => t.dateCreated).ToList();
        }

        public void DeleteTicket(Review review)
        {
            _context.Remove(review);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        IEnumerable<Review> IReviewRepository.GetReviews()
        {
            throw new NotImplementedException();
        }

        public void UpdateReview(Review review)
        {
            throw new NotImplementedException();
        }

        public void DeleteReview(Review review)
        {
            throw new NotImplementedException();
        }
    }
}
