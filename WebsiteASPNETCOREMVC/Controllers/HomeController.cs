using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebsiteASPNETCOREMVC.Models;

namespace WebsiteASPNETCOREMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserDBContext userdb;
        private readonly ContactViewDBContext contactdb;
        private static User user;

        public HomeController(UserDBContext userdb, ContactViewDBContext contactdb)
        {
            this.userdb = userdb;
            this.contactdb = contactdb;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User u)
        {
            if(ModelState.IsValid)
            {
                userdb.Users.Add(u);
                await userdb.SaveChangesAsync();
                return RedirectToAction("Signin", "Home");
            }
            return View();
        }

        public IActionResult Signin()
        {
            TempData["alert"] = false;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signin(string Email,string Password)
        {
            TempData["alert"] = false;
            if (ModelState.IsValid)
            {
                user = await userdb.Users.FirstOrDefaultAsync(x => x.Email == Email && x.Password == Password);
                if (user != null)
                {
                    HttpContext.Session.SetString("username", user.Name);
                    return RedirectToAction("Index","Page");
                }
                else
                {
                    TempData["alert"] = true;
                    return View();
                }
            }
            return View();
        }

        public IActionResult Profile()
        {

            return View(user);
        }

        [HttpGet]
        public IActionResult Edit()
        {

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, User pr)
        {
            if(id != pr.Id)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                user = pr;
                HttpContext.Session.SetString("username", user.Name);
                userdb.Update(pr);
                await userdb.SaveChangesAsync();
                return RedirectToAction("Profile", "Home");
            }
            else
            {
                return NotFound();
            }
            
        }
        public IActionResult Contact()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(ContactView con)
        {
            if(ModelState.IsValid)
            {
                contactdb.Contacts.AddAsync(con);
                await contactdb.SaveChangesAsync();
                TempData["message"] = true;
                return RedirectToAction("Index" ,"Home");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
