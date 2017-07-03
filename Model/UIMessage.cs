// Decompiled with JetBrains decompiler
// Type: UIMessage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIMessage : MonoBehaviour
{
  public UICharacterPortrait portrait;
  public UICharacterPortrait driverPortrait;
  public GameObject portraitsList;
  public GameObject readBackground;
  public GameObject unreadBackground;
  public TextMeshProUGUI senderName;
  public TextMeshProUGUI senderRole;
  public TextMeshProUGUI priority;
  public TextMeshProUGUI time;
  public TextMeshProUGUI messageText;
  public Button[] responseButton;
  private Message mMessage;
  private bool mIsNotificationMessage;

  public Message message
  {
    get
    {
      return this.mMessage;
    }
  }

  public bool isNotificationMessage
  {
    get
    {
      return this.mIsNotificationMessage;
    }
    set
    {
      this.mIsNotificationMessage = value;
    }
  }

  private void Start()
  {
    for (int index = 0; index < this.responseButton.Length; ++index)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      UIMessage.\u003CStart\u003Ec__AnonStoreyAB startCAnonStoreyAb = new UIMessage.\u003CStart\u003Ec__AnonStoreyAB();
      // ISSUE: reference to a compiler-generated field
      startCAnonStoreyAb.\u003C\u003Ef__this = this;
      // ISSUE: reference to a compiler-generated field
      startCAnonStoreyAb.button = this.responseButton[index];
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated method
      startCAnonStoreyAb.button.onClick.AddListener(new UnityAction(startCAnonStoreyAb.\u003C\u003Em__227));
    }
  }

  private void OnEnable()
  {
    this.SetBackground();
    if (this.mMessage == null || this.mMessage.hasBeenRead || this.mIsNotificationMessage)
      ;
  }

  public void SetMessage(Message inMessage)
  {
    this.mMessage = inMessage;
    UICharacterPortrait characterPortrait = !(inMessage.sender is Driver) ? this.portrait : this.driverPortrait;
    this.SetPlaceholderPortrait(inMessage.portraitId);
    if (inMessage.sender != null)
    {
      this.senderName.text = inMessage.sender.shortName;
      if ((Object) characterPortrait != (Object) null)
      {
        characterPortrait.gameObject.SetActive(true);
        characterPortrait.SetPortrait(inMessage.sender);
      }
    }
    else
    {
      this.senderName.text = string.Empty;
      if ((Object) characterPortrait != (Object) null)
        characterPortrait.gameObject.SetActive(false);
    }
    this.priority.text = this.mMessage.priority.ToString();
    this.time.text = this.mMessage.deliverDate.ToShortTimeString();
    this.messageText.text = this.mMessage.localisedDescription;
    for (int inIndex = 0; inIndex < this.responseButton.Length; ++inIndex)
    {
      if (inIndex < this.mMessage.responseCount)
      {
        this.responseButton[inIndex].gameObject.SetActive(true);
        this.responseButton[inIndex].GetComponentsInChildren<TextMeshProUGUI>(true)[0].text = this.mMessage.GetResponse(inIndex).description;
      }
      else
        this.responseButton[inIndex].gameObject.SetActive(false);
    }
    this.SetBackground();
  }

  private void SetPlaceholderPortrait(int inPortraitNumber)
  {
    for (int index = 0; index < this.portraitsList.transform.childCount; ++index)
      this.portraitsList.transform.GetChild(index).gameObject.SetActive(false);
    if (!((Object) this.portraitsList != (Object) null))
      return;
    this.portraitsList.transform.GetChild(inPortraitNumber).gameObject.SetActive(true);
  }

  private void SetBackground()
  {
    if (this.mMessage != null && this.mMessage.hasBeenRead)
    {
      this.readBackground.SetActive(true);
      this.unreadBackground.SetActive(false);
    }
    else
    {
      this.readBackground.SetActive(false);
      this.unreadBackground.SetActive(true);
    }
  }

  public void OnResponseButton(Button inButton)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    for (int inIndex = 0; inIndex < this.responseButton.Length; ++inIndex)
    {
      this.responseButton[inIndex].gameObject.SetActive(false);
      if ((Object) this.responseButton[inIndex] == (Object) inButton)
        this.mMessage.GetResponse(inIndex).DoAction();
    }
  }

  public void PlayActivateAnimation()
  {
    this.StartCoroutine(this.DoActivateAnimation());
  }

  [DebuggerHidden]
  private IEnumerator DoActivateAnimation()
  {
    // ISSUE: object of a compiler-generated type is created
    return (IEnumerator) new UIMessage.\u003CDoActivateAnimation\u003Ec__Iterator22()
    {
      \u003C\u003Ef__this = this
    };
  }
}
