using System;
using System.Collections.Generic;

namespace Marketplace.Models;

public partial class RoleEmployee
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<EmployeeMarketplace> EmployeeMarketplaces { get; set; } = new List<EmployeeMarketplace>();
}
