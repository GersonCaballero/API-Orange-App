using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using OrangeAPI.Models;

namespace OrangeAPI.Controllers
{
    [Authorize]
    [EnableCors("*", "*", "*")]
    public class TypeOfUsersController : ApiController
    {
        private OrangeAPIContext db = new OrangeAPIContext();

        [HttpGet]
        [Route("api/orange/typeUser")]
        public IQueryable<TypeOfUser> GetTypeOfUsers()
        {
            return db.TypeOfUsers;
        }

        [HttpGet]
        [Route("api/orange/typeUser")]
        [ResponseType(typeof(TypeOfUser))]
        public IHttpActionResult GetTypeOfUser([FromUri]int id)
        {
            TypeOfUser typeOfUser = db.TypeOfUsers.Find(id);
            if (typeOfUser == null)
            {
                return NotFound();
            }

            return Ok(typeOfUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TypeOfUserExists(int id)
        {
            return db.TypeOfUsers.Count(e => e.IdUserType == id) > 0;
        }
    }
}