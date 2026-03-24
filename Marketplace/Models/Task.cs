using System;
using System.Collections.Generic;

namespace Marketplace.Models;

public partial class Task
{
    public int TaskId { get; set; }

    public int WarehouseId { get; set; }

    public int? Priority { get; set; }

    public decimal? ActualHourseWork { get; set; }

    public int? OrderId { get; set; }

    public DateTime? DateCreated { get; set; }

    public virtual Order? Order { get; set; }

    public virtual ICollection<TaskHistory> TaskHistories { get; set; } = new List<TaskHistory>();

    public virtual Warehouse Warehouse { get; set; } = null!;
}
