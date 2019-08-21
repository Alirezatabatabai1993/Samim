namespace Samim.PresentationLayer
{
	public static class SamimRouteManager
	{
		public static class AdminRole
		{
			public static string Controller = "AdminRole";
			public static string Index()
			{
				return string.Format("/{0}/Index", Controller);
			}

			public static string Create()
			{
				return string.Format("/{0}/Create", Controller);
			}

			public static string Edit()
			{
				return string.Format("/{0}/Edit", Controller);
			}
		}
	}
}
