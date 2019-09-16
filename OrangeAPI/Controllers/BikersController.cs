﻿using OrangeAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace OrangeAPI.Controllers
{
    public class BikersController : ApiController
    {
        private OrangeAPIContext db = new OrangeAPIContext();

        [HttpPost]
        [Route("api/orange/biker/breate")]
        public IHttpActionResult CreateBiker(Biker biker)
        {
            var bike = db.Bikers.FirstOrDefault(x => x.Name == biker.Name);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (bike != null)
            {
                return Ok(new { message = "Ya exite un motorista con este nombre."});
            }

            if (bike.Name == "" || bike.Email == "" || bike.Telephone == "" || bike.Password == "" || bike.Age == "")
                return Ok(new { message = "Todos los campos deben de estar llenos." });

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
                return Ok(new { message = "Motorista no exite." });

            db.Entry(biker).State = EntityState.Modified;

            db.SaveChanges();
            
            return Ok(new { message = "El motorista se actualizo correctamente."});
        }

        [HttpGet]
        [Route("api/orange/biker/biker")]
        public IQueryable<Biker> Bikers()
        {
            return db.Bikers;
        }

        [HttpGet]
        [Route("api/orange/biker/biker")]
        public IHttpActionResult BikerId([FromUri]int MotoristaId)
        {
            var biker = db.Bikers.Find(MotoristaId);

            if(biker == null)
                return Ok(new { Message = "El motorista no existe."});

            return Ok(biker);
        }

        [HttpDelete]
        [Route("api/orange/biker/delete")]
        public IHttpActionResult BikerDelete([FromUri]int MotoristaId)
        {
            var biker = db.Bikers.Find(MotoristaId);

            if (biker == null)
            {
                return Ok(new { Message = "El motorista no existe." });
            }

            db.Bikers.Remove(biker);
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