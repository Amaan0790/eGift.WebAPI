﻿using eGift.WebAPI.Models;
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
            return list;
        }

        // GET api/<AddressController>/5
        [HttpGet("{id}")]
        public AddressModel Get(int id)
        {
            var model = _context.Address.Find(id);
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
