using System;
using System.Collections.Generic;

namespace Marketplace.Models;

public partial class ProductPlace
{
    public int ProductPlaceId { get; set; }

    public int PresentCardId { get; set; }

    public string? Shelving { get; set; }

    public string? Shelf { get; set; }

    public int Quantity { get; set; }

    public int IdWarehouse { get; set; }

    public virtual Warehouse IdWarehouseNavigation { get; set; } = null!;

    public virtual PresentCard PresentCard { get; set; } = null!;
}
