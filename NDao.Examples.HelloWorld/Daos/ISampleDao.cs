using NDao.Attributes;
using NDao.Examples.HelloWorld.Dtos;
using NDao.Examples.HelloWorld.Entities;

namespace NDao.Examples.HelloWorld.Daos;

[Dao]
public interface ISampleDao
{
	// Client Method

	[Affect]
	int Affect();

	[Single]
	string Single(int id);

	[SingleOrDefault]
	string? SingleOrDefault(int id);

	List<string> Query();

	// Return Type

	[Single]
	string GetString();

	[Single]
	DateTime GetDateTime();

	[Single]
	(int Id, string Name) GetTuple();

	[Single]
	SampleDto GetDto();

	// Parameter Type

	int Delete(string occupation);

	List<Person> Search(string? name, int? age, string? occupation);

	List<Person> SearchByComplexParameter(SampleDaoSearchByComplexParameterCondition condition);
}

public class SampleDaoSearchByComplexParameterCondition
{
	public string? Name { get; set; }

	public int? Age { get; set; }

	public string? Occupation { get; set; }
}
