using System;
using System.Collections.Generic;

#nullable disable

namespace AspNetCoreCookiesAuthDemo.Models
{
    public partial class Tag
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
