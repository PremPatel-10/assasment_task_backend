using assasment_task_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace assasment_task_backend.Controllers
{
    [ApiController]
    [Route("orderDetail")]
    public class OrderDetailController : Controller
    {
        private readonly InventoryContext context;

        public OrderDetailController(InventoryContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetDetails()
        {
            var details = await context.OrderDetails.ToListAsync();

            return Ok(details);
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetDetailsByOrderID(int id)
        {
            var details = await context.OrderDetails.Where(o => o.OrderId == id).FirstOrDefaultAsync();

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
                if (details == null)
                {
                    return NotFound("Details Not found");
                }

                var record = await context.OrderDetails.AddAsync(details);
                await context.SaveChangesAsync();

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
                var record = await context.OrderDetails.FindAsync(id);

                if (record == null)
                {
                    return NotFound("Record Not Found");
                }

                record.OrderId = details.OrderId;
                record.ItemId = details.ItemId;
                record.Price = details.Price;
                record.Quantity = details.Quantity;

                await context.SaveChangesAsync();

                return Ok(record);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("getBulk/{id}")]
        public async Task<IActionResult> GetBulkDetails(int id)
        {
            var record = await context.OrderDetails.Where(d => d.OrderId == id).ToListAsync();

            if (!record.Any())
            {
                return NotFound("Record not found");
            }

            return Ok(record);
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> AddBulkDetails(List<OrderDetail> details)
        {
            try
            {
                if (details == null)
                {
                    return NotFound("Details not Found");
                }
                foreach (var item in details)
                {
                    context.OrderDetails.Add(item);
                }

                await context.SaveChangesAsync();
                return Ok(details);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
