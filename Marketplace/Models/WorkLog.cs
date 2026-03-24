using System;
using System.Collections.Generic;

namespace Marketplace.Models;

public partial class WorkLog
{
    public int WorkLogId { get; set; }

    public DateOnly WorkDate { get; set; }

    public decimal HoursSpent { get; set; }

    public int QuantityTask { get; set; }

    public int UserId { get; set; }

    public virtual EmployeeMarketplace User { get; set; } = null!;
}
