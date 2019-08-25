using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Samim.BusinessLayer.Services;
using Samim.DataLayer.AppUser;
using Samim.DomainLayer;

namespace Samim.DataLayer.Context
{
	public class SamimDbContext : IdentityDbContext<ApplicationUser>
	{
		private readonly IConnectionStringProvider _connectionStringProvider;
		public SamimDbContext(DbContextOptions<SamimDbContext> options, IConnectionStringProvider connectionStringProvider) : base(options)
		{
			_connectionStringProvider = connectionStringProvider;
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(_connectionStringProvider.GetConnectionString());
			}
		}

		public DbSet<SideBarMenu> SideBarMenu { get; set; }
		public DbSet<ApplicationUser> ApplicationUser { get; set; }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.Entity<ApplicationUser>().ToTable("ApplicationUser");
			builder.Entity<IdentityRole>().ToTable("Role");
			builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim");
			builder.Entity<IdentityUserRole<string>>().ToTable("UserRole");
			builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");
			builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim");
			builder.Entity<IdentityUserToken<string>>().ToTable("UserToken");

		}
	}
}
