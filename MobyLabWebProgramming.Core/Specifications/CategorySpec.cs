using MobyLabWebProgramming.Core.Entities;
using Ardalis.Specification;

namespace MobyLabWebProgramming.Core.Specifications;

/// <summary>
/// This is a simple specification to filter the Category entities from the database via the constructors.
/// Note that this is a sealed class, meaning it cannot be further derived.
/// </summary>
public sealed class CategorySpec : BaseSpec<CategorySpec, Category>
{
    public CategorySpec(Guid id) : base(id)
    {
    }

    public CategorySpec(string Name)
    {
        Query.Where(e => e.Name == Name);
    }
}