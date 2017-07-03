// Decompiled with JetBrains decompiler
// Type: UIRacePodiumTrophyWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class UIRacePodiumTrophyWidget : MonoBehaviour
{
  public float dragModifier = 1.5f;
  public float autoRotateAngleSecond = 20f;
  public float autoRotateCooldown = 2f;
  public float maxRotationSpeed = 7.5f;
  private float mRotationYSpeedModifier = 0.01f;
  public GameObject[] singleSeaterTrophies;
  public GameObject[] gtTrophies;
  public GameObject currentTrophy;
  public ParticleSystem confettiPrimary;
  public ParticleSystem confettiSecondary;
  private float mOrbitSpeedX;
  private float mRotationTimer;
  private bool mManuallySetConfettiColor;

  public void Setup()
  {
    this.RegisterEvents();
    this.SetTrophy();
    this.SetScene();
  }

  public void Setup(Championship inChampionship)
  {
    this.RegisterEvents();
    this.SetTrophy(inChampionship);
    this.SetScene();
  }

  public void Setup(Trophy inTrophy)
  {
    this.RegisterEvents();
    this.SetTrophy(inTrophy);
    this.SetScene();
  }

  public void SetupNoTrophy()
  {
    this.RegisterEvents();
    this.SetScene();
  }

  private void RegisterEvents()
  {
    IT_Gesture.onDraggingE -= new IT_Gesture.DraggingHandler(this.OnDragging);
    IT_Gesture.onDraggingE += new IT_Gesture.DraggingHandler(this.OnDragging);
  }

  private void SetScene()
  {
    if (!this.mManuallySetConfettiColor)
    {
      TeamColor teamColor = Game.instance.player.team.GetTeamColor();
      this.confettiPrimary.startColor = teamColor.primaryUIColour.normal;
      this.confettiSecondary.startColor = teamColor.secondaryUIColour.normal;
    }
    this.confettiPrimary.Simulate(15f, false, true);
    this.confettiPrimary.Play();
    this.confettiSecondary.Simulate(15f, false, true);
    this.confettiSecondary.Play();
    this.mOrbitSpeedX = 0.0f;
    this.mRotationTimer = this.autoRotateCooldown;
  }

  private void Update()
  {
    if (!((Object) this.currentTrophy != (Object) null))
      return;
    this.mRotationTimer += GameTimer.deltaTime;
    this.mOrbitSpeedX *= (float) (1.0 - (double) GameTimer.deltaTime * 3.0);
    if ((double) this.mRotationTimer >= (double) this.autoRotateCooldown)
      this.mOrbitSpeedX = this.autoRotateAngleSecond * GameTimer.deltaTime;
    this.mOrbitSpeedX = Mathf.Clamp(this.mOrbitSpeedX, -this.maxRotationSpeed, this.maxRotationSpeed);
    this.currentTrophy.transform.rotation = Quaternion.Euler(0.0f, this.currentTrophy.transform.rotation.eulerAngles.y + -this.mOrbitSpeedX, 0.0f);
  }

  public void SetConfettiColor(Color inPrimaryColor, Color inSecondaryColor)
  {
    this.confettiPrimary.startColor = inPrimaryColor;
    this.confettiSecondary.startColor = inSecondaryColor;
    this.mManuallySetConfettiColor = true;
  }

  private void SetTrophy()
  {
    int inPosition = 0;
    Championship championship = (Championship) null;
    List<RaceEventResults.ResultData> resultData = Game.instance.sessionManager.eventDetails.results.GetResultsForSession(SessionDetails.SessionType.Race).resultData;
    int count = resultData.Count;
    for (int index = 0; index < count; ++index)
    {
      if (resultData[index].driver.IsPlayersDriver())
      {
        championship = resultData[index].driver.contract.GetTeam().championship;
        inPosition = resultData[index].position;
        break;
      }
    }
    if (championship == null)
      return;
    if (championship.series == Championship.Series.GTSeries)
      this.SetPositionTrophyForSeriesActive(ref this.gtTrophies, inPosition);
    else
      this.SetPositionTrophyForSeriesActive(ref this.singleSeaterTrophies, inPosition);
  }

  private void SetPositionTrophyForSeriesActive(ref GameObject[] inTrophies, int inPosition)
  {
    if (inPosition <= 0 || inPosition - 1 >= inTrophies.Length)
      return;
    this.SetTrophyActive(inTrophies[inPosition - 1]);
  }

  private void SetTrophy(Championship inChampionship)
  {
    this.SetChampionshipIdTrophyActive(inChampionship.championshipID);
  }

  private void SetTrophy(Trophy inTrophy)
  {
    this.SetChampionshipIdTrophyActive(inTrophy.championship.championshipID);
  }

  private void SetChampionshipIdTrophyActive(int inChampionshipId)
  {
    if (inChampionshipId >= this.singleSeaterTrophies.Length)
      this.SetTrophyActive(this.gtTrophies[inChampionshipId - this.singleSeaterTrophies.Length]);
    else
      this.SetTrophyActive(this.singleSeaterTrophies[inChampionshipId]);
  }

  private void SetTrophyActive(GameObject inTrophy)
  {
    this.currentTrophy = inTrophy;
    for (int index = 0; index < this.singleSeaterTrophies.Length; ++index)
      GameUtility.SetActive(this.singleSeaterTrophies[index], (Object) this.singleSeaterTrophies[index] == (Object) this.currentTrophy);
    for (int index = 0; index < this.gtTrophies.Length; ++index)
      GameUtility.SetActive(this.gtTrophies[index], (Object) this.gtTrophies[index] == (Object) this.currentTrophy);
  }

  private void OnDragging(DragInfo dragInfo)
  {
    this.mOrbitSpeedX = dragInfo.delta.x * this.mRotationYSpeedModifier * GameTimer.deltaTime * (float) Screen.width * this.dragModifier;
    this.mRotationTimer = 0.0f;
  }

  private void OnDisable()
  {
    IT_Gesture.onDraggingE -= new IT_Gesture.DraggingHandler(this.OnDragging);
  }
}
