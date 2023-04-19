using MobyLabWebProgramming.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class FeedbackDTO
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? Content { get; set; }
    public int? Rating { get; set; }
    public Guid ProductId { get; set; }
}
