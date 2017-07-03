// Decompiled with JetBrains decompiler
// Type: UITyreSetIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class UITyreSetIcon : MonoBehaviour
{
  private TyreSet.Compound mTyreSetCompound = TyreSet.Compound.Soft;
  private Image mImage;

  public TyreSet.Compound compound
  {
    get
    {
      return this.mTyreSetCompound;
    }
  }

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

  public void SetTyreSet(TyreSet inTyreSet)
  {
    this.mTyreSetCompound = inTyreSet.GetCompound();
    this.UpdateIcon();
  }

  public void SetTyreSet(TyreSet.Compound inTyreSetCompound)
  {
    this.mTyreSetCompound = inTyreSetCompound;
    this.UpdateIcon();
  }

  private void UpdateIcon()
  {
    if (!((Object) this.mImage != (Object) null))
      return;
    switch (this.mTyreSetCompound)
    {
      case TyreSet.Compound.SuperSoft:
        this.mImage.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "TyresNew-Small-TyreSoft");
        break;
      case TyreSet.Compound.Soft:
        this.mImage.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "TyresNew-Small-TyreSuperSoft");
        break;
      case TyreSet.Compound.Medium:
        this.mImage.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "TyresNew-Small-TyreMedium");
        break;
      case TyreSet.Compound.Hard:
        this.mImage.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "TyresNew-Small-TyreHard");
        break;
      case TyreSet.Compound.Intermediate:
        this.mImage.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "TyresNew-Small-TyreInter");
        break;
      case TyreSet.Compound.Wet:
        this.mImage.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "TyresNew-Small-TyreWet");
        break;
      case TyreSet.Compound.UltraSoft:
        this.mImage.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "TyresNew-Small-UltraSoft");
        break;
    }
  }
}
