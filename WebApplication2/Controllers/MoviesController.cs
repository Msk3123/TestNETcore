using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;

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
    public async Task<IActionResult> Index(string? id)
    {
        IQueryable<Movie> query = _context.Movie; // DbSet<Movie> як IQueryable

        if (!string.IsNullOrWhiteSpace(id))
            query = query.Where(x => x.Title != null &&
                                     EF.Functions.Like(x.Title, $"%{id}%"));

        var items = await query.AsNoTracking().ToListAsync(); // читання — без трекінгу
        return View(items);
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