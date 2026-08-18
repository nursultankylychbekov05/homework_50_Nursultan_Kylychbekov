using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using homeWork_50.Models;

namespace homeWork_50.Controllers;

public class CategoryController : Controller
{
    private readonly StoreContext _context;

    public CategoryController(StoreContext context)
    {
        _context = context;
    }
    
    public async Task<IActionResult> Create()
    {
        ViewBag.Categories = await _context.Categories.ToListAsync();
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Category category)
    {
        bool exists = await _context.Categories
            .AnyAsync(c => c.Name.ToLower() == category.Name.Trim().ToLower());

        if (exists)
        {
            ModelState.AddModelError("Name", $"Категория \"{category.Name}\" уже существует!");
        }

        if (ModelState.IsValid)
        {
            category.Name = category.Name.Trim();
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Create));
        }

        ViewBag.Categories = await _context.Categories.ToListAsync();
        return View(category);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category != null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Create));
    }
}