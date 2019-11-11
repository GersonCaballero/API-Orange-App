using OrangeAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace OrangeAPI.Controllers
{
    [Authorize]
    [EnableCors("*", "*", "*")]
    public class ShopsController : ApiController
    {
        private OrangeAPIContext db = new OrangeAPIContext();

        [HttpGet]
        [Route("api/orange/shops")]
        public IHttpActionResult GetShops()
        {
            var result = db.Commerces.Where(x => x.State == true).Include("Category").ToList().Select(s => new
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
        [Route("api/orange/shop")]
        public IHttpActionResult GetShopId([FromUri]int id)
        {
            var result = db.Commerces.Where(i => i.IdCommerce == id && i.State == true).Select(s => new {
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
        [Route("api/orange/shop/create")]
        public IHttpActionResult CreateShop(Commerce commerce)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            if (commerce.Name == "" || commerce.Phone == "" || commerce.RTN == "" || commerce.Password == "" || commerce.Email == "")
            {
                return BadRequest("Todos los campos deben estar llenos.");
            }

            commerce.State = true;

            db.Commerces.Add(commerce);
            db.SaveChanges();

            return Ok(new { message = "Comercio creado exitosamente", commerce.Name});
        }

        [HttpPut]
        [Route("api/orange/shop/update")]
        public IHttpActionResult UpdateShop([FromUri]int id, Commerce commerce)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(id != commerce.IdCommerce)
            {
                return BadRequest("Comercio no existe.");
            }

            if (commerce.Name == "" || commerce.Phone == "" || commerce.RTN == "" || commerce.Password == "" || commerce.Email == "")
            {
                return BadRequest("Todos los campos deben estar llenos.");
            }

            commerce.State = true;
            db.Entry(commerce).State = EntityState.Modified;

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
        [Route("api/orange/shop/delete")]
        public IHttpActionResult DeleteShops([FromUri]int id)
        {
            Commerce commerce = db.Commerces.FirstOrDefault(x => x.IdCommerce == id && x.State == true);

            if(commerce == null)
            {
                return BadRequest("Usuario no existe.");
            }

            var products = db.Products.Where(s => s.IdCommerce == id).ToList();

            if(products.Count > 0)
            {
                return BadRequest("Este comercio tiene productos asociados");
            }

            commerce.State = false;

            db.Entry(commerce).State = EntityState.Modified;
            db.SaveChanges();

            return Ok(new { message = "Se elimino el comercio", commerce.Name});
        }

        private bool CommerceExists(int id)
        {
            return db.Commerces.Count(e => e.IdCommerce == id && e.State == true) > 0;
        }
    }
}
