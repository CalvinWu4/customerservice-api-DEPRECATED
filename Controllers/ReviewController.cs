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

        // POST api/agents/:agentId/reviews/:reivew
        [HttpPost]
        public IActionResult Post([FromBody]ReviewDtoForCreation review)
        {
            if (review == null)
            {
                return BadRequest();
            }

            var finalReview = Mapper.Map<Entities.Review>(review);
            _reviewRepository.AddReview(finalReview);

            if (!_reviewRepository.Save())
            {
                return StatusCode(500, "An error happened while creating Review");
            }

            var createdReview = Mapper.Map<Models.ReviewDto>(finalReview);

            return CreatedAtRoute("GetReview", new { Id = createdReview.Id }, createdReview);
        }

        // PUT api/agents/:agentId/reviews/:reivew
        [HttpPut("{id}")]
        public IActionResult Update(int Id, [FromBody] ReviewDtoForUpdate reviewData)
        {
            if (reviewData == null)
            {
                return BadRequest();
            }

            var review = _reviewRepository.GetReview(Id);
            if (review == null)
            {
                return NotFound();
            }

            review.content = reviewData.content == null ? review.content: reviewData.content;
            review.dateCreated = reviewData.dateCreated == null ? review.dateCreated : reviewData.dateCreated;

            _reviewRepository.UpdateReview(review);

            if (!_reviewRepository.Save())
            {
                return BadRequest();
            }

            return new NoContentResult();
        }

        // DELETE api/agents/:agentId/reviews/:reivew
        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            var review = _reviewRepository.GetReview(Id);
            if (review == null)
            {
                return NotFound();
            }

            _reviewRepository.DeleteReview(review);
            if (!_reviewRepository.Save())
            {
                return BadRequest();
            }

            return new NoContentResult();
        }
    }
}