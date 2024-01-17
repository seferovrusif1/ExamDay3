namespace WebApplication1.Areas.Admin.ViewModels.AccessoriesVMs
{
    public class AccessorUpdateVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile? ImagePath { get; set; }
        public string? ImagePathStr { get; set; }
    }
}
