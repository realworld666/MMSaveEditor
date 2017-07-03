// Decompiled with JetBrains decompiler
// Type: StaffInfoRollover
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StaffInfoRollover : UIDialogBox
{
  public List<UIStat> driverStats = new List<UIStat>();
  private Vector3 mMouseOffset = Vector3.zero;
  public RectTransform rectTransform;
  public Flag flag;
  public Image backing;
  public TextMeshProUGUI personName;
  public TextMeshProUGUI personAge;
  public TextMeshProUGUI poachability;
  public TextMeshProUGUI marketability;
  public UIAbilityStars stars;
  public UITeamLogo logo;
  public GameObject mechanicIcon;
  public GameObject engineerIcon;
  public UIPersonContractDetailsWidget contractDetails;
  private Person mPerson;
  private bool mUseMousePosition;
  private bool mIgnoreRotation;
  private RectTransform mReference;

  public static void ShowTooltip(Person inPerson, RectTransform inTransform, bool inUseMousePos = false, bool inIgnoreRotation = true)
  {
    scSoundManager.BlockSoundEvents = true;
    UIManager.instance.dialogBoxManager.GetDialog<StaffInfoRollover>().ShowRollover(inPerson, inTransform, inUseMousePos, inIgnoreRotation);
    scSoundManager.BlockSoundEvents = false;
  }

  public static void HideTooltip()
  {
    UIManager.instance.dialogBoxManager.GetDialog<StaffInfoRollover>().HideRollover();
  }

  public void ShowRollover(Person inPerson, RectTransform inTransform, bool inUseMousePos, bool inIgnoreRotation)
  {
    this.mUseMousePosition = inUseMousePos;
    this.mIgnoreRotation = inIgnoreRotation;
    this.mReference = inTransform;
    this.UpdateTooltipPosition();
    this.Setup(inPerson);
    GameUtility.SetActive(this.gameObject, true);
  }

  public void HideRollover()
  {
    if (!((Object) this.gameObject != (Object) null))
      return;
    GameUtility.SetActive(this.gameObject, false);
  }

  private void Setup(Person inPerson)
  {
    this.mPerson = inPerson;
    this.contractDetails.Setup(this.mPerson);
    this.backing.color = this.mPerson.GetTeamColor().primaryUIColour.normal;
    this.flag.SetNationality(this.mPerson.nationality);
    this.personName.text = this.mPerson.name;
    this.personAge.text = this.mPerson.GetAge().ToString();
    this.poachability.text = this.mPerson.GetPoachabilityString(Random.Range(0, 5));
    this.marketability.text = this.mPerson.GetMarketabilityString(0);
    this.logo.SetTeam(this.mPerson.contract.GetTeam());
    GameUtility.SetActive(this.mechanicIcon, this.mPerson is Mechanic);
    GameUtility.SetActive(this.engineerIcon, this.mPerson is Engineer);
    GameUtility.SetActive(this.stars.gameObject, true);
    this.stars.SetAbilityStarsData(this.mPerson);
    Dictionary<string, float> stats = PersonStats.GetStats(this.mPerson, Game.instance.player.team.championship.series);
    int index1 = 0;
    using (Dictionary<string, float>.Enumerator enumerator = stats.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        KeyValuePair<string, float> current = enumerator.Current;
        UIStat driverStat = this.driverStats[index1];
        driverStat.SetStat(current.Key, current.Value, this.mPerson);
        GameUtility.SetActive(driverStat.gameObject, true);
        ++index1;
      }
    }
    for (int index2 = index1; index2 < this.driverStats.Count; ++index2)
      GameUtility.SetActive(this.driverStats[index2].gameObject, false);
  }

  private void UpdateTooltipPosition()
  {
    GameUtility.SetTooltipTransformInsideScreen(this.rectTransform, !this.mUseMousePosition ? this.mReference : (RectTransform) null, this.mMouseOffset, this.mIgnoreRotation, (RectTransform) null);
  }

  private void Update()
  {
    if (!this.mUseMousePosition)
      return;
    this.UpdateTooltipPosition();
  }
}
