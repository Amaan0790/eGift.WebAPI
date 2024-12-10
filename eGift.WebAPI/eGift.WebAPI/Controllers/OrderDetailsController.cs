using eGift.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eGift.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        #region Variables

        private readonly ApplicationDbContext _context;

        #endregion

        #region Constructors

        public OrderDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region OrderDetails Default CRUD Actions

        // GET: api/<OrderDetailsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<OrderDetailsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OrderDetailsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<OrderDetailsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderDetailsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        #endregion

        #region Order Details Get By Customer ID

        // Get order detail list by customer ID
        // GET api/<OrderDetailsController>/GetByCustomerId/5
        [HttpGet("GetByCustomerId/{id}")]
        public List<OrderDetailsModel> GetByCustomerId(int id)
        {
            // Set command timeout for this specific query (e.g., 5 minutes)
            _context.Database.SetCommandTimeout(TimeSpan.FromSeconds(300));

            var list = _context.OrderDetails.Where(x => !x.IsDeleted && x.CreatedBy == id).ToList();
            foreach (var item in list)
            {
                // Product table data
                var product = _context.Product.Find(item.ProductId);
                item.ProductName = product?.Name;
                item.ProductImageData = product?.ProductImageData;

                // Order table data
                var order = _context.Order.Find(item.OrderId);
                item.OrderNumber = order?.OrderNumber;
                item.Notes = order?.Notes;
                item.StatusId = (int)order?.StatusId;
                item.DispatchedDate = order?.DispatchedDate;
                item.ShippedDate = order?.ShippedDate;
                item.DeliveryDate = order?.DeliveryDate;
                item.CancelDate = order?.CancelDate;
            }
            return list;
        }

        #endregion
    }
}
