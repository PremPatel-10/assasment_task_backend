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

        [HttpGet("allOrder")]
        public async Task<IActionResult> GetAllOrder()
        {
            var record = await orderService.GetAllOrder();

            if (record == null)
            {
                return NotFound("Data Not Found");
            }

            return Ok(record);
        }
    }
}
