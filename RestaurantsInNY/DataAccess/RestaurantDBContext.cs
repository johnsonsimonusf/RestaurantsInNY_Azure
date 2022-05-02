using Microsoft.EntityFrameworkCore;
using RestaurantsInNY.Models;

namespace RestaurantsInNY.DataAccess
{
    public class RestaurantDBContext : DbContext
    {
        public RestaurantDBContext()
        {
        }

        public RestaurantDBContext(DbContextOptions<RestaurantDBContext> options) : base(options) { }
        public DbSet<Restaurant> Restaurant { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Nutrition> Nutrition { get; set; }

    }
}
