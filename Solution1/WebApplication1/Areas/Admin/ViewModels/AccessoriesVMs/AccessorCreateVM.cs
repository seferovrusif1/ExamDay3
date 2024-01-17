namespace WebApplication1.Areas.Admin.ViewModels.AccessoriesVMs
{
    public class AccessorCreateVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile ImagePath { get; set; }
    }
}
