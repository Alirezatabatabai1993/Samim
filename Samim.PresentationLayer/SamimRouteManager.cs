namespace Samim.PresentationLayer
{
	public static class SamimRouteManager
	{
		public static class User
		{
			private static string Controller = "User";
			public static string Create()
			{
				return string.Format("/{0}/Create", Controller);
			}
		}
	}
}
