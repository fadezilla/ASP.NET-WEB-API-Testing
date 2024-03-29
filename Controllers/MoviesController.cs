using ApiEfProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiEfProject.Data;

namespace ApiEfProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public MoviesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //GET api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            if (_dataContext.Movies == null)
            {
                return NotFound();
            }

            return await _dataContext.Movies.ToListAsync();
        }

        //GET api/Movies/{id}
        [HttpGet("{Id}")]
        public async Task<ActionResult<Movie>> GetMovie(int Id)
        {
            if (_dataContext.Movies == null)
            {
                return NotFound();
            }
            var movie = await _dataContext.Movies.FindAsync(Id);
            if (movie is null)
            {
                return NotFound();
            }
            return movie;
        }

        //POST api/Movies
        [HttpPost]
        public async Task<ActionResult<Movie>> AddMovie(Movie movie)
        {
            _dataContext.Movies.Add(movie);
            await _dataContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
        }

        //PUT api/Movies/{id}
        [HttpPut("{Id}")]
        public async Task<ActionResult<Movie>> UpdateMovie(int Id, Movie movie)
        {
            if (Id != movie.Id)
            {
                return BadRequest();
            }
            //Updates entity properties that have been modified
            _dataContext.Update(movie);
            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(Id))
                {
                    return NotFound();
                }
                else { throw; }
            }

            return NoContent();
        }
        private bool MovieExists(int id)
        {
            return (_dataContext.Movies?.Any(movie => movie.Id == id)).GetValueOrDefault();
        }

        //DELETE api/Movies/{id}
        [HttpDelete("{Id}")]
        public async Task<ActionResult<Movie>> DeleteMovie(int Id)
        {
            if (_dataContext.Movies == null)
            {
                return NotFound();
            }
            var movie = await _dataContext.Movies.FindAsync(Id);
            if (movie is null)
            {
                return NotFound();
            }
            _dataContext.Movies.Remove(movie);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }
    }
}