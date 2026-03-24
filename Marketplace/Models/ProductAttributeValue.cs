using System;
using System.Collections.Generic;

namespace Marketplace.Models;

public partial class ProductAttributeValue
{
    public int AttributeValueId { get; set; }

    public int PresentCardId { get; set; }

    public int AttributeCategoryId { get; set; }

    public string Value { get; set; } = null!;

    public virtual ProductAttributeCategory AttributeCategory { get; set; } = null!;

    public virtual PresentCard PresentCard { get; set; } = null!;
}
