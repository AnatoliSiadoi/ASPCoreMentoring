# IO.Swagger.Api.ProductAPIApi

All URIs are relative to *https://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**CreateProduct**](ProductAPIApi.md#createproduct) | **POST** /api/ProductAPI | 
[**DeleteProduct**](ProductAPIApi.md#deleteproduct) | **DELETE** /api/ProductAPI/{id} | 
[**GetProductsList**](ProductAPIApi.md#getproductslist) | **GET** /api/ProductAPI | 
[**UpdateProduct**](ProductAPIApi.md#updateproduct) | **PUT** /api/ProductAPI/{id} | 


<a name="createproduct"></a>
# **CreateProduct**
> void CreateProduct (ProductModel model = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class CreateProductExample
    {
        public void main()
        {
            var apiInstance = new ProductAPIApi();
            var model = new ProductModel(); // ProductModel |  (optional) 

            try
            {
                apiInstance.CreateProduct(model);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProductAPIApi.CreateProduct: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **model** | [**ProductModel**](ProductModel.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json-patch+json, application/json, text/json, application/_*+json
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="deleteproduct"></a>
# **DeleteProduct**
> void DeleteProduct (int? id)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class DeleteProductExample
    {
        public void main()
        {
            var apiInstance = new ProductAPIApi();
            var id = 56;  // int? | 

            try
            {
                apiInstance.DeleteProduct(id);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProductAPIApi.DeleteProduct: " + e.Message );
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

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getproductslist"></a>
# **GetProductsList**
> List<ProductDTO> GetProductsList (bool? includeCategory = null, bool? includeSupplier = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class GetProductsListExample
    {
        public void main()
        {
            var apiInstance = new ProductAPIApi();
            var includeCategory = true;  // bool? |  (optional)  (default to false)
            var includeSupplier = true;  // bool? |  (optional)  (default to false)

            try
            {
                List&lt;ProductDTO&gt; result = apiInstance.GetProductsList(includeCategory, includeSupplier);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProductAPIApi.GetProductsList: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **includeCategory** | **bool?**|  | [optional] [default to false]
 **includeSupplier** | **bool?**|  | [optional] [default to false]

### Return type

[**List<ProductDTO>**](ProductDTO.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="updateproduct"></a>
# **UpdateProduct**
> void UpdateProduct (int? id, ProductModel model = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class UpdateProductExample
    {
        public void main()
        {
            var apiInstance = new ProductAPIApi();
            var id = 56;  // int? | 
            var model = new ProductModel(); // ProductModel |  (optional) 

            try
            {
                apiInstance.UpdateProduct(id, model);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProductAPIApi.UpdateProduct: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **int?**|  | 
 **model** | [**ProductModel**](ProductModel.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json-patch+json, application/json, text/json, application/_*+json
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

