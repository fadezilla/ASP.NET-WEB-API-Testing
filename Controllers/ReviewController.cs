using ApiEfProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiEfProject.Data;

namespace ApiEfProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public ReviewController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //GET api/Genres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
        {
            if (_dataContext.Reviews == null)
            {
                return NotFound();
            }

            return await _dataContext.Reviews.ToListAsync();
        }

        //GET api/Genres/{id}
        [HttpGet("{Id}")]
        public async Task<ActionResult<Review>> GetReview(int Id)
        {
            if (_dataContext.Reviews == null)
            {
                return NotFound();
            }
            var Review = await _dataContext.Reviews.FindAsync(Id);
            if (Review is null)
            {
                return NotFound();
            }
            return Review;
        }

        //POST api/Genres
        [HttpPost]
        public async Task<ActionResult<Review>> AddReview(Review review)
        {
            _dataContext.Reviews.Add(review);
            await _dataContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetReview), new { id = review.Id }, review);
        }

        //PUT api/Genres/{id}
        [HttpPut("{Id}")]
        public async Task<ActionResult<Review>> UpdateReview(int Id, Review review)
        {
            if (Id != review.Id)
            {
                return BadRequest();
            }
            //Updates entity properties that have been modified
            _dataContext.Update(review);
            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(Id))
                {
                    return NotFound();
                }
                else { throw; }
            }

            return NoContent();
        }
        private bool ReviewExists(int id)
        {
            return (_dataContext.Reviews?.Any(review => review.Id == id)).GetValueOrDefault();
        }

        //DELETE api/Genres/{id}
        [HttpDelete("{Id}")]
        public async Task<ActionResult<Review>> DeleteReview(int Id)
        {
            if (_dataContext.Reviews == null)
            {
                return NotFound();
            }
            var review = await _dataContext.Reviews.FindAsync(Id);
            if (review is null)
            {
                return NotFound();
            }
            _dataContext.Reviews.Remove(review);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }
    }
}