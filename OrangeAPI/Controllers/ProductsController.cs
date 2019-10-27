﻿using OrangeAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace OrangeAPI.Controllers
{
    [Authorize]
    [EnableCors("*", "*", "*")]
    public class ProductsController : ApiController
    {
        private OrangeAPIContext db = new OrangeAPIContext();

        [HttpPost]
        [Route("api/orange/product/create")]
        public IHttpActionResult CreateProduct(Product product)
        {
            var prod = db.Products.FirstOrDefault(p => p.Name == product.Name);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (product.Description == "") return BadRequest("Debe de ingresar una descripción.");

            if (product.Name == "") return BadRequest("Debe de ingresar un nombre.");

            if (prod != null) return BadRequest("Este producto ya existe");

            product.State = true;

            db.Products.Add(product);
            db.SaveChanges();

            return Ok(new { message = "Producto creado con exito." });
        }

        [HttpPut]
        [Route("api/orange/product/update")]
        public IHttpActionResult UpdateProduct([FromUri]int Productid, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (Productid != product.IdProduct) return BadRequest("Producto no existe.");

            db.Entry(product).State = EntityState.Modified;

            db.SaveChanges();

            return Ok(new { message = "Producto editado correctamente." });
        }

        [HttpGet]
        [Route("api/orange/product")]
        public IEnumerable<Object> Product()
        {
            var result = db.Products.Where(x => x.State == true)
                .Include("Commerce")
                .Select(s => new
                {
                    s.IdProduct,
                    s.Name,
                    s.Description,
                    s.Image,
                    s.Precio,
                    s.Quantity,
                    Commerce = s.Commerce
                }).ToList();


            return result.ToList();

            //return db.Products;
        }


        [HttpGet]
        [Route("api/orange/product")]
        public IHttpActionResult ProductId([FromUri]int productId)
        {
            var product = db.Products.FirstOrDefault(x => x.IdProduct == productId && x.State == true);

            if (product == null)
                return BadRequest("Producto no exite.");

            return Ok(product);
        }

        [HttpDelete]
        [Route("api/orange/product/delete")]
        public IHttpActionResult DeleteProduct([FromUri]int productId)
        {
            var product = db.Products.Find(productId);

            if (product == null) return BadRequest("Producto no existe.");

            product.State = false;

            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();

            return Ok(new { message = "Producto eliminado exitosamente." });
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
