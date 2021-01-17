namespace backend.Repository
{
    using System;
    using System.IO;

    using Microsoft.Extensions.Logging;

    public class ArchiveRepository : IArchiveRepository
    {
        private readonly string _rootPath;

        private readonly ILogger _logger;

        public ArchiveRepository(string rootPath, ILogger<ArchiveRepository> logger)
        {
            _rootPath = rootPath ?? throw new ArgumentNullException(nameof(rootPath));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public string GenerateUniqueName()
        {
            return Guid.NewGuid().ToString("D");
        }

        public void Save(string filename, MemoryStream stream)
        {
            if (!Directory.Exists(_rootPath))
            {
                _logger.LogError("Directory \"{0}\" not exist.", _rootPath);
                return;
            }

            if (stream == null)
            {
                _logger.LogError("Saved empty stream.");
                return;
            }

            var filePath = Path.Combine(_rootPath, filename);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                stream.Position = 0;
                stream.CopyTo(fileStream);
            }
        }
    }
}
