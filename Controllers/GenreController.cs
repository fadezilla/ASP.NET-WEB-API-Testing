using ApiEfProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiEfProject.Data;

namespace ApiEfProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public GenreController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //GET api/Genres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
        {
            if (_dataContext.Genres == null)
            {
                return NotFound();
            }

            return await _dataContext.Genres.ToListAsync();
        }

        //GET api/Genres/{id}
        [HttpGet("{Id}")]
        public async Task<ActionResult<Genre>> GetGenre(int Id)
        {
            if (_dataContext.Genres == null)
            {
                return NotFound();
            }
            var genre = await _dataContext.Genres.FindAsync(Id);
            if (genre is null)
            {
                return NotFound();
            }
            return genre;
        }

        //POST api/Genres
        [HttpPost]
        public async Task<ActionResult<Genre>> AddGenre(Genre genre)
        {
            _dataContext.Genres.Add(genre);
            await _dataContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetGenre), new { id = genre.Id }, genre);
        }

        //PUT api/Genres/{id}
        [HttpPut("{Id}")]
        public async Task<ActionResult<Genre>> UpdateGenre(int Id, Genre genre)
        {
            if (Id != genre.Id)
            {
                return BadRequest();
            }
            //Updates entity properties that have been modified
            _dataContext.Update(genre);
            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreExists(Id))
                {
                    return NotFound();
                }
                else { throw; }
            }

            return NoContent();
        }
        private bool GenreExists(int id)
        {
            return (_dataContext.Genres?.Any(genre => genre.Id == id)).GetValueOrDefault();
        }

        //DELETE api/Genres/{id}
        [HttpDelete("{Id}")]
        public async Task<ActionResult<Genre>> DeleteGenre(int Id)
        {
            if (_dataContext.Genres == null)
            {
                return NotFound();
            }
            var genre = await _dataContext.Genres.FindAsync(Id);
            if (genre is null)
            {
                return NotFound();
            }
            _dataContext.Genres.Remove(genre);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }
    }
}