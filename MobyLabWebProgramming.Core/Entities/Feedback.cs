using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Entities
{
    public class Feedback : BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
