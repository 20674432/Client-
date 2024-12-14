using System;
using System.Collections.Generic;

namespace ClientPortal.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? ClientId { get; set; }

    public int? ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal TotalPrice { get; set; }

    public int? StatusId { get; set; }

    public DateTime? OrderDate { get; set; }

    public DateTime? LastUpdated { get; set; }

    public virtual Client? Client { get; set; }

    public virtual Product? Product { get; set; }

    public virtual OrderStatus? Status { get; set; }
}
