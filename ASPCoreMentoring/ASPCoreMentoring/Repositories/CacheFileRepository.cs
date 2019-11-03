using DataAccessLayer.Repositories.Interfaces;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class CacheFileRepository : ICacheFileRepository
    {
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        public async Task DeleteFileByNameAsync(string fileName, string dirPath)
        {
            await semaphore.WaitAsync();

            try
            {
                if (File.Exists($"{dirPath}{fileName}"))
                {
                    File.Delete($"{dirPath}{fileName}");
                }
            }
            finally
            {
                semaphore.Release();
            }
        }

        public FileInfo[] GetAllFiles(string dirPath)
        {
            if (Directory.Exists(dirPath))
            {
                var dir = new DirectoryInfo(dirPath);
                var files = dir.GetFiles();

                return files;
            }

            return null;
        }

        public async Task<byte[]> GetByNameAsync(string fileName, string dirPath)
        {
            await semaphore.WaitAsync();

            try
            {
                if (File.Exists($"{dirPath}{fileName}"))
                {
                    var fileData = await File.ReadAllBytesAsync($"{dirPath}{fileName}");

                    return fileData;
                }
            }
            finally
            {
                semaphore.Release();
            }

            return null;
        }

        public async Task SaveFileAsync(string fileName, string dirPath, byte[] bytes)
        {
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            await semaphore.WaitAsync();

            try
            {
                await File.WriteAllBytesAsync($"{dirPath}{fileName}", bytes);
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}
