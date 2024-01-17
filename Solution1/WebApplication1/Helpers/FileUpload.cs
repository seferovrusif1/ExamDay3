namespace WebApplication1.Helpers
{
    public static class FileUploadExtension
    {
        public static async Task<string> SaveImg(this IFormFile file, string path)
        {
            string extension = Path.GetExtension(file.FileName);
            string filename = Path.GetFileNameWithoutExtension(file.FileName);
            if (filename.Length > 32)
            {
                filename = filename.Substring(filename.Length - 32);
            }
            filename=Path.Combine(path, filename+Path.GetRandomFileName+extension);
            using(FileStream fs =File.Create(Path.Combine(PathConstants.RoothPath, filename)))
            {
                await file.CopyToAsync(fs);
            }
            return filename;
        }
    }
}
