using System;
using System.Collections.Generic;

namespace QuickKartDataAccessLayer.Models
{
    public partial class User
    {
        public User()
        {
            PurchaseDetails = new HashSet<PurchaseDetail>();
        }

        public string EmailId { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public byte RoleId { get; set; }
        public string Gender { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; } = null!;

        public virtual Role Role { get; set; }
        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; }
    }
}
