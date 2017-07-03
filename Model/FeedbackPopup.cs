// Decompiled with JetBrains decompiler
// Type: FeedbackPopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FeedbackPopup : UIDialogBox
{
  private List<FeedbackPopup.FeedbackContainer> dispacthList = new List<FeedbackPopup.FeedbackContainer>();
  public Animator animator;
  public TextMeshProUGUI header;
  public TextMeshProUGUI description;

  public static void Open(string inHeader, string inDescription)
  {
    UIManager.instance.dialogBoxManager.GetDialog<FeedbackPopup>().Show(inHeader, inDescription);
    scSoundManager.Instance.PlaySound(SoundID.Sfx_FeedbackWhooshOn, 0.0f);
    scSoundManager.Instance.PlaySound(SoundID.Sfx_FeedbackTick, 0.6f);
    scSoundManager.Instance.PlaySound(SoundID.Sfx_FeedbackWhooshOff, 2.35f);
  }

  public static void Close()
  {
    UIManager.instance.dialogBoxManager.GetDialog<FeedbackPopup>().Hide();
  }

  public void Show(string inHeader, string inDescription)
  {
    FeedbackPopup.FeedbackContainer inFeedback = new FeedbackPopup.FeedbackContainer(inHeader, inDescription);
    if (!this.IsRepeatedMessage(inFeedback))
      this.dispacthList.Add(inFeedback);
    if (this.gameObject.activeSelf)
      return;
    this.Dispatch();
  }

  private bool IsRepeatedMessage(FeedbackPopup.FeedbackContainer inFeedback)
  {
    for (int index = 0; index < this.dispacthList.Count; ++index)
    {
      FeedbackPopup.FeedbackContainer dispacth = this.dispacthList[index];
      if (dispacth.description == inFeedback.description && dispacth.header == inFeedback.header)
        return true;
    }
    return false;
  }

  private void Dispatch()
  {
    if (this.dispacthList.Count > 0)
    {
      FeedbackPopup.FeedbackContainer dispacth = this.dispacthList[0];
      this.dispacthList.Remove(dispacth);
      this.Setup(dispacth);
      this.gameObject.SetActive(true);
    }
    else
      this.gameObject.SetActive(false);
  }

  private void Setup(FeedbackPopup.FeedbackContainer inEntry)
  {
    this.header.text = inEntry.header;
    this.description.text = inEntry.description;
  }

  private void Update()
  {
    if ((double) this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0)
      return;
    this.animator.Play(0);
    this.Dispatch();
  }

  private class FeedbackContainer
  {
    public string header = string.Empty;
    public string description = string.Empty;

    public FeedbackContainer(string inHeader, string inDescription)
    {
      this.header = inHeader;
      this.description = inDescription;
    }
  }
}
