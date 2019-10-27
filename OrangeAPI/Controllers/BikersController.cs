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
using System.Web.Http.Description;

namespace OrangeAPI.Controllers
{
    [Authorize]
    [EnableCors("*", "*", "*")]
    public class BikersController : ApiController
    {
        private OrangeAPIContext db = new OrangeAPIContext();

        [HttpPost]
        [Route("api/orange/biker/create")]
        public IHttpActionResult CreateBiker(Biker biker)
        {
            var bike = db.Bikers.FirstOrDefault(x => x.Name == biker.Name && x.State == true || x.Email == biker.Email && x.State == true);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (bike != null)
            {
                return BadRequest("Ya exite un motorista con este nombre o correo.");
            }

            if (bike.Name == "" || bike.Email == "" || bike.Telephone == "" || bike.Password == "" || bike.Age == "")
                return BadRequest("Todos los campos deben de estar llenos.");

            biker.State = true;

            db.Bikers.Add(biker);
            db.SaveChanges();

            return Ok(new { message = "Motorista creado exitosamente." });
        }

        [HttpPut]
        [Route("api/orange/biker/update")]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateBiker([FromUri]int Bikerid, Biker biker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if(Bikerid != biker.IdBiker)
                return BadRequest("Motorista no existe.");

            db.Entry(biker).State = EntityState.Modified;

            db.SaveChanges();
            
            return Ok(new { message = "El motorista se actualizo correctamente."});
        }

        [HttpGet]
        [Route("api/orange/biker")]
        public IQueryable<Biker> Bikers()
        {
            return db.Bikers.Where(x => x.State == true);
        }

        [HttpGet]
        [Route("api/orange/biker")]
        public IHttpActionResult BikerId([FromUri]int MotoristaId)
        {
            var biker = db.Bikers.FirstOrDefault(x => x.IdBiker == MotoristaId && x.State == true);

            if(biker == null)
                return BadRequest("El motorista no existe.");

            return Ok(biker);
        }

        [HttpDelete]
        [Route("api/orange/biker/delete")]
        public IHttpActionResult BikerDelete([FromUri]int MotoristaId)
        {
            var biker = db.Bikers.FirstOrDefault(x => x.IdBiker == MotoristaId && x.State == true);

            if (biker == null)
            {
                return BadRequest("El motorista no existe.");
            }

            biker.State = false;

            db.Entry(biker).State = EntityState.Modified;
            db.SaveChanges();

            return Ok(new { message ="Motorista eliminado con exito."});
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
