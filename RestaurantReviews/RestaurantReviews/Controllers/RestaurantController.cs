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
    public class RestaurantController : ApiController
    {
        private RRModel db = new RRModel();
        // GET api/<controller>
        public IEnumerable<Restaurant> GetRestaurants(string city)
        {
            var restaurants = db.Restaurants.Where(r => city.Equals(r.City, StringComparison.CurrentCultureIgnoreCase))
                                .ToList();
            return restaurants;
        }

        [ResponseType(typeof(Restaurant))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var rev = await db.Restaurants.FindAsync(id);

            //make sure it exists
            if (rev == null)
                return NotFound();

            return Ok(rev);
        }

        [ResponseType(typeof(Restaurant))]
        public async Task<IHttpActionResult> Post(Restaurant restaurant)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            db.Restaurants.Add(restaurant);
            await db.SaveChangesAsync();
            return CreatedAtRoute("DefaultApi", new { id = restaurant.Id }, restaurant);
        }

        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Put(int id, Restaurant restaurant)
        {
            if (restaurant == null || id != restaurant.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            db.Entry(restaurant).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (db.Restaurants.Count(e => e.Id == id) == 0)
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

       
    }
}