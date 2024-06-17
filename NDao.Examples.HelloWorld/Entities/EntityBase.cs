using NDao.Attributes;

namespace NDao.Examples.HelloWorld.Entities;

public class EntityBase
{
	[Generated(ValueGenerationTiming.OnInsert, "datetime('now')")]
	public DateTime CreatedDate { get; set; } = default!;

	[Generated(ValueGenerationTiming.OnInsertOrUpdate, "datetime('now')")]
	public DateTime UpdatedDate { get; set; } = default!;
}
