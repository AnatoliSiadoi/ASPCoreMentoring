using System.IO;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface ICacheFileRepository
    {
        Task<byte[]> GetByNameAsync(string fileName, string dirPath);

        FileInfo[] GetAllFiles(string dirPath);

        Task SaveFileAsync(string fileName, string dirPath, byte[] bytes);

        Task DeleteFileByNameAsync(string fileName, string dirPath);
    }
}
