using eGift.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eGift.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        #region Variables

        private readonly ApplicationDbContext _context;

        #endregion

        #region Constructors

        public SubCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region SubCategory Default CRUD Actions

        // GET: api/<SubCategoryController>
        [HttpGet]
        public List<SubCategoryModel> Get()
        {
            // Set command timeout for this specific query (e.g., 5 minutes)
            _context.Database.SetCommandTimeout(TimeSpan.FromSeconds(300));

            var list = _context.SubCategory.Where(x => !x.IsDeleted).ToList();
            return list;
        }

        // GET api/<SubCategoryController>/5
        [HttpGet("{id}")]
        public SubCategoryModel Get(int id)
        {
            var model = _context.SubCategory.Find(id);
            return model;
        }

        // POST api/<SubCategoryController>
        [HttpPost]
        public SubCategoryModel Post([FromBody] SubCategoryModel model)
        {
            if (model.ID == 0)
            {
                _context.SubCategory.Add(model);
                _context.SaveChanges();
                return model;
            }
            return null;
        }

        // PUT api/<SubCategoryController>/5
        [HttpPut("{id}")]
        public SubCategoryModel Put(int id, [FromBody] SubCategoryModel model)
        {
            if (model.ID > 0)
            {
                model.UpdatedDate = DateTime.Now;
                _context.SubCategory.Update(model);
                _context.SaveChanges();
                return model;
            }
            return null;
        }

        // DELETE api/<SubCategoryController>/5
        [HttpDelete("{id}")]
        public int Delete(int id, int loginUserId)
        {
            var model = _context.SubCategory.Where(x => !x.IsDeleted && x.ID == id).FirstOrDefault();
            if (model != null)
            {
                model.IsDeleted = true;
                model.UpdatedDate = DateTime.Now;
                model.UpdatedBy = loginUserId;
                _context.SubCategory.Update(model);
                _context.SaveChanges();

                // Delete all product of this SubCategory
                var productList = _context.Product.Where(x => !x.IsDeleted && x.SubCategoryId == model.ID).ToList();
                foreach (var product in productList)
                {
                    product.IsDeleted = true;
                    product.UpdatedDate = DateTime.Now;
                    product.UpdatedBy = loginUserId;
                    _context.Product.Update(product);
                    _context.SaveChanges();
                }
                return id;
            }
            return 0;
        }

        #endregion

        #region Ajax Actions

        // GET api/<SubCategoryController>/GetSubCategoriesByCategory/5
        [HttpGet("GetSubCategoriesByCategory/{id}")]
        public List<SubCategoryModel> GetSubCategoriesByCategory(int id)
        {
            var subCategoryList = _context.SubCategory.Where(x => !x.IsDeleted && x.CategoryId == id).ToList();
            return subCategoryList;
        }

        #endregion

        #region Remote Validation Actions

        // GET api/<SubCategoryController>/VerifySubCategoryName
        [HttpGet("VerifySubCategoryName")]
        public bool VerifySubCategoryName(int id, int categoryId, string subCategoryName)
        {
            var verifySubCategory = _context.SubCategory.Where(x => !x.IsDeleted && x.CategoryId == categoryId && x.SubCategoryName == subCategoryName).FirstOrDefault();
            if (verifySubCategory != null)
            {
                var existingSubCategory = _context.SubCategory.Where(x => x.ID == id).FirstOrDefault();

                if (existingSubCategory?.SubCategoryName == verifySubCategory?.SubCategoryName)
                {
                    if (existingSubCategory?.CategoryId == verifySubCategory?.CategoryId)
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        #endregion
    }
}
