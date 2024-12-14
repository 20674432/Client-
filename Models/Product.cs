using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ClientPortal.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    public string? Image { get; set; }

    public DateTime? CreatedAt { get; set; }


    [JsonIgnore]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
