using NDao.Attributes;

namespace NDao.Examples.HelloWorld.Daos;

[Dao]
public interface IInitializerDao
{
	[Affect]
	void CreateAccounts();

	[Affect]
	void CreateBlogs();

	[Affect]
	void CreateArticles();

	[Affect]
	void CreatePersons();
}
