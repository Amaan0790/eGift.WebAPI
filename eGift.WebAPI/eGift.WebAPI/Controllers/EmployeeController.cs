using eGift.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eGift.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        #region Variables
        private readonly ApplicationDbContext _context;
        #endregion

        #region Constructors
        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Employee Default CRUD Actions

        // GET: api/<EmployeeController>
        [HttpGet]
        public List<EmployeeModel> Get()
        {
            // Set command timeout for this specific query (e.g., 5 minutes)
            _context.Database.SetCommandTimeout(TimeSpan.FromSeconds(300));

            var list = _context.Employee.Where(x => !x.IsDeleted).ToList();
            return list;
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public EmployeeModel Get(int id)
        {
            var model = _context.Employee.Find(id);
            return model;
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public EmployeeModel Post([FromBody] EmployeeModel model)
        {
            if (model.ID == 0)
            {
                _context.Employee.Add(model);
                _context.SaveChanges();
                return model;
            }
            return null;
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public EmployeeModel Put(int id, [FromBody] EmployeeModel model)
        {
            if (model.ID > 0)
            {
                model.UpdatedDate = DateTime.Now;
                _context.Employee.Update(model);
                _context.SaveChanges();
                return model;
            }
            return null;
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public int Delete(int id, int loginUserId)
        {
            var model = _context.Employee.Where(x => !x.IsDeleted && x.ID == id).FirstOrDefault();
            if (model != null)
            {
                model.IsDeleted = true;
                model.UpdatedDate = DateTime.Now;
                model.UpdatedBy = loginUserId;
                _context.Employee.Update(model);
                _context.SaveChanges();
                return id;
            }
            return 0;
        }
        #endregion
    }
}
