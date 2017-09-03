using System.Collections.Generic;

public class LocalisationEntry
{
    public string id = string.Empty;
    public string group = string.Empty;
    public Dictionary<string, string> text = new Dictionary<string, string>();
    public Dictionary<string, string> userData = new Dictionary<string, string>();
}
