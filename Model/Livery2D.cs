// Decompiled with JetBrains decompiler
// Type: Livery2D
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class Livery2D : MonoBehaviour
{
  private Championship.Series mSeries = Championship.Series.Count;
  private int mLiveryID = -1;
  public Image liveryImage;
  public Image frontTrim;
  public Image backTrim;
  public ActivateForSeries.GameObjectData[] overlay;
  private Texture mBaseLivery;
  private Texture mDetailLivery;
  private Texture mSelectedTexture;
  private bool mSetSprite;

  public void SetLivery(LiveryData inLiveryData, TeamColor inTeamColor, Championship inChampionship)
  {
    if (!this.mSetSprite || this.mLiveryID != inLiveryData.id || this.mSeries != inChampionship.series)
    {
      this.mBaseLivery = (Texture) null;
      this.mDetailLivery = (Texture) null;
      this.mSelectedTexture = (Texture) null;
      inLiveryData.chassis.GetTexture(out this.mBaseLivery, out this.mDetailLivery);
      this.mSelectedTexture = inLiveryData.chassis.detailProjection != LiveryData.LiveryShaderDetailProjection.Side ? this.mDetailLivery : this.mBaseLivery;
      this.DestroySprite();
      this.liveryImage.sprite = Sprite.Create(this.mSelectedTexture as Texture2D, new Rect(0.0f, 0.0f, (float) this.mSelectedTexture.width, (float) this.mSelectedTexture.height), new Vector2(0.5f, 0.5f));
      this.mLiveryID = inLiveryData.id;
      GameUtility.SetActiveForSeries(inChampionship, this.overlay);
      this.mSeries = inChampionship.series;
      this.mSetSprite = true;
    }
    this.liveryImage.material.SetColor("_PrimaryColor", inTeamColor.livery.primary);
    this.liveryImage.material.SetColor("_SecondaryColor", inTeamColor.livery.secondary);
    this.liveryImage.material.SetColor("_TertiaryColor", inTeamColor.livery.tertiary);
    this.liveryImage.material.SetColor("_TrimColor", inTeamColor.livery.trim);
    this.frontTrim.color = inTeamColor.livery.trim;
    this.backTrim.color = inTeamColor.livery.trim;
    this.mBaseLivery = (Texture) null;
    this.mDetailLivery = (Texture) null;
    this.mSelectedTexture = (Texture) null;
  }

  private void OnEnable()
  {
    this.mSetSprite = false;
  }

  private void OnDisable()
  {
    this.DestroySprite();
  }

  private void DestroySprite()
  {
    if (!this.mSetSprite || !((Object) this.liveryImage.sprite != (Object) null))
      return;
    Sprite sprite = this.liveryImage.sprite;
    this.liveryImage.sprite = (Sprite) null;
    Object.Destroy((Object) sprite);
  }
}
