using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace WebApplication2.Controllers;

public class MoviesController : Controller
{
    private readonly MvcMovieContext _context;
    private bool MovieExists(int id)
    {
        return _context.Movie.Any(e => e.Id == id);
    }
    public MoviesController(MvcMovieContext context)
    {
        _context = context;
    }
    // GET: Movies/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        
        if (id == null || id <= 0)
        {
            return NotFound();
        }

        var movie = await _context.Movie.FindAsync(id);
        
        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }
    
    [HttpPost]
    public string Index(string searchString, bool notUsed)
    {
        return "From [HttpPost]Index: filter on " + searchString;
    }
    public async Task<IActionResult> Index(string movieGenre, string searchString)
    {
        if (_context.Movie == null)
        {
            return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
        }

        // Use LINQ to get list of genres.
        IQueryable<string> genreQuery = from m in _context.Movie
            orderby m.Genre
            select m.Genre;
        var movies = from m in _context.Movie
            select m;

        if (!string.IsNullOrEmpty(searchString))
        {
            movies = movies.Where(s => s.Title!.ToUpper().Contains(searchString.ToUpper()));
        }

        if (!string.IsNullOrEmpty(movieGenre))
        {
            movies = movies.Where(x => x.Genre == movieGenre);
        }

        var movieGenreVM = new MovieGenreViewModel
        {
            Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
            Movies = await movies.ToListAsync()
        };

        return View(movieGenreVM);
    }
    
    public IActionResult Create()
    {
        return View();
    }
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie.FindAsync(id);
        if (movie == null)
        {
            return NotFound();
        }
        return View(movie);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price")] Movie movie)
    {
        if(id != movie.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(movie);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(movie.Id))
                {
                    return NotFound();
                }
                else{throw;}
                
            }
            return RedirectToAction(nameof(Index));

        }
        return View(movie);
        
    }
    
    public IActionResult Delete()
    {
        return View();
    }
}