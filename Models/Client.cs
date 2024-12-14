using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ClientPortal.Models;

public partial class Client
{
    public int ClientId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    [JsonIgnore]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
