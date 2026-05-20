using assasment_task_backend.Models;
using assasment_task_backend.Services;
using assasment_task_backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace assasment_task_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var record = await orderService.GetOrderById(id);

            return Ok(record);
        }

        [HttpGet("search/{vender}")]
        public async Task<IActionResult> Search(string venderName)
        {
            var record = await orderService.Search(venderName);

            if (record == null)
            {
                return NotFound("No data Found in records");
            }

            return Ok(record);
        }

        [HttpPost("add")]
        public async Task<IActionResult> InsertOrder([FromBody] Order order)
        {
            try
            {
                await orderService.AddOrder(order);

                return Ok(order);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] Order order)
        {
            try
            {
                await orderService.EditOrder(id, order);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteItem(int id)
        {
            orderService.IsDelete(id);

            return Ok(new { message = "Item deleted successfully" });
        }

        [HttpGet("pages/{noOfPages}/{pageSize}")]
        public async Task<IActionResult> Pagination(int noOfPages, int pageSize)
        {
            var record = await orderService.Pagination(noOfPages, pageSize);

            return Ok(record);
        }
    }
}
