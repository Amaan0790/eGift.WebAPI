using eGift.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eGift.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        #region Variables

        private readonly ApplicationDbContext _context;

        #endregion

        #region Constructors

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Customer Default CRUD Actions

        // GET: api/<CustomerController>
        [HttpGet]
        public List<CustomerModel> Get()
        {
            // Set command timeout for this specific query (e.g., 5 minutes)
            _context.Database.SetCommandTimeout(TimeSpan.FromSeconds(300));

            var list = _context.Customer.Where(x => !x.IsDeleted).ToList();
            return list;
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public CustomerModel Get(int id)
        {
            var model = _context.Customer.Find(id);
            return model;
        }

        // POST api/<CustomerController>
        [HttpPost]
        public CustomerModel Post([FromBody] CustomerModel model)
        {
            if (model.ID == 0)
            {
                _context.Customer.Add(model);
                _context.SaveChanges();
                return model;
            }
            return null;
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public CustomerModel Put(int id, [FromBody] CustomerModel model)
        {
            if (model.ID > 0)
            {
                model.UpdatedDate = DateTime.Now;
                _context.Customer.Update(model);
                _context.SaveChanges();
                return model;
            }
            return null;
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public int Delete(int id, int loginUserId)
        {
            var model = _context.Customer.Where(x => !x.IsDeleted && x.ID == id).FirstOrDefault();
            if (model != null)
            {
                model.IsDeleted = true;
                model.UpdatedDate = DateTime.Now;
                model.UpdatedBy = loginUserId;
                _context.Customer.Update(model);
                _context.SaveChanges();
                return id;
            }
            return 0;
        }

        #endregion
    }
}
