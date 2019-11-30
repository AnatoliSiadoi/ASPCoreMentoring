
namespace APIClient.Client.Resources
{
    internal abstract class ResourceGatewayBase<T> 
    {
        protected ResourceGatewayBase(T apiClient)
        {
            ApiClient = apiClient;
        }
        
        protected T ApiClient { get; }
    }
}
