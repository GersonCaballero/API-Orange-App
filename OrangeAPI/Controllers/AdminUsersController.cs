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
    public class AdminUsersController : ApiController
    {
        private OrangeAPIContext db = new OrangeAPIContext();

        [HttpGet]
        [Route("api/orange/adminUsers")]
        public IHttpActionResult GetAdminUsers()
        {
            var result = db.UserAdmins.ToList().Select(s => new {
                s.IdAdmin,
                s.Name,
                s.IdUserType,
                s.Phone,
                s.Email,
                s.Password
            });
            return Ok(result);
        }

        [HttpGet]
        [Route("api/orange/adminUsers")]
        public IHttpActionResult GetAdminUserId([FromUri]int id)
        {
            var result = db.UserAdmins.Where(a => a.IdAdmin == id).Select(s => new {
                s.IdAdmin,
                s.Name,
                s.IdUserType,
                s.Phone,
                s.Email,
                s.Password
            });

            if (result == null)
            {
                return Ok(new { message = "No existe este usuario."});
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("api/orange/adminUser/create")]
        public IHttpActionResult CreateAdmin(UserAdmin userAdmin)
        {           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            var admin = db.UserAdmins.FirstOrDefault(x => x.Name == userAdmin.Name || x.Email == userAdmin.Email);

            if (admin != null)
            {
                return BadRequest("Ya existe un Administrador con este nombre o correo.");
            }

            if (userAdmin.Email == "" || userAdmin.Name == "" || userAdmin.Password == "" || userAdmin.Phone == "" || userAdmin.IdUserType.ToString() == "")
            {
                return BadRequest("Todos los campos deben estar llenos.");
            }

            db.UserAdmins.Add(userAdmin);
            db.SaveChanges();

            return Ok(new { message = "Usuario administrador creado exitosamente"});
        }
        
        [HttpPut]
        [Route("api/orange/adminUser/update")]
        public IHttpActionResult UpdateUserAdmin([FromUri]int id, UserAdmin userAdmin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userAdmin.IdAdmin)
            {
                return BadRequest( "Este usuario no existe.");
            }

            if (userAdmin.Email == "" || userAdmin.Name == "" || userAdmin.Password == "" || userAdmin.Phone == "" || userAdmin.IdUserType.ToString() == "")
            {
                return BadRequest("Todos los campos deben estar llenos.");
            }

            db.Entry(userAdmin).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(new { message = "Usuario actualizado correctamente"});
        }

        [HttpDelete]
        [Route("api/orange/adminUser/delete")]
        public IHttpActionResult DeleteAdminUser([FromUri]int id)
        {
            UserAdmin userAdmin = db.UserAdmins.Find(id);

            if (userAdmin == null)
            {
                return NotFound();
            }

            db.UserAdmins.Remove(userAdmin);
            db.SaveChanges();

            return Ok(new { message = "Se elimino el usuario administrador."});
        }

        private bool AdminUserExists(int id)
        {
            return db.UserAdmins.Count(e => e.IdAdmin == id) > 0;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
