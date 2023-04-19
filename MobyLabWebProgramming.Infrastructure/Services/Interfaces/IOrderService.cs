using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

/// <summary>
/// This service will be uses to mange Order information.
/// As most routes and business logic will need to know what Order is currently using the backend this service will be the most used.
/// </summary>
public interface IOrderService
{
    /// <summary>
    /// GetOrder will provide the information about a Order given its Order Id.
    /// </summary>
    public Task<ServiceResponse<OrderDTO>> GetOrder(Guid id, CancellationToken cancellationToken = default);
    /// <summary>
    /// GetOrders returns page with Order information from the database.
    /// </summary>
    public Task<ServiceResponse<PagedResponse<OrderDTO>>> GetOrders(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    /// <summary>
    /// AddOrder adds an Order and verifies if requesting Order has permissions to add one.
    /// If the requesting Order is null then no verification is performed as it indicates that the application.
    /// </summary>
    public Task<ServiceResponse> AddOrder(OrderAddDTO Order, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    /// <summary>
    /// UpdateOrder updates an Order and verifies if requesting Order has permissions to update it, if the Order is his own then that should be allowed.
    /// If the requesting Order is null then no verification is performed as it indicates that the application.
    /// </summary>
    public Task<ServiceResponse> UpdateOrder(OrderDTO Order, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    /// <summary>
    /// DeleteOrder deletes an Order and verifies if requesting Order has permissions to delete it, if the Order is his own then that should be allowed.
    /// If the requesting Order is null then no verification is performed as it indicates that the application.
    /// </summary>
    public Task<ServiceResponse> DeleteOrder(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}
