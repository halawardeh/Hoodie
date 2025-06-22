using System;
using System.Collections.Generic;

namespace Hoodie.Models;

public partial class Tag
{
    public int TagId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
