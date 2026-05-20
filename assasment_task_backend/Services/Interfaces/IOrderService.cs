using assasment_task_backend.Models;

namespace assasment_task_backend.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<Order> GetOrderById(int id);
        public Task<List<Order>> Search(string venderName);
        public Task<Order> AddOrder(Order order);
        public Task<Order> EditOrder(int id, Order order);
        public void IsDelete(int id);
        public Task<List<Order>> Pagination(int page = 1, int pageSize = 5);
    }
}
