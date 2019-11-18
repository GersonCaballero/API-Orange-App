using OrangeAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Security;

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
            var result = db.UserAdmins.Where(x => x.State == true).ToList().Select(s => new {
                s.IdAdmin,
                s.Name,
                s.IdUserType,
                s.Phone,
                s.Email
            });

            return Ok(result);
        }

        [HttpGet]
        [Route("api/orange/adminUsers")]
        public IHttpActionResult GetAdminUserId([FromUri]int id)
        {
            var result = db.UserAdmins.Where(a => a.IdAdmin == id && a.State == true).Select(s => new {
                s.IdAdmin,
                s.Name,
                s.IdUserType,
                s.Phone,
                s.Email
            });

            if (result == null)
            {
                return BadRequest("No existe este usuario.");
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("api/orange/adminUser/create")]
        public IHttpActionResult CreateAdmin(UserAdmin userAdmin)
        {          

            var admin = db.UserAdmins.FirstOrDefault(x => x.Name == userAdmin.Name || x.Email == userAdmin.Email);

            if (admin != null)
            {
                return BadRequest("Ya existe un Administrador con este nombre o correo.");
            }

            if (userAdmin.Email == null || userAdmin.Name == null || userAdmin.Password == null || userAdmin.Phone == null || userAdmin.IdUserType.ToString() == null)
            {
                return BadRequest("Todos los campos deben estar llenos.");
            }

            userAdmin.State = true;

            db.UserAdmins.Add(userAdmin);
            db.SaveChanges();

            return Ok(new { message = "Usuario administrador creado exitosamente"});
        }
        
        [HttpPut]
        [Route("api/orange/adminUser/update")]
        public IHttpActionResult UpdateUserAdmin([FromUri]int id, EditAdministrator userAdmin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (id != userAdmin.IdAdmin)
            //{
            //    return BadRequest( "Este usuario no existe.");
            //}

            if (userAdmin.Email == "" || userAdmin.Name == ""  || userAdmin.Phone == "")
            {
                return BadRequest("Todos los campos deben estar llenos.");
            }

            var admin = db.UserAdmins.Find(id);
            admin.Name = userAdmin.Name;
            admin.Phone = userAdmin.Phone;
            admin.Email = userAdmin.Email;

            db.Entry(admin).State = EntityState.Modified;
            db.SaveChanges();

            return Ok(new { message = "Usuario actualizado correctamente"});
        }

        [HttpDelete]
        [Route("api/orange/adminUser/delete")]
        public IHttpActionResult DeleteAdminUser([FromUri]int id)
        {
            UserAdmin userAdmin = db.UserAdmins.FirstOrDefault(x => x.IdAdmin == id && x.State == true);

            if (userAdmin == null)
            {
                return BadRequest("Usuario no existe.");
            }

            userAdmin.State = false;

            db.Entry(userAdmin).State = EntityState.Modified;
            db.SaveChanges();

            return Ok(new { message = "Se elimino el usuario administrador."});
        }

        private bool AdminUserExists(int id)
        {
            return db.UserAdmins.Count(e => e.IdAdmin == id && e.State == true) > 0;
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
