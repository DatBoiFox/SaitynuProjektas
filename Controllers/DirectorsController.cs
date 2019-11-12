using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaitynuProjektas.Models;

namespace SaitynuProjektas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorsController : ControllerBase
    {
        private readonly DataBaseContext _context;
        public DirectorsController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: api/Directors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Director>>> GetDirectors()
        {
            return await _context.Directors.ToListAsync();
        }

        // GET: api/Directors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Director>> GetDirector(int id)
        {
            Director director = await _context.Directors.FindAsync(id);
            director = await _context.Directors.Include(movies => movies.movies).FirstOrDefaultAsync(dir => dir.id == id);
            if (director == null)
            {
                return NotFound();
            }

            
            return director;
        }

        [HttpGet("{id}/movies")]
        public async Task<ActionResult<List<Movie>>> GetDirectorMovies(int id)
        {
            var req = await _context.Directors.Include(movies => movies.movies).FirstOrDefaultAsync(i => i.id == id);
            try
            {
                return req.movies;
            }
            catch(Exception e)
            {
                return NotFound();
            }



            
        }

        [HttpGet("{id}/movies/{ids}")]
        public async Task<ActionResult<Movie>> GetDirectorMovie(int id, int ids)
        {

            var req = await _context.Directors.Include(movies => movies.movies).FirstOrDefaultAsync(i => i.id == id);
            try
            {
                return req.movies[ids];
            }catch(Exception e)
            {
                return NotFound();
            }
        }

        // PUT: api/Directors/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutDirector(int id, Director director)
        {
            if (id != director.id)
            {
                return NotFound();
            }

            _context.Entry(director).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DirectorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/Directors
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Director>> PostDirector(Director director)
        {
            _context.Directors.Add(director);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDirector", new { id = director.id }, director);
        }

        // DELETE: api/Directors/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Director>> DeleteDirector(int id)
        {
            var director = await _context.Directors.FindAsync(id);
            if (director == null)
            {
                return NotFound();
            }

            _context.Directors.Remove(director);
            await _context.SaveChangesAsync();

            return director;
        }

        private bool DirectorExists(int id)
        {
            return _context.Directors.Any(e => e.id == id);
        }
    }
}
