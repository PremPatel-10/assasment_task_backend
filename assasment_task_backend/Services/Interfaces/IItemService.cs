using assasment_task_backend.Models;

namespace assasment_task_backend.Services.Interfaces
{
    public interface IItemService
    {
        public Task<List<Item>> GetAllItem();
    }
}
