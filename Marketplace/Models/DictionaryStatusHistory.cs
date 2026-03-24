using System;
using System.Collections.Generic;

namespace Marketplace.Models;

public partial class DictionaryStatusHistory
{
    public int DictionaryStatusHistoryId { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<StatusHistoryOrder> StatusHistoryOrders { get; set; } = new List<StatusHistoryOrder>();
}
