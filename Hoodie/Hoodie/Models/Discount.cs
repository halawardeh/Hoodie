using System;
using System.Collections.Generic;

namespace Hoodie.Models;

public partial class Discount
{
    public int DiscountId { get; set; }

    public int? StoreId { get; set; }

    public int? ProductId { get; set; }

    public decimal? DiscountPercentage { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? Description { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Store? Store { get; set; }
}
