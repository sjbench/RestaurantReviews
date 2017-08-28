using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestaurantReviews.Models;
using System.Web.Http.Description;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace RestaurantReviews.Controllers
{
    public class ReviewController : ApiController
    {
        private RRModel db = new RRModel();
        // GET api/<controller>

        public IEnumerable<Review> GetByUser(int user)
        {
            var reviews = db.Reviews.Where(r => user.Equals(r.UserId))
                            .OrderByDescending(r => r.LastUpdated)
                            .ToList();
            return reviews;
        }

        [ResponseType(typeof(Review))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var rev = await db.Reviews.FindAsync(id);

            //make sure it exists
            if (rev == null)
                return NotFound();

            return Ok(rev);
        }

        [ResponseType(typeof(Review))]
        public async Task<IHttpActionResult> Post(Review rev)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            db.Reviews.Add(rev);
            await db.SaveChangesAsync();
            return CreatedAtRoute("DefaultApi", new { id = rev.Id }, rev);
        }

        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Put(int id, Review rev)
        {
            if (rev == null || id != rev.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            db.Entry(rev).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.Reviews.Count(e => e.Id == id) == 0)
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

        [ResponseType(typeof(Review))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            Review rev = await db.Reviews.FindAsync(id);

            //make sure it exists
            if (rev==null)
            {
                return NotFound();
            }

            //otherwise, continue with the delete
            db.Reviews.Remove(rev);
            await db.SaveChangesAsync();
            
            return Ok(rev);
        }
    }
}