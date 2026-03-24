using System;
using System.Collections.Generic;

namespace Marketplace.Models;

public partial class CartItem
{
    public int CartItemId { get; set; }

    public string ByerId { get; set; } = null!;

    public int PresentCardId { get; set; }

    public int Quantity { get; set; }

    public virtual Byer Byer { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual PresentCard PresentCard { get; set; } = null!;
}
