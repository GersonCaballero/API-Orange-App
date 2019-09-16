using OrangeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OrangeAPI.Controllers
{
    public class ProductsController : ApiController
    {
        private OrangeAPIContext db = new OrangeAPIContext();

        [HttpPost]
        [Route("api/orange/menu/create")]
        public IHttpActionResult CreateMenu(Product product)
        {
            var prod = db.Products.FirstOrDefault(p => p.Name == product.Name);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (product.Description == "") return Ok(new { message = "Debe de ingresar una descripción." });

            if (product.Name == "") return Ok(new { message = "Debe de ingresar un nombre." });

            if (prod != null)
                return Ok(new { message = "Este producto ya existe" });

            db.Products.Add(product);
            db.SaveChanges();

            return Ok(new { message = "Menú creado con exito." });
        }

        [HttpPut]
        [Route("api/orange/menu/update")]
        public IHttpActionResult UpdateMenu([FromUri]int Productid, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (Productid != product.IdProduct)
                return Ok(new { message = "Producto no existe." });

            db.Entry(product).State = EntityState.Modified;

            db.SaveChanges();

            return Ok(new { message = "Menú editado correctamente." });
        }

        [HttpGet]
        [Route("api/orange/product/product")]
        public IQueryable<Product> Product()
        {
            return db.Products;
        }


        [HttpGet]
        [Route("api/orange/product/product")]
        public IHttpActionResult ProductId([FromUri]int productId)
        {
            var product = db.Products.Find(productId);

            if (product == null)
                return Ok(new { message = "Producto no exite." });

            return Ok(product);
        }

        [HttpDelete]
        [Route("api/orange/product/delete")]
        public IHttpActionResult DeleteProduct([FromUri]int productId)
        {
            var product = db.Products.Find(productId);

            if (product == null)
                return Ok(new { message = "Producto no exite." });

            db.Products.Remove(product);
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
}
