using System;
using System.Collections.Generic;

namespace Hoodie.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? UserId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public decimal? TotalAmount { get; set; }

    public DateOnly? OrderDate { get; set; }

    public string? Status { get; set; }

    public virtual Product? Product { get; set; }

    public virtual User? User { get; set; }
}
