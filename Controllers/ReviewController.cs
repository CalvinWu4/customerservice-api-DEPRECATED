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

        // POST api/review/1
        [HttpPost]
        public IActionResult Post([FromBody]ReviewDtoForCreation review)
        {
            if (review == null)
            {
                return BadRequest();
            }

            //set ticket as 'new' status
            //ticket.Status = "new";

            var finalReview = Mapper.Map<Entities.Review>(review);

            // Default values until Clients & Agent API is mocked
            finalReview.AgentId = 0;

            _reviewRepository.AddReview(finalReview);

            if (!_reviewRepository.Save())
            {
                return StatusCode(500, "An error happened while creating a review");
            }

            var createdReview = Mapper.Map<Models.ReviewDto>(finalReview);

            return CreatedAtRoute("GetReview", new { id = createdReview.Id }, createdReview);
        }

        // PUT api/reviews/1
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/reviews/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var review = _reviewRepository.GetReview(id);
            if (review == null)
            {
                return NotFound();
            }

            _reviewRepository.DeleteReview(review);
            if (!_reviewRepository.Save())
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
