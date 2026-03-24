using System;
using System.Collections.Generic;

namespace Marketplace.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ProductAttributeCategory> ProductAttributeCategories { get; set; } = new List<ProductAttributeCategory>();
}
