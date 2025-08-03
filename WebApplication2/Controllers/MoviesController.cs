using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers;

public class MoviesController : Controller
{
    private readonly MvcMovieContext _context;

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
    
    // GET: Movies
    public async Task<IActionResult> Index()
    {
        return View(await _context.Movie.ToListAsync());
    }
    
    public IActionResult Create()
    {
        return View();
    }

    public IActionResult Edit()
    {

        return View();
    }
    
    public IActionResult Delete()
    {
        return View();
    }
}