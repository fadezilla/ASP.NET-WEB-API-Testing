using ApiEfProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiEfProject.Data;

namespace ApiEfProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudioController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public StudioController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //GET api/Studios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Studio>>> GetStudios()
        {
            if (_dataContext.Studios == null)
            {
                return NotFound();
            }

            return await _dataContext.Studios.ToListAsync();
        }

        //GET api/Studios/{id}
        [HttpGet("{Id}")]
        public async Task<ActionResult<Studio>> GetStudio(int Id)
        {
            if (_dataContext.Studios == null)
            {
                return NotFound();
            }
            var studio = await _dataContext.Studios.FindAsync(Id);
            if (studio is null)
            {
                return NotFound();
            }
            return studio;
        }

        //POST api/Studios
        [HttpPost]
        public async Task<ActionResult<Studio>> AddStudio(Studio studio)
        {
            _dataContext.Studios.Add(studio);
            await _dataContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStudio), new { id = studio.Id }, studio);
        }

        //PUT api/Studios/{id}
        [HttpPut("{Id}")]
        public async Task<ActionResult<Studio>> UpdateStudio(int Id, Studio studio)
        {
            if (Id != studio.Id)
            {
                return BadRequest();
            }
            //Updates entity properties that have been modified
            _dataContext.Update(studio);
            try
            {
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudioExists(Id))
                {
                    return NotFound();
                }
                else { throw; }
            }

            return NoContent();
        }
        private bool StudioExists(int id)
        {
            return (_dataContext.Studios?.Any(studio => studio.Id == id)).GetValueOrDefault();
        }

        //DELETE api/Studios/{id}
        [HttpDelete("{Id}")]
        public async Task<ActionResult<Studio>> DeleteStudio(int Id)
        {
            if (_dataContext.Studios == null)
            {
                return NotFound();
            }
            var studio = await _dataContext.Studios.FindAsync(Id);
            if (studio is null)
            {
                return NotFound();
            }
            _dataContext.Studios.Remove(studio);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }
    }
}