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

public class CategoryService : ICategoryService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    /// <summary>
    /// Inject the required services through the constructor.
    /// </summary>
    public CategoryService(IRepository<WebAppDatabaseContext> repository, ILoginService loginService, IMailService mailService)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<CategoryDTO>> GetCategory(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new CategoryProjectionSpec(id), cancellationToken); // Get a Category using a specification on the repository.

        return result != null ?
            ServiceResponse<CategoryDTO>.ForSuccess(result) :
            ServiceResponse<CategoryDTO>.FromError(CommonErrors.CategoryNotFound); // Pack the result or error into a ServiceResponse.
    }

    public async Task<ServiceResponse<PagedResponse<CategoryDTO>>> GetCategories(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new CategoryProjectionSpec(pagination.Search), cancellationToken); // Use the specification and pagination API to get only some entities from the database.

        return ServiceResponse<PagedResponse<CategoryDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse> AddCategory(CategoryAddDTO Category, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin) // Verify who can add the Category, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can add Categorys!", ErrorCodes.CannotAdd));
        }

        var result = await _repository.GetAsync(new CategorySpec(Category.Name), cancellationToken);

        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The Category already exists!", ErrorCodes.CategoryAlreadyExists));
        }

        await _repository.AddAsync(new Category
        {
            Name = Category.Name,
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateCategory(CategoryDTO Category, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        //if (requestingCategory != null && requestingCategory.Role != CategoryRoleEnum.Admin && requestingCategory.Id != Category.Id) // Verify who can add the Category, you can change this however you se fit.
        //{
        //    return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or the own Category can update the Category!", ErrorCodes.CannotUpdate));
        //}

        //var entity = await _repository.GetAsync(new CategorySpec(Category.Id), cancellationToken);

        //if (entity != null) // Verify if the Category is not found, you cannot update an non-existing entity.
        //{
        //    entity.Name = Category.Name ?? entity.Name;
        //    entity.Password = Category.Password ?? entity.Password;

        //    await _repository.UpdateAsync(entity, cancellationToken); // Update the entity and persist the changes.
        //}

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteCategory(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin) // Verify who can add the Category, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or the own Category can delete the Category!", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync<Category>(id, cancellationToken);

        return ServiceResponse.ForSuccess();
    }
}
