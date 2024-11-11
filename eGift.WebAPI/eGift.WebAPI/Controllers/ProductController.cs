using eGift.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eGift.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        #region Variables
        private readonly ApplicationDbContext _context;
        #endregion

        #region Constructors
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Product Default CRUD Actions

        // GET: api/<ProductController>
        [HttpGet]
        public List<ProductModel> Get()
        {
            // Set command timeout for this specific query (e.g., 5 minutes)
            _context.Database.SetCommandTimeout(TimeSpan.FromSeconds(600));

            var list = _context.Product.Where(x => !x.IsDeleted).ToList();
            return list;
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public ProductModel Get(int id)
        {
            var model = _context.Product.Find(id);
            return model;
        }

        // POST api/<ProductController>
        [HttpPost]
        public ProductModel Post([FromBody] ProductModel model)
        {
            if (model.ID == 0)
            {
                _context.Product.Add(model);
                _context.SaveChanges();
                return model;
            }
            return null;
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public ProductModel Put(int id, [FromBody] ProductModel model)
        {
            if (model.ID > 0)
            {
                _context.Product.Update(model);
                _context.SaveChanges();
                return model;
            }
            return null;
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public int Delete(int id, int loginUserId)
        {
            var model = _context.Product.Where(x => !x.IsDeleted && x.ID == id).FirstOrDefault();
            if (model != null)
            {
                model.IsDeleted = true;
                model.UpdatedDate = DateTime.Now;
                model.UpdatedBy = loginUserId;
                _context.Product.Update(model);
                _context.SaveChanges();

                return id;
            }
            return 0;
        }
        #endregion

        #region Remote Validation Actions
        // For Product Name
        // GET api/<ProductController>/VerifyProductName
        [HttpGet("VerifyProductName")]
        public bool VerifyProductName(int id, int categoryId, int subCategoryId, string Name)
        {
            var verifyProduct = _context.Product.Where(x =>
                !x.IsDeleted && x.CategoryId == categoryId && x.SubCategoryId == subCategoryId && x.Name == Name).FirstOrDefault();
            if (verifyProduct != null)
            {
                var existingProduct = _context.Product.Find(id);
                if(existingProduct?.CategoryId == verifyProduct.CategoryId && existingProduct?.SubCategoryId == verifyProduct.SubCategoryId && existingProduct?.Name == verifyProduct.Name)
                {
                    return false;
                }
                return true;
            }
            return false;
        }
        #endregion
    }
}
