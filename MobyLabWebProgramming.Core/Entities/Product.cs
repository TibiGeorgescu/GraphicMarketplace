using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<UserFavoriteProduct> FavoritedByUsers { get; set; } = new List<UserFavoriteProduct>();


    }
}
