// Decompiled with JetBrains decompiler
// Type: UICircuitImage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class UICircuitImage : MonoBehaviour
{
  private Image mImage;
  private Circuit mCircuit;

  public Circuit circuit
  {
    get
    {
      return this.mCircuit;
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

  public void SetCircuitIcon(Circuit inCircuit)
  {
    this.mCircuit = inCircuit;
    this.UpdateIcon();
  }

  private void UpdateIcon()
  {
    if (!((Object) this.mImage != (Object) null) || this.mCircuit == null)
      return;
    this.mImage.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.TrackImages, "TrackImages-" + this.mCircuit.spriteName);
  }
}
