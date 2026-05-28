using assasment_task_backend.Models;

namespace assasment_task_backend.Services.Interfaces
{
    public interface IOrderDetailService
    {
        public Task<List<OrderDetail>> GetAllDetails();
        public Task<OrderDetail> GetByOrderId(int id);
        public Task<OrderDetail> InsertDetails(OrderDetail orderInfo);
        public Task<OrderDetail> UpdateDetails(int id, OrderDetail orderInfo);
        public Task AddBulkDetails(List<OrderDetail> details);
    }
}
