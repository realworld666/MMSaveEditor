// Decompiled with JetBrains decompiler
// Type: UIPlayerApplicationEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPlayerApplicationEntry : MonoBehaviour
{
  public Button button;
  public TextMeshProUGUI teamLabel;
  public TextMeshProUGUI championshipPositionLabel;
  public GameObject[] status;
  private PlayerJobApplication mApplication;
  private Team mTeam;
  private int mPosition;

  public void Setup(PlayerJobApplication inApplication)
  {
    this.button.onClick.RemoveAllListeners();
    this.button.onClick.AddListener(new UnityAction(this.OnButton));
    this.mApplication = inApplication;
    this.mTeam = this.mApplication.team;
    this.teamLabel.text = this.mTeam.name;
    bool flag1 = this.mTeam.championship.eventNumber > 0;
    bool flag2 = this.mTeam.championship.InPreseason();
    this.mPosition = this.mTeam.championship.standings.GetEntry((Entity) this.mTeam).GetCurrentChampionshipPosition();
    StringVariableParser.ordinalNumberString = GameUtility.FormatForPosition(this.mPosition, (string) null);
    this.championshipPositionLabel.text = !flag2 ? (!flag1 ? Localisation.LocaliseID("PSG_10010360", (GameObject) null) : Localisation.LocaliseID("PSG_10011093", (GameObject) null)) : Localisation.LocaliseID("PSG_10008793", (GameObject) null);
    for (int index = 0; index < this.status.Length; ++index)
      GameUtility.SetActive(this.status[index], (PlayerJobApplication.Status) index == this.mApplication.status);
  }

  private void OnButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (this.mTeam == null)
      return;
    UIManager.instance.ChangeScreen("TeamScreen", (Entity) this.mTeam, UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
  }
}
