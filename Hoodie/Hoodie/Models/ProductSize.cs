using System;
using System.Collections.Generic;

namespace Hoodie.Models;

public partial class ProductSize
{
    public int ProductId { get; set; }

    public int SizeId { get; set; }

    public string? Additional { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Size Size { get; set; } = null!;
}
