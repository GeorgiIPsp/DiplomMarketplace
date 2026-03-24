using System;
using System.Collections.Generic;

namespace Marketplace.Models;

public partial class PresentCardDiscount
{
    public int PresentCardDiscountId { get; set; }

    public int PresentCardId { get; set; }

    public int DiscountId { get; set; }

    public virtual Discount Discount { get; set; } = null!;

    public virtual PresentCard PresentCard { get; set; } = null!;
}
