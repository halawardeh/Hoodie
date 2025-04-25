using System;
using System.Collections.Generic;

namespace Hoodie.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? UserId { get; set; }

    public decimal? TotalAmount { get; set; }

    public DateOnly? OrderDate { get; set; }

    public string? Status { get; set; }

    // العلاقة مع الـ User
    public virtual User? User { get; set; }

    // علاقة مع OrderItems لتخزين تفاصيل المنتجات في الطلب
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    // يمكنك الإبقاء على العلاقة مع Checkout إذا كان له دور محدد
    public virtual ICollection<Checkout> Checkouts { get; set; } = new List<Checkout>();
}
