using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CustomerServiceAPI.Models;
using CustomerServiceAPI.Entities;
using CustomerServiceAPI.Services;

namespace CustomerServiceAPI.Controllers
{
    [Route("api/[controller]")]
    public class ReviewsController : Controller
    {
        IReviewRepository _reviewRepository;

        public ReviewsController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        // GET: api/reviews
        [HttpGet]
        public IActionResult Get()
        {
            var review = _reviewRepository.GetReviews();
            var results = Mapper.Map<IEnumerable<ReviewDto>>(review);

            return Ok(_reviewRepository.GetReviews());
        }

        // GET: api/reviews/1
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

        // POST api/reviews
        [HttpPost]
        public IActionResult Post([FromBody]ReviewDtoForCreation review)
        {
            if (review == null) return BadRequest();

            var finalReview = Mapper.Map<Review>(review);
            _reviewRepository.AddReview(finalReview);

            if (!_reviewRepository.Save())
            {
                return BadRequest();
            }

            return CreatedAtRoute("GetReview", new { id = finalReview.Id }, finalReview);
        }

        // PUT api/reviews/1
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ReviewDtoForUpdate reviewData)
        {
            if (reviewData == null) return BadRequest();

            var review = _reviewRepository.GetReview(id);
            if (review == null) return NotFound();

            review.AgentId = reviewData.AgentId == null ? review.AgentId : reviewData.AgentId;
            review.Content = reviewData.Content == null ? review.Content : reviewData.Content;
            review.DateCreated = reviewData.DateCreated == null ? review.DateCreated : reviewData.DateCreated;


            _reviewRepository.UpdateReview(review);
            if (!_reviewRepository.Save()) return BadRequest();

            return NoContent();
        }

        // DELETE api/reviews/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var review = _reviewRepository.GetReview(id);
            if (review == null) NotFound();

            _reviewRepository.DeleteReview(review);
            if (!_reviewRepository.Save())
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}