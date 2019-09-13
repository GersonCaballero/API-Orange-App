using OrangeAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OrangeAPI.Controllers
{
    public class ShopsController : ApiController
    {
        private OrangeAPIContext db = new OrangeAPIContext();

        [HttpGet]
        [Route("api/orange/shops")]
        public IHttpActionResult GetShops()
        {
            var result = db.Commerces.Include("Category").ToList().Select(s => new
            {
                s.IdCommerce,
                s.Name,
                s.RTN,
                s.Image,
                s.Email,
                s.Password,
                s.Phone,
                s.Category.IdCategory
            });
            return Ok(result);
        }

        [HttpGet]
        [Route("api/orange/shops/{id}")]
        public IHttpActionResult GetShopId(int id)
        {
            var result = db.Commerces.Where(i => i.IdCommerce == id).Select(s => new {
                s.IdCommerce,
                s.Name,
                s.RTN,
                s.Image,
                s.Email,
                s.Password,
                s.Phone,
                s.Category.IdCategory
            });
            return Ok(result);
        }

        [HttpPost]
        [Route("api/orange/shop")]
        public IHttpActionResult CreateShop(Commerce commerce)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            db.Commerces.Add(commerce);
            db.SaveChanges();

            return Ok(new { message = "Comercio creado exitosamente", commerce.Name});
        }

        [HttpPut]
        [Route("api/orange/shops/{id}")]
        public IHttpActionResult UpdateShop(int id, Commerce commerce)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(id != commerce.IdCommerce)
            {
                return BadRequest();
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommerceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(new { message = "Comercio actualizado correctamente", commerce.Name});
        }

        [HttpDelete]
        [Route("api/orange/shop")]
        public IHttpActionResult DeleteShops(int id)
        {
            Commerce commerce = db.Commerces.Find(id);

            if(commerce == null)
            {
                return NotFound();
            }

            db.Commerces.Remove(commerce);
            db.SaveChanges();

            return Ok(new { message = "Se elimino el comercio", commerce.Name});
        }

        private bool CommerceExists(int id)
        {
            return db.Commerces.Count(e => e.IdCommerce == id) > 0;
        }
    }
}
