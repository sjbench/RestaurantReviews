namespace RestaurantReviews.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity.Infrastructure;

    public class RRModel : DbContext
    {
        // Your context has been configured to use a 'RRModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'RestaurantReviews.Models.RRModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'RRModel' 
        // connection string in the application configuration file.
        public RRModel()
            : base("name=RRModel")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public  DbSet<Restaurant> Restaurants { get; set; }
        public  DbSet<Review> Reviews { get; set; }
        public  DbSet<User> Users { get; set; }
    }

    public partial class Restaurant
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Zip { get; set; }
        public string Phone { get; set; }

        public List<Review>Reviews { get; set; }
        
    }

    public partial class Review
    {
        public int Id { get; set; }
        [Required]
        public int RestaurantId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        [Range(1,5)]
        public int Rating { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime LastUpdated { get; set; }

        public Restaurant Restaurant { get; set; }
        public User User { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public List<Review>Reviews { get; set; }
    }
}