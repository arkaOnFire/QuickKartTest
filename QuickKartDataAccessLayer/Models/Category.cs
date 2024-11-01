using System;
using System.Collections.Generic;

namespace QuickKartDataAccessLayer.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public byte CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
