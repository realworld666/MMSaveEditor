// Decompiled with JetBrains decompiler
// Type: UITutorialMarkAllMessagesRead
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UITutorialMarkAllMessagesRead : UITutorialBespokeScript
{
  protected override void Activate()
  {
    UITutorialMarkAllMessagesRead.MarkAllMessagesReadForTutorial();
  }

  public static void MarkAllMessagesReadForTutorial()
  {
    if (!Game.IsActive())
      return;
    Game.instance.messageManager.DeliverDelayedMessages();
    Game.instance.messageManager.MarkAllMessagesAsRead();
    TextDynamicData textDynamicData = new TextDynamicData();
    textDynamicData.SetMessageTextFields(Localisation.LocaliseID("PSG_10012170", (GameObject) null));
    for (int inIndex = 0; inIndex < Game.instance.messageManager.count; ++inIndex)
    {
      Message entity = Game.instance.messageManager.GetEntity(inIndex);
      if (entity.mustRespond)
      {
        entity.messageResponseData = textDynamicData;
        entity.SetResponded();
        entity.SetPriorityType(Message.Priority.Normal);
      }
    }
    UIManager.instance.navigationBars.bottomBar.MarkContinueButtonForUpdate();
  }
}
