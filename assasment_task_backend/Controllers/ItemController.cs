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

            //if (record == null)
            //{
            //    return NotFound("Data Not Found");
            //}

            return Ok(record);
        }
    }
}
