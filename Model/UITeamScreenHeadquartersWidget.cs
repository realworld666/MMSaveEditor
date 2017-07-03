// Decompiled with JetBrains decompiler
// Type: UITeamScreenHeadquartersWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UITeamScreenHeadquartersWidget : MonoBehaviour
{
  public Flag flag;
  public TextMeshProUGUI locationName;
  public TextMeshProUGUI totalStaff;
  public TextMeshProUGUI factoryLevel;
  public TextMeshProUGUI designCenterLevel;
  public UIAbilityStars stars;
  public UITeamColor carColor;
  public Button HQButton;
  private Headquarters mHQ;

  private void Awake()
  {
    this.HQButton.onClick.AddListener(new UnityAction(this.GoToHQScreen));
  }

  public void Setup(Headquarters inHQ)
  {
    this.mHQ = inHQ;
    this.carColor.SetTeamColor(this.mHQ.team.GetTeamColor());
    this.flag.SetNationality(this.mHQ.nationality);
    this.locationName.text = Localisation.LocaliseID(this.mHQ.team.locationID, (GameObject) null);
    this.totalStaff.text = this.mHQ.GetStaffCount().ToString();
    StringVariableParser.knowledgeLevel = this.mHQ.GetBuilding(HQsBuildingInfo.Type.Factory).currentLevelUIAsInt;
    this.factoryLevel.text = Localisation.LocaliseID("PSG_10009816", (GameObject) null);
    StringVariableParser.knowledgeLevel = this.mHQ.GetBuilding(HQsBuildingInfo.Type.DesignCentre).currentLevelUIAsInt;
    this.designCenterLevel.text = Localisation.LocaliseID("PSG_10009816", (GameObject) null);
    float num = Mathf.Round((float) ((double) this.mHQ.GetNormalizedRating() * 5.0 * 2.0)) / 2f;
    this.stars.SetStarsValue(num, num);
    this.HQButton.interactable = App.instance.gameStateManager.currentState is FrontendState || App.instance.gameStateManager.currentState is PreSeasonState;
    GameUtility.SetActive(this.HQButton.gameObject, this.mHQ.team.IsPlayersTeam());
  }

  private void GoToHQScreen()
  {
    UIManager.instance.ChangeScreen("HeadquartersScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
  }
}
