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
            var records = await context.OrderDetails.FirstOrDefaultAsync(d => d.OrderId == id);

            if (records == null)
            {
                throw new Exception("Record not found");
            }

            return records;
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
