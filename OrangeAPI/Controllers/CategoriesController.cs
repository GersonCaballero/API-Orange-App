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
    public class CategoriesController : ApiController
    {
        private OrangeAPIContext db = new OrangeAPIContext();

        [HttpGet]
        [Route("api/orange/categories")]
        public IQueryable<Category> GetCategories()
        {
            return db.Categories.Where(x => x.State == true);
        }

        [HttpGet]
        [Route("api/orange/categories")]
        [ResponseType(typeof(Category))]
        public IHttpActionResult GetCategory([FromUri]int id)
        {
            Category category = db.Categories.FirstOrDefault(x => x.IdCategory == id && x.State == true);

            if (category == null)
            {
                return BadRequest("Este usuario no existe.");
            }

            return Ok(category);
        }

        [HttpPut]
        [Route("api/orange/categories/update")]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateCategory([FromUri]int id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.IdCategory)
            {
                return BadRequest("Este usuario no existe.");
            }

            if(category.Name == "")
            {
                return BadRequest("Es necesario asignarle un nombre.");
            }

            db.Entry(category).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { message = "Categoria actualizada correctamente."});
        }

        [HttpPost]
        [Route("api/orange/categories/create")]
        [ResponseType(typeof(Category))]
        public IHttpActionResult CreateCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (category.Name == "")
            {
                return BadRequest("Es necesario asignarle un nombre.");
            }

            category.State = true;

            db.Categories.Add(category);
            db.SaveChanges();

            return Ok(new { message = "Categoria creada exitosamente."});
        }

        [HttpDelete]
        [Route("api/orange/categories/delete")]
        [ResponseType(typeof(Category))]
        public IHttpActionResult DeleteCategory([FromUri]int id)
        {
            Category category = db.Categories.Find(id);

            if (category == null)
            {
                return BadRequest("Este usuario no existe.");
            }

            var shops = db.Commerces.Where(s => s.IdCategory == id);

            if (shops != null)
            {
                return BadRequest("Esta categoria tiene comercios asociados.");
            }

            category.State = false;
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();

            return Ok(new { message = "Se elimino la categoria."});
        }

        private bool CategoryExists(int id)
        {
            return db.Categories.Count(e => e.IdCategory == id) > 0;
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