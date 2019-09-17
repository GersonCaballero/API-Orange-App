using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using OrangeAPI.Models;

namespace OrangeAPI.Controllers
{
    public class CategoriesController : ApiController
    {
        private OrangeAPIContext db = new OrangeAPIContext();

        [HttpGet]
        [Route("api/orange/categories")]
        public IQueryable<Category> GetCategories()
        {
            return db.Categories;
        }

        [HttpGet]
        [Route("api/orange/categories/{id}")]
        [ResponseType(typeof(Category))]
        public IHttpActionResult GetCategory(int id)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPut]
        [Route("api/orange/categories/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateCategory(int id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.IdCategory)
            {
                return BadRequest();
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

            return Ok(new { message = "Categoria actualizada correctamente", category.Name });
        }

        [HttpPost]
        [Route("api/orange/categories")]
        [ResponseType(typeof(Category))]
        public IHttpActionResult CreateCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categories.Add(category);
            db.SaveChanges();

            return Ok(new { message = "Categoria creada exitosamente", category.Name });
        }

        [HttpDelete]
        [Route("api/orange/categories/{id}")]
        [ResponseType(typeof(Category))]
        public IHttpActionResult DeleteCategory(int id)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            var shops = db.Commerces.Where(s => s.IdCategory == id);

            if (shops != null)
            {
                return BadRequest("Esta categoria tiene comercios asociados");
            }

            db.Categories.Remove(category);
            db.SaveChanges();

            return Ok(new { message = "Se elimino la categoria", category.Name });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryExists(int id)
        {
            return db.Categories.Count(e => e.IdCategory == id) > 0;
        }
    }
}