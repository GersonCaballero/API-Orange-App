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
                s.TypeOfUser.Type,
                s.Phone,
                s.Email,
                s.Password
            });
            return Ok(result);
        }

        [HttpGet]
        [Route("api/orange/adminUsersId/{id}")]
        public IHttpActionResult GetAdminUserId(int id)
        {
            var result = db.UserAdmins.Where(a => a.IdAdmin == id).Select(s => new {
                s.IdAdmin,
                s.Name,
                s.TypeOfUser.Type,
                s.Phone,
                s.Email,
                s.Password
            });
            return Ok(result);
        }

        [HttpPost]
        [Route("api/orange/adminUser")]
        public IHttpActionResult CreateAdmin(UserAdmin userAdmin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            db.UserAdmins.Add(userAdmin);
            db.SaveChanges();

            return Ok(new { message = "Usuario administrador creado exitosamente"});
        }
        
        [HttpPut]
        [Route("api/orange/adminUser")]
        public IHttpActionResult UpdateUserAdmin(int id, UserAdmin userAdmin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userAdmin.IdAdmin)
            {
                return BadRequest();
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
            return Ok(new { message = "Usuario actualizado correctamente", userAdmin.Name});
        }

        [HttpDelete]
        [Route("api/orange/adminUser/{id}")]
        public IHttpActionResult DeleteAdminUser(int id)
        {
            UserAdmin userAdmin = db.UserAdmins.Find(id);

            if (userAdmin == null)
            {
                return NotFound();
            }

            db.UserAdmins.Remove(userAdmin);
            db.SaveChanges();

            return Ok(new { message = "Se elimino el comercio", userAdmin.Name });
        }

        private bool AdminUserExists(int id)
        {
            return db.UserAdmins.Count(e => e.IdAdmin == id) > 0;
        }

    }
}
