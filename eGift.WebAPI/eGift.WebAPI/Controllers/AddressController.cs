using eGift.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eGift.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        #region Variables

        private readonly ApplicationDbContext _context;

        #endregion

        #region Constructors

        public AddressController(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Address Default CRUD Actions

        // GET: api/<AddressController>
        [HttpGet]
        public List<AddressModel> Get()
        {
            // Set command timeout for this specific query (e.g., 5 minutes)
            _context.Database.SetCommandTimeout(TimeSpan.FromSeconds(300));

            var list = _context.Address.Where(x => !x.IsDeleted).ToList();
            var cities = _context.City.Where(x => !x.IsDeleted).ToList();
            var states = _context.State.Where(x => !x.IsDeleted).ToList();
            var counties = _context.Country.Where(x => !x.IsDeleted).ToList();

            //// Sinle join
            //list = list.Join(cities, a => a.CityId, c => c.ID, (a, c) => new { a, c })
            //        .Select(m =>
            //        {
            //            m.a.CityName = m.c.CityName;
            //            return m.a;
            //        }).ToList();

            //// Double join
            //list = list.Join(cities, a => a.CityId, c => c.ID, (a, c) => new { a, c })
            //        .Join(states, ac => ac.a.StateId, s => s.ID, (ac, s) => new { ac, s })
            //        .Select(m =>
            //        {
            //            m.ac.a.CityName = m.ac.c.CityName;
            //            m.ac.a.StateName = m.s.StateName;
            //            return m.ac.a;
            //        }).ToList();

            // Multiple join
            list = list.Join(cities, a => a.CityId, c => c.ID, (a, c) => new { a, c })
                    .Join(states, ac => ac.a.StateId, s => s.ID, (ac, s) => new { ac, s })
                    .Join(counties, acs => acs.ac.a.CountryId, co => co.ID, (acs, co) => new { acs, co })
                    .Select(m =>
                    {
                        m.acs.ac.a.CityName = m.acs.ac.c.CityName;
                        m.acs.ac.a.StateName = m.acs.s.StateName;
                        m.acs.ac.a.CountryName = m.co.CountryName;
                        return m.acs.ac.a;
                    }).ToList();

            //list.ForEach(x => { x.FullAddress = $"{x.Street1}, {x.CityName}, {x.StateName}, {x.CountryName} - {x.Pincode}."; });
            return list;
        }

        // GET api/<AddressController>/5
        [HttpGet("{id}")]
        public AddressModel Get(int id)
        {
            var model = _context.Address.Find(id);
            if (model != null)
            {
                model.CityName = _context.City.Find(model.CityId)?.CityName;
                model.StateName = _context.State.Find(model.StateId)?.StateName;
                model.CountryName = _context.Country.Find(model.CountryId)?.CountryName;
                
                // For Full Address
                //model.FullAddress = $"{model.Street1}, {model.CityName}, {model.StateName}, {model.CountryName} - {model.Pincode}.";
            }
            return model;
        }

        // POST api/<AddressController>
        [HttpPost]
        public AddressModel Post([FromBody] AddressModel model)
        {
            if (model.ID == 0)
            {
                _context.Address.Add(model);
                _context.SaveChanges();
                return model;
            }
            return null;
        }

        // PUT api/<AddressController>/5
        [HttpPut("{id}")]
        public AddressModel Put(int id, [FromBody] AddressModel model)
        {
            if (model.ID > 0)
            {
                model.UpdatedDate = DateTime.Now;
                _context.Address.Update(model);
                _context.SaveChanges();
                return model;
            }
            return null;
        }

        // DELETE api/<AddressController>/5
        [HttpDelete("{id}")]
        public int Delete(int id, int loginUserId)
        {
            var model = _context.Address.Where(x => !x.IsDeleted && x.ID == id).FirstOrDefault();
            if (model != null)
            {
                model.IsDeleted = true;
                model.UpdatedDate = DateTime.Now;
                model.UpdatedBy = loginUserId;
                _context.Address.Update(model);
                _context.SaveChanges();
                return id;
            }
            return 0;
        }

        #endregion
    }
}
