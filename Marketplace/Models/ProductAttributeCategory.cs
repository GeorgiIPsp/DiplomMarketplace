using System;
using System.Collections.Generic;

namespace Marketplace.Models;

public partial class ProductAttributeCategory
{
    public int AttributeCategoryId { get; set; }

    public int AttributeId { get; set; }

    public int CategoryId { get; set; }

    public virtual CategoryAttribute Attribute { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<ProductAttributeValue> ProductAttributeValues { get; set; } = new List<ProductAttributeValue>();
}
