using System;
using System.Collections.Generic;

namespace Hoodie.Models;

public partial class ProductColor
{
    public int ProductId { get; set; }

    public int ColorId { get; set; }

    public string? Additional { get; set; }

    public virtual Color Color { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
