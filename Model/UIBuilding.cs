// Decompiled with JetBrains decompiler
// Type: UIBuilding
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class UIBuilding : MonoBehaviour
{
  private Image mImage;
  private HQsBuilding_v1 mBuilding;
  private int mLevel;

  public HQsBuilding_v1 building
  {
    get
    {
      return this.mBuilding;
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

  public void SetBuilding(HQsBuilding_v1 inBuilding)
  {
    this.mBuilding = inBuilding;
    this.mLevel = this.mBuilding.currentLevel;
    this.UpdateIcon();
  }

  public void SetBuilding(HQsBuilding_v1 inBuilding, int inLevel)
  {
    this.mBuilding = inBuilding;
    this.mLevel = inLevel;
    this.UpdateIcon();
  }

  public void SetLevel(int inLevel)
  {
    this.mLevel = inLevel;
    this.UpdateIcon();
  }

  private void UpdateIcon()
  {
    if (!((Object) this.mImage != (Object) null) || this.mBuilding == null)
      return;
    string empty = string.Empty;
    string inName = !this.mBuilding.isLocked ? (!this.mBuilding.isLeveling ? this.mBuilding.info.type.ToString() + UIBuilding.FormatLevel(this.mLevel + 1) : "GenericUpgrade") : "Locked" + this.mBuilding.info.type.ToString() + UIBuilding.FormatLevel(this.mLevel + 1);
    this.mImage.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.HQIcons, inName);
  }

  private static string FormatLevel(int inLevel)
  {
    if (inLevel >= 10)
      return inLevel.ToString();
    return "0" + inLevel.ToString();
  }
}
