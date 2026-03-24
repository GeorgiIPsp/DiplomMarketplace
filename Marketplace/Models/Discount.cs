using System;
using System.Collections.Generic;

namespace Marketplace.Models;

public partial class Discount
{
    public int DiscountId { get; set; }

    public string NameDiscount { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public decimal DiscountAmount { get; set; }

    public virtual ICollection<PresentCardDiscount> PresentCardDiscounts { get; set; } = new List<PresentCardDiscount>();
}
