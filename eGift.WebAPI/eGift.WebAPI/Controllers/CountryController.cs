using eGift.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eGift.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        #region Variables

        private readonly ApplicationDbContext _context;

        #endregion

        #region Constructors

        public CountryController(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Country Default CRUD Actions

        // GET: api/<CountryController>
        [HttpGet]
        public List<CountryModel> Get()
        {
            // Set command timeout for this specific query (e.g., 5 minutes)
            _context.Database.SetCommandTimeout(TimeSpan.FromSeconds(300));

            var list = _context.Country.Where(x => !x.IsDeleted).ToList();
            return list;
        }

        // GET api/<CountryController>/5
        [HttpGet("{id}")]
        public CountryModel Get(int id)
        {
            var model = _context.Country.Find(id);
            return model;
        }

        // POST api/<CountryController>
        [HttpPost]
        public CountryModel Post([FromBody] CountryModel model)
        {
            if (model.ID == 0)
            {
                _context.Country.Add(model);
                _context.SaveChanges();
                return model;
            }
            return null;
        }

        // PUT api/<CountryController>/5
        [HttpPut("{id}")]
        public CountryModel Put(int id, [FromBody] CountryModel model)
        {
            if (model.ID > 0)
            {
                model.UpdatedDate = DateTime.Now;
                _context.Country.Update(model);
                _context.SaveChanges();
                return model;
            }
            return null;
        }

        // DELETE api/<CountryController>/5
        [HttpDelete("{id}")]
        public int Delete(int id, int loginUserId)
        {
            var model = _context.Country.Where(x => !x.IsDeleted && x.ID == id).FirstOrDefault();
            if (model != null)
            {
                model.IsDeleted = true;
                model.UpdatedDate = DateTime.Now;
                model.UpdatedBy = loginUserId;
                _context.Country.Update(model);
                _context.SaveChanges();

                return id;
            }
            return 0;
        }

        #endregion

        #region Remote Validation Actions

        // GET api/<CountryController>/VerifyCountryName
        [HttpGet("VerifyCountryName")]
        public bool VerifyCountryName(int id, string countryName)
        {
            var verifyCountry = _context.Country.Where(x => !x.IsDeleted && x.CountryName == countryName).FirstOrDefault();
            if (verifyCountry != null)
            {
                var existingCountry = _context.Country.Where(x => x.ID == id).FirstOrDefault();
                if (existingCountry?.CountryName == verifyCountry?.CountryName)
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

        // GET api/<CountryController>/VerifyCountryCode
        [HttpGet("VerifyCountryCode")]
        public bool VerifyCountryCode(int id, string countryCode)
        {
            var verifyCountry = _context.Country.Where(x => !x.IsDeleted && x.CountryCode == countryCode).FirstOrDefault();
            if (verifyCountry != null)
            {
                var existingCountry = _context.Country.Where(x => x.ID == id).FirstOrDefault();
                if (existingCountry?.CountryCode == verifyCountry?.CountryCode)
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
