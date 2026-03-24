using System;
using System.Collections.Generic;

namespace Marketplace.Models;

public partial class TaskHistory
{
    public int HistoryId { get; set; }

    public int TaskId { get; set; }

    public int StatusTaskId { get; set; }

    public int EmployeeMarketplaceId { get; set; }

    public int? SellerId { get; set; }

    public DateTime? ChangedAt { get; set; }

    public virtual EmployeeMarketplace EmployeeMarketplace { get; set; } = null!;

    public virtual Seller? Seller { get; set; }

    public virtual TaskCompletionStatus StatusTask { get; set; } = null!;

    public virtual Task Task { get; set; } = null!;
}
