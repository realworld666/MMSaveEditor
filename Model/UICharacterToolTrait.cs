// Decompiled with JetBrains decompiler
// Type: UICharacterToolTrait
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UICharacterToolTrait : MonoBehaviour
{
  public UICharacterToolTrait.Trait trait = UICharacterToolTrait.Trait.Accessories;
  private Portrait mPortrait = new Portrait();
  public Button button;
  public UICharacterPortrait driver;
  public UICharacterPortrait staff;
  public Image image;
  public UICharacterToolCustomize widget;
  private int mNum;
  private bool mDriver;
  private Person.Gender mPersonGender;

  public void OnStart()
  {
    this.button.onClick.RemoveAllListeners();
    this.button.onClick.AddListener(new UnityAction(this.OnButton));
  }

  public void Setup(int inNum, Person.Gender inGender, bool inDriver = false)
  {
    this.mNum = inNum;
    this.mPersonGender = inGender;
    this.mDriver = inDriver;
    GameUtility.SetActive(this.driver.gameObject, this.mDriver);
    GameUtility.SetActive(this.staff.gameObject, !this.mDriver);
    this.SetDefaultPortrait();
    this.SetTrait();
    this.SetPortrait();
  }

  public void Setup(int inNum)
  {
    this.mNum = inNum;
  }

  public void SetColor(Color inColor)
  {
    this.image.color = inColor;
  }

  private void SetDefaultPortrait()
  {
    this.mPortrait.head = 0;
    this.mPortrait.hairColor = 7;
    this.mPortrait.hair = 0;
    this.mPortrait.facialHair = 0;
    this.mPortrait.accessory = 0;
    this.mPortrait.glasses = 0;
    this.mPortrait.brows = 0;
  }

  private void SetPortrait()
  {
    if (this.mDriver)
      this.driver.SetPortrait(this.mPortrait, this.mPersonGender, 20, -1, (TeamColor) null, UICharacterPortraitBody.BodyType.None, -1, -1);
    else
      this.staff.SetPortrait(this.mPortrait, this.mPersonGender, 20, -1, (TeamColor) null, UICharacterPortraitBody.BodyType.None, -1, -1);
  }

  private void SetTrait()
  {
    switch (this.trait)
    {
      case UICharacterToolTrait.Trait.SkinColor:
        this.mPortrait.head = this.mNum;
        break;
      case UICharacterToolTrait.Trait.HairColor:
        this.mPortrait.hairColor = this.mNum;
        break;
      case UICharacterToolTrait.Trait.HairStyle:
        this.mPortrait.hair = this.mNum;
        break;
      case UICharacterToolTrait.Trait.Glasses:
        this.mPortrait.glasses = this.mNum;
        break;
      case UICharacterToolTrait.Trait.Accessories:
        this.mPortrait.accessory = this.mNum;
        break;
      case UICharacterToolTrait.Trait.FacialHair:
        this.mPortrait.facialHair = this.mNum;
        break;
    }
  }

  private void OnButton()
  {
    this.widget.SetTrait(this, this.mNum);
  }

  public enum Trait
  {
    SkinColor,
    HairColor,
    HairStyle,
    Glasses,
    Accessories,
    FacialHair,
  }
}
