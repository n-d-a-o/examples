using NDao.Attributes;

namespace NDao.Examples.HelloWorld.Entities;

[Entity]
public class Article : EntityBase
{
	public int ArticleId { get; set; }

	public int BlogId { get; set; }

	public int AccountId { get; set; }

	public string Title { get; set; } = default!;

	public string Content { get; set; } = default!;

	[Version]
	public int Version { get; set; }
}
