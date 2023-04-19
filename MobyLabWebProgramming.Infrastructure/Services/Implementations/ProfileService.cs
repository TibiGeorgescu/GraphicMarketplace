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

public class ProfileService : IProfileService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    /// <summary>
    /// Inject the required services through the constructor.
    /// </summary>
    public ProfileService(IRepository<WebAppDatabaseContext> repository, ILoginService loginService, IMailService mailService)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<ProfileDTO>> GetProfile(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new ProfileProjectionSpec(id), cancellationToken); // Get a Profile using a specification on the repository.

        return result != null ?
            ServiceResponse<ProfileDTO>.ForSuccess(result) :
            ServiceResponse<ProfileDTO>.FromError(CommonErrors.ProfileNotFound); // Pack the result or error into a ServiceResponse.
    }

    public async Task<ServiceResponse<PagedResponse<ProfileDTO>>> GetProfiles(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new ProfileProjectionSpec(pagination.Search), cancellationToken); // Use the specification and pagination API to get only some entities from the database.

        return ServiceResponse<PagedResponse<ProfileDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse> AddProfile(ProfileAddDTO Profile, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin) // Verify who can add the Profile, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can add Profiles!", ErrorCodes.CannotAdd));
        }

<<<<<<< HEAD
        var result = await _repository.GetAsync(new UserSpec(Profile.UserId), cancellationToken);

        if (result == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The User doesn't exist!", ErrorCodes.UserDoesntExists));
        }


        var result2 = await _repository.GetAsync(new ProfileSpec(Profile.UserId, Profile.FirstName), cancellationToken);

        if (result2 != null)
=======
        var result = await _repository.GetAsync(new ProfileSpec(Profile.FirstName, Profile.LastName), cancellationToken);

        if (result != null)
>>>>>>> dce5d55beda079ed5a7cf7f9861e0b1d6a191f40
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The Profile already exists!", ErrorCodes.ProfileAlreadyExists));
        }

        await _repository.AddAsync(new Profile
        {
            FirstName = Profile.FirstName,
            LastName = Profile.LastName,
            Address = Profile.Address,
            PhoneNumber = Profile.PhoneNumber,
<<<<<<< HEAD
            UserId = Profile.UserId,
=======
            UserId = Profile.UserId
>>>>>>> dce5d55beda079ed5a7cf7f9861e0b1d6a191f40
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateProfile(ProfileDTO Profile, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
<<<<<<< HEAD
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Id != Profile.Id) // Verify who can add the Profile, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or the own Profile can update the Profile!", ErrorCodes.CannotUpdate));
        }

        var entity = await _repository.GetAsync(new ProfileSpec(Profile.Id), cancellationToken);

        if (entity != null) // Verify if the Profile is not found, you cannot update an non-existing entity.
        {
            entity.FirstName = Profile.FirstName ?? entity.FirstName;
            entity.LastName = Profile.LastName ?? entity.LastName; 
            entity.Address = Profile.Address ?? entity.Address;
            entity.PhoneNumber = Profile.PhoneNumber ?? entity.PhoneNumber;
            
            await _repository.UpdateAsync(entity, cancellationToken); // Update the entity and persist the changes.
        }
=======
        //if (requestingProfile != null && requestingProfile.Role != ProfileRoleEnum.Admin && requestingProfile.Id != Profile.Id) // Verify who can add the Profile, you can change this however you se fit.
        //{
        //    return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or the own Profile can update the Profile!", ErrorCodes.CannotUpdate));
        //}

        //var entity = await _repository.GetAsync(new ProfileSpec(Profile.Id), cancellationToken);

        //if (entity != null) // Verify if the Profile is not found, you cannot update an non-existing entity.
        //{
        //    entity.Name = Profile.Name ?? entity.Name;
        //    entity.Password = Profile.Password ?? entity.Password;

        //    await _repository.UpdateAsync(entity, cancellationToken); // Update the entity and persist the changes.
        //}
>>>>>>> dce5d55beda079ed5a7cf7f9861e0b1d6a191f40

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteProfile(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin) // Verify who can add the Profile, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or the own Profile can delete the Profile!", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync<Profile>(id, cancellationToken);

        return ServiceResponse.ForSuccess();
    }
}
