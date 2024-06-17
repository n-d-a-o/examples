using NDao.Attributes;
using NDao.Examples.HelloWorld.Dtos;
using NDao.Examples.HelloWorld.Entities;

namespace NDao.Examples.HelloWorld.Daos;

[Dao]
public interface IBlogDao
{
	ArticleDto? GetArticleById(int articleId);

	List<Article> GetArticlesByBlogId(int blogId);

	List<Blog> GetBlogsByAccountId(int accountId);
}
