using eGift.WebAPI.Common;
using eGift.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eGift.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        #region Variables

        private readonly ApplicationDbContext _context;

        #endregion

        #region Constructors

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Default CRUD Actions

        // GET: api/<LoginController>
        [HttpGet]
        public List<LoginModel> Get()
        {
            var list = _context.Login.ToList();
            return list;
        }

        // GET api/<LoginController>/5
        [HttpGet("{id}")]
        public LoginModel Get(int id)
        {
            var model = _context.Login.Where(x => x.ID == id).FirstOrDefault();
            return model;
        }

        // POST api/<LoginController>
        [HttpPost]
        public LoginModel Post([FromBody] LoginModel model)
        {
            if (model.ID == 0)
            {
                _context.Login.Add(model);
                _context.SaveChanges();
                return model;
            }
            return null;
        }

        // PUT api/<LoginController>/5
        [HttpPut("{id}")]
        public LoginModel Put(int id, [FromBody] LoginModel model)
        {
            if (model.ID > 0)
            {
                _context.Login.Update(model);
                _context.SaveChanges();
                return model;
            }
            return null;
        }

        // DELETE api/<LoginController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        #endregion

        #region Get Employee Login Actions

        // GET api/<LoginController>/5
        [HttpGet("GetEmployeeLogin/{id}")]
        public LoginModel GetEmployeeLogin(int id)
        {
            var model = _context.Login.Where(x => x.RefId == id && x.RefType == Role.Employee.ToString()).FirstOrDefault();
            return model;
        }

        #endregion

        #region Get Customer Login Actions

        // GET api/<LoginController>/5
        [HttpGet("GetCustomerLogin/{id}")]
        public LoginModel GetCustomerLogin(int id)
        {
            var model = _context.Login.Where(x => x.RefId == id && x.RefType == Role.Customer.ToString()).FirstOrDefault();
            return model;
        }

        #endregion
    }
}
