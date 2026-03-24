using System;
using System.Collections.Generic;

namespace Marketplace.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public DateTime? OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public string ByerId { get; set; } = null!;

    public virtual Byer Byer { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<StatusHistoryOrder> StatusHistoryOrders { get; set; } = new List<StatusHistoryOrder>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
