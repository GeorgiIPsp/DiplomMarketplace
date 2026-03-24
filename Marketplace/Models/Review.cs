using System;
using System.Collections.Generic;

namespace Marketplace.Models;

public partial class Review
{
    public int ReviewsId { get; set; }

    public DateTime? DateCreated { get; set; }

    public int? Estimation { get; set; }

    public string? CommentByer { get; set; }

    public string? AnswerSeller { get; set; }

    public int OrderItemsId { get; set; }

    public virtual OrderItem OrderItems { get; set; } = null!;
}
