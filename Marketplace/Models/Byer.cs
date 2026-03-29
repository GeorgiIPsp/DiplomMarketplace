using System;
using System.Collections.Generic;

namespace Marketplace.Models;

public partial class Byer
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Patronomyc { get; set; }

    public string? Phone { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool IsActive { get; set; }

    public decimal? PersonalDiscount { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
