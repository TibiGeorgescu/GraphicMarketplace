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

public class ProductService : IProductService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    /// <summary>
    /// Inject the required services through the constructor.
    /// </summary>
    public ProductService(IRepository<WebAppDatabaseContext> repository, ILoginService loginService, IMailService mailService)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<ProductDTO>> GetProduct(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new ProductProjectionSpec(id), cancellationToken); // Get a Product using a specification on the repository.

        return result != null ?
            ServiceResponse<ProductDTO>.ForSuccess(result) :
            ServiceResponse<ProductDTO>.FromError(CommonErrors.ProductNotFound); // Pack the result or error into a ServiceResponse.
    }

    public async Task<ServiceResponse<PagedResponse<ProductDTO>>> GetProducts(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new ProductProjectionSpec(pagination.Search), cancellationToken); // Use the specification and pagination API to get only some entities from the database.

        return ServiceResponse<PagedResponse<ProductDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse> AddProduct(ProductAddDTO Product, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin) // Verify who can add the Product, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can add Products!", ErrorCodes.CannotAdd));
        }

        var result = await _repository.GetAsync(new ProductSpec(Product.Name), cancellationToken);

        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The Product already exists!", ErrorCodes.ProductAlreadyExists));
        }

        await _repository.AddAsync(new Product
        {
            Name = Product.Name,
            Description = Product.Description,
            Price = Product.Price,
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateProduct(ProductDTO Product, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        //if (requestingProduct != null && requestingProduct.Role != ProductRoleEnum.Admin && requestingProduct.Id != Product.Id) // Verify who can add the Product, you can change this however you se fit.
        //{
        //    return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or the own Product can update the Product!", ErrorCodes.CannotUpdate));
        //}

        //var entity = await _repository.GetAsync(new ProductSpec(Product.Id), cancellationToken);

        //if (entity != null) // Verify if the Product is not found, you cannot update an non-existing entity.
        //{
        //    entity.Name = Product.Name ?? entity.Name;
        //    entity.Password = Product.Password ?? entity.Password;

        //    await _repository.UpdateAsync(entity, cancellationToken); // Update the entity and persist the changes.
        //}

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteProduct(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin) // Verify who can add the Product, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or the own Product can delete the Product!", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync<Product>(id, cancellationToken);

        return ServiceResponse.ForSuccess();
    }
}
