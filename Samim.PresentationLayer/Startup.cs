﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Samim.BusinessLayer.Services;
using Samim.DataLayer.AppUser;
using Samim.DataLayer.Context;
using Samim.DataLayer.UnitOfWork;
using Samim.DataLayer.UnitOfWork.SideBarMenuRepo;
using Samim.DataLayer.UnitOfWork.UserRepo;
using Samim.PresentationLayer.Models;
using Samim.ViewModel;

namespace Samim.PresentationLayer
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddEntityFrameworkSqlServer()
				.AddDbContextPool<SamimDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DBConnection")))
				.AddTransient<SamimDbContext>();

			services.AddSingleton<IConnectionStringProvider, ConnectionStringProvider>();

			services.AddIdentity<ApplicationUser, IdentityRole>(options =>
			{
				options.Stores.MaxLengthForKeys = 128;
				options.Password.RequireUppercase = false;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireLowercase = false;
				options.Password.RequiredLength = 3;
				options.Password.RequireDigit = false;
				options.Password.RequiredUniqueChars = 0;
			})
			   .AddEntityFrameworkStores<SamimDbContext>().AddDefaultUI()
			   .AddDefaultTokenProviders();

			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
			services.AddTransient<ISideBarMenuService, SideBarMenuService>();
			services.AddTransient<ISideBarMenuRepository, SideBarMenuRepository>();
			services.AddTransient<IUserRepository, UserRepository>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			//app.UseCookiePolicy();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
