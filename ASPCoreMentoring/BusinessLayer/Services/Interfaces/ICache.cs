using System.Threading.Tasks;

namespace BusinessLayer.Services.Interfaces
{
    public interface ICache
    {
        string PathDirectory {get;set;}

        int MaxCount { get; set; }

        int ExpirationTime { get; set; }

        Task<byte[]> GetPictureByNameAsync(string name);

        Task SetPictureByNameAsync(string name, byte[] bytes);
    }
}
