using MobyLabWebProgramming.Core.Entities;
using Ardalis.Specification;

namespace MobyLabWebProgramming.Core.Specifications;

/// <summary>
/// This is a simple specification to filter the Profile entities from the database via the constructors.
/// Note that this is a sealed class, meaning it cannot be further derived.
/// </summary>
public sealed class ProfileSpec : BaseSpec<ProfileSpec, Profile>
{
    public ProfileSpec(Guid id) : base(id)
    {
    }

    public ProfileSpec(string firstname, string lastname)
    {
        Query.Where(e => (e.FirstName == firstname && e.LastName == lastname));
    }
}