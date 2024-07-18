namespace NDao.Examples.HelloWorld.PageModels;

public class Routes
{
	public static readonly string Index					= $"/{nameof(Index)}";

	public static readonly string SqlExecutionMethod	= $"/{nameof(SqlExecutionMethod)}";

	public static readonly string DaoMethodParameter	= $"/{nameof(DaoMethodParameter)}";

	public static readonly string DaoMethodResult		= $"/{nameof(DaoMethodResult)}";

	public static readonly string Transaction			= $"/{nameof(Transaction)}";

	public class App
	{
		public static readonly string Index		= $"/{nameof(App)}/{nameof(Index)}";

		public static readonly string Article	= $"/{nameof(App)}/{nameof(Article)}";

		public static readonly string Blog		= $"/{nameof(App)}/{nameof(Blog)}";

		public static readonly string Error		= $"/{nameof(App)}/{nameof(Error)}";

		public static readonly string Signin	= $"/{nameof(App)}/{nameof(Signin)}";

		public static readonly string Signup	= $"/{nameof(App)}/{nameof(Signup)}";

		public class Settings
		{
			public static readonly string Account		= $"/{nameof(App)}/{nameof(Settings)}/{nameof(Account)}";

			public static readonly string Article		= $"/{nameof(App)}/{nameof(Settings)}/{nameof(Article)}";

			public static readonly string ArticleList	= $"/{nameof(App)}/{nameof(Settings)}/{nameof(ArticleList)}";

			public static readonly string Blog			= $"/{nameof(App)}/{nameof(Settings)}/{nameof(Blog)}";

			public static readonly string BlogList		= $"/{nameof(App)}/{nameof(Settings)}/{nameof(BlogList)}";

			public static readonly string Home			= $"/{nameof(App)}/{nameof(Settings)}/{nameof(Home)}";
		}
	}
}
