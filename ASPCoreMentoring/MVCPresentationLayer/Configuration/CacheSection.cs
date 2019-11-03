
namespace MVCPresentationLayer.Configuration
{
    public class CacheSection
    {
        public string PathDirectory { get; set; }

        public int MaxCount { get; set; }

        public int ExpirationTime { get; set; }
    }
}
