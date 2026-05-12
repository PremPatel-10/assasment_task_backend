using System;
using System.Collections.Generic;

namespace assasment_task_backend.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int OrderNumber { get; set; }

    public string VendorName { get; set; } = null!;

    public DateOnly? OrderDate { get; set; }

    public int? OrderTotal { get; set; }
}
