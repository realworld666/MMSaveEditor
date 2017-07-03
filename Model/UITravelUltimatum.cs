// Decompiled with JetBrains decompiler
// Type: UITravelUltimatum
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;

public class UITravelUltimatum : UITravelStepOption
{
  public UICharacterPortrait chairmanPortrait;
  public TextMeshProUGUI chairmanName;
  public TextMeshProUGUI ultimatumObjective;

  public override void OnStart()
  {
  }

  public override void Setup()
  {
    Chairman chairman = Game.instance.player.team.chairman;
    this.chairmanPortrait.SetPortrait((Person) chairman);
    this.chairmanName.text = chairman.name;
    this.RefreshText();
  }

  public override void RefreshText()
  {
    this.ultimatumObjective.text = GameUtility.FormatForPositionOrAbove(Game.instance.player.team.chairman.ultimatum.positionExpected, (string) null);
  }

  public override bool IsReady()
  {
    return true;
  }
}
