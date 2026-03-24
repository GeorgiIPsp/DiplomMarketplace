using System;
using System.Collections.Generic;

namespace Marketplace.Models;

public partial class EmployeeMarketplace
{
    public int EmployeeMarketplaceId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime? DateCreated { get; set; }

    public int RoleId { get; set; }

    public int WarehouseId { get; set; }

    public virtual RoleEmployee Role { get; set; } = null!;

    public virtual ICollection<TaskHistory> TaskHistories { get; set; } = new List<TaskHistory>();

    public virtual Warehouse Warehouse { get; set; } = null!;

    public virtual ICollection<WorkLog> WorkLogs { get; set; } = new List<WorkLog>();
}
