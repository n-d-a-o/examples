using NDao.Attributes;
using NDao.Examples.HelloWorld.Entities;

namespace NDao.Examples.HelloWorld.Daos;

[Dao]
public interface IAccountDao
{
	[SingleOrDefault]
	Account? GetAccountByName(string name);
}
