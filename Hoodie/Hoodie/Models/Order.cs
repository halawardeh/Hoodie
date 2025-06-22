using System;
using System.Collections.Generic;

namespace Hoodie.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? UserId { get; set; }

    public int? ProductId { get; set; }

    public decimal? TotalAmount { get; set; }

    public DateOnly? OrderDate { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Checkout> Checkouts { get; set; } = new List<Checkout>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Product? Product { get; set; }

    public virtual User? User { get; set; }
}
