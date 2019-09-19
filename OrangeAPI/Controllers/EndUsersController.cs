using OrangeAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OrangeAPI.Controllers
{
    public class EndUsersController : ApiController
    {
        private OrangeAPIContext db = new OrangeAPIContext();

        [HttpGet]
        [Route("api/orange/endUsers")]
        public IHttpActionResult GetEndUser()
        {
            var result = db.EndUsers.ToList();
            return Ok(result);
        }

        [HttpGet]
        [Route("api/orange/endUser/{id}")]
        public IHttpActionResult GetEndUserId(int id)
        {
            var result = db.EndUsers.Find();

            if(result == null)
            {
                return BadRequest("No existe el usuario");
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("api/orange/endUser")]
        public IHttpActionResult CreateEndUser( EndUser endUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            db.EndUsers.Add(endUser);
            db.SaveChanges();

            return Ok(new { message = "Usuario creado exitosamente", endUser.Name });
        }

        [HttpPost]
        [Route("api/orange/endUser/{id}")]
        public IHttpActionResult UpdateEndUser(int id, EndUser endUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != endUser.IdEndUser)
            {
                return BadRequest();
            }

            db.Entry(endUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EndUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(new { message = "Usuario actualizado correctamente", endUser.Name });
        }

        [HttpDelete]
        [Route("api/orange/endUser")]
        public IHttpActionResult DeleteShops(int id)
        {
            EndUser endUser = db.EndUsers.Find(id);

            if (endUser == null)
            {
                return NotFound();
            }

            db.EndUsers.Remove(endUser);
            db.SaveChanges();

            return Ok(new { message = "Se elimino el comercio", endUser.Name });
        }

        private bool EndUserExists(int id)
        {
            return db.Commerces.Count(e => e.IdCommerce == id) > 0;
        }
    }
}
