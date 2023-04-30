using activity1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace activity1.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext _context;


        public ProductController()
        {
            _context = new ApplicationDbContext();
        }
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(m => m.productId == id);
            if (product == null)
            {
                return NotFound();
            }
            var data1 = _context.Products.Include(x => x.Brand).ToList();
            var data2 = _context.Products.Include(x => x.Category).ToList();
            return View(product);
        }
    }
}
