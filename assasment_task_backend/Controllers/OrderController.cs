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
    }
}
