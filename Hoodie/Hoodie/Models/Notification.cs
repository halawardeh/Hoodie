using System;
using System.Collections.Generic;

namespace Hoodie.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public int? UserId { get; set; }

    public string? Message { get; set; }

    public bool? IsRead { get; set; }

    public DateOnly? Date { get; set; }

    public virtual User? User { get; set; }
}
