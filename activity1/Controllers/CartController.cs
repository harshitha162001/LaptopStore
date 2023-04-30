using activity1.Helpers;
using activity1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace activity.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext _context;
        public CartController()
        {
            _context = new ApplicationDbContext();
        }
        [Authorize(Roles = "user")]
        public IActionResult Index()
        {

            var cart = SessionHelper.getObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
            return View();
        }
        public IActionResult Buy(int id)
        {
            /* ViewBag.Id = id;*/
            if (SessionHelper.getObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)//checks if cart is null that means cart is not added to the session
            {
                List<Item> cart = new List<Item>();

                cart.Add(new Item() { Product = _context.Products.SingleOrDefault(p => p.productId == id), Quantity = 1 });//checking id
                SessionHelper.setObjectAsJson(HttpContext.Session, "cart", cart);//new key is set as json
            }
            else//if it is already exist
            {
                List<Item> cart = SessionHelper.getObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = isExists(id);
                if (index != -1)
                {
                    cart[index].Quantity++;//increase the quantity
                }
                else
                {
                    cart.Add(new Item() { Product = _context.Products.SingleOrDefault(p => p.productId == id), Quantity = 1 });
                }
                SessionHelper.setObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }
        public int isExists(int id)
        {
            List<Item> cart = SessionHelper.getObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.productId == id)
                {
                    return i;
                }

            }
            return -1;

        }
        [Authorize(Roles = "user")]
        public IActionResult Remove(int id)
        {
            List<Item> cart = SessionHelper.getObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExists(id);
            cart.RemoveAt(index);
            SessionHelper.setObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");

        }
        [Authorize(Roles = "user")]
        public IActionResult Checkout()
        {
            List<Item> cart = SessionHelper.getObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.count = cart.Count();
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
            return View();
        }
        [Authorize(Roles = "user")]
        /*  public IActionResult RemoveAll()
          {
              List<Item> cart = SessionHelper.getObjectFromJson<List<Item>>(HttpContext.Session, "cart");

              cart.RemoveRange(0, cart.Count);
              SessionHelper.setObjectAsJson(HttpContext.Session, "cart", cart);
              return RedirectToAction("Index");
          }*/
        public IActionResult EmptyCart()
        {
            List<Item> cart = SessionHelper.getObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            if (cart.IsNullOrEmpty())
            {
                return View();
            }
            else
            {
                cart.Clear();
            }
            return View();
        }
        [Authorize(Roles = "user")]
        public IActionResult Confirmation()
        {
            return View();
        }
    }
}
