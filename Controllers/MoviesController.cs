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
    public class MoviesController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public MoviesController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return await _context.Movies
                //.Include(directors => directors.director)
                //.Include(genres => genres.movieGenres)
                //.ThenInclude(g => g.genre)
                .ToListAsync();

            //var q = from e in _context.Movies where !e.userScores.Equals(null) select e;
            //return q;
        }

        // GET: api/Movies/5/
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movies
                .Include(directors => directors.director)
                .Include(genres => genres.movieGenres)
                .ThenInclude(g => g.genre)
                .FirstOrDefaultAsync(i => i.id == id);
            
            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }
        // GET: api/Movies/5/genres/id
        [HttpGet("{id}/genres/{ids}")]
        public async Task<ActionResult<Genre>> GetMovieGenresId(int id, int ids)
        {
            Movie movie = await _context.Movies.Include(genres => genres.movieGenres).FirstOrDefaultAsync(ia => ia.id == id);

            try
            {
                int i = movie.movieGenres[ids].genreId;
                Genre g = _context.Genres.Find(i);
                return g;
            }catch(Exception e)
            {
                return NotFound();
            }

        }

        // GET: api/Movies/genres
        [HttpGet("{id}/genres")]
        public async Task<ActionResult<List<Genre>>> GetMovieGenres(int id)
        {
            Movie movie = await _context.Movies.Include(genres => genres.movieGenres).FirstOrDefaultAsync(ia => ia.id == id);
            List<Genre> genre = new List<Genre>();

            foreach (MovieGenre mg in movie.movieGenres)
            {
                genre.Add(_context.Genres.Find(mg.genreId));
            }

            return genre;
        }

        // GET: api/Movies/genres
        [HttpGet("{id}/director")]
        public async Task<ActionResult<Director>> GetMovieDirector(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var movie = await _context.Movies
                .Include(directors => directors.director)
                .FirstOrDefaultAsync(i => i.id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie.director;
        }


        // GET: api/Movies/5/userscores/id
        [HttpGet("{id}/userScores/{ids}")]
        public async Task<ActionResult<UserScore>> GetMovieUserScore(int id, int ids)
        {
            Movie movie = await _context.Movies.Include(userScores => userScores.userScores).FirstOrDefaultAsync(ia => ia.id == id);

            int i = movie.userScores[ids].id;
            UserScore g = _context.UserScores.Find(i);
            User us = _context.Users.Find(g.userId);
            g.user = us;
            return g;

        }

        // GET: api/Movies/userscores
        [HttpGet("{id}/userScores")]
        public async Task<ActionResult<List<UserScore>>> GetMovieUserScores(int id)
        {
            Movie movie = await _context.Movies.Include(scores => scores.userScores).FirstOrDefaultAsync(ia => ia.id == id);
            List<UserScore> sc = new List<UserScore>();

            foreach (UserScore mg in movie.userScores)
            {
                sc.Add(_context.UserScores.Find(mg.id));
            }

            return sc;
        }


        // PUT: api/Movies/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.id)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
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

        // POST: api/Movies
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Movie>> PostMovie(MovieBinder movie)
        {
            //Director m = _context.Directors.Find(dir);
            //movie.director = m;

            Movie m = new Movie();
            m.title = movie.title;
            m.releaseDate = movie.releaseDate;
            m.director = _context.Directors.Find(movie.directorId);

            _context.Movies.Add(m);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = m.id }, m);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Movie>> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return movie;
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.id == id);
        }
    }
}
