// Decompiled with JetBrains decompiler
// Type: UISeriesWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISeriesWidget : MonoBehaviour
{
  [SerializeField]
  private Button mainButton;
  [SerializeField]
  private Animator animator;
  [SerializeField]
  private CanvasGroup canvasGroup;
  [SerializeField]
  private GameObject lockedParent;
  [SerializeField]
  private Image backingImage;
  [SerializeField]
  private Image backingImageRollover;
  [SerializeField]
  private TextMeshProUGUI title;
  [SerializeField]
  private TextMeshProUGUI description;
  [SerializeField]
  private TextMeshProUGUI tierNumber;
  [SerializeField]
  private UIChampionshipLogo logo;
  [SerializeField]
  private TextMeshProUGUI prizeFund;
  [SerializeField]
  private TextMeshProUGUI estTvAudience;
  [SerializeField]
  private TextMeshProUGUI teamsNumber;
  [SerializeField]
  private TextMeshProUGUI racesNumber;
  public ChooseSeriesScreen screen;
  private Championship mChampionship;
  private bool mLocked;

  protected void Awake()
  {
    GameUtility.Assert((UnityEngine.Object) this.mainButton != (UnityEngine.Object) null, "mainButton != null", (UnityEngine.Object) this);
    GameUtility.Assert((UnityEngine.Object) this.animator != (UnityEngine.Object) null, "animator != null", (UnityEngine.Object) this);
    GameUtility.Assert((UnityEngine.Object) this.canvasGroup != (UnityEngine.Object) null, "canvasGroup != null", (UnityEngine.Object) this);
    GameUtility.Assert((UnityEngine.Object) this.lockedParent != (UnityEngine.Object) null, "lockedParent != null", (UnityEngine.Object) this);
    GameUtility.Assert((UnityEngine.Object) this.backingImage != (UnityEngine.Object) null, "backingImage != null", (UnityEngine.Object) this);
    GameUtility.Assert((UnityEngine.Object) this.backingImageRollover != (UnityEngine.Object) null, "backingImageRollover != null", (UnityEngine.Object) this);
    GameUtility.Assert((UnityEngine.Object) this.title != (UnityEngine.Object) null, "title != null", (UnityEngine.Object) this);
    GameUtility.Assert((UnityEngine.Object) this.description != (UnityEngine.Object) null, "description != null", (UnityEngine.Object) this);
    GameUtility.Assert((UnityEngine.Object) this.tierNumber != (UnityEngine.Object) null, "tierNumber != null", (UnityEngine.Object) this);
    GameUtility.Assert((UnityEngine.Object) this.logo != (UnityEngine.Object) null, "logo != null", (UnityEngine.Object) this);
    GameUtility.Assert((UnityEngine.Object) this.prizeFund != (UnityEngine.Object) null, "prizeFund != null", (UnityEngine.Object) this);
    GameUtility.Assert((UnityEngine.Object) this.estTvAudience != (UnityEngine.Object) null, "estTvAudience != null", (UnityEngine.Object) this);
    GameUtility.Assert((UnityEngine.Object) this.teamsNumber != (UnityEngine.Object) null, "teamsNumber != null", (UnityEngine.Object) this);
    GameUtility.Assert((UnityEngine.Object) this.racesNumber != (UnityEngine.Object) null, "racesNumber != null", (UnityEngine.Object) this);
  }

  public void OnStart()
  {
    this.mainButton.onClick.AddListener(new UnityAction(this.OnButton));
  }

  public void Setup(Championship inChampionship)
  {
    this.mChampionship = inChampionship;
    this.SetDetails();
    this.SetLocked(Game.instance.gameType == Game.GameType.Career && !inChampionship.isChoosable && inChampionship.isBlockedByChallenge);
  }

  private void SetDetails()
  {
    Sprite sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Frontend1, "ChooseSeriesScreen-Series" + (object) this.mChampionship.logoID + "Image2");
    this.backingImage.sprite = sprite;
    this.backingImageRollover.sprite = sprite;
    this.title.text = this.mChampionship.GetChampionshipName(false);
    this.logo.SetChampionship(this.mChampionship);
    this.tierNumber.text = "Tier " + this.mChampionship.logoID.ToString();
    this.prizeFund.text = this.mChampionship.prizeFund.ToString("C0", (IFormatProvider) CultureInfo.CurrentCulture);
    this.estTvAudience.text = this.mChampionship.tvAudience.ToString("N0", (IFormatProvider) CultureInfo.CurrentCulture);
    this.teamsNumber.text = this.mChampionship.standings.teamEntryCount.ToString();
    this.racesNumber.text = this.mChampionship.eventCount.ToString();
  }

  private void SetLocked(bool inValue)
  {
    this.mLocked = inValue;
    this.canvasGroup.blocksRaycasts = !inValue;
    GameUtility.SetActive(this.lockedParent, true);
    if (!inValue)
      this.animator.SetTrigger(AnimationHashes.Unlock);
    else
      this.animator.SetTrigger(AnimationHashes.Lock);
  }

  private void OnButton()
  {
    if (this.mChampionship == null || this.mLocked)
      return;
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.screen.SelectChampionship(this.mChampionship);
  }
}
