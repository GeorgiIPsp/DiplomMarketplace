using System;
using System.Collections.Generic;

namespace Marketplace.Models;

public partial class StatusHistoryOrder
{
    public int StatusHistoryOrderId { get; set; }

    public int DictionaryStatusHistoryId { get; set; }

    public int OrderId { get; set; }

    public DateTime? DataEdit { get; set; }

    public virtual DictionaryStatusHistory DictionaryStatusHistory { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
