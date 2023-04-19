using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using System.Linq.Expressions;

namespace MobyLabWebProgramming.Core.Specifications;

public class OrderProjectionSpec : BaseSpec<OrderProjectionSpec, Order, OrderDTO>
{
    protected override Expression<Func<Order, OrderDTO>> Spec => e => new()
    {
        Id = e.Id,
        Quantity = e.Quantity,
        UserId = e.UserId,
        ProductId = e.ProductId,
    };

    public OrderProjectionSpec(Guid id) : base(id)
    {
    }

    public OrderProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Id.ToString(), searchExpr)); // This is an example on who database specific expressions can be used via C# expressions.
                                                                     // Note that this will be translated to the database something like "where user.Name ilike '%str%'".
    }
}
