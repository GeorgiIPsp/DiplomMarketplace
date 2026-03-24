using System;
using System.Collections.Generic;

namespace Marketplace.Models;

public partial class CategoryAttribute
{
    public int AttributeId { get; set; }

    public string NameAttribute { get; set; } = null!;

    public string? Unit { get; set; }

    public string DataType { get; set; } = null!;

    public virtual ICollection<ProductAttributeCategory> ProductAttributeCategories { get; set; } = new List<ProductAttributeCategory>();
}
