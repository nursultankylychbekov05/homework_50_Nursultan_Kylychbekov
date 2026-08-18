using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using homeWork_50.Models;

namespace homeWork_50.Controllers;

public class OrderController : Controller
{
    private readonly StoreContext _context;

    public OrderController(StoreContext context)
    {
        _context = context;
    }
    
    public async Task<IActionResult> Index()
    {
        var orders = await _context.Orders
            .Include(o => o.Product)
            .OrderByDescending(o => o.OrderDate)
            .ToListAsync();

        return View(orders);
    }
    
    public async Task<IActionResult> Create(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();

        ViewBag.Product = product;
        var order = new Order { ProductId = id };
        return View(order);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Order order)
    {
        var product = await _context.Products.FindAsync(order.ProductId);
        if (product == null) return NotFound();

        if (ModelState.IsValid)
        {
            order.OrderDate = DateTime.Now;
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Product = product;
        return View(order);
    }
}