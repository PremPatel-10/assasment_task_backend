using assasment_task_backend.Models;
using assasment_task_backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace assasment_task_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : Controller
    {
        private readonly IItemService itemService;

        public ItemController(IItemService itemService)
        {
            this.itemService = itemService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("allItems")]
        public async Task<IActionResult> GetAllItems()
        {
            var record = await itemService.GetAllItem();

            if (record == null)
            {
                return NotFound("Data Not Found");
            }

            return Ok(record);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemById(int id)
        {
            var record = await itemService.GetItemById(id);

            return Ok(record);
        }

        [HttpGet("search/{item}")]
        public async Task<IActionResult> Search(string item)
        {
            var record = await itemService.Search(item);

            if (record == null)
            {
                return NotFound("No data Found in records");
            }

            return Ok(record);
        }

        [HttpPost("add")]
        public async Task<IActionResult> InsertItem([FromBody] Item item)
        {
            try
            {
                await itemService.AddItem(item);

                return Ok(item);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] Item item)
        {
            try
            {
                await itemService.EditItem(id, item);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteItem(int id)
        {
            itemService.IsDelete(id);

            return Ok(new { message = "Item deleted successfully" });
        }

        [HttpGet("pages/{noOfPages}/{pageSize}")]
        public async Task<IActionResult> Pagination(int noOfPages,int pageSize)
        {
            var record = await itemService.Pagination(noOfPages, pageSize);

            return Ok(record);
        }
    }
}
