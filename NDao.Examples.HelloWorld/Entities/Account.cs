using NDao.Attributes;

namespace NDao.Examples.HelloWorld.Entities;

[Entity]
public class Account : EntityBase
{
	public int AccountId { get; set; }

	public string Name { get; set; } = default!;
}
