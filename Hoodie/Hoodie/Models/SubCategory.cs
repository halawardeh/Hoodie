using System;
using System.Collections.Generic;

namespace Hoodie.Models;

public partial class SubCategory
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Image { get; set; }

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;
}
