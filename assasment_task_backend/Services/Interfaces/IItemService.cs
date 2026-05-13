using assasment_task_backend.Models;

namespace assasment_task_backend.Services.Interfaces
{
    public interface IItemService
    {
        public Task<List<Item>> GetAllItem();
        //public Task<Item?> SerchItemSingle(string itemName);
        //public Task<List<Item>> SerchItemMany(string itemName);
        public Task<Item> GetItemById(int id);
        public Task<List<Item>> Search(string itemName);
        public Task<Item> AddItem(Item item);
        public Task<Item> EditItem(int id, Item item);
        public Task<bool> IsDelete(int id);
    }
}
