// Decompiled with JetBrains decompiler
// Type: UIInvestorLogo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class UIInvestorLogo : MonoBehaviour
{
  private Image mImage;
  private Investor mInvestor;

  public Investor investor
  {
    get
    {
      return this.mInvestor;
    }
  }

  private void Awake()
  {
    this.mImage = this.GetComponentInChildren<Image>();
  }

  public void SetInvestor(Investor inInvestor)
  {
    this.mInvestor = inInvestor;
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
    if (this.mInvestor != null)
      this.mImage.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Logos1, "Sponsors-SponsorLogo" + (object) this.mInvestor.logoID);
    else
      this.mImage.sprite = (Sprite) null;
  }
}
