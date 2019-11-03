using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Repositories.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace BusinessLayer.Services
{
    public class Cache : ICache
    {
        private const string PathDirectoryDefault = "Cache/";
        private const int MaxCountDefault = 5;
        private const int ExpirationTimeDefault = 30000;

        private readonly ICacheFileRepository _cacheRepository;
        private Timer _timer = new Timer();

        public string PathDirectory { get; set; }

        public int MaxCount { get; set; }

        public int ExpirationTime { get; set; }

        public Cache(ICacheFileRepository cacheRepository)
        {
            _cacheRepository = cacheRepository;
            PathDirectory = PathDirectoryDefault;
            MaxCount = MaxCountDefault;
            ExpirationTime = ExpirationTimeDefault;
        }

        public async Task<byte[]> GetPictureByNameAsync(string fileName)
        {
            if (!String.IsNullOrEmpty(fileName))
            {
                return await _cacheRepository.GetByNameAsync(fileName, PathDirectory);
            }

            return null;
        }

        public async Task SetPictureByNameAsync(string fileName, byte[] bytes)
        {
            if(!String.IsNullOrEmpty(fileName))
            {
                var filesInfo = _cacheRepository.GetAllFiles(PathDirectory);
                if (filesInfo == null
                    || (filesInfo.Length < MaxCount
                    && !filesInfo.Where(w => w.Name == fileName).Any()))
                {
                    await _cacheRepository.SaveFileAsync(fileName, PathDirectory, bytes);
                    ClearCacheWithDelay(fileName, _cacheRepository.DeleteFileByNameAsync);
                }
            }
        }

        private void ClearCacheWithDelay(string fileName, Func<string, string, Task> action)
        {
            lock (_timer)
            {
                if (!_timer.Enabled)
                {
                    _timer = new Timer(ExpirationTime) { AutoReset = false };
                    _timer.Elapsed += async (sender, e) => { await action(fileName, PathDirectory); };
                    _timer.Start();
                }
                else
                {
                    _timer.Stop();
                    _timer.Start();
                }
            }
        }
    }
}
