using eGift.WebAPI.Models;
using eGift.WebAPI.Models.RequestModels;
using eGift.WebAPI.Models.ResponseModels;
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
        public List<OrderModel> Get()
        {
            // Set command timeout for this specific query (e.g., 5 minutes)
            _context.Database.SetCommandTimeout(TimeSpan.FromSeconds(300));

            var list = _context.Order.Where(x => !x.IsDeleted).ToList();

            return list;
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
        public OrderModel Put(int id, [FromBody] OrderResponseModel orderResponseModel)
        {
            if (orderResponseModel.OrderModel.ID > 0)
            {
                // Updating order model
                _context.Order.Update(orderResponseModel.OrderModel);
                _context.SaveChanges();

                // Updating order detail list items
                foreach (var item in orderResponseModel.OrderDetailList)
                {
                    _context.OrderDetails.Update(item);
                }
                _context.SaveChanges();

                return orderResponseModel.OrderModel;
            }
            return null;
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        #endregion

        #region Order Response Actions

        // GET api/<OrderController>/GetOrderResponse/5
        [HttpGet("GetOrderResponse/{id}")]
        public OrderResponseModel GetOrderResponse(int id)
        {
            // Set command timeout for this specific query (e.g., 5 minutes)
            _context.Database.SetCommandTimeout(TimeSpan.FromSeconds(300));

            var orderResponseModel = new OrderResponseModel();

            // Order model data
            orderResponseModel.OrderModel = _context.Order.Find(id);

            // Customer model data
            var customerModel = _context.Customer.Find(orderResponseModel.OrderModel.CustomerId);
            orderResponseModel.OrderModel.MobileNumber = customerModel.Mobile;
            orderResponseModel.OrderModel.CustomerName = customerModel.FirstName + " " + customerModel.LastName;

            // Address model data
            var addressModel = _context.Address.Find(customerModel.AddressId);
            addressModel.CityName = _context.City.Find(addressModel?.CityId).CityName;
            addressModel.StateName = _context.State.Find(addressModel?.StateId).StateName;
            addressModel.CountryName = _context.Country.Find(addressModel?.CountryId).CountryName;

            orderResponseModel.OrderModel.Address = addressModel.FullAddress;

            // Order detail list
            orderResponseModel.OrderDetailList = _context.OrderDetails.Where(x => !x.IsDeleted && x.OrderId == id).ToList();
            foreach (var orderDetail in orderResponseModel.OrderDetailList)
            {
                orderDetail.ProductName = _context.Product.Find(orderDetail.ProductId).Name;
            }

            return orderResponseModel;
        }

        #endregion
    }
}
