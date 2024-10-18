using eGift.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eGift.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        #region Variables

        private readonly ApplicationDbContext _context;

        #endregion

        #region Constructors

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Category Default CRUD Actions

        // GET: api/<CategoryController>
        [HttpGet]
        public List<CategoryModel> Get()
        {
            // Set command timeout for this specific query (e.g., 5 minutes)
            _context.Database.SetCommandTimeout(TimeSpan.FromSeconds(300));

            var list = _context.Category.Where(x => !x.IsDeleted).ToList();
            return list;
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public CategoryModel Get(int id)
        {
            var model = _context.Category.Find(id);
            return model;
        }

        // POST api/<CategoryController>
        [HttpPost]
        public CategoryModel Post([FromBody] CategoryModel model)
        {
            if (model.ID == 0)
            {
                _context.Category.Add(model);
                _context.SaveChanges();
                return model;
            }
            return null;
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public CategoryModel Put(int id, [FromBody] CategoryModel model)
        {
            if (model.ID > 0)
            {
                model.UpdatedDate = DateTime.Now;
                _context.Category.Update(model);
                _context.SaveChanges();
                return model;
            }
            return null;
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public int Delete(int id, int loginUserId)
        {
            var model = _context.Category.Where(x => !x.IsDeleted && x.ID == id).FirstOrDefault();
            if (model != null)
            {
                model.IsDeleted = true;
                model.UpdatedDate = DateTime.Now;
                model.UpdatedBy = loginUserId;
                _context.Category.Update(model);
                _context.SaveChanges();
                return id;
            }
            return 0;
        }

        #endregion

        #region Remote Validation Actions

        // GET api/<CategoryController>/VerifyCategoryName
        [HttpGet("VerifyCategoryName")]
        public bool VerifyCategoryName(int id, string categoryName)
        {
            var verifyCategory = _context.Category.Where(x => !x.IsDeleted && x.CategoryName == categoryName).FirstOrDefault();
            if (verifyCategory != null)
            {
                var existingCategory = _context.Category.Where(x => x.ID == id).FirstOrDefault();
                if (existingCategory?.CategoryName == verifyCategory?.CategoryName)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}
