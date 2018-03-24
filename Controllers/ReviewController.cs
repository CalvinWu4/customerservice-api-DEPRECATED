using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CustomerServiceAPI.Models;
using CustomerServiceAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerServiceAPI.Controllers
{
    [Route("api/[controller]")]
    public class ReviewController : Controller
    {
        private IReviewRepository _reviewRepository;

        public ReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        // GET: api/agents/:agentId/reviews
        [HttpGet]
        public IActionResult Get()
        {
            var review = _reviewRepository.GetReviews();
            var results = Mapper.Map<IEnumerable<ReviewDto>>(review);

            return Ok(results);
        }

        // GET: api/agents/:agentId/reviews/:reivew
        [HttpGet("{id}", Name = "GetReview")]
        public IActionResult Get(int id)
        {
            var review = _reviewRepository.GetReview(id);
            if (review == null)
            {
                return NotFound();
            }

            var result = Mapper.Map<ReviewDto>(review);

            return Ok(result);
        }
    }
}