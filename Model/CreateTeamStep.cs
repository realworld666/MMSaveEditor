// Decompiled with JetBrains decompiler
// Type: CreateTeamStep
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CreateTeamStep : MonoBehaviour
{
  public CreateTeamManager.Step step = CreateTeamManager.Step.PickTeamLogo;
  private bool mAllowMenuSounds = true;
  public CreateTeamOption option;
  public Toggle toggle;
  public GameObject tick;
  public Animator[] highlightAnimator;
  private bool mIsSeen;

  public bool isComplete
  {
    get
    {
      if (this.mIsSeen)
        return this.option.isReady;
      return false;
    }
  }

  public bool isIncomplete
  {
    get
    {
      if (this.mIsSeen)
        return !this.option.isReady;
      return false;
    }
  }

  public bool allowMenuSounds
  {
    get
    {
      return this.mAllowMenuSounds;
    }
    set
    {
      this.mAllowMenuSounds = value;
    }
  }

  public void OnStart()
  {
    this.toggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggle));
    GameUtility.SetActive(this.option.gameObject, false);
    this.option.OnStart();
  }

  public void Setup()
  {
    this.mIsSeen = CreateTeamManager.GetStep(this.step);
    this.SetTick();
    this.option.Setup();
  }

  public void OnExit()
  {
    this.option.OnExit();
  }

  public void Highlight()
  {
    if (this.highlightAnimator == null)
      return;
    for (int index = 0; index < this.highlightAnimator.Length; ++index)
      this.highlightAnimator[index].SetTrigger(AnimationHashes.Play);
  }

  private void SetTick()
  {
    GameUtility.SetActive(this.tick, this.mIsSeen && this.option.isReady);
  }

  private void OnToggle(bool inValue)
  {
    if (inValue)
    {
      if (this.mAllowMenuSounds)
        scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
      this.option.OnEnter();
      this.mIsSeen = true;
      CreateTeamManager.SetStep(this.step, true);
    }
    GameUtility.SetActive(this.option.gameObject, inValue);
    this.SetTick();
  }
}
