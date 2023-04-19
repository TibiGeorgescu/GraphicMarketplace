using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

/// <summary>
/// This service will be uses to mange Feedback information.
/// As most routes and business logic will need to know what Feedback is currently using the backend this service will be the most used.
/// </summary>
public interface IFeedbackService
{
    /// <summary>
    /// GetFeedback will provide the information about a Feedback given its Feedback Id.
    /// </summary>
    public Task<ServiceResponse<FeedbackDTO>> GetFeedback(Guid id, CancellationToken cancellationToken = default);
    /// <summary>
    /// GetFeedbacks returns page with Feedback information from the database.
    /// </summary>
    public Task<ServiceResponse<PagedResponse<FeedbackDTO>>> GetFeedbacks(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    /// <summary>
    /// AddFeedback adds an Feedback and verifies if requesting Feedback has permissions to add one.
    /// If the requesting Feedback is null then no verification is performed as it indicates that the application.
    /// </summary>
    public Task<ServiceResponse> AddFeedback(FeedbackAddDTO Feedback, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    /// <summary>
    /// UpdateFeedback updates an Feedback and verifies if requesting Feedback has permissions to update it, if the Feedback is his own then that should be allowed.
    /// If the requesting Feedback is null then no verification is performed as it indicates that the application.
    /// </summary>
    public Task<ServiceResponse> UpdateFeedback(FeedbackDTO Feedback, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    /// <summary>
    /// DeleteFeedback deletes an Feedback and verifies if requesting Feedback has permissions to delete it, if the Feedback is his own then that should be allowed.
    /// If the requesting Feedback is null then no verification is performed as it indicates that the application.
    /// </summary>
    public Task<ServiceResponse> DeleteFeedback(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}
