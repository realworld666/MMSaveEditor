// Decompiled with JetBrains decompiler
// Type: MessageAction_SetDriverMentalState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class MessageAction_SetDriverMentalState : MessageAction
{
  private DriverMentalState.Emotion mEmotion = DriverMentalState.Emotion.Confident;

  public void SetEmotion(DriverMentalState.Emotion inEmotion)
  {
    this.mEmotion = inEmotion;
  }

  public void DoAction(Message inMessage)
  {
    if (inMessage.sender == null || !(inMessage.sender is Driver))
      return;
    (inMessage.sender as Driver).mentalState.SetEmotion(this.mEmotion);
  }
}
