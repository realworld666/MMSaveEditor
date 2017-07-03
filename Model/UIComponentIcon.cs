// Decompiled with JetBrains decompiler
// Type: UIComponentIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class UIComponentIcon : MonoBehaviour
{
  [SerializeField]
  private UIComponentIcon.Mode mode;
  private Image mImage;
  private CarPartComponent mCarPartComponent;
  private MechanicBonus mMechanicBonus;

  private void Awake()
  {
    this.mImage = this.GetComponent<Image>();
  }

  private void OnEnable()
  {
    if ((Object) this.mImage == (Object) null)
      this.mImage = this.GetComponent<Image>();
    this.UpdateIcon();
  }

  public void SetComponent(CarPartComponent inPartComponent)
  {
    this.mCarPartComponent = inPartComponent;
    this.UpdateIcon();
  }

  public void SetComponent(MechanicBonus inMechanicBonus)
  {
    this.mMechanicBonus = inMechanicBonus;
    this.UpdateIcon();
  }

  private void UpdateIcon()
  {
    if (!((Object) this.mImage != (Object) null))
      return;
    switch (this.mode)
    {
      case UIComponentIcon.Mode.EngineerComponents:
        if (this.mCarPartComponent == null)
          break;
        this.mImage.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Frontend1, "PartDevIcons-" + this.mCarPartComponent.iconPath);
        break;
      case UIComponentIcon.Mode.MechanicBonuses:
        if (this.mMechanicBonus == null)
          break;
        this.mImage.sprite = MechanicBonus.GetSpriteFromIconInt(this.mMechanicBonus.icon);
        break;
    }
  }

  public enum Mode
  {
    EngineerComponents,
    MechanicBonuses,
  }
}
