using activity1.Models;
using Microsoft.AspNetCore.Mvc;

namespace activity1.Controllers
{
   
        public class ConfirmationController : Controller
        {
            private ApplicationDbContext _context;
            public ConfirmationController()
            {
                _context = new ApplicationDbContext();
            }
            public IActionResult Index()
            {
                var data = _context.Confirmations.ToList();

                return View(data);

            }
            public IActionResult Create()
            {
                return View();
            }
            [HttpPost]

            public IActionResult Create(Confirmation confirmation)
            {
                _context.Confirmations.Add(confirmation);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        }  
}
