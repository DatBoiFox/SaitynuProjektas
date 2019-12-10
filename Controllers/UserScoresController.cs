using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaitynuProjektas.Models;

namespace SaitynuProjektas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserScoresController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public UserScoresController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: api/UserScores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserScore>>> GetUserScores()
        {
            return await _context.UserScores.ToListAsync();
        }

        // GET: api/UserScores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserScore>> GetUserScore(int id)
        {
            var userScore = await _context.UserScores.FindAsync(id);

            if (userScore == null)
            {
                return NotFound();
            }

            return userScore;
        }

        // PUT: api/UserScores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserScore(int id, SimpleUserScore simpleScore)
        {
            UserScore userScore = new UserScore();
            userScore.id = simpleScore.id;
            userScore.movieId = simpleScore.movieId;
            userScore.userId = simpleScore.userId;
            userScore.score = simpleScore.score;

            userScore.user = await _context.Users.FindAsync(simpleScore.userId);
            userScore.movie = await _context.Movies.FindAsync(simpleScore.movieId);


            if (id != userScore.id)
            {
                return BadRequest();
            }

            _context.Entry(userScore).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserScoreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserScores
        [HttpPost]
        public async Task<ActionResult<UserScore>> PostUserScore(SimpleUserScore simpleScore)
        {

            UserScore userScore = new UserScore();

            userScore.movieId = simpleScore.movieId;
            userScore.userId = simpleScore.userId;
            userScore.score = simpleScore.score;

            userScore.user = await _context.Users.FindAsync(simpleScore.userId);
            userScore.movie = await _context.Movies.FindAsync(simpleScore.movieId);
            _context.UserScores.Add(userScore);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserScore", new { id = userScore.id }, userScore);
        }

        // DELETE: api/UserScores/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserScore>> DeleteUserScore(int id)
        {
            var userScore = await _context.UserScores.FindAsync(id);
            if (userScore == null)
            {
                return NotFound();
            }

            _context.UserScores.Remove(userScore);
            await _context.SaveChangesAsync();

            return userScore;
        }

        private bool UserScoreExists(int id)
        {
            return _context.UserScores.Any(e => e.id == id);
        }
    }
}
