using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

/// <summary>
/// This service will be uses to mange Category information.
/// As most routes and business logic will need to know what Category is currently using the backend this service will be the most used.
/// </summary>
public interface ICategoryService
{
    /// <summary>
    /// GetCategory will provide the information about a Category given its Category Id.
    /// </summary>
    public Task<ServiceResponse<CategoryDTO>> GetCategory(Guid id, CancellationToken cancellationToken = default);
    /// <summary>
    /// GetCategorys returns page with Category information from the database.
    /// </summary>
    public Task<ServiceResponse<PagedResponse<CategoryDTO>>> GetCategories(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    /// <summary>
    /// AddCategory adds an Category and verifies if requesting Category has permissions to add one.
    /// If the requesting Category is null then no verification is performed as it indicates that the application.
    /// </summary>
    public Task<ServiceResponse> AddCategory(CategoryAddDTO Category, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    /// <summary>
    /// UpdateCategory updates an Category and verifies if requesting Category has permissions to update it, if the Category is his own then that should be allowed.
    /// If the requesting Category is null then no verification is performed as it indicates that the application.
    /// </summary>
    public Task<ServiceResponse> UpdateCategory(CategoryDTO Category, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    /// <summary>
    /// DeleteCategory deletes an Category and verifies if requesting Category has permissions to delete it, if the Category is his own then that should be allowed.
    /// If the requesting Category is null then no verification is performed as it indicates that the application.
    /// </summary>
    public Task<ServiceResponse> DeleteCategory(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}
