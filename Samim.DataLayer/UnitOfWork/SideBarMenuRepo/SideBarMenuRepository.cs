using Samim.DataLayer.Context;
using Samim.DomainLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Samim.DataLayer.UnitOfWork.SideBarMenuRepo
{
	public interface ISideBarMenuRepository : IBaseRepository<SideBarMenu>
	{

	}
	public class SideBarMenuRepository : BaseRepository<SideBarMenu>, ISideBarMenuRepository
	{
		public SideBarMenuRepository(SamimDbContext context) : base(context)
		{
		}
	}
}
