using NDao.Attributes;

namespace NDao.Examples.HelloWorld.Entities;

[Entity]
public class Blog : EntityBase
{
	public int BlogId { get; set; }

	public int AccountId { get; set; }

	public string BlogName { get; set; } = default!;

	public string Description { get; set; } = default!;
}
