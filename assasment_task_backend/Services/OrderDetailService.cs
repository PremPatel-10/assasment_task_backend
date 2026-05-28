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
            var record = await context.OrderDetails.Where(o => o.OrderId == id).FirstOrDefaultAsync();

            if (record == null)
            {
                throw new Exception("Record Not Found");
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

        public async Task<OrderDetail> UpdateDetails(int id, OrderDetail details)
        {
            var record = await context.OrderDetails.FindAsync(id);

            if (record == null)
            {
                throw new Exception("Details Not Found");
            }

            record.OrderId = details.OrderId;
            record.ItemId = details.ItemId;
            record.Price = details.Price;
            record.Quantity = details.Quantity;

            await context.SaveChangesAsync();

            return record;
        }
        public async Task AddBulkDetails(List<OrderDetail> details)
        {
            foreach (var item in details)
            {
                context.OrderDetails.Add(item);
            }
            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
