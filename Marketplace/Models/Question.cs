using System;
using System.Collections.Generic;

namespace Marketplace.Models;

public partial class Question
{
    public int QuestionsId { get; set; }

    public DateTime? DateCreated { get; set; }

    public string QuestionsByer { get; set; } = null!;

    public string? AnswerSeller { get; set; }

    public string ByerId { get; set; } = null!;

    public int PresentCardId { get; set; }

    public virtual Byer Byer { get; set; } = null!;

    public virtual PresentCard PresentCard { get; set; } = null!;
}
