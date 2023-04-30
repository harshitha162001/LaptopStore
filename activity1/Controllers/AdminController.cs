using activity1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using Microsoft.AspNetCore.Authorization;


namespace project.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;
        public AdminController()
        {
            _context = new ApplicationDbContext();
        }
        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            var data = _context.Users.ToList();
            return View(data);
        }

        public ActionResult Delete(int id)
        {
            var data = _context.Users.FirstOrDefault(x => x.UserId == id);
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, User user)
        {
            try
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Details(int id)
        {
            var data = _context.Users.FirstOrDefault(x => x.UserId == id);
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserId == id);
            return View(user);
        }
        [HttpPost]
        public IActionResult Edit(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "admin")]
        public IActionResult ViewProduct()
        {
            var data1 = _context.Products.Include(x => x.Brand).ToList();
            var data2 = _context.Products.Include(x => x.Category).ToList();
            return View(data1);
        }
        public ActionResult PDelete(int id)
        {
            var data1 = _context.Products.Include(x => x.Brand).ToList();
            var data2 = _context.Products.Include(x => x.Category).ToList();
            var data = _context.Products.FirstOrDefault(x => x.productId == id);
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PDelete(int id, Product product)
        {
            try
            {
              
                _context.Products.Remove(product);
                _context.SaveChanges();
                return RedirectToAction("ViewProduct");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult PDetails(int id)
        {
            var data = _context.Products.FirstOrDefault(x => x.productId == id);
            var data1 = _context.Products.Include(x => x.Brand).ToList();
            var data2 = _context.Products.Include(x => x.Category).ToList();
            return View(data);
        }
        public IActionResult PCreate()
        {
            var product = _context.Products.Select(a => new SelectListItem { Value = a.BrandId.ToString(), Text = a.Brand.Name }).ToList();
            var product1 = _context.Products.Select(a => new SelectListItem { Value = a.CategoryId.ToString(), Text = a.Category.Name }).ToList();
            ViewBag.product = product;
            ViewBag.product1 = product1;

            return View();
        }
        [HttpPost]

        public IActionResult PCreate(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("ViewProduct");
        }
        public IActionResult PEdit(int id)
        {
            var product = _context.Products.Select(a => new SelectListItem { Value = a.BrandId.ToString(), Text = a.Brand.Name }).ToList();
            var product1 = _context.Products.Select(a => new SelectListItem { Value = a.CategoryId.ToString(), Text = a.Category.Name }).ToList();
            ViewBag.product = product;
            ViewBag.product1 = product1;
            var user = _context.Products.FirstOrDefault(x => x.productId == id);
            return View(user);
        }
        [HttpPost]
        public IActionResult PEdit(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
            return RedirectToAction("ViewProduct");
        }

    }
}