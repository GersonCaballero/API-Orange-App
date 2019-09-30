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
        [Route("api/orange/endUser")]
        public IHttpActionResult GetEndUserId([FromUri]int id)
        {
            var result = db.EndUsers.Find();

            if (result == null)
            {
                return BadRequest("Usuario no existe.");
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("api/orange/endUser/create")]
        public IHttpActionResult CreateEndUser(EndUser endUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            if (endUser.Name == "" || endUser.Address == "" || endUser.Email == "" || endUser.Phone == "" || endUser.Password == "" || endUser.typeOfUser.ToString() == "")
            {
                return BadRequest( "No pueden haber campos vacios.");
            }

            db.EndUsers.Add(endUser);
            db.SaveChanges();

            return Ok(new { message = "Usuario creado exitosamente."});
        }

        [HttpPost]
        [Route("api/orange/endUser/update")]
        public IHttpActionResult UpdateEndUser([FromUri]int id, EndUser endUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != endUser.IdEndUser)
            {
                return BadRequest("Usuario no existe.");
            }

            if (endUser.Name == "" || endUser.Address == "" || endUser.Email == "" || endUser.Phone == "" || endUser.Password == "" || endUser.typeOfUser.ToString() == "")
            {
                return BadRequest("No pueden haber campos vacios.");
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
                    return BadRequest("Usuario no existe.");
                }
                else
                {
                    throw;
                }
            }
            return Ok(new { message = "Usuario actualizado correctamente."});
        }

        [HttpDelete]
        [Route("api/orange/endUser/delete")]
        public IHttpActionResult DeleteShops([FromUri]int id)
        {
            EndUser endUser = db.EndUsers.Find(id);

            if (endUser == null)
            {
                return BadRequest("Usuario no existe.");
            }

            db.EndUsers.Remove(endUser);
            db.SaveChanges();

            return Ok(new { message = "Se elimino el usuario."});
        }

        private bool EndUserExists(int id)
        {
            return db.Commerces.Count(e => e.IdCommerce == id) > 0;
        }
    }
}
