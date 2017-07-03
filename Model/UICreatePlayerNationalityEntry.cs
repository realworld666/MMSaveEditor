// Decompiled with JetBrains decompiler
// Type: UICreatePlayerNationalityEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UICreatePlayerNationalityEntry : MonoBehaviour
{
  public Toggle toggle;
  public Flag countryFlag;
  public TextMeshProUGUI countryName;
  public UICreatePlayerDetailsWidget widget;
  public CreateTeamOptionNameOrigin teamWidget;
  private Nationality mNationality;

  public void Setup(Nationality inNationality)
  {
    if (!(inNationality != (Nationality) null))
      return;
    this.mNationality = inNationality;
    this.toggle.onValueChanged.RemoveAllListeners();
    this.toggle.onValueChanged.AddListener((UnityAction<bool>) (param0 => this.OnToggle()));
    this.countryFlag.SetNationality(this.mNationality);
    this.countryName.text = this.mNationality.localisedCountry;
  }

  private void OnToggle()
  {
    if (!this.toggle.isOn)
      return;
    if ((Object) this.widget != (Object) null)
    {
      this.widget.SelectCountry(this.mNationality);
    }
    else
    {
      if (!((Object) this.teamWidget != (Object) null))
        return;
      this.teamWidget.SelectCountry(this.mNationality);
    }
  }
}
