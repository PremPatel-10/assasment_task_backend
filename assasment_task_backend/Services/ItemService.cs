using assasment_task_backend.Models;
using assasment_task_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace assasment_task_backend.Services
{
    public class ItemService : IItemService
    {
        private readonly InventoryContext context;

        public ItemService(InventoryContext context)
        {
            this.context = context;
        }

        public async Task<List<Item>> GetAllItem()
        {
            return await context.Items.ToListAsync();
        }
    }
}
