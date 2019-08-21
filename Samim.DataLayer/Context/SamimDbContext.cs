using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Samim.DataLayer.Context
{
	public class SamimDbContext : IdentityDbContext<IdentityUser>
	{
		public SamimDbContext(DbContextOptions<SamimDbContext> options) : base(options)
		{

		}
	}
}
