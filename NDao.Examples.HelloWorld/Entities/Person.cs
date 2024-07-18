using NDao.Attributes;

namespace NDao.Examples.HelloWorld.Entities;

[Entity]
public class Person
{
	public int Id { get; set; }

	public string Name { get; set; } = default!;

	public int Age { get; set; }

	public string Occupation { get; set; } = default!;
}
