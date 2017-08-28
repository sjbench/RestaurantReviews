using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantReviews.Models
{
    public partial class Restaurant
    {
        public double Rating
        {
            get
            {
               if (this.Reviews != null)
                {
                    var ratings = (from r in this.Reviews
                                   select r.Rating).ToList();
                    return ratings.Average();
                }

                return 0.0;
            }
        }
    }
}