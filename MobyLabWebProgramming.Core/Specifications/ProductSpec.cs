﻿using MobyLabWebProgramming.Core.Entities;
using Ardalis.Specification;

namespace MobyLabWebProgramming.Core.Specifications;

/// <summary>
/// This is a simple specification to filter the Product entities from the database via the constructors.
/// Note that this is a sealed class, meaning it cannot be further derived.
/// </summary>
public sealed class ProductSpec : BaseSpec<ProductSpec, Product>
{
    public ProductSpec(Guid id) : base(id)
    {
    }

    public ProductSpec(string Name)
    {
        Query.Where(e => e.Name == Name);
    }
}