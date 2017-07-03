// Decompiled with JetBrains decompiler
// Type: NavigationButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NavigationButton : MonoBehaviour
{
  public string notificationName = string.Empty;
  public string goToScreen = string.Empty;
  public string openDialogBox = string.Empty;
  public List<string> screensToToggleOn = new List<string>();
  public GameObject subMenuContentHolder;
  public Animator subMenu;
  public GameObject notificationGameObject;
  public TextMeshProUGUI notificationCountLabel;
  public Image notificationProgressBar;
  private Notification mNotification;
  private int currentNotificationCount;
  private Toggle mToggle;
  private Button mButton;
  private Animator mAnimator;

  private void Awake()
  {
    Game.OnGameDataChanged += new Action(this.FindNotification);
    UIManager.OnScreenChange += new Action(this.ActivateSubMenu);
    this.mToggle = this.GetComponent<Toggle>();
    this.mButton = this.GetComponent<Button>();
    this.mAnimator = this.GetComponent<Animator>();
    if ((UnityEngine.Object) this.mToggle != (UnityEngine.Object) null)
      this.mToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggle));
    if (!((UnityEngine.Object) this.mButton != (UnityEngine.Object) null))
      return;
    this.mButton.onClick.AddListener(new UnityAction(this.OnButton));
  }

  private void Update()
  {
    if (this.mNotification != null && (UnityEngine.Object) this.notificationGameObject != (UnityEngine.Object) null)
    {
      GameUtility.SetActive(this.notificationGameObject, this.mNotification.count > 0);
      if (this.notificationGameObject.activeSelf && this.currentNotificationCount != this.mNotification.count)
      {
        this.currentNotificationCount = this.mNotification.count;
        this.notificationCountLabel.text = this.currentNotificationCount.ToString();
      }
      if ((UnityEngine.Object) this.notificationProgressBar != (UnityEngine.Object) null)
      {
        this.notificationProgressBar.transform.parent.gameObject.SetActive(!Mathf.Approximately(this.mNotification.progress, 0.0f));
        if (this.notificationProgressBar.gameObject.activeSelf)
          GameUtility.SetImageFillAmountIfDifferent(this.notificationProgressBar, this.mNotification.progress, 1f / 512f);
      }
    }
    if (this.mToggle.isOn != this.HighlightIfOnScreen())
    {
      this.mToggle.onValueChanged.RemoveAllListeners();
      this.mToggle.isOn = this.HighlightIfOnScreen();
      this.mToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggle));
    }
    if (this.mAnimator.GetBool("IsOn") == this.mToggle.isOn)
      return;
    this.mAnimator.SetBool("IsOn", this.mToggle.isOn);
  }

  private void LateUpdate()
  {
    if (!((UnityEngine.Object) this.subMenu != (UnityEngine.Object) null) || !this.HighlightIfOnScreen() || this.subMenu.GetBool("Active") == ((UnityEngine.Object) this.subMenuContentHolder != (UnityEngine.Object) null))
      return;
    this.subMenu.SetBool("Active", (UnityEngine.Object) this.subMenuContentHolder != (UnityEngine.Object) null);
  }

  public virtual bool HighlightIfOnScreen()
  {
    if ((UnityEngine.Object) UIManager.instance.currentScreen == (UnityEngine.Object) null)
      return false;
    string currentScreenName = UIManager.instance.currentScreen_name;
    string screenScreenName = UIManager.instance.currentScreen_screenName;
    if (this.goToScreen == screenScreenName || this.goToScreen == currentScreenName)
      return true;
    for (int index = 0; index < this.screensToToggleOn.Count; ++index)
    {
      if ((UnityEngine.Object) UIManager.instance.currentScreen != (UnityEngine.Object) null && (currentScreenName == this.screensToToggleOn[index] || screenScreenName == this.screensToToggleOn[index]))
        return true;
    }
    return false;
  }

  private void OnEnable()
  {
    if (!Game.IsActive())
      return;
    this.FindNotification();
    this.Update();
  }

  private void OnDestroy()
  {
    UIManager.OnScreenChange -= new Action(this.ActivateSubMenu);
    Game.OnGameDataChanged -= new Action(this.FindNotification);
  }

  public void FindNotification()
  {
    if (!Game.IsActive())
      return;
    this.mNotification = Game.instance.notificationManager.GetNotification(this.notificationName);
    this.currentNotificationCount = -1;
    if (!((UnityEngine.Object) this.notificationGameObject != (UnityEngine.Object) null))
      return;
    this.notificationGameObject.SetActive(false);
  }

  public void OnToggle(bool inValue)
  {
    if (inValue)
    {
      this.OnButton();
    }
    else
    {
      if (!this.HighlightIfOnScreen() || !(UIManager.instance.currentScreen_screenName != this.goToScreen))
        return;
      this.OnButton();
    }
  }

  private void ActivateSubMenu()
  {
    if (!((UnityEngine.Object) this.subMenuContentHolder != (UnityEngine.Object) null))
      return;
    this.subMenuContentHolder.SetActive(this.HighlightIfOnScreen());
  }

  public virtual void OnButton()
  {
    UIManager.instance.navigationBars.bottomBar.calendarButton.SetCalendarVisibility(false);
    if (!string.IsNullOrEmpty(this.goToScreen))
    {
      UIManager.instance.ChangeScreen(this.goToScreen, UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
    }
    else
    {
      if (string.IsNullOrEmpty(this.openDialogBox))
        return;
      UIManager.instance.dialogBoxManager.Show(this.openDialogBox);
    }
  }
}
