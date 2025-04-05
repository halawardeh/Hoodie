using System;
using System.Collections.Generic;

namespace Hoodie.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public int? Quantity { get; set; }

    public int? Solds { get; set; }

    public decimal? OldPrice { get; set; }

    public bool? Availability { get; set; }

    public string? Color { get; set; }

    public string? Size { get; set; }

    public string? Image { get; set; }

    public int? StoreId { get; set; }

    public int? CategoryId { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Category? Category { get; set; }

    public virtual Discount? Discount { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual Store? Store { get; set; }

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
