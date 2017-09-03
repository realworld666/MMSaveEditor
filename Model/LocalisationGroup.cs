using System.Collections.Generic;

public class LocalisationGroup
{
    public List<LocalisationEntry> entries = new List<LocalisationEntry>();

    public string GetName()
    {
        if (this.entries.Count > 0)
            return this.entries[0].group;
        return string.Empty;
    }
}
