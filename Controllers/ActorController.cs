using ApiEfProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiEfProject.Data;

namespace ApiEfProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public ActorController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //GET api/Actors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Actor>>> GetActors()
        {
            if (_dataContext.Actors == null)
            {
                return NotFound();
            }

            return await _dataContext.Actors.ToListAsync();
        }

        //GET api/Actors/{id}
        [HttpGet("{Id}")]
        public async Task<ActionResult<Actor>> GetActor(int Id)
        {
            if (_dataContext.Actors == null)
            {
                return NotFound();
            }
            var studio = await _dataContext.Actors.FindAsync(Id);
            if (studio is null)
            {
                return NotFound();
            }
            return studio;
        }

        //POST api/Actors
        [HttpPost]
        public async Task<ActionResult<Actor>> AddActor(Actor studio)
        {
            _dataContext.Actors.Add(studio);
            await _dataContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetActor), new { id = studio.Id }, studio);
        }

        //PUT api/Actors/{id}
        [HttpPut("{Id}")]
        public async Task<ActionResult<Actor>> UpdateActor(int Id, Actor studio)
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
                if (!ActorExists(Id))
                {
                    return NotFound();
                }
                else { throw; }
            }

            return NoContent();
        }
        private bool ActorExists(int id)
        {
            return (_dataContext.Actors?.Any(studio => studio.Id == id)).GetValueOrDefault();
        }

        //DELETE api/Actors/{id}
        [HttpDelete("{Id}")]
        public async Task<ActionResult<Actor>> DeleteActor(int Id)
        {
            if (_dataContext.Actors == null)
            {
                return NotFound();
            }
            var studio = await _dataContext.Actors.FindAsync(Id);
            if (studio is null)
            {
                return NotFound();
            }
            _dataContext.Actors.Remove(studio);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }
    }
}