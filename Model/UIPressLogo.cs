// Decompiled with JetBrains decompiler
// Type: UIPressLogo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class UIPressLogo : MonoBehaviour
{
  private Image mImage;
  private MediaOutlet mMediaOutlet;

  public MediaOutlet mediaOutlet
  {
    get
    {
      return this.mMediaOutlet;
    }
  }

  private void Awake()
  {
    this.mImage = this.GetComponentInChildren<Image>();
  }

  public void SetMediaOutlet(MediaOutlet inMediaOutlet)
  {
    this.mMediaOutlet = inMediaOutlet;
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
    if (this.mMediaOutlet != null)
    {
      Texture2D mediaLogo = App.instance.assetManager.GetMediaLogo(this.mMediaOutlet.logoIndex);
      if ((Object) mediaLogo != (Object) null)
        this.mImage.sprite = Sprite.Create(mediaLogo, new Rect(0.0f, 0.0f, (float) mediaLogo.width, (float) mediaLogo.height), Vector2.zero);
      else
        this.mImage.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Logos1, "PressLogos-PressLogo" + this.mMediaOutlet.logoIndex.ToString());
    }
    else
      this.mImage.sprite = (Sprite) null;
  }
}
