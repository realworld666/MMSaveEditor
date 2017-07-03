// Decompiled with JetBrains decompiler
// Type: UITopBarNotificationWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UITopBarNotificationWidget : MonoBehaviour
{
  private int mLastNotificationCount = -1;
  public GameObject[] dropdownMouseContainers;
  public GameObject dropdownContainer;
  public GameObject graphicsContainer;
  public TextMeshProUGUI numberOfNotificationsLabel;

  public void Update()
  {
    if (this.dropdownContainer.activeSelf)
    {
      bool flag = false;
      for (int index = 0; index < this.dropdownMouseContainers.Length; ++index)
      {
        if (UIManager.instance.IsObjectAtMousePosition(this.dropdownMouseContainers[index]))
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        this.dropdownContainer.SetActive(false);
    }
    this.UpdateSelf();
  }

  private void UpdateSelf()
  {
    if (Game.instance == null)
      return;
    int visibleCount = Game.instance.notificationManager.visibleCount;
    if (visibleCount != this.mLastNotificationCount)
    {
      this.mLastNotificationCount = visibleCount;
      this.numberOfNotificationsLabel.text = this.mLastNotificationCount.ToString();
    }
    GameState currentState = App.instance.gameStateManager.currentState;
    bool flag = currentState is FrontendState || currentState is PreSeasonState || currentState is PostEventFrontendState;
    GameUtility.SetActive(this.graphicsContainer, visibleCount != 0 && flag);
  }
}
