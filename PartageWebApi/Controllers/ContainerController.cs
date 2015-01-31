using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using PartageContext.Models;
using PartageWebApi.Models;

namespace PartageWebApi.Controllers
{
    public class ContainerController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Container
        public IQueryable<Container> GetContainer()
        {
            return db.Container;
        }

        // GET: api/Container/5
        [ResponseType(typeof(Container))]
        public async Task<IHttpActionResult> GetContainer(int id)
        {
            Container container = await db.Container.FindAsync(id);
            if (container == null)
            {
                return NotFound();
            }

            return Ok(container);
        }

        // PUT: api/Container/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutContainer(int id, Container container)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != container.Id)
            {
                return BadRequest();
            }

            db.Entry(container).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContainerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Container
        [ResponseType(typeof(Container))]
        public async Task<IHttpActionResult> PostContainer(Container container)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Container.Add(container);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = container.Id }, container);
        }

        // DELETE: api/Container/5
        [ResponseType(typeof(Container))]
        public async Task<IHttpActionResult> DeleteContainer(int id)
        {
            Container container = await db.Container.FindAsync(id);
            if (container == null)
            {
                return NotFound();
            }

            db.Container.Remove(container);
            await db.SaveChangesAsync();

            return Ok(container);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContainerExists(int id)
        {
            return db.Container.Count(e => e.Id == id) > 0;
        }
    }
}