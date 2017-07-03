// Decompiled with JetBrains decompiler
// Type: UITextLinkHandler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITextLinkHandler : MonoBehaviour
{
  private TextMeshProUGUI[] mTextElements = new TextMeshProUGUI[0];
  private int mCurrentLink = -1;

  public void Awake()
  {
    this.mTextElements = this.gameObject.GetComponentsInChildren<TextMeshProUGUI>(true);
    for (int index = 0; index < this.mTextElements.Length; ++index)
    {
      TextMeshProUGUI mTextElement = this.mTextElements[index];
      if (mTextElement.raycastTarget)
        mTextElement.gameObject.AddComponent<UITextLinkEventHandler>().Setup(this);
    }
  }

  public void Update()
  {
    for (int index = 0; index < this.mTextElements.Length; ++index)
    {
      GameObject gameObject = this.mTextElements[index].gameObject;
      if (gameObject.activeSelf && UIManager.instance.IsObjectAtMousePosition(gameObject))
      {
        this.mCurrentLink = TMP_TextUtilities.FindIntersectingLink((TMP_Text) this.mTextElements[index], Input.mousePosition, UIManager.instance.UICamera);
        if (this.mCurrentLink != -1)
        {
          UIManager.instance.cursorManager.SetState(CursorManager.State.Clickable);
          break;
        }
      }
    }
  }

  public void FindLinkData()
  {
    TMP_LinkInfo inLink = new TMP_LinkInfo();
    bool flag = false;
    for (int index = 0; index < this.mTextElements.Length; ++index)
    {
      TextMeshProUGUI mTextElement = this.mTextElements[index];
      if (mTextElement.isActiveAndEnabled)
      {
        int intersectingLink = TMP_TextUtilities.FindIntersectingLink((TMP_Text) mTextElement, Input.mousePosition, UIManager.instance.UICamera);
        if (intersectingLink != -1)
        {
          inLink = mTextElement.GetTextInfo(mTextElement.text).linkInfo[intersectingLink];
          flag = true;
          break;
        }
      }
    }
    if (!flag)
      return;
    this.ExecuteLink(inLink);
  }

  private void ExecuteLink(TMP_LinkInfo inLink)
  {
    string[] strArray = inLink.GetLinkID().Split('=');
    string key = strArray[0];
    if (key == null)
      return;
    // ISSUE: reference to a compiler-generated field
    if (UITextLinkHandler.\u003C\u003Ef__switch\u0024map3C == null)
    {
      // ISSUE: reference to a compiler-generated field
      UITextLinkHandler.\u003C\u003Ef__switch\u0024map3C = new Dictionary<string, int>(3)
      {
        {
          "PersonScreen",
          0
        },
        {
          "TeamScreen",
          1
        },
        {
          "WebsiteLinkOverlay",
          2
        }
      };
    }
    int num;
    // ISSUE: reference to a compiler-generated field
    if (!UITextLinkHandler.\u003C\u003Ef__switch\u0024map3C.TryGetValue(key, out num))
      return;
    switch (num)
    {
      case 0:
        Guid inID1 = new Guid(this.PrepareGUID(strArray[1]));
        Person entity1 = Game.instance.entityManager.GetEntity(inID1) as Person;
        if (entity1 is Player && UIManager.instance.IsScreenLoaded("PlayerScreen"))
        {
          UIManager.instance.ChangeScreen("PlayerScreen", (Entity) entity1, UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
          break;
        }
        if (entity1 is Driver)
        {
          if (UIManager.instance.currentScreen is SessionHUD)
          {
            App.instance.cameraManager.SetTarget((Vehicle) Game.instance.vehicleManager.GetVehicle((Driver) entity1), CameraManager.Transition.Smooth);
            break;
          }
          UIManager.instance.ChangeScreen("DriverScreen", (Entity) entity1, UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
          break;
        }
        if (!(entity1 is Engineer) && !(entity1 is Mechanic) && !(entity1 is TeamPrincipal))
          break;
        UIManager.instance.ChangeScreen("StaffDetailsScreen", (Entity) entity1, UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
        break;
      case 1:
        Guid inID2 = new Guid(this.PrepareGUID(strArray[1]));
        Team entity2 = Game.instance.entityManager.GetEntity(inID2) as Team;
        if (UIManager.instance.currentScreen is SessionHUD)
          break;
        UIManager.instance.ChangeScreen("TeamScreen", (Entity) entity2, UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
        break;
      case 2:
        App.instance.modManager.OpenWebpageLinkOnOverlay(strArray[1]);
        break;
    }
  }

  private string PrepareGUID(string inString)
  {
    inString = inString.Trim();
    if (inString.Length > 36)
      inString = inString.Remove(36);
    return inString;
  }
}
