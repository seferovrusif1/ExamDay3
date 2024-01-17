using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Areas.Admin.ViewModels.AccessoriesVMs;
using WebApplication1.Context;
using WebApplication1.Helpers;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize (Roles = "Admin")]
    public class HomeController : Controller
    {
        ExamDay3DBContext _db { get; }

        public HomeController(ExamDay3DBContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var data = _db.Accessories.Select(x => new ListItemAccessor
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                ImagePath = x.ImagePath,
                IsDeleted = x.IsDeleted,
            }).ToList();
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AccessorCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            Models.Accessories accessor = new Models.Accessories
            {
                Title=vm.Title,
                Description=vm.Description,
                ImagePath=await vm.ImagePath.SaveImg(PathConstants.ImgFolder)
            }; 
            await _db.Accessories.AddAsync(accessor);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            var data = await _db.Accessories.FindAsync(id);

            return View(new AccessorUpdateVM
            {
                Title=data.Title,
                Description=data.Description,
                ImagePathStr = data.ImagePath
            });
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, AccessorCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var data = await _db.Accessories.FindAsync(id);
            data.Title=vm.Title;
            data.Description=vm.Description;
            data.ImagePath = await vm.ImagePath.SaveImg(PathConstants.ImgFolder);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _db.Accessories.FindAsync(id);
            _db.Accessories.Remove(data);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
