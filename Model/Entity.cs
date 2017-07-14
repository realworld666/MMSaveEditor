using System;

public class Entity
{
	public string name = string.Empty;
	public Guid id = Guid.NewGuid();

	public string Name
	{
		get { return name; }
		set { name = value; }
	}
}
