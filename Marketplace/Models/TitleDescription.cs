using System;
using System.Collections.Generic;

namespace Marketplace.Models;

public partial class TitleDescription
{
    public int DescriptionId { get; set; }

    public string TitleDescription1 { get; set; } = null!;

    public virtual ICollection<TaskCompletionStatus> TaskCompletionStatuses { get; set; } = new List<TaskCompletionStatus>();
}
