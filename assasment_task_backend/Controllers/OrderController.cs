using assasment_task_backend.Models;
using assasment_task_backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace assasment_task_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly InventoryContext context;

        public OrderController(IOrderService orderService, InventoryContext context)
        {
            this.orderService = orderService;
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("allOrder")]
        public async Task<IActionResult> GetAllOrders()
        {
            var record = await orderService.GetAllOrder();

            if (record == null)
            {
                return NotFound("Data Not Found");
            }

            return Ok(record);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var record = await orderService.GetOrderById(id);

            return Ok(record);
        }

        [HttpGet("search/{venderName}")]
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
            var record = await context.Orders
                .Skip((noOfPages - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(); ;

            return Ok(record);
        }
    }
}
