using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

/// <summary>
/// This service will be uses to mange Product information.
/// As most routes and business logic will need to know what Product is currently using the backend this service will be the most used.
/// </summary>
public interface IProductService
{
    /// <summary>
    /// GetProduct will provide the information about a Product given its Product Id.
    /// </summary>
    public Task<ServiceResponse<ProductDTO>> GetProduct(Guid id, CancellationToken cancellationToken = default);
    /// <summary>
    /// GetProducts returns page with Product information from the database.
    /// </summary>
    public Task<ServiceResponse<PagedResponse<ProductDTO>>> GetProducts(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    /// <summary>
    /// AddProduct adds an Product and verifies if requesting Product has permissions to add one.
    /// If the requesting Product is null then no verification is performed as it indicates that the application.
    /// </summary>
    public Task<ServiceResponse> AddProduct(ProductAddDTO Product, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    /// <summary>
    /// UpdateProduct updates an Product and verifies if requesting Product has permissions to update it, if the Product is his own then that should be allowed.
    /// If the requesting Product is null then no verification is performed as it indicates that the application.
    /// </summary>
    public Task<ServiceResponse> UpdateProduct(ProductDTO Product, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    /// <summary>
    /// DeleteProduct deletes an Product and verifies if requesting Product has permissions to delete it, if the Product is his own then that should be allowed.
    /// If the requesting Product is null then no verification is performed as it indicates that the application.
    /// </summary>
    public Task<ServiceResponse> DeleteProduct(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}
