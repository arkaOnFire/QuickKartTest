using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace QuickKartDataAccessLayer.Models
{
    public partial class Product
    {
        public Product()
        {
            PurchaseDetails = new HashSet<PurchaseDetail>();
        }

        public string ProductId { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public byte? CategoryId { get; set; }
        public decimal Price { get; set; }
        public int QuantityAvailable { get; set; }
        //Not considered
        [JsonIgnore]
        public virtual Category Category { get; set; }
        [JsonIgnore]
        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; }
    }
}
