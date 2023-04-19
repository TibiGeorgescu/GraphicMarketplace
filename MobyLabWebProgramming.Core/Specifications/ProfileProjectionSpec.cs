using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using System.Linq.Expressions;

namespace MobyLabWebProgramming.Core.Specifications;

public class ProfileProjectionSpec : BaseSpec<ProfileProjectionSpec, Profile, ProfileDTO>
{
    protected override Expression<Func<Profile, ProfileDTO>> Spec => e => new()
    {
<<<<<<< HEAD
        Id = e.Id,
=======
>>>>>>> dce5d55beda079ed5a7cf7f9861e0b1d6a191f40
        FirstName = e.FirstName,
        LastName = e.LastName,
        Address = e.Address,
        PhoneNumber = e.PhoneNumber,
        UserId = e.UserId,
<<<<<<< HEAD
=======

>>>>>>> dce5d55beda079ed5a7cf7f9861e0b1d6a191f40
    };

    public ProfileProjectionSpec(Guid id) : base(id)
    {
    }

    public ProfileProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

<<<<<<< HEAD
        Query.Where(e => EF.Functions.ILike(e.FirstName + e.LastName + e.Address, searchExpr)); // This is an example on who database specific expressions can be used via C# expressions.
=======
        Query.Where(e => EF.Functions.ILike(e.FirstName + e.LastName, searchExpr)); // This is an example on who database specific expressions can be used via C# expressions.
>>>>>>> dce5d55beda079ed5a7cf7f9861e0b1d6a191f40
                                                                  // Note that this will be translated to the database something like "where user.Name ilike '%str%'".
    }
}
