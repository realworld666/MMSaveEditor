// Decompiled with JetBrains decompiler
// Type: UICharacterPortraitBody
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class UICharacterPortraitBody : MonoBehaviour
{
  public static readonly string[] maleHeads = new string[6]
  {
    "MaleHeads-WhiteLight",
    "MaleHeads-WhiteMedium",
    "MaleHeads-WhiteDark",
    "MaleHeads-BlackLight",
    "MaleHeads-BlackMedium",
    "MaleHeads-BlackDark"
  };
  public static readonly string[] femaleHeads = new string[6]
  {
    "FemaleHeads-FemaleWhiteLight",
    "FemaleHeads-FemaleWhiteMedium",
    "FemaleHeads-FemaleWhiteDark",
    "FemaleHeads-FemaleBlackLight",
    "FemaleHeads-FemaleBlackMedium",
    "FemaleHeads-FemaleBlackDark"
  };
  private Portrait.AgeGroup mAgeGroup = Portrait.AgeGroup.Medium;
  private AtlasManager.Atlas mAtlas = AtlasManager.Atlas.CharacterPortraits1;
  private Color mPrimaryColor = Color.white;
  private Color mSecondaryColor = Color.white;
  [SerializeField]
  private Image head;
  [SerializeField]
  private Image brows;
  [SerializeField]
  private Image hair;
  [SerializeField]
  private Image glasses;
  [SerializeField]
  private Image accessories;
  [SerializeField]
  private Image body;
  [SerializeField]
  private Image bodyOverlay;
  [SerializeField]
  private GameObject bodyLogo;
  [SerializeField]
  private UITeamLogo bodyTeamLogo;
  [SerializeField]
  private Image facialHair;
  [SerializeField]
  private Image injury;
  [SerializeField]
  private Image hat;
  [SerializeField]
  private Image hatOverlay;
  [SerializeField]
  private GameObject hatLogo;
  [SerializeField]
  private UITeamLogo hatTeamLogo;
  [SerializeField]
  private GameObject headset;
  [SerializeField]
  private Image headsetTint;
  private Person mPerson;
  private Portrait mPortrait;
  private Person.Gender mGender;
  private UICharacterPortraitBody.BodyType mBodyType;
  private Team mTeam;
  private int mTeamID;
  private int mHatStyle;
  private int mBodyStyle;
  private AtlasManager mManager;

  public void LoadPortrait(Person inPerson)
  {
    this.mPerson = inPerson;
    this.mPortrait = this.mPerson.portrait;
    this.mGender = this.mPerson.gender;
    this.mAgeGroup = Portrait.GetAgeGroup(inPerson.GetAge());
    this.mTeam = inPerson.contract.GetTeam();
    this.mTeamID = this.mTeam == null ? -1 : this.mTeam.teamID;
    this.mBodyType = UICharacterPortraitBody.BodyType.None;
    this.mHatStyle = -1;
    this.mBodyStyle = -1;
    this.SetColors((TeamColor) null);
    this.SetPortrait();
  }

  public void LoadPortrait(Portrait inPortrait, Person.Gender inGender, Portrait.AgeGroup inAgeGroup, int inTeamID = -1, TeamColor inTeamColor = null, UICharacterPortraitBody.BodyType inBodyType = UICharacterPortraitBody.BodyType.None, int inHatStyle = -1, int inBodyStyle = -1)
  {
    this.mPerson = (Person) null;
    this.mPortrait = inPortrait;
    this.mGender = inGender;
    this.mAgeGroup = inAgeGroup;
    this.mTeam = (Team) null;
    this.mTeamID = inTeamID;
    this.mBodyType = inBodyType;
    this.mHatStyle = inHatStyle;
    this.mBodyStyle = inBodyStyle;
    this.SetColors(inTeamColor);
    this.SetPortrait();
  }

  private void SetColors(TeamColor inTeamColor = null)
  {
    if (inTeamColor != null)
    {
      switch (this.mBodyType)
      {
        case UICharacterPortraitBody.BodyType.None:
        case UICharacterPortraitBody.BodyType.Mechanic:
        case UICharacterPortraitBody.BodyType.Engineer:
        case UICharacterPortraitBody.BodyType.Driver:
          this.mPrimaryColor = inTeamColor.staffColor.primary;
          this.mSecondaryColor = inTeamColor.staffColor.secondary;
          break;
        case UICharacterPortraitBody.BodyType.Chairman:
          this.mPrimaryColor = inTeamColor.staffColor.secondary;
          this.mSecondaryColor = inTeamColor.staffColor.primary;
          break;
        case UICharacterPortraitBody.BodyType.Media:
          this.mPrimaryColor = inTeamColor.staffColor.primary;
          break;
      }
    }
    else if (!Game.IsActive())
    {
      this.mPrimaryColor = Color.white;
      this.mSecondaryColor = Color.white;
    }
    else if (this.mPerson != null && this.mPerson.contract.GetMediaOutlet() != null)
      this.mPrimaryColor = this.mPerson.contract.GetMediaOutlet().shirtColor;
    else if (this.mTeam == null || this.mTeam is NullTeam)
    {
      this.mPrimaryColor = App.instance.teamColorManager.GetColor(0).staffColor.primary;
      this.mSecondaryColor = App.instance.teamColorManager.GetColor(0).staffColor.secondary;
    }
    else
    {
      this.mPrimaryColor = this.mPerson.GetTeamColor().staffColor.primary;
      this.mSecondaryColor = this.mPerson.GetTeamColor().staffColor.secondary;
    }
  }

  private void SetPortrait()
  {
    this.mManager = App.instance.atlasManager;
    this.LoadHead();
    this.LoadBrows();
    this.LoadHair();
    this.LoadGlasses();
    this.LoadAccessories();
    this.LoadBody();
    this.LoadFacialHair();
    this.LoadInjury();
    this.LoadHat();
    this.LoadHeadSet();
  }

  private void LoadHead()
  {
    string ageString = this.GetAgeString(this.mAgeGroup);
    switch (this.mGender)
    {
      case Person.Gender.Male:
        this.head.sprite = this.mManager.GetSprite(this.mAtlas, UICharacterPortraitBody.maleHeads[this.mPortrait.head] + ageString);
        break;
      case Person.Gender.Female:
        this.head.sprite = this.mManager.GetSprite(this.mAtlas, UICharacterPortraitBody.femaleHeads[this.mPortrait.head] + ageString);
        break;
    }
  }

  private void LoadBrows()
  {
    this.brows.color = Portrait.hairColors[this.mPortrait.hairColor];
  }

  private void LoadHair()
  {
    if (this.mPerson != null && this.mPerson is Driver || this.mBodyType == UICharacterPortraitBody.BodyType.Driver)
    {
      switch (this.mGender)
      {
        case Person.Gender.Male:
          this.hair.sprite = this.mManager.GetSprite(this.mAtlas, "MaleHair-Drivers-MaleDriverHair" + this.mPortrait.hair.ToString());
          break;
        case Person.Gender.Female:
          this.hair.sprite = this.mManager.GetSprite(this.mAtlas, "FemaleHair-Drivers-FemaleDriverHair" + this.mPortrait.hair.ToString());
          break;
      }
    }
    else
    {
      switch (this.mGender)
      {
        case Person.Gender.Male:
          this.hair.sprite = this.mManager.GetSprite(this.mAtlas, "MaleHair-Staff-MaleStaffHair" + this.mPortrait.hair.ToString());
          break;
        case Person.Gender.Female:
          this.hair.sprite = this.mManager.GetSprite(this.mAtlas, "FemaleHair-Staff-FemaleHair" + this.mPortrait.hair.ToString());
          break;
      }
    }
    this.hair.color = Portrait.hairColors[this.mPortrait.hairColor];
  }

  private void LoadGlasses()
  {
    if (this.mPerson != null && this.mPerson is Driver || this.mBodyType == UICharacterPortraitBody.BodyType.Driver)
    {
      switch (this.mGender)
      {
        case Person.Gender.Male:
          this.glasses.sprite = this.mManager.GetSprite(this.mAtlas, "MaleDriverGlasses-MaleGlasses" + this.mPortrait.glasses.ToString());
          break;
        case Person.Gender.Female:
          this.glasses.sprite = this.mManager.GetSprite(this.mAtlas, "FemaleDriverGlasses-MaleGlasses" + this.mPortrait.glasses.ToString());
          break;
      }
    }
    else
    {
      switch (this.mGender)
      {
        case Person.Gender.Male:
          this.glasses.sprite = this.mManager.GetSprite(this.mAtlas, "MaleGlasses-MaleGlasses" + this.mPortrait.glasses.ToString());
          break;
        case Person.Gender.Female:
          this.glasses.sprite = this.mManager.GetSprite(this.mAtlas, "FemaleGlasses-MaleGlasses" + this.mPortrait.glasses.ToString());
          break;
      }
    }
  }

  private void LoadAccessories()
  {
    GameUtility.SetActive(this.accessories.gameObject, false);
  }

  private void LoadBody()
  {
    if (this.mPerson == null)
    {
      this.body.color = this.mPrimaryColor;
      this.bodyOverlay.color = this.mSecondaryColor;
      GameUtility.SetActive(this.bodyLogo, false);
    }
    else
    {
      if (this.mPerson is TeamPrincipal || this.mPerson is Chairman)
      {
        this.body.color = this.mSecondaryColor;
        this.bodyOverlay.color = this.mPrimaryColor;
      }
      else if (this.mPerson.contract.GetMediaOutlet() != null)
      {
        this.body.color = this.mPrimaryColor;
        this.bodyOverlay.color = this.mPrimaryColor;
      }
      else
      {
        this.body.color = this.mPrimaryColor;
        this.bodyOverlay.color = this.mSecondaryColor;
      }
      GameUtility.SetActive(this.bodyLogo, !(this.mTeam is NullTeam));
      if (this.bodyLogo.activeSelf)
        this.bodyTeamLogo.SetTeam(this.mTeamID);
    }
    if (this.mTeam != null && !(this.mTeam is NullTeam))
      this.mBodyStyle = this.mTeam.driversBodyStyle;
    string str = this.mBodyStyle + 1 >= 2 ? (this.mBodyStyle + 1).ToString() : string.Empty;
    if (this.mBodyType == UICharacterPortraitBody.BodyType.Driver || this.mPerson != null && this.mPerson is Driver)
    {
      switch (this.mGender)
      {
        case Person.Gender.Male:
          this.bodyOverlay.sprite = this.mManager.GetSprite(AtlasManager.Atlas.CharacterPortraits1, "Clothes-Drivers-Male-DriverBodyDetail" + str);
          break;
        case Person.Gender.Female:
          this.bodyOverlay.sprite = this.mManager.GetSprite(AtlasManager.Atlas.CharacterPortraits1, "Clothes-Drivers-Female-DriverBodyDetailFemale" + str);
          break;
      }
    }
    else
      this.bodyOverlay.sprite = this.mManager.GetSprite(AtlasManager.Atlas.CharacterPortraits1, "Clothes-Staff-StaffBodyDetail" + str);
  }

  private void LoadFacialHair()
  {
    if (!((Object) this.facialHair != (Object) null))
      return;
    GameUtility.SetActive(this.facialHair.gameObject, this.mGender == Person.Gender.Male);
    if (!this.facialHair.gameObject.activeSelf)
      return;
    this.facialHair.sprite = this.mManager.GetSprite(this.mAtlas, "MaleFacialHair-MaleFacialHair" + this.mPortrait.facialHair.ToString());
    this.facialHair.color = Portrait.hairColors[this.mPortrait.hairColor];
  }

  private void LoadInjury()
  {
    if (!((Object) this.injury != (Object) null))
      return;
    if (this.mPerson is Driver)
    {
      Driver mPerson = this.mPerson as Driver;
      GameUtility.SetActive(this.injury.gameObject, mPerson.personalityTraitController.HasInjuryTrait());
      if (!this.injury.gameObject.activeSelf)
        return;
      switch (mPerson.gender)
      {
        case Person.Gender.Male:
          this.injury.sprite = this.mManager.GetSprite(AtlasManager.Atlas.CharacterPortraits1, "MaleInjurys-Male" + mPerson.personalityTraitController.GetInjurySprite());
          break;
        case Person.Gender.Female:
          this.injury.sprite = this.mManager.GetSprite(AtlasManager.Atlas.CharacterPortraits1, "FemaleInjurys-Female" + mPerson.personalityTraitController.GetInjurySprite());
          break;
      }
    }
    else
      GameUtility.SetActive(this.injury.gameObject, false);
  }

  private void LoadHat()
  {
    if (!((Object) this.hat != (Object) null))
      return;
    int num = this.mTeam != null && !(this.mTeam is NullTeam) || this.mHatStyle != -1 ? (this.mHatStyle != -1 ? this.mHatStyle : this.mTeam.driversHatStyle) : 0;
    this.hat.color = this.mPrimaryColor;
    this.hatOverlay.sprite = this.mManager.GetSprite(this.mAtlas, "DriverCaps-HatOverlay" + num.ToString());
    this.hatOverlay.color = this.mSecondaryColor;
    GameUtility.SetActive(this.hatLogo, this.mTeam != null && !(this.mTeam is NullTeam));
    if (!this.hatLogo.activeSelf)
      return;
    this.hatTeamLogo.SetTeam(this.mTeamID);
  }

  private void LoadHeadSet()
  {
    if (!((Object) this.headset != (Object) null))
      return;
    GameUtility.SetActive(this.headset, this.mPerson != null && this.mPerson is Engineer || this.mBodyType == UICharacterPortraitBody.BodyType.Engineer);
    GameUtility.SetActive(this.headsetTint.gameObject, this.headset.activeSelf);
    if (!this.headset.activeSelf)
      return;
    this.headsetTint.color = this.mPrimaryColor;
  }

  private string GetAgeString(Portrait.AgeGroup inAgeGroup)
  {
    return "Age" + ((int) (inAgeGroup + 1)).ToString();
  }

  public enum BodyType
  {
    None,
    Chairman,
    Mechanic,
    Engineer,
    Driver,
    Media,
  }
}
