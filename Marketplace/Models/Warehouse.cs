using System;
using System.Collections.Generic;

namespace Marketplace.Models;

public partial class Warehouse
{
    public int WarehouseId { get; set; }

    public string? Type { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<EmployeeMarketplace> EmployeeMarketplaces { get; set; } = new List<EmployeeMarketplace>();

    public virtual ICollection<ProductPlace> ProductPlaces { get; set; } = new List<ProductPlace>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
