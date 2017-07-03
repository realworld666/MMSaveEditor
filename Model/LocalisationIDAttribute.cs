using System;

[AttributeUsage(AttributeTargets.Field)]
public class LocalisationIDAttribute : Attribute
{
  public string ID = string.Empty;

  public LocalisationIDAttribute(string inID)
  {
    this.ID = inID;
  }
}
