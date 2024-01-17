using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Context;
using WebApplication1.Models;
using WebApplication1.ViewModels.AccessoriesVMs;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
       ExamDay3DBContext _db { get; }

        public HomeController(ExamDay3DBContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var data=  _db.Accessories.Where(x=>!x.IsDeleted).Select(i=>new AccessoriesVM
            {
                Title = i.Title,
                Description = i.Description,
                ImagePath = i.ImagePath
            }).ToList();
            return View(data);
        }

    }
}