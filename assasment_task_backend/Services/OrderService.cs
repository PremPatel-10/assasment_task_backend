using assasment_task_backend.Models;
using assasment_task_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace assasment_task_backend.Services
{
    public class OrderService : IOrderService
    {
        private readonly InventoryContext context;

        public OrderService(InventoryContext context)
        {
            this.context = context;
        }

        public async Task<List<Order>> GetAllOrder()
        {
            return await context.Orders.ToListAsync();
        }

    }
}
