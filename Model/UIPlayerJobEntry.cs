// Decompiled with JetBrains decompiler
// Type: UIPlayerJobEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPlayerJobEntry : MonoBehaviour
{
  public Button button;
  public UIJobSecurity jobSecurity;
  public TextMeshProUGUI teamName;
  public TextMeshProUGUI principalName;
  public TextMeshProUGUI championshipPosition;
  public Button applyButton;
  public TextMeshProUGUI applyButtonLabel;
  public GameObject vacant;
  public GameObject security;
  public GameObject available;
  public GameObject unavailable;
  private Team mTeam;
  private int mPosition;

  public void Setup(Team inTeam)
  {
    if (inTeam == null)
      return;
    this.mTeam = inTeam;
    bool flag1 = this.mTeam.championship.eventNumber > 0;
    bool flag2 = App.instance.gameStateManager.currentState.type == GameState.Type.PreSeasonState | App.instance.gameStateManager.currentState.type == GameState.Type.PreSeasonTestingState;
    this.mPosition = this.mTeam.championship.standings.GetEntry((Entity) this.mTeam).GetCurrentChampionshipPosition();
    this.teamName.text = this.mTeam.name;
    this.principalName.text = this.mTeam.teamPrincipal.name;
    if (flag2)
      this.championshipPosition.text = Localisation.LocaliseID("PSG_10008793", (GameObject) null);
    else if (flag1)
    {
      StringVariableParser.ordinalNumberString = GameUtility.FormatForPosition(this.mPosition, (string) null);
      this.championshipPosition.text = Localisation.LocaliseID("PSG_10011128", (GameObject) null);
    }
    else
      this.championshipPosition.text = Localisation.LocaliseID("PSG_10010360", (GameObject) null);
    TeamPrincipal.JobSecurity jobSecurity = this.mTeam.teamPrincipal.jobSecurity;
    bool inIsActive1 = this.mTeam.contractManager.GetSlot(Contract.Job.TeamPrincipal).IsVacant();
    bool inIsActive2 = this.mTeam.contractManager.GetSlot(Contract.Job.TeamPrincipal).canPlayerApply();
    GameUtility.SetActive(this.available, inIsActive2);
    GameUtility.SetActive(this.applyButton.gameObject, this.available.activeSelf);
    if (this.available.activeSelf)
    {
      bool flag3 = Game.instance.player.HasAppliedForTeam(this.mTeam);
      this.applyButton.onClick.RemoveAllListeners();
      this.applyButton.onClick.AddListener(new UnityAction(this.OnApplyButton));
      this.applyButton.interactable = !flag3;
      this.applyButtonLabel.text = flag3 ? Localisation.LocaliseID("PSG_10006968", (GameObject) null) : Localisation.LocaliseID("PSG_10004598", (GameObject) null);
    }
    GameUtility.SetActive(this.security, !inIsActive1);
    if (this.security.activeSelf)
      this.jobSecurity.SetJobSecurity(jobSecurity);
    GameUtility.SetActive(this.vacant, inIsActive1);
    GameUtility.SetActive(this.unavailable, !inIsActive2);
    this.button.onClick.RemoveAllListeners();
    this.button.onClick.AddListener(new UnityAction(this.OnButton));
  }

  private void OnApplyButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    UIManager.instance.dialogBoxManager.GetDialog<ApproachDialogBox>().Show((Person) this.mTeam.chairman, ApproachDialogBox.ApproachType.SignNewContract);
  }

  private void OnButton()
  {
    UIManager.instance.ChangeScreen("TeamScreen", (Entity) this.mTeam, UIManager.ScreenTransition.None, 0.0f, UIManager.NavigationType.Normal);
  }
}
