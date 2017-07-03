using FullSerializer;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class MediaOutlet : Entity
{
  public List<Person> journalists = new List<Person>();
  public Nationality nationality = new Nationality();
  private DialogQueryCreator mQueryCreator = new DialogQueryCreator();
  public int logoIndex;
  public Color primaryColor;
  public Color secondaryColor;
  public Color shirtColor;

}
