using System;
using System.ComponentModel;

public class Entity
{
    public string name = string.Empty;
    public Guid id = Guid.NewGuid();

    [Browsable(false)]
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
}
