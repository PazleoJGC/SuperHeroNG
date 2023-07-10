using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Models;
using SuperHeroAPI.Data;
using Microsoft.AspNetCore.Authorization;

namespace HeroMediaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroMediaController : ControllerBase
    {
        private readonly DataContext _context;

        public HeroMediaController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<HeroMedia>>> GetHeroMedia()
        {
            return Ok(await _context.HeroMedia.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HeroMedia>> GetHeroMedium(int id)
        {
            var dbMedium = await _context.HeroMedia.Include(m => m.Characters).FirstOrDefaultAsync(m => m.Id == id);
            if (dbMedium == null)
                return NotFound("Medium not found.");
            else
                return Ok(dbMedium);
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<List<HeroMedia>>> CreateHeroMedia(HeroMedia medium)
        {
            _context.HeroMedia.Add(medium);
            await _context.SaveChangesAsync();

            return await GetHeroMedia();
        }

        [HttpPost("{id}/addHeroes"), Authorize]
        public async Task<ActionResult<HeroMedia>> HeroMediaAddCharacters(int id, List<int> heroes = null)
        {
            var dbMedium = await _context.HeroMedia.Include(m => m.Characters).FirstOrDefaultAsync(m => m.Id == id);
            if (dbMedium == null)
                return NotFound("Medium not found.");

            if (heroes != null)
                foreach(int hero in heroes)
                {
                    var dbhero = await _context.SuperHeroes.FindAsync(hero);
                    if (dbhero != null && !dbMedium.Characters.Contains(dbhero))
                        dbMedium.Characters.Add(dbhero);
                }
                await _context.SaveChangesAsync();

            return await GetHeroMedium(id);
        }

        [HttpPut, Authorize]
        public async Task<ActionResult<List<HeroMedia>>> UpdateHeroMedia(HeroMedia medium)
        {
            var dbMedium = (await GetHeroMedium(medium.Id)).Value;
            if (dbMedium == null)
                return BadRequest("Medium not found.");

            dbMedium.Type = medium.Type;
            dbMedium.Title = medium.Title;
            dbMedium.Characters = medium.Characters;
            dbMedium.ReleaseDate = medium.ReleaseDate;

            await _context.SaveChangesAsync();

            return await GetHeroMedia();
        }

        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<List<HeroMedia>>> DeleteHeroMedia(int id)
        {
            var dbMedium = await _context.HeroMedia.FindAsync(id);
            if (dbMedium == null)
                return BadRequest("Medium not found.");

            _context.HeroMedia.Remove(dbMedium);
            await _context.SaveChangesAsync();

            return await GetHeroMedia();
        }
    }
}
