// Decompiled with JetBrains decompiler
// Type: MessageAction_ChangeScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using FullSerializer;
using System;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class MessageAction_ChangeScreen : MessageAction
{
  private string mScreenName = string.Empty;

  public void SetScreen(string inScreenName)
  {
    this.mScreenName = inScreenName;
  }

  public void DoAction(Message inMessage)
  {
    UIManager.instance.ChangeScreen(this.mScreenName, UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
  }
}
