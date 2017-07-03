// Decompiled with JetBrains decompiler
// Type: RaceEventLoadingScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RaceEventLoadingScreen : UIScreen
{
  [SerializeField]
  private TextMeshProUGUI tipText;
  [SerializeField]
  private TextMeshProUGUI roundTitle;
  [SerializeField]
  private TextMeshProUGUI circuitName;
  [SerializeField]
  private Flag circuitFlag;
  [SerializeField]
  private UITrackLayout trackLayout;
  [SerializeField]
  private RawImage image;
  private Circuit mCircuit;

  public override void OnStart()
  {
    base.OnStart();
  }

  public override void OnEnter()
  {
    base.OnEnter();
    this.showNavigationBars = false;
    this.mCircuit = Game.instance.sessionManager.eventDetails.circuit;
    this.SetBackgroundImage();
    this.SetCircuitDetails();
    GameUtility.SetLoadingTip(this.tipText);
    scSoundManager.LoadSoundBank_RaceSession();
  }

  public override void OnExit()
  {
    base.OnExit();
    UIManager.instance.ClearNavigationStacks();
  }

  private void SetBackgroundImage()
  {
    string path = "UI/LoadingScreenImages/" + this.mCircuit.spriteName;
    Texture texture = UnityEngine.Resources.Load<Texture>(path);
    if ((Object) texture == (Object) null)
      Debug.LogWarningFormat("Unable to find loading screen image {0} for track {1}. Background will be blank.", new object[2]
      {
        (object) path,
        (object) this.mCircuit.locationName
      });
    this.image.texture = texture;
  }

  private void SetCircuitDetails()
  {
    Championship championship = Game.instance.player.team.championship;
    int eventNumberForUi = championship.eventNumberForUI;
    int eventCount = championship.eventCount;
    this.circuitFlag.SetNationality(this.mCircuit.nationality);
    StringVariableParser.randomCircuit = this.mCircuit;
    this.circuitName.text = Localisation.LocaliseID("PSG_10010221", (GameObject) null);
    this.trackLayout.SetCircuitIcon(this.mCircuit);
    if (Game.instance.isCareer)
    {
      StringVariableParser.intValue1 = eventNumberForUi;
      StringVariableParser.intValue2 = eventCount;
      this.roundTitle.text = Localisation.LocaliseID("PSG_10002217", (GameObject) null);
    }
    else
      this.roundTitle.text = Localisation.LocaliseID("PSG_10004577", (GameObject) null);
  }
}
