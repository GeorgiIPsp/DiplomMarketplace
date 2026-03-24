using System;
using System.Collections.Generic;

namespace Marketplace.Models;

public partial class TaskCompletionStatus
{
    public int StatusTaskId { get; set; }

    public int DescriptionId { get; set; }

    public string TitleStatus { get; set; } = null!;

    public virtual TitleDescription Description { get; set; } = null!;

    public virtual ICollection<TaskHistory> TaskHistories { get; set; } = new List<TaskHistory>();
}
