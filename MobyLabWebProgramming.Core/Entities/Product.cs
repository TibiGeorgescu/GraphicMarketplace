using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Entities
{
    public class Product : BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }

        // PRODUCT OWNER
        public int OwnerId { get; set; }
        public User User { get; set; }
        
        // PRODUCT CATEGORY
        public int CategoryId { get; set; }
        public Categories Category { get; set; }
    }
}
