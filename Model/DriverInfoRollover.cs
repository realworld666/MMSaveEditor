// Decompiled with JetBrains decompiler
// Type: DriverInfoRollover
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DriverInfoRollover : UIDialogBox
{
  public List<UIStat> driverStats = new List<UIStat>();
  private Vector3 mMouseOffset = Vector3.zero;
  public RectTransform rectTransform;
  public Image backing;
  public Flag flag;
  public TextMeshProUGUI driverName;
  public TextMeshProUGUI driverAge;
  public TextMeshProUGUI driverWeigth;
  public TextMeshProUGUI poachability;
  public TextMeshProUGUI marketability;
  public UIAbilityStars stars;
  public GameObject unkownStarsContent;
  public UITeamLogo logo;
  public UIDriverHelmet driverHelmet;
  public UIDriverSeriesIcons driverSeriesIcon;
  public UIPersonContractDetailsWidget contractDetails;
  private Driver mDriver;
  private bool mUseMousePosition;
  private bool mIgnoreRotation;
  private RectTransform mReference;

  public static void ShowTooltip(Driver inDriver, RectTransform inTransform, bool inUseMousePos = false, bool inIgnoreRotation = true)
  {
    UIManager.instance.dialogBoxManager.GetDialog<DriverInfoRollover>().ShowRollover(inDriver, inTransform, inUseMousePos, inIgnoreRotation);
  }

  public static void HideTooltip()
  {
    if (!UIManager.InstanceExists)
      return;
    DriverInfoRollover dialog = UIManager.instance.dialogBoxManager.GetDialog<DriverInfoRollover>();
    if (!((Object) dialog != (Object) null))
      return;
    dialog.HideRollover();
  }

  public void ShowRollover(Driver inDriver, RectTransform inTransform, bool inUseMousePos, bool inIgnoreRotation)
  {
    scSoundManager.BlockSoundEvents = true;
    this.mUseMousePosition = inUseMousePos;
    this.mIgnoreRotation = inIgnoreRotation;
    this.mReference = inTransform;
    this.UpdateTooltipPosition();
    this.Setup(inDriver);
    GameUtility.SetActive(this.gameObject, true);
    scSoundManager.BlockSoundEvents = false;
  }

  public void HideRollover()
  {
    if (!((Object) this.gameObject != (Object) null))
      return;
    GameUtility.SetActive(this.gameObject, false);
  }

  private void Setup(Driver inDriver)
  {
    this.mDriver = inDriver;
    this.contractDetails.Setup((Person) this.mDriver);
    this.driverSeriesIcon.Setup(this.mDriver);
    this.flag.SetNationality(this.mDriver.nationality);
    this.backing.color = this.mDriver.GetTeamColor().primaryUIColour.normal;
    this.driverName.text = this.mDriver.name;
    this.driverAge.text = this.mDriver.GetAge().ToString();
    GameUtility.SetActive(this.driverWeigth.gameObject, this.mDriver.hasWeigthSet);
    this.driverWeigth.text = GameUtility.GetWeightText((float) this.mDriver.weight, 2);
    this.poachability.text = this.mDriver.GetPoachabilityString(Random.Range(0, 5));
    this.marketability.text = this.mDriver.GetMarketabilityString((int) this.mDriver.GetDriverStats().marketability);
    this.logo.SetTeam(this.mDriver.contract.GetTeam());
    this.driverHelmet.SetHelmet(this.mDriver);
    if (this.mDriver.CanShowStats())
    {
      GameUtility.SetActive(this.unkownStarsContent, false);
      GameUtility.SetActive(this.stars.gameObject, true);
      this.stars.SetAbilityStarsData(this.mDriver);
    }
    else
    {
      GameUtility.SetActive(this.unkownStarsContent, true);
      GameUtility.SetActive(this.stars.gameObject, false);
    }
    Dictionary<string, float> stats = PersonStats.GetStats((Person) inDriver, Game.instance.player.team.championship.series);
    int index = 0;
    using (Dictionary<string, float>.Enumerator enumerator = stats.GetEnumerator())
    {
      while (enumerator.MoveNext())
      {
        KeyValuePair<string, float> current = enumerator.Current;
        this.driverStats[index].SetStat(current.Key, current.Value, (Person) this.mDriver);
        ++index;
      }
    }
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
