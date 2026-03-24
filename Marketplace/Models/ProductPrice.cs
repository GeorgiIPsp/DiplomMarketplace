using System;
using System.Collections.Generic;

namespace Marketplace.Models;

public partial class ProductPrice
{
    public int ProductPriceId { get; set; }

    public int PresentCardId { get; set; }

    public decimal Price { get; set; }

    public virtual PresentCard PresentCard { get; set; } = null!;
}
