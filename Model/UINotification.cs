// Decompiled with JetBrains decompiler
// Type: UINotification
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;

public class UINotification : MonoBehaviour
{
  public string notificationName = string.Empty;
  private int mLastCount = -1;
  [SerializeField]
  private GameObject info;
  [SerializeField]
  private TextMeshProUGUI notificationLabel;
  private Notification mNotification;

  private void Start()
  {
    Game.OnGameDataChanged += new Action(this.FindNotification);
  }

  private void OnEnable()
  {
    this.FindNotification();
    this.mLastCount = -1;
    this.Update();
  }

  private void OnDestroy()
  {
    Game.OnGameDataChanged -= new Action(this.FindNotification);
  }

  public void FindNotification()
  {
    if (!Game.IsActive())
      return;
    this.mNotification = Game.instance.notificationManager.GetNotification(this.notificationName);
    this.mLastCount = -1;
    this.Update();
  }

  private void Update()
  {
    if (this.mNotification == null)
    {
      GameUtility.SetActive(this.info, false);
    }
    else
    {
      if (this.mNotification == null || this.mLastCount == this.mNotification.count)
        return;
      if (this.mNotification.count > 0)
      {
        if (!this.info.activeSelf)
          GameUtility.SetActive(this.info, true);
        this.notificationLabel.text = this.mNotification.count.ToString();
      }
      else if (this.info.activeSelf)
        GameUtility.SetActive(this.info, false);
      this.mLastCount = this.mNotification.count;
    }
  }
}
