using System;
using System.Collections.Generic;

namespace Hoodie.Models;

public partial class Color
{
    public int ColorId { get; set; }

    public string Name { get; set; } = null!;

    public string? HexCode { get; set; }

    public virtual ICollection<ProductColor> ProductColors { get; set; } = new List<ProductColor>();
}
