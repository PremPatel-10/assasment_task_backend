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

        public async Task<Order> GetOrderById(int id)
        {
            var record = await context.Orders.FindAsync(id);

            if (record == null)
            {
                throw new Exception("No Data Found");
            }

            return record;
        }

        public async Task<List<Order>> Search(string venderName)
        {
            var orders = await context.Orders.Where(o => o.VendorName.Contains(venderName)).ToListAsync();

            if (orders.Count == 1)
            {
                orders.FirstOrDefault();
            }

            return orders;
        }

        public async Task<Order> AddOrder(Order order)
        {
            bool exists = await context.Orders.AnyAsync(o => o.OrderNumber == order.OrderNumber);
            if (exists)
            {
                throw new Exception("Order code already exists");
            }

            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> EditOrder(int id, Order order)
        {
            var record = await context.Orders.FindAsync(id);

            if (record == null)
            {
                throw new Exception("Data Doesn't Exist");
            }

            record.OrderNumber = order.OrderNumber;
            record.VendorName = order.VendorName;
            record.OrderTotal = order.OrderTotal;

            await context.SaveChangesAsync();

            return record;
        }

        public void IsDelete(int id)
        {
            var record = context.Orders.Find(id);

            context.Orders.Remove(record!);
            context.SaveChanges();
        }

        public async Task<List<Order>> Pagination(int page, int pageSize)
        {
            var totalItems = await context.Orders.CountAsync();
            var totalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);

            var itemPerPage = await context.Orders
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return itemPerPage;
        }
    }
}
