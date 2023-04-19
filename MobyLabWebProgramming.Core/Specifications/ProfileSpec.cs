using MobyLabWebProgramming.Core.Entities;
using Ardalis.Specification;
<<<<<<< HEAD
using System.Xml.Linq;
=======
>>>>>>> dce5d55beda079ed5a7cf7f9861e0b1d6a191f40

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

<<<<<<< HEAD
    public ProfileSpec(Guid UserId, string FirstName)
    {
        Query.Where(e => e.UserId == UserId);
=======
    public ProfileSpec(string firstname, string lastname)
    {
        Query.Where(e => (e.FirstName == firstname && e.LastName == lastname));
>>>>>>> dce5d55beda079ed5a7cf7f9861e0b1d6a191f40
    }
}