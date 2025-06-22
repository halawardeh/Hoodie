using System;
using System.Collections.Generic;

namespace Hoodie.Models;

public partial class Checkout
{
    public int CheckoutId { get; set; }

    public int UserId { get; set; }

    public int OrderId { get; set; }

    public string? PaymentMethod { get; set; }

    public string? PaymentStatus { get; set; }

    public DateTime? CheckoutDate { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
