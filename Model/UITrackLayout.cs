// Decompiled with JetBrains decompiler
// Type: UITrackLayout
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class UITrackLayout : MonoBehaviour
{
  private Vector2 mDefaultSize = Vector2.zero;
  [SerializeField]
  private UITrackLayout.Resize resize;
  private Image mImage;
  private Circuit mCircuit;
  private RectTransform mRectTransform;

  public Circuit circuit
  {
    get
    {
      return this.mCircuit;
    }
  }

  public Sprite sprite
  {
    get
    {
      return this.mImage.sprite;
    }
  }

  private void Awake()
  {
    this.SetImage();
  }

  private void OnEnable()
  {
    if ((Object) this.mImage == (Object) null)
      this.SetImage();
    this.UpdateIcon();
  }

  private void SetImage()
  {
    this.mImage = this.GetComponent<Image>();
    if (!((Object) this.mImage != (Object) null))
      return;
    this.mRectTransform = this.mImage.GetComponent<RectTransform>();
    if (!((Object) this.mRectTransform != (Object) null))
      return;
    this.mDefaultSize = this.mRectTransform.sizeDelta;
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
    string inName = string.Format("Minimap_{0}{1}", (object) this.mCircuit.spriteName, (object) this.mCircuit.GetTrackVariation());
    this.mImage.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.TrackOutlines1, inName);
    this.ResizeSprite(this.mImage.sprite);
  }

  private void ResizeSprite(Sprite inSprite)
  {
    if (!((Object) this.mRectTransform != (Object) null) || !((Object) this.mImage != (Object) null) || (!((Object) this.mImage.sprite != (Object) null) || !((Object) inSprite != (Object) this.mImage.sprite)))
      return;
    switch (this.resize)
    {
      case UITrackLayout.Resize.Auto:
        this.mRectTransform.sizeDelta = this.GetResizeDimensions(this.mImage.sprite.rect.size, this.mDefaultSize);
        break;
      case UITrackLayout.Resize.NativeSize:
        this.mRectTransform.sizeDelta = this.mImage.sprite.rect.size;
        break;
    }
  }

  private Vector2 GetResizeDimensions(Vector2 inSize, Vector2 inBounds)
  {
    Vector2 vector2 = new Vector2(inSize.x, inSize.y);
    float num = Mathf.Min(inBounds.x / inSize.x, inBounds.y / inSize.y);
    vector2 = new Vector2(inSize.x * num, inSize.y * num);
    return vector2;
  }

  public enum Resize
  {
    None,
    Auto,
    NativeSize,
  }
}
