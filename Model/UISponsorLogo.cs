// Decompiled with JetBrains decompiler
// Type: UISponsorLogo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class UISponsorLogo : MonoBehaviour
{
  private Image mImage;
  private Sponsor mSponsor;

  public Sponsor sponsor
  {
    get
    {
      return this.mSponsor;
    }
  }

  private void Awake()
  {
    this.mImage = this.GetComponentInChildren<Image>();
  }

  public void SetSponsor(Sponsor inSponsor)
  {
    this.mSponsor = inSponsor;
    if ((Object) this.mImage == (Object) null)
      this.mImage = this.GetComponentInChildren<Image>(true);
    this.UpdateLogo();
  }

  private void OnEnable()
  {
    this.UpdateLogo();
  }

  private void UpdateLogo()
  {
    if (this.mSponsor != null)
    {
      Texture2D sponsorLogo = App.instance.assetManager.GetSponsorLogo(this.mSponsor.logoIndex, false);
      if ((Object) sponsorLogo != (Object) null)
        this.mImage.sprite = Sprite.Create(sponsorLogo, new Rect(0.0f, 0.0f, (float) sponsorLogo.width, (float) sponsorLogo.height), Vector2.zero);
      else
        this.mImage.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Logos1, "Sponsors-SponsorLogo" + (object) this.mSponsor.logoIndex);
    }
    else
      this.mImage.sprite = (Sprite) null;
  }
}
