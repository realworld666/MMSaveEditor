using FullSerializer;
using System.Collections.Generic;
using UnityEngine;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class MediaOutlet : Entity
{
    public int logoIndex;
    public List<Person> journalists = new List<Person>();
    public Color primaryColor;
    public Color secondaryColor;
    public Color shirtColor;
    public Nationality nationality = new Nationality();
    private DialogQueryCreator mQueryCreator = new DialogQueryCreator();

}
