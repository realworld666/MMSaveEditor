// Decompiled with JetBrains decompiler
// Type: UIChampionshipLogo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class UIChampionshipLogo : MonoBehaviour
{
  [SerializeField]
  private UIChampionshipLogo.Resolution logoResolution = UIChampionshipLogo.Resolution.High;
  private Image mImage;
  private Championship mChampionship;

  public Championship championship
  {
    get
    {
      return this.mChampionship;
    }
  }

  public void SetChampionship(Championship inChampionship)
  {
    this.mChampionship = inChampionship;
    this.UpdateLogo();
  }

  private void OnEnable()
  {
    this.UpdateLogo();
  }

  private void UpdateLogo()
  {
    if ((Object) this.mImage == (Object) null)
      this.mImage = this.GetComponentInChildren<Image>();
    if (this.mChampionship != null)
    {
      Texture2D championshipLogo = App.instance.assetManager.GetChampionshipLogo(this.mChampionship, this.logoResolution == UIChampionshipLogo.Resolution.Low);
      if ((Object) championshipLogo != (Object) null)
      {
        this.mImage.sprite = Sprite.Create(championshipLogo, new Rect(0.0f, 0.0f, (float) championshipLogo.width, (float) championshipLogo.height), Vector2.zero);
      }
      else
      {
        string str = string.Empty;
        if (this.logoResolution == UIChampionshipLogo.Resolution.Low)
          str = "BW";
        this.mImage.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Logos1, "SeriesLogos-Series" + (object) this.mChampionship.logoID + str);
      }
    }
    else
      this.mImage.sprite = (Sprite) null;
  }

  public enum Resolution
  {
    Low,
    High,
  }
}
