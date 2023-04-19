using MobyLabWebProgramming.Core.Entities;
using Ardalis.Specification;

namespace MobyLabWebProgramming.Core.Specifications;

/// <summary>
/// This is a simple specification to filter the Feedback entities from the database via the constructors.
/// Note that this is a sealed class, meaning it cannot be further derived.
/// </summary>
public sealed class FeedbackSpec : BaseSpec<FeedbackSpec, Feedback>
{
    public FeedbackSpec(Guid id) : base(id)
    {
    }

    public FeedbackSpec(Guid UserId, Guid ProductId)
    {
        Query.Where(e => (e.UserId == UserId && e.ProductId == ProductId));
    }
}