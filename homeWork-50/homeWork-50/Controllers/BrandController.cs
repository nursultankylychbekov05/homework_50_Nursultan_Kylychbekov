using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using homeWork_50.Models;

namespace homeWork_50.Controllers;

public class BrandController : Controller
{
    private readonly StoreContext _context;

    public BrandController(StoreContext context)
    {
        _context = context;
    }
    
    public async Task<IActionResult> Create()
    {
        ViewBag.Brands = await _context.Brands.ToListAsync();
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Brand brand)
    {
        bool exists = await _context.Brands
            .AnyAsync(b => b.Name.ToLower() == brand.Name.Trim().ToLower());

        if (exists)
        {
            ModelState.AddModelError("Name", $"Бренд \"{brand.Name}\" уже существует!");
        }

        if (ModelState.IsValid)
        {
            brand.Name = brand.Name.Trim();
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Create));
        }

        ViewBag.Brands = await _context.Brands.ToListAsync();
        return View(brand);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var brand = await _context.Brands.FindAsync(id);
        if (brand != null)
        {
            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Create));
    }
}