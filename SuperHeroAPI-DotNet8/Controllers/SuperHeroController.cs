using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI_DotNet8.Data;
using SuperHeroAPI_DotNet8.Entities;

namespace SuperHeroAPI_DotNet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;
        public SuperHeroController(DataContext context) 
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAllHeroes() 
        {
            var heroes = await _context.SuperHero.ToListAsync();
            return Ok(heroes);
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<SuperHero>>> GetHero(int id)
        {
            var hero = await _context.SuperHero.FindAsync(id);
            if(hero == null) 
            {
                return NotFound("Hreo is NotFound");
            }
            return Ok(hero);
        }
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHero.Add(hero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHero.ToListAsync());
        }
        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero Updatehero)
        {
            var dbHero =await _context.SuperHero.FindAsync(Updatehero.Id);
            if(dbHero == null)
            {
               return BadRequest("Hero not found");
            }
            dbHero.Name=Updatehero.Name;
            dbHero.FirstName = Updatehero.FirstName;
            dbHero.LastName = Updatehero.LastName;
            dbHero.Place = Updatehero.Place;

            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHero.ToListAsync());
        }
        [HttpDelete]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var dbHero = await _context.SuperHero.FindAsync(id);
            if (dbHero == null)
            {
                return BadRequest("Hero not found");
            }
            _context.SuperHero.Remove(dbHero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHero.ToListAsync());

        }
    }
}
