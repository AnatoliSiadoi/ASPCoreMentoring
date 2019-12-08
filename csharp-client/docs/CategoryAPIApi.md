# IO.Swagger.Api.CategoryAPIApi

All URIs are relative to *https://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**GetCategoriesList**](CategoryAPIApi.md#getcategorieslist) | **GET** /api/CategoryAPI | 
[**GetCategoryImageById**](CategoryAPIApi.md#getcategoryimagebyid) | **GET** /api/CategoryAPI/image/{id} | 
[**UpdateImage**](CategoryAPIApi.md#updateimage) | **PUT** /api/CategoryAPI/image | 


<a name="getcategorieslist"></a>
# **GetCategoriesList**
> List<CategoryDTO> GetCategoriesList ()



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class GetCategoriesListExample
    {
        public void main()
        {
            var apiInstance = new CategoryAPIApi();

            try
            {
                List&lt;CategoryDTO&gt; result = apiInstance.GetCategoriesList();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CategoryAPIApi.GetCategoriesList: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**List<CategoryDTO>**](CategoryDTO.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getcategoryimagebyid"></a>
# **GetCategoryImageById**
> byte[] GetCategoryImageById (int? id)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class GetCategoryImageByIdExample
    {
        public void main()
        {
            var apiInstance = new CategoryAPIApi();
            var id = 56;  // int? | 

            try
            {
                byte[] result = apiInstance.GetCategoryImageById(id);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CategoryAPIApi.GetCategoryImageById: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **int?**|  | 

### Return type

**byte[]**

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="updateimage"></a>
# **UpdateImage**
> void UpdateImage (CategoryPictureModel model = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class UpdateImageExample
    {
        public void main()
        {
            var apiInstance = new CategoryAPIApi();
            var model = new CategoryPictureModel(); // CategoryPictureModel |  (optional) 

            try
            {
                apiInstance.UpdateImage(model);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CategoryAPIApi.UpdateImage: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **model** | [**CategoryPictureModel**](CategoryPictureModel.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json-patch+json, application/json, text/json, application/_*+json
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

