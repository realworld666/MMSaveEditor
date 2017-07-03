// Decompiled with JetBrains decompiler
// Type: UICharacterPortrait
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class UICharacterPortrait : MonoBehaviour
{
  [SerializeField]
  private UICharacterPortrait.ShowRollover showRollover;
  [SerializeField]
  private UICharacterPortraitBody female;
  [SerializeField]
  private UICharacterPortraitBody male;
  [SerializeField]
  private RectTransform rectTransform;
  [SerializeField]
  private GameObject hoverHighlight;
  [SerializeField]
  private GameObject containerCustomPortrait;
  [SerializeField]
  private Image customPortrait;
  private Person mPerson;
  private Person.Gender mPersonGender;
  private UICharacterPortraitBody mBody;

  private void Awake()
  {
    GameUtility.Assert((Object) this.male != (Object) null, "Portrait has missing objects", (Object) this.gameObject);
    GameUtility.Assert((Object) this.female != (Object) null, "Portrait has missing objects", (Object) this.gameObject);
    GameUtility.Assert((Object) this.containerCustomPortrait != (Object) null, "Portrait has missing objects", (Object) this.gameObject);
  }

  public void SetPortrait(Person inPerson)
  {
    this.mPerson = inPerson;
    this.mPersonGender = inPerson.gender;
    if (this.TryLoadCustomPortrait(inPerson))
    {
      GameUtility.SetActive(this.female.gameObject, false);
      GameUtility.SetActive(this.male.gameObject, false);
    }
    else
    {
      this.LoadPortrait();
      this.mBody.LoadPortrait(this.mPerson);
    }
  }

  public void SetPortrait(Portrait inPortrait, Person.Gender inPortraitGender, int inAge, int inTeamID = -1, TeamColor inTeamColor = null, UICharacterPortraitBody.BodyType inBodyType = UICharacterPortraitBody.BodyType.None, int inHatStyle = -1, int inBodyStyle = -1)
  {
    this.mPerson = (Person) null;
    this.mPersonGender = inPortraitGender;
    this.LoadPortrait();
    this.mBody.LoadPortrait(inPortrait, inPortraitGender, Portrait.GetAgeGroup(inAge), inTeamID, inTeamColor, inBodyType, inHatStyle, inBodyStyle);
  }

  public void LoadPortrait()
  {
    GameUtility.SetActive(this.male.gameObject, this.mPersonGender == Person.Gender.Male);
    GameUtility.SetActive(this.female.gameObject, this.mPersonGender == Person.Gender.Female);
    GameUtility.SetActiveAndCheckNull(this.containerCustomPortrait, false);
    this.mBody = this.mPersonGender != Person.Gender.Male ? this.female : this.male;
  }

  private bool TryLoadCustomPortrait(Person inPerson)
  {
    Texture2D portraitForPerson = App.instance.assetManager.GetPortraitForPerson(inPerson);
    if ((Object) this.customPortrait != (Object) null && (Object) portraitForPerson != (Object) null)
    {
      GameUtility.SetActiveAndCheckNull(this.containerCustomPortrait, true);
      this.customPortrait.sprite = Sprite.Create(portraitForPerson, new Rect(0.0f, 0.0f, (float) portraitForPerson.width, (float) portraitForPerson.height), Vector2.zero);
    }
    else
      GameUtility.SetActiveAndCheckNull(this.containerCustomPortrait, false);
    return (Object) portraitForPerson != (Object) null;
  }

  private void OnDisable()
  {
    if (!UIManager.InstanceExists)
      return;
    this.OnMouseExit();
  }

  public void OnMouseClick()
  {
    if (!Game.IsActive() || this.showRollover != UICharacterPortrait.ShowRollover.YesAndAllowClick || this.mPerson == null)
      return;
    if (this.mPerson is Driver)
    {
      UIManager.instance.ChangeScreen("DriverScreen", (Entity) (this.mPerson as Driver), UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
    }
    else
    {
      if (!(this.mPerson is Mechanic) && !(this.mPerson is Engineer))
        return;
      UIManager.instance.ChangeScreen("StaffDetailsScreen", (Entity) this.mPerson, UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
    }
  }

  public void OnMouseEnter()
  {
    if (this.showRollover == UICharacterPortrait.ShowRollover.No || !Game.IsActive() || this.mPerson == null)
      return;
    if (this.mPerson is Driver)
    {
      DriverInfoRollover.ShowTooltip(this.mPerson as Driver, this.rectTransform, false, true);
      GameUtility.SetActive(this.hoverHighlight, true);
    }
    else
    {
      if (!(this.mPerson is Mechanic) && !(this.mPerson is Engineer))
        return;
      StaffInfoRollover.ShowTooltip(this.mPerson, this.rectTransform, false, true);
      GameUtility.SetActive(this.hoverHighlight, true);
    }
  }

  public void OnMouseExit()
  {
    if (this.showRollover == UICharacterPortrait.ShowRollover.No || !Game.IsActive() || this.mPerson == null)
      return;
    if (this.mPerson is Driver && UIManager.instance.dialogBoxManager != null)
      DriverInfoRollover.HideTooltip();
    else if ((this.mPerson is Mechanic || this.mPerson is Engineer) && UIManager.instance.dialogBoxManager != null)
      StaffInfoRollover.HideTooltip();
    if (!(bool) ((Object) this.hoverHighlight))
      return;
    GameUtility.SetActive(this.hoverHighlight, false);
  }

  public enum ShowRollover
  {
    No,
    YesAndAllowClick,
    Yes,
  }
}
