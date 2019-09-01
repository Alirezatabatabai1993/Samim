namespace Samim.PresentationLayer
{
	public static class SamimRouteManager
	{
		public static class User
		{
			private static string Controller = "User";
            public static string Index()
            {
                return string.Format("/{0}/Index", Controller);
            }
            public static string Create()
			{
				return string.Format("/{0}/Create", Controller);
			}
			public static string Delete()
			{
				return string.Format("/{0}/Delete", Controller);
			}
			public static string Edit()
			{
				return string.Format("/{0}/Edit", Controller);
			}
			public static string EditPassword()
			{
				return string.Format("/{0}/EditPassword", Controller);
			}
		}
	}
}
