using System;
using System.Collections.Generic;

namespace Hoodie.Models;

public partial class Message
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string Messagee { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }
}
