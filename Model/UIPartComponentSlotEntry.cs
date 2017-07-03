// Decompiled with JetBrains decompiler
// Type: UIPartComponentSlotEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPartComponentSlotEntry : MonoBehaviour
{
  private UIPartComponentSlotEntry.State mState = UIPartComponentSlotEntry.State.Inactive;
  public Button button;
  public Animator animator;
  public UIPartComponentIcon icon;

  private void Start()
  {
    this.button.onClick.AddListener(new UnityAction(this.OnButtonClick));
  }

  private void OnEnable()
  {
    if (this.mState == UIPartComponentSlotEntry.State.Active)
      this.animator.SetTrigger(AnimationHashes.Active);
    else
      this.animator.SetTrigger(AnimationHashes.Inactive);
  }

  private void OnButtonClick()
  {
    if (this.mState != UIPartComponentSlotEntry.State.Active)
      return;
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
  }

  public void SetState(UIPartComponentSlotEntry.State inState)
  {
    if (inState == UIPartComponentSlotEntry.State.Active)
    {
      this.animator.ResetTrigger(AnimationHashes.Inactive);
      this.animator.SetTrigger(AnimationHashes.Active);
    }
    else if (this.mState != inState)
    {
      this.animator.ResetTrigger(AnimationHashes.Active);
      this.animator.SetTrigger(AnimationHashes.Inactive);
    }
    this.mState = inState;
  }

  public enum State
  {
    Active,
    Inactive,
  }
}
