using System;
using System.Collections.Generic;

namespace assasment_task_backend.Models;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int? OrderId { get; set; }

    public int? ItemId { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public decimal? Total { get; set; }

    public virtual Item? Item { get; set; }

    public virtual Order? Order { get; set; }
}
