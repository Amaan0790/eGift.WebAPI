using eGift.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eGift.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        #region Variables

        private readonly ApplicationDbContext _context;

        #endregion

        #region Constructors

        public StateController(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region State Default CRUD Actions

        // GET: api/<StateController>
        [HttpGet]
        public List<StateModel> Get()
        {
            // Set command timeout for this specific query (e.g., 5 minutes)
            _context.Database.SetCommandTimeout(TimeSpan.FromSeconds(300));

            var list = _context.State.Where(x => !x.IsDeleted).ToList();
            return list;
        }

        // GET api/<StateController>/5
        [HttpGet("{id}")]
        public StateModel Get(int id)
        {
            var model = _context.State.Find(id);
            return model;
        }

        // POST api/<StateController>
        [HttpPost]
        public StateModel Post([FromBody] StateModel model)
        {
            if (model.ID == 0)
            {
                _context.State.Add(model);
                _context.SaveChanges();
                return model;
            }
            return null;
        }

        // PUT api/<StateController>/5
        [HttpPut("{id}")]
        public StateModel Put(int id, [FromBody] StateModel model)
        {
            if (model.ID > 0)
            {
                model.UpdatedDate = DateTime.Now;
                _context.State.Update(model);
                _context.SaveChanges();
                return model;
            }
            return null;
        }

        // DELETE api/<StateController>/5
        [HttpDelete("{id}")]
        public int Delete(int id, int loginUserId)
        {
            var model = _context.State.Where(x => !x.IsDeleted && x.ID == id).FirstOrDefault();
            if (model != null)
            {
                model.IsDeleted = true;
                model.UpdatedDate = DateTime.Now;
                model.UpdatedBy = loginUserId;
                _context.State.Update(model);
                _context.SaveChanges();
                return id;
            }
            return 0;
        }

        #endregion

        #region Remote Validation Actions

        // For state name
        // GET api/<StateController>/VerifyStateName
        [HttpGet("VerifyStateName")]
        public bool VerifyStateName(int id, int countryId, string stateName)
        {
            var verifyState = _context.State.Where(x => !x.IsDeleted && x.CountryId == countryId && x.StateName == stateName).FirstOrDefault();
            if (verifyState != null)
            {
                var existingState = _context.State.Where(x => x.ID == id).FirstOrDefault();

                if (existingState?.StateName == verifyState?.StateName)
                {
                    if (existingState?.CountryId == verifyState?.CountryId)
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        // For state code
        // GET api/<StateController>/VerifyStateCode
        [HttpGet("VerifyStateCode")]
        public bool VerifyStateCode(int id, int countryId, string stateCode)
        {
            var verifyState = _context.State.Where(x => !x.IsDeleted && x.CountryId == countryId && x.StateCode == stateCode).FirstOrDefault();
            if (verifyState != null)
            {
                var existingState = _context.State.Where(x => x.ID == id).FirstOrDefault();

                if (existingState?.StateCode == verifyState?.StateCode)
                {
                    if (existingState?.CountryId == verifyState?.CountryId)
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
