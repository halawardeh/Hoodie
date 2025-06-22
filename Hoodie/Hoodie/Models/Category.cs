using System;
using System.Collections.Generic;

namespace Hoodie.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string? Name { get; set; }

    public string? Image { get; set; }

    public int? StoreId { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual Store? Store { get; set; }

    public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}
