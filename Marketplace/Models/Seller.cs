using System;
using System.Collections.Generic;

namespace Marketplace.Models;

public partial class Seller
{
    public int SellerId { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Patronomcy { get; set; }

    public string? Phone { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsActive { get; set; }

    public string? CompanyName { get; set; }

    public virtual ICollection<PresentCard> PresentCards { get; set; } = new List<PresentCard>();

    public virtual ICollection<TaskHistory> TaskHistories { get; set; } = new List<TaskHistory>();
}
