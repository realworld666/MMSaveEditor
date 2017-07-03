// Decompiled with JetBrains decompiler
// Type: UICharacterToolHeaderEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UICharacterToolHeaderEntry : MonoBehaviour
{
  public UICharacterToolHeaderEntry.Field field = UICharacterToolHeaderEntry.Field.Accessory;
  public Toggle toggle;
  public UICharacterToolHeaderEntry.Type type;
  public TextMeshProUGUI textLabel;
  public UICharacterToolPopup popup;
  private Person mPerson;

  public void OnStart()
  {
    if (!((Object) this.toggle != (Object) null))
      return;
    this.toggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnToggle()));
  }

  public void Setup(Person inPerson)
  {
    if (inPerson == null)
      return;
    this.mPerson = inPerson;
    if (this.type != UICharacterToolHeaderEntry.Type.Example)
      return;
    this.textLabel.text = this.GetField(this.mPerson);
  }

  public string GetField(Person inPerson)
  {
    if (inPerson != null)
    {
      switch (this.field)
      {
        case UICharacterToolHeaderEntry.Field.FirstName:
          return inPerson.firstName;
        case UICharacterToolHeaderEntry.Field.LastName:
          return inPerson.lastName;
        case UICharacterToolHeaderEntry.Field.Gender:
          return inPerson.gender.ToString();
        case UICharacterToolHeaderEntry.Field.Nationality:
          return inPerson.nationality.localisedCountry;
        case UICharacterToolHeaderEntry.Field.DOB:
          return inPerson.dateOfBirth.ToString("dd/MM/yyyy");
        case UICharacterToolHeaderEntry.Field.SkinColour:
          return inPerson.portrait.head.ToString();
        case UICharacterToolHeaderEntry.Field.Hair:
          return inPerson.portrait.hair.ToString();
        case UICharacterToolHeaderEntry.Field.HairColour:
          return inPerson.portrait.hairColor.ToString();
        case UICharacterToolHeaderEntry.Field.Glasses:
          return inPerson.portrait.glasses.ToString();
        case UICharacterToolHeaderEntry.Field.Accessory:
          return inPerson.portrait.accessory.ToString();
        case UICharacterToolHeaderEntry.Field.FacialHair:
          return inPerson.portrait.facialHair.ToString();
      }
    }
    return string.Empty;
  }

  public string GetHeader()
  {
    switch (this.field)
    {
      case UICharacterToolHeaderEntry.Field.FirstName:
        return "First Name";
      case UICharacterToolHeaderEntry.Field.LastName:
        return "Last Name";
      case UICharacterToolHeaderEntry.Field.Gender:
        return "Gender";
      case UICharacterToolHeaderEntry.Field.Nationality:
        return "Nationality";
      case UICharacterToolHeaderEntry.Field.DOB:
        return "DOB";
      case UICharacterToolHeaderEntry.Field.SkinColour:
        return "Skin Colour";
      case UICharacterToolHeaderEntry.Field.Hair:
        return "Hair";
      case UICharacterToolHeaderEntry.Field.HairColour:
        return "Hair Colour";
      case UICharacterToolHeaderEntry.Field.Glasses:
        return "Glasses";
      case UICharacterToolHeaderEntry.Field.Accessory:
        return "Accessory";
      case UICharacterToolHeaderEntry.Field.FacialHair:
        return "Facial Hair";
      default:
        return string.Empty;
    }
  }

  public void SetField()
  {
    if (!((Object) this.toggle != (Object) null))
      return;
    this.popup.SetField(this.field, this.toggle.isOn);
  }

  private void OnToggle()
  {
    this.SetField();
  }

  public enum Field
  {
    FirstName,
    LastName,
    Gender,
    Nationality,
    DOB,
    SkinColour,
    Hair,
    HairColour,
    Glasses,
    Accessory,
    FacialHair,
  }

  public enum Type
  {
    HeaderToggle,
    Header,
    Example,
  }
}
