using System;
using System.Collections.Generic;

namespace Marketplace.Models;

public partial class PresentCard
{
    public int PresentCardId { get; set; }

    public int SellerId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Brand { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsAvailable { get; set; }

    public string? Images { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual ICollection<PresentCardDiscount> PresentCardDiscounts { get; set; } = new List<PresentCardDiscount>();

    public virtual ICollection<ProductAttributeValue> ProductAttributeValues { get; set; } = new List<ProductAttributeValue>();

    public virtual ICollection<ProductPlace> ProductPlaces { get; set; } = new List<ProductPlace>();

    public virtual ICollection<ProductPrice> ProductPrices { get; set; } = new List<ProductPrice>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual Seller Seller { get; set; } = null!;
}
