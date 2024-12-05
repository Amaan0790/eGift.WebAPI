using eGift.WebAPI.Models;
using eGift.WebAPI.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eGift.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        #region Variables

        private readonly ApplicationDbContext _context;

        #endregion

        #region Constructors

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Order Default CRUD Actions

        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OrderController>
        [HttpPost]
        public OrderModel Post([FromBody] OrderRequestModel requestModel)
        {
            if (requestModel.OrderModel.ID == 0)
            {
                var orderModel = requestModel.OrderModel;

                _context.Order.Add(orderModel);
                _context.SaveChanges();

                if (orderModel.ID > 0)
                {
                    var orderDetailList = requestModel.OrderDetailList;
                    orderDetailList.ForEach(x => x.OrderId = orderModel.ID);

                    _context.OrderDetails.AddRange(orderDetailList);
                    _context.SaveChanges();
                }

                return orderModel;
            }
            return null;
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        #endregion
    }
}
