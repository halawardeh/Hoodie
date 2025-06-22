using System;
using System.Collections.Generic;

namespace Hoodie.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? Fname { get; set; }

    public string? Lname { get; set; }

    public string? Email { get; set; }

    public string? Phonenumber { get; set; }

    public string? Gender { get; set; }

    public DateOnly? Birthday { get; set; }

    public string? Image { get; set; }

    public string? UserType { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Checkout> Checkouts { get; set; } = new List<Checkout>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Store> Stores { get; set; } = new List<Store>();

    public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
}
