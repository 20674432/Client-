using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ClientPortal.Models;

public partial class OrderStatus
{
    public int StatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public string? Description { get; set; }


    [JsonIgnore]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
