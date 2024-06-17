namespace NDao.Examples.HelloWorld.PageModels;

public class Paths
{
	public class Pages
	{
		public static readonly string Article	= $"/{nameof(Article)}";

		public static readonly string Blog		= $"/{nameof(Blog)}";

		public static readonly string Error		= $"/{nameof(Error)}";

		public static readonly string Index		= $"/{nameof(Index)}";

		public static readonly string Signin	= $"/{nameof(Signin)}";

		public static readonly string Signup	= $"/{nameof(Signup)}";

		public class Settings
		{
			public static readonly string Account		= $"/{nameof(Settings)}/{nameof(Account)}";

			public static readonly string Article		= $"/{nameof(Settings)}/{nameof(Article)}";

			public static readonly string ArticleList	= $"/{nameof(Settings)}/{nameof(ArticleList)}";

			public static readonly string Blog			= $"/{nameof(Settings)}/{nameof(Blog)}";

			public static readonly string BlogList		= $"/{nameof(Settings)}/{nameof(BlogList)}";

			public static readonly string Home			= $"/{nameof(Settings)}/{nameof(Home)}";
		}
	}
}
