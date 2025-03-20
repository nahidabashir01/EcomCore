namespace ProductMicroservice.Services
{ 
    public class FileStorageService
    {
        private readonly string _storagePath;

        public FileStorageService(IWebHostEnvironment env)
        {
            _storagePath = Path.Combine(env.WebRootPath, "uploads");
            if (!Directory.Exists(_storagePath))
            {
                Directory.CreateDirectory(_storagePath);
            }
        }

        public async Task<List<string>> SaveFilesAsync(List<IFormFile> files)
        {
            var savedPaths = new List<string>();

            foreach (var file in files)
            {
                var fileName = $"{Guid.NewGuid()}_{file.FileName}";
                var filePath = Path.Combine(_storagePath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                savedPaths.Add(filePath);
            }

            return savedPaths;
        }

        public List<byte[]> GetFilesAsByteArray(List<string>? filePaths)
        {
            if (filePaths == null || filePaths.Count == 0)
                return new List<byte[]>();

            return filePaths.Select(path => File.ReadAllBytes(path)).ToList();
        }
    }

}
