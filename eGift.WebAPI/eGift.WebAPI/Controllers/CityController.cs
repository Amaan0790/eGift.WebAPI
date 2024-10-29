using eGift.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eGift.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        #region Variables

        private readonly ApplicationDbContext _context;

        #endregion

        #region Constructors

        public CityController(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region City Default CRUD Actions

        // GET: api/<CityController>
        [HttpGet]
        public List<CityModel> Get()
        {
            // Set command timeout for this specific query (e.g., 5 minutes)
            _context.Database.SetCommandTimeout(TimeSpan.FromSeconds(300));

            var list = _context.City.Where(x => !x.IsDeleted).ToList();
            return list;
        }

        // GET api/<CityController>/5
        [HttpGet("{id}")]
        public CityModel Get(int id)
        {
            var model = _context.City.Find(id);
            return model;
        }

        // POST api/<CityController>
        [HttpPost]
        public CityModel Post([FromBody] CityModel model)
        {
            if (model.ID == 0)
            {
                _context.City.Add(model);
                _context.SaveChanges();
                return model;
            }
            return null;
        }

        // PUT api/<CityController>/5
        [HttpPut("{id}")]
        public CityModel Put(int id, [FromBody] CityModel model)
        {
            if (model.ID > 0)
            {
                model.UpdatedDate = DateTime.Now;
                _context.City.Update(model);
                _context.SaveChanges();
                return model;
            }
            return null;
        }

        // DELETE api/<CityController>/5
        [HttpDelete("{id}")]
        public int Delete(int id, int loginUserId)
        {
            var model = _context.City.Where(x => !x.IsDeleted && x.ID == id).FirstOrDefault();
            if (model != null)
            {
                model.IsDeleted = true;
                model.UpdatedDate = DateTime.Now;
                model.UpdatedBy = loginUserId;
                _context.City.Update(model);
                _context.SaveChanges();
                return id;
            }
            return 0;
        }

        #endregion

        #region Remote Validation Actions

        // For city name
        // GET api/<CityController>/VerifyCityName
        [HttpGet("VerifyCityName")]
        public bool VerifyCityName(int id, int stateId, string cityName)
        {
            var verifyCity = _context.City.Where(x => !x.IsDeleted && x.StateId == stateId && x.CityName == cityName).FirstOrDefault();
            if (verifyCity != null)
            {
                var existingCity = _context.City.Where(x => x.ID == id).FirstOrDefault();

                if (existingCity?.CityName == verifyCity?.CityName)
                {
                    if (existingCity?.StateId == verifyCity?.StateId)
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        // For city code
        // GET api/<CityController>/VerifyCityCode
        [HttpGet("VerifyCityCode")]
        public bool VerifyCityCode(int id, int stateId, string cityCode)
        {
            var verifyCity = _context.City.Where(x => !x.IsDeleted && x.StateId == stateId && x.CityCode == cityCode).FirstOrDefault();
            if (verifyCity != null)
            {
                var existingCity = _context.City.Where(x => x.ID == id).FirstOrDefault();

                if (existingCity?.CityCode == verifyCity?.CityCode)
                {
                    if (existingCity?.StateId == verifyCity?.StateId)
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
