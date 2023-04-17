using MobyLabWebProgramming.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class ProductAddDTO
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }

    public Guid categoryId { get; set; }
}
