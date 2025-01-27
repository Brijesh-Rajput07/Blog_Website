using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebsiteASPNETCOREMVC.Models;

namespace WebsiteASPNETCOREMVC.Controllers
{
    
    public class PageController : Controller
    {
        private readonly BlogDBContext blog;
        private readonly IWebHostEnvironment webHostEnvironment;
        
        public PageController(BlogDBContext blog,IWebHostEnvironment webHostEnvironment)
        {
            this.blog = blog;
            this .webHostEnvironment = webHostEnvironment;

        }
        public IActionResult Index()
        {
            var b = blog.Blogs.ToList();
            return View(b);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id!=null)
            {
                var b = await blog.Blogs.FirstOrDefaultAsync(x=>x.Id==id);
                if (b != null)
                    return View(b);
                else
                    return NotFound();
            }
            return NotFound();
        }

        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var b = await blog.Blogs.FirstOrDefaultAsync(x => x.Id == id);
            if (b != null)
            {
                blog.Blogs.Remove(b);
            }
            await blog.SaveChangesAsync();
            return RedirectToAction("Index", "Page");
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog b, IFormFile FeaturedImage)
        {
            if (ModelState.IsValid)
            {
                if (FeaturedImage != null && FeaturedImage.Length > 0)
                {
                    string folder = "images/";
                    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetFileName(FeaturedImage.FileName);
                    b.FilePath = folder + uniqueFileName;

                    string serverFolder = Path.Combine(webHostEnvironment.WebRootPath, folder, uniqueFileName);

                    // Save file to wwwroot/images
                    using (var stream = new FileStream(serverFolder, FileMode.Create))
                    {
                        await FeaturedImage.CopyToAsync(stream);
                    }
                }

                blog.Blogs.Add(b);
                await blog.SaveChangesAsync();
                TempData["upblog"] =false;
                return RedirectToAction("Newblog", "Page", new { id = b.Id });
            }

            return View(b);
        }

        public async Task<IActionResult> Newblog(int id)
        {
            var bl = await blog.Blogs.FirstOrDefaultAsync(x => x.Id == id);
            return View(bl);
        }
        public IActionResult Details(int? id)
        {
            // Set TempData
            TempData["upblog"] = false;

            var b = blog.Blogs.FirstOrDefault(b => b.Id == id);
            if (b == null)
            {
                return NotFound();
            }

            return RedirectToAction("Newblog","Page", new {id=b.Id});
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                var bl = await blog.Blogs.FirstOrDefaultAsync(x => x.Id == id);
                return View(bl);
            }
            return RedirectToAction("Create","Page");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,Blog b, IFormFile FeaturedImage)
        {
            if (ModelState.IsValid)
            {
                if (FeaturedImage != null)
                {
                    string folder = "images/";
                    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetFileName(FeaturedImage.FileName);
                    b.FilePath = folder + uniqueFileName;

                    string serverFolder = Path.Combine(webHostEnvironment.WebRootPath, folder, uniqueFileName);

                    // Save file to wwwroot/images
                    using (var stream = new FileStream(serverFolder, FileMode.Create))
                    {
                        await FeaturedImage.CopyToAsync(stream);
                    }
                }

                if (id == b.Id)
                {
                    blog.Blogs.Update(b);
                    await blog.SaveChangesAsync();
                    TempData["upblog"] = true;
                    return RedirectToAction("Newblog", "Page", new { id = b.Id });
                }
                else
                {
                    return NotFound();
                }

            }

            return View(b);
        }
    }
}

