using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using homeWork_50.Models;

namespace homeWork_50.Controllers;

public class ProductsController : Controller
{
    private readonly StoreContext _context;

    public ProductsController(StoreContext context)
    {
        _context = context;
    }
    
    public async Task<IActionResult> Index(int? categoryId, int? brandId)
    {
        var query = _context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .AsQueryable();
        
        if (categoryId.HasValue && categoryId.Value > 0)
        {
            query = query.Where(p => p.CategoryId == categoryId.Value);
        }
        
        if (brandId.HasValue && brandId.Value > 0)
        {
            query = query.Where(p => p.BrandId == brandId.Value);
        }

        var products = await query
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
        ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name", categoryId);
        ViewBag.Brands = new SelectList(await _context.Brands.ToListAsync(), "Id", "Name", brandId);

        return View(products);
    }
    
    public async Task<IActionResult> Details(int id)
    {
        var product = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null) return NotFound();

        return View(product);
    }
    
    public async Task<IActionResult> Create()
    {
        ViewBag.CategoryId = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
        ViewBag.BrandId = new SelectList(await _context.Brands.ToListAsync(), "Id", "Name");
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product product)
    {
        bool categoryExists = await _context.Categories.AnyAsync(c => c.Id == product.CategoryId);
        bool brandExists = await _context.Brands.AnyAsync(b => b.Id == product.BrandId);

        if (!categoryExists) ModelState.AddModelError("CategoryId", "Категория не найдена.");
        if (!brandExists) ModelState.AddModelError("BrandId", "Бренд не найден.");

        if (ModelState.IsValid)
        {
            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.CategoryId = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name", product.CategoryId);
        ViewBag.BrandId = new SelectList(await _context.Brands.ToListAsync(), "Id", "Name", product.BrandId);
        return View(product);
    }
}