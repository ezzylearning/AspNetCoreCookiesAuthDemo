using System;
using System.Collections.Generic;

#nullable disable

namespace AspNetCoreCookiesAuthDemo.Models
{
    public partial class Product
    {
        public Product()
        {
            Tags = new HashSet<Tag>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public int? UnitsInStock { get; set; }
        public decimal? UnitPrice { get; set; }
        public bool? IsApproved { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
