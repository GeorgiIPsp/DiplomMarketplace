using System;
using System.Collections.Generic;

namespace Marketplace.Models;

public partial class OrderItem
{
    public int OrderItemsId { get; set; }

    public int OrderId { get; set; }

    public int CartItemId { get; set; }

    public string? StatusBoughtOut { get; set; }

    public virtual CartItem CartItem { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
