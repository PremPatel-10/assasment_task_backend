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

        public async Task<Item> GetItemById(int id)
        {
            var record = await context.Items.FindAsync(id);

            if (record == null)
            {
                throw new Exception("No Data Found");
            }

            return record;
        }

        //public async Task<Item?> SerchItemSingle(string itemName)
        //{
        //    return await context.Items.FirstOrDefaultAsync(i => i.ItemName == itemName);
        //}

        //public async Task<List<Item>> SerchItemMany(string itemName)
        //{
        //    return await context.Items.Where(i => i.ItemName == itemName).ToListAsync();
        //}

        public async Task<List<Item>> Search(string itemName)
        {
            var items = await context.Items.Where(i => i.ItemName.Contains(itemName)).ToListAsync();

            if (items.Count == 1)
            {
                items.FirstOrDefault();
            }

            return items;
        }

        public async Task<Item> AddItem(Item item)
        {
            bool exists = await context.Items.AnyAsync(i => i.ItemCode == item.ItemCode);
            if (exists)
            {
                throw new Exception("Item code already exists");
            }

            await context.Items.AddAsync(item);
            await context.SaveChangesAsync();
            return item;
        }

        public async Task<Item> EditItem(int id, Item item)
        {
            var record = await context.Items.FindAsync(id);

            if (record == null)
            {
                throw new Exception("Data Doesn't Exist");
            }

            record.ItemName = item.ItemName;
            record.ItemCode = item.ItemCode;

            await context.SaveChangesAsync();

            return record;
        }

        public void IsDelete(int id)
        {
            var record = context.Items.Find(id);

            if (record == null)
            {
                throw new Exception("Record Not Found");
            }

            context.Items.Remove(record);
            context.SaveChanges();
        }

        public async Task<List<Item>> Pagination(int page, int pageSize)
        {
            var totalItems = await context.Items.CountAsync();
            var totalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);

            var itemPerPage = await context.Items
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return itemPerPage;
        }
    }
}
