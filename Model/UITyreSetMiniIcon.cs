// Decompiled with JetBrains decompiler
// Type: UITyreSetMiniIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class UITyreSetMiniIcon : MonoBehaviour
{
  private TyreSet.Compound mTyreSetCompound = TyreSet.Compound.Soft;
  public Image image;

  public TyreSet.Compound compound
  {
    get
    {
      return this.mTyreSetCompound;
    }
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
    if (!((Object) this.image != (Object) null))
      return;
    switch (this.mTyreSetCompound)
    {
      case TyreSet.Compound.SuperSoft:
        this.image.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "TyresNew-Mini-MiniTyreSoft");
        break;
      case TyreSet.Compound.Soft:
        this.image.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "TyresNew-Mini-MiniTyreSuperSoft");
        break;
      case TyreSet.Compound.Medium:
        this.image.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "TyresNew-Mini-MiniTyreMedium");
        break;
      case TyreSet.Compound.Hard:
        this.image.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "TyresNew-Mini-MiniTyreHard");
        break;
      case TyreSet.Compound.Intermediate:
        this.image.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "TyresNew-Mini-MiniTyreInter");
        break;
      case TyreSet.Compound.Wet:
        this.image.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "TyresNew-Mini-MiniTyreWet");
        break;
      case TyreSet.Compound.UltraSoft:
        this.image.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, "TyresNew-Mini-MiniTyreUberSoft");
        break;
    }
  }
}
