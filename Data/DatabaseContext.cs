using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieApi.Models;

namespace MovieApi.Data
{
	public class DatabaseContext : IdentityDbContext<CustomUser>
    {
		public DatabaseContext(DbContextOptions<DatabaseContext> options)
			: base(options) { }

		public DbSet<Movie> Movies { get; set; }

        public DbSet<Review> Reviews { get; set; }
    }
}

