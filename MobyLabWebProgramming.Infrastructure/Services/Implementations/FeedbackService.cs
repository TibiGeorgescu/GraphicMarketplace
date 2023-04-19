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

public class FeedbackService : IFeedbackService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    /// <summary>
    /// Inject the required services through the constructor.
    /// </summary>
    public FeedbackService(IRepository<WebAppDatabaseContext> repository, ILoginService loginService, IMailService mailService)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<FeedbackDTO>> GetFeedback(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new FeedbackProjectionSpec(id), cancellationToken); // Get a Feedback using a specification on the repository.

        return result != null ?
            ServiceResponse<FeedbackDTO>.ForSuccess(result) :
            ServiceResponse<FeedbackDTO>.FromError(CommonErrors.FeedbackNotFound); // Pack the result or error into a ServiceResponse.
    }

    public async Task<ServiceResponse<PagedResponse<FeedbackDTO>>> GetFeedbacks(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new FeedbackProjectionSpec(pagination.Search), cancellationToken); // Use the specification and pagination API to get only some entities from the database.

        return ServiceResponse<PagedResponse<FeedbackDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse> AddFeedback(FeedbackAddDTO Feedback, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin) // Verify who can add the Feedback, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can add Feedbacks!", ErrorCodes.CannotAdd));
        }

        var result = await _repository.GetAsync(new UserSpec(Feedback.UserId), cancellationToken);

        if (result == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The User doesn't exist!", ErrorCodes.UserDoesntExists));
        }

        var result2 = await _repository.GetAsync(new ProductSpec(Feedback.ProductId), cancellationToken);

        if (result2 == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The Product doesn't exist!", ErrorCodes.ProductDoesntExists));
        }


        var result3 = await _repository.GetAsync(new FeedbackSpec(Feedback.UserId, Feedback.ProductId), cancellationToken);

        if (result3 != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The Feedback already exists!", ErrorCodes.FeedbackAlreadyExists));
        }

        await _repository.AddAsync(new Feedback
        {
            Content = Feedback.Content,
            Rating = Feedback.Rating,
            ProductId = Feedback.ProductId,
            UserId = Feedback.UserId,
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateFeedback(FeedbackDTO Feedback, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Id != Feedback.Id) // Verify who can add the Feedback, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or the own Feedback can update the Feedback!", ErrorCodes.CannotUpdate));
        }

        var entity = await _repository.GetAsync(new FeedbackSpec(Feedback.Id), cancellationToken);

        if (entity != null) // Verify if the Feedback is not found, you cannot update an non-existing entity.
        {
            entity.Content = Feedback.Content ?? entity.Content;
            entity.Rating = Feedback.Rating ?? entity.Rating;

            await _repository.UpdateAsync(entity, cancellationToken); // Update the entity and persist the changes.
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteFeedback(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin) // Verify who can add the Feedback, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or the own Feedback can delete the Feedback!", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync<Feedback>(id, cancellationToken);

        return ServiceResponse.ForSuccess();
    }
}
