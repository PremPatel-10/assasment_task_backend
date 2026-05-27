using assasment_task_backend.Models;
using assasment_task_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace assasment_task_backend.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly InventoryContext context;

        public OrderDetailService(InventoryContext context)
        {
            this.context = context;
        }

        public async Task<List<OrderDetail>> GetAllDetails()
        {
            return await context.OrderDetails.ToListAsync();
        }

        public async Task<OrderDetail> GetByOrderId(int id)
        {
            var record = await (from d in context.OrderDetails
                                where d.OrderId == id
                                select new OrderDetail
                                {
                                    OrderDetailId = d.OrderDetailId,
                                    OrderId = d.OrderId,
                                    ItemId = d.ItemId,
                                    Price = d.Price,
                                    Quantity = d.Quantity,
                                    Total = d.Total,
                                    Item = new Item
                                    {
                                        ItemId = (int)d.ItemId!,
                                        ItemName = d.Item!.ItemName,
                                        ItemCode = d.Item.ItemCode
                                    },
                                    Order = new Order
                                    {
                                        OrderId = id,
                                        OrderNumber = d.Order!.OrderNumber,
                                        VendorName = d.Order.VendorName,
                                        OrderDate = d.Order.OrderDate,
                                        OrderTotal = d.Order.OrderTotal
                                    }
                                }).FirstOrDefaultAsync();

            if (record == null)
            {
                throw new Exception("Record not found");
            }

            return record;
        }

        public async Task<OrderDetail> InsertDetails(OrderDetail orderInfo)
        {
            if (orderInfo == null)
            {
                throw new Exception("Order Detials is Null");
            }

            await context.OrderDetails.AddAsync(orderInfo);
            await context.SaveChangesAsync();

            return orderInfo;
        }

        public async Task<OrderDetail> UpdateDetails(int id, OrderDetail orderInfo)
        {
            var record = await context.OrderDetails.FindAsync(id);
            if (record == null)
            {
                throw new Exception("Record not Found");
            }

            record.OrderId = orderInfo.OrderId;
            record.ItemId = orderInfo.ItemId;
            record.Price = orderInfo.Price;
            record.Quantity = orderInfo.Quantity;

            await context.SaveChangesAsync();

            return record;
        }
    }
}
