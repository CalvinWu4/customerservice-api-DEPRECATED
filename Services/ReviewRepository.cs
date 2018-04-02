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

        public Review GetReview(int reviewId)
        {
            return _context.Reviews.FirstOrDefault(t => t.Id == reviewId);
        }

        public IEnumerable<Review> GetReviews()
        {
            return _context.Reviews.OrderBy(t => t.DateCreated).ToList();
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void DeleteReview(Review review)
        {
            _context.Remove(review);
        }

        public void UpdateReview(Review review)
        {
            _context.Update(review);
        }
    }
}
