using assasment_task_backend.Models;
using assasment_task_backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace assasment_task_backend.Controllers
{
    [ApiController]
    [Route("orderDetail")]
    public class OrderDetailController : Controller
    {
        private readonly IOrderDetailService orderDetailService;

        public OrderDetailController(IOrderDetailService orderDetailService)
        {
            this.orderDetailService = orderDetailService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetDetails()
        {
            var details = await orderDetailService.GetAllDetails();

            return Ok(details);
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetDetailsByOrderID(int id)
        {
            var details = await orderDetailService.GetByOrderId(id);

            if (details == null)
            {
                return NotFound("Data Not Exist");
            }

            return Ok(details);
        }

        [HttpPost("addDetails")]
        public async Task<IActionResult> PostDetails([FromBody] OrderDetail details)
        {
            try
            {
                var record = await orderDetailService.InsertDetails(details);

                return Ok(record);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("editDetails/{id}")]
        public async Task<IActionResult> PutDetails(int id, [FromBody] OrderDetail details)
        {
            try
            {
                var record = await orderDetailService.UpdateDetails(id, details);

                if(record == null)
                {
                    return NotFound("Record Not Found");
                }

                return Ok(record);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
