using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

/// <summary>
/// This service will be uses to mange Profile information.
/// As most routes and business logic will need to know what Profile is currently using the backend this service will be the most used.
/// </summary>
public interface IProfileService
{
    /// <summary>
    /// GetProfile will provide the information about a Profile given its Profile Id.
    /// </summary>
    public Task<ServiceResponse<ProfileDTO>> GetProfile(Guid id, CancellationToken cancellationToken = default);
    /// <summary>
    /// GetProfiles returns page with Profile information from the database.
    /// </summary>
    public Task<ServiceResponse<PagedResponse<ProfileDTO>>> GetProfiles(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    /// <summary>
    /// AddProfile adds an Profile and verifies if requesting Profile has permissions to add one.
    /// If the requesting Profile is null then no verification is performed as it indicates that the application.
    /// </summary>
    public Task<ServiceResponse> AddProfile(ProfileAddDTO Profile, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    /// <summary>
    /// UpdateProfile updates an Profile and verifies if requesting Profile has permissions to update it, if the Profile is his own then that should be allowed.
    /// If the requesting Profile is null then no verification is performed as it indicates that the application.
    /// </summary>
    public Task<ServiceResponse> UpdateProfile(ProfileDTO Profile, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    /// <summary>
    /// DeleteProfile deletes an Profile and verifies if requesting Profile has permissions to delete it, if the Profile is his own then that should be allowed.
    /// If the requesting Profile is null then no verification is performed as it indicates that the application.
    /// </summary>
    public Task<ServiceResponse> DeleteProfile(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}
