namespace ProductMicroservice.Services
{
    public class FileStorageService
    {
        private readonly string _storagePath;
        private readonly ILogger<FileStorageService> _logger;

        public FileStorageService(IWebHostEnvironment env, ILogger<FileStorageService> logger)
        {
            _logger = logger;
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

                try
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    savedPaths.Add(fileName); // Store only file name, not full path
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error saving file: {file.FileName}");
                }
            }

            return savedPaths;
        }

        public List<byte[]> GetFilesAsByteArray(List<string>? fileNames)
        {
            if (fileNames == null || fileNames.Count == 0)
                return new List<byte[]>();

            return fileNames
                .Select(fileName => Path.Combine(_storagePath, fileName))
                .Where(File.Exists)
                .Select(File.ReadAllBytes)
                .ToList();
        }

        public async Task<bool> DeleteFileAsync(string fileName)
        {
            try
            {
                var fullPath = Path.Combine(_storagePath, fileName);

                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    _logger.LogInformation($"File deleted: {fileName}");
                    return true;
                }
                else
                {
                    _logger.LogWarning($"File not found: {fileName}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting file: {fileName}");
            }
            return false;
        }
    }
}
