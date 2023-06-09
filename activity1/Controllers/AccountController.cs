﻿using activity1.Models;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace activity1.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationDbContext _context;
        public AccountController()
        {
            _context = new ApplicationDbContext();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            
            
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login");
           
        }
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index","Product");
            }
            return View();
        }
        [AllowAnonymous]
        [HttpPost]

        public async Task<IActionResult> Login(string username, string password)
        {
            if (true)
            {
                var data = _context.Users.Where(x => x.UserName.Equals(username) && x.Password.Equals(password)).FirstOrDefault();

                if (data != null)
                {
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, username),
                        new Claim(ClaimTypes.Role, data.Role),
                        new Claim("OtherProperties","Example Role"),
                    };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);
                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), properties);

                    HttpContext.Session.SetString("username", username);
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
        [Authorize(Roles = "user")]
        public IActionResult UserWelcomePage()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        public IActionResult AdminPage()
        {
            return View();
        }
    }
}