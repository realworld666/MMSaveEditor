// Decompiled with JetBrains decompiler
// Type: UIScoutingFilterAbility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIScoutingFilterAbility : MonoBehaviour
{
  private float mStars = 3f;
  public Button leftButton;
  public Button rightButton;
  public Toggle allToggle;
  public Toggle specificToggle;
  public UIAbilityStars stars;
  public CanvasGroup canvasGroup;
  public UIScoutingSearchResultsWidget widget;
  private UIScoutingFilterAbility.Filter mFilter;

  public UIScoutingFilterAbility.Filter filter
  {
    get
    {
      return this.mFilter;
    }
  }

  public float abilityStars
  {
    get
    {
      return this.mStars;
    }
  }

  public void OnStart()
  {
    this.allToggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnFilter(this.allToggle, UIScoutingFilterAbility.Filter.All)));
    this.specificToggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnFilter(this.specificToggle, UIScoutingFilterAbility.Filter.Specific)));
    this.leftButton.onClick.AddListener((UnityAction) (() => this.OnButton(-0.5f)));
    this.rightButton.onClick.AddListener((UnityAction) (() => this.OnButton(0.5f)));
    this.mStars = Mathf.Clamp(this.mStars, 1f, 5f);
    this.stars.SetStarsValue(this.mStars, this.mStars);
  }

  private void OnFilter(Toggle inToggle, UIScoutingFilterAbility.Filter inFilter)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (!inToggle.isOn)
      return;
    this.mFilter = inFilter;
    this.widget.filterAbility = inFilter;
    this.widget.Refresh();
    this.UpdateState();
  }

  private void OnButton(float inDirection)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mStars = Mathf.Clamp(this.mStars + inDirection, 0.5f, 5f);
    this.stars.SetStarsValue(this.mStars, this.mStars);
    this.UpdateState();
    this.widget.filterAbilityStars = this.mStars;
    this.widget.Refresh();
  }

  public void UpdateState()
  {
    this.canvasGroup.alpha = this.mFilter != UIScoutingFilterAbility.Filter.Specific ? 0.2f : 1f;
    this.canvasGroup.interactable = this.mFilter == UIScoutingFilterAbility.Filter.Specific;
    this.canvasGroup.blocksRaycasts = this.mFilter == UIScoutingFilterAbility.Filter.Specific;
    this.rightButton.interactable = (double) this.mStars < 5.0;
    this.leftButton.interactable = (double) this.mStars > 0.5;
  }

  public enum Filter
  {
    All,
    Specific,
  }
}
