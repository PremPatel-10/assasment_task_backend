using System;
using System.Collections.Generic;

namespace assasment_task_backend.Models;

public partial class Item
{
    public int ItemId { get; set; }

    public string ItemName { get; set; } = null!;

    public int ItemCode { get; set; }
}
