// Decompiled with JetBrains decompiler
// Type: ChooseTeamEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChooseTeamEntry : MonoBehaviour
{
  public Toggle toggle;
  public GameObject lockedParent;
  public CanvasGroup canvasGroup;
  public UITeamLogo logo;
  public Image[] stars;
  public ChooseTeamScreen screen;
  private Team mTeam;

  public Team team
  {
    get
    {
      return this.mTeam;
    }
  }

  public void OnStart()
  {
    this.toggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnToggle()));
    this.toggle.group = this.screen.toggleGroup;
  }

  public void Setup(Team inTeam)
  {
    scSoundManager.BlockSoundEvents = true;
    if (inTeam != null)
    {
      this.mTeam = inTeam;
      this.logo.SetTeam(this.mTeam);
      this.SetStars();
    }
    scSoundManager.BlockSoundEvents = false;
  }

  public void SetStars()
  {
    if (this.mTeam == null)
      return;
    float scale = MathsUtility.RoundToScale(this.mTeam.GetStarsStat(), 4f);
    int index = 0;
    while (index < this.stars.Length)
    {
      GameUtility.SetActive(this.stars[index].gameObject, true);
      GameUtility.SetImageFillAmountIfDifferent(this.stars[index], Mathf.Clamp01(scale), 1f / 512f);
      ++index;
      --scale;
    }
  }

  public void Highlight(bool inValue)
  {
    if (inValue)
    {
      this.canvasGroup.alpha = 1f;
      this.toggle.Select();
    }
    else
      this.canvasGroup.alpha = 1f;
  }

  public void SetLocked(bool inValue)
  {
    this.toggle.interactable = !inValue;
    GameUtility.SetActive(this.lockedParent, inValue);
  }

  public void OnToggle()
  {
    if (!this.toggle.isOn)
      return;
    this.screen.SelectTeam(this.mTeam, false);
  }
}
