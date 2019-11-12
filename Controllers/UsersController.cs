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
    public class UsersController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public UsersController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        [Authorize(Roles = "Administrator, User")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator, User")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // GET: api/Users/5
        [HttpGet("{id}/favorites")]
        [Authorize(Roles = "Administrator, User")]
        public async Task<ActionResult<List<Movie>>> GetUserFavoritMovies(int id)
        {
            var user = await _context.Users.Include(fav => fav.favoriteMovies).FirstOrDefaultAsync(i => i.id == id);

            if (user == null)
            {
                return NotFound();
            }

            List<Movie> movies = new List<Movie>();
            foreach(FavoriteMovie fm in user.favoriteMovies)
            {
                movies.Add(await _context.Movies.FindAsync(fm.movieId));
            }


            return movies;
        }

        // GET: api/Users/5
        [HttpGet("{id}/favorites/{ids}")]
        [Authorize(Roles = "Administrator, User")]
        public async Task<ActionResult<Movie>> GetUserFavoritMovie(int id, int ids)
        {
            var user = await _context.Users.Include(fav => fav.favoriteMovies).FirstOrDefaultAsync(i => i.id == id);

            if (user == null)
            {
                return NotFound();
            }

            try
            {
                return await _context.Movies.FindAsync(user.favoriteMovies[ids].movieId);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("{id}/planToWatch")]
        [Authorize(Roles = "Administrator, User")]
        public async Task<ActionResult<List<Movie>>> GetUserPlanToWatches(int id)
        {
            var user = await _context.Users.Include(fav => fav.planToWatchMovies).FirstOrDefaultAsync(i => i.id == id);

            if (user == null)
            {
                return NotFound();
            }

            List<Movie> movies = new List<Movie>();
            foreach (PlanToWatch fm in user.planToWatchMovies)
            {
                movies.Add(await _context.Movies.FindAsync(fm.movieId));
            }


            return movies;
        }

        // GET: api/Users/5
        [HttpGet("{id}/planToWatch/{ids}")]
        [Authorize(Roles = "Administrator, User")]
        public async Task<ActionResult<Movie>> GetUserPlanToWatch(int id, int ids)
        {
            var user = await _context.Users.Include(fav => fav.planToWatchMovies).FirstOrDefaultAsync(i => i.id == id);

            if (user == null)
            {
                return NotFound();
            }

            try
            {
                return await _context.Movies.FindAsync(user.planToWatchMovies[ids].movieId);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("{id}/scores")]
        [Authorize(Roles = "Administrator, User")]
        public async Task<ActionResult<List<UserScore>>> GetUserScores(int id)
        {
            var user = await _context.Users.Include(fav => fav.userScores).FirstOrDefaultAsync(i => i.id == id);

            if (user == null)
            {
                return NotFound();
            }

            List<UserScore> scores = new List<UserScore>();
            foreach (UserScore fm in user.userScores)
            {
                scores.Add(await _context.UserScores.FindAsync(fm.id));
            }


            return scores;
        }

        // GET: api/Users/5
        [HttpGet("{id}/scores/{ids}")]
        [Authorize(Roles = "Administrator, User")]
        public async Task<ActionResult<UserScore>> GetUserScore(int id, int ids)
        {
            var user = await _context.Users.Include(fav => fav.userScores).FirstOrDefaultAsync(i => i.id == id);

            if (user == null)
            {
                return NotFound();
            }

            try
            {
                return  user.userScores[ids];
            }
            catch
            {
                return NotFound();
            }
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [HttpPost]
        [Authorize(Roles = "Administrator, User")]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.id == id);
        }
    }
}
