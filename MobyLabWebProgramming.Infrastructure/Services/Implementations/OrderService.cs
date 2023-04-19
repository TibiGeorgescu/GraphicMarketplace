using System.Net;
using MobyLabWebProgramming.Core.Constants;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    /// <summary>
    /// Inject the required services through the constructor.
    /// </summary>
    public OrderService(IRepository<WebAppDatabaseContext> repository, ILoginService loginService, IMailService mailService)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<OrderDTO>> GetOrder(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new OrderProjectionSpec(id), cancellationToken); // Get a Order using a specification on the repository.

        return result != null ?
            ServiceResponse<OrderDTO>.ForSuccess(result) :
            ServiceResponse<OrderDTO>.FromError(CommonErrors.OrderNotFound); // Pack the result or error into a ServiceResponse.
    }

    public async Task<ServiceResponse<PagedResponse<OrderDTO>>> GetOrders(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new OrderProjectionSpec(pagination.Search), cancellationToken); // Use the specification and pagination API to get only some entities from the database.

        return ServiceResponse<PagedResponse<OrderDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse> AddOrder(OrderAddDTO Order, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin) // Verify who can add the Order, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can add Orders!", ErrorCodes.CannotAdd));
        }

        var result = await _repository.GetAsync(new UserSpec(Order.UserId), cancellationToken);

        if (result == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The User doesn't exist!", ErrorCodes.UserDoesntExists));
        }

        var result2 = await _repository.GetAsync(new ProductSpec(Order.ProductId), cancellationToken);

        if (result2 == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The Product doesn't exist!", ErrorCodes.ProductDoesntExists));
        }

        await _repository.AddAsync(new Order
        {
            Quantity = Order.Quantity,
            ProductId = Order.ProductId,
            UserId = Order.UserId,
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateOrder(OrderDTO Order, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Id != Order.Id) // Verify who can add the Order, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or the own Order can update the Order!", ErrorCodes.CannotUpdate));
        }

        var entity = await _repository.GetAsync(new OrderSpec(Order.Id), cancellationToken);

        if (entity != null) // Verify if the Order is not found, you cannot update an non-existing entity.
        {
            entity.Quantity = Order.Quantity ?? entity.Quantity;

            await _repository.UpdateAsync(entity, cancellationToken); // Update the entity and persist the changes.
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteOrder(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin) // Verify who can add the Order, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or the own Order can delete the Order!", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync<Order>(id, cancellationToken);

        return ServiceResponse.ForSuccess();
    }
}
