using System;
using System.Collections.Generic;

namespace Hoodie.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public int? UserId { get; set; }

    public int? ProductId { get; set; }

    public string? Comment { get; set; }

    public int? Stars { get; set; }

    public DateOnly? Date { get; set; }

    public int? HelpfulNumber { get; set; }

    public virtual Product? Product { get; set; }

    public virtual User? User { get; set; }
}
