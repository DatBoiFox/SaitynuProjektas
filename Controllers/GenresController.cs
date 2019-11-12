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
    public class GenresController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public GenresController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: api/Genres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
        {
            return await _context.Genres.ToListAsync();
        }

        // GET: api/Genres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> GetGenre(int id)
        {
            var genre = await _context.Genres.FindAsync(id);

            if (genre == null)
            {
                return NotFound();
            }

            return genre;
        }

        // PUT: api/Genres/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutGenre(int id, Genre genre)
        {
            if (id != genre.id)
            {
                return BadRequest();
            }

            _context.Entry(genre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreExists(id))
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

        // POST: api/Genres
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Genre>> PostGenre(Genre genre)
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGenre", new { id = genre.id }, genre);
        }

        // DELETE: api/Genres/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Genre>> DeleteGenre(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();

            return genre;
        }



        // GET: api/Movies/5/genres/id
        [HttpGet("{id}/movies")]
        public async Task<ActionResult<List<Movie>>> GetGenreMovies(int id)
        {
            //List<MovieGenre> a = _context.MovieGenres.Include(movie => movie.movie).ThenInclude(director => director.director).ToList();
            List<MovieGenre> a = _context.MovieGenres.Include(movie => movie.movie).ToList();

            List<Movie> movies = new List<Movie>();

            foreach(MovieGenre mg in a)
            {
                if (mg.genreId.Equals(id))
                {
                    movies.Add(_context.Movies.Find(mg.movieId));
                }
            }

            if (movies.Count <=0)
            {
                return NotFound();
            }

            return movies;

        }

        // GET: api/Movies/5/genres/id
        [HttpGet("{id}/movies/{ids}")]
        public async Task<ActionResult<Movie>> GetGenreMovie(int id, int ids)
        {
            //List<MovieGenre> a = _context.MovieGenres.Include(movie => movie.movie).ThenInclude(director => director.director).ToList();
            List<MovieGenre> a = _context.MovieGenres.Include(movie => movie.movie).ToList();

            List<Movie> movies = new List<Movie>();

            foreach (MovieGenre mg in a)
            {
                if (mg.genreId.Equals(id))
                {
                    movies.Add(_context.Movies.Find(mg.movieId));
                }
            }

            try
            {
                return movies[ids];
            }catch(Exception e)
            {
                return NotFound();
            }

        }



        private bool GenreExists(int id)
        {
            return _context.Genres.Any(e => e.id == id);
        }
    }
}
