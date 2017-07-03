// Decompiled with JetBrains decompiler
// Type: UIScoutingFilter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIScoutingFilter : MonoBehaviour
{
  private bool mAsc = true;
  public UIScoutingFilter.Type filterType;
  public Toggle toggle;
  public GameObject arrowUp;
  public GameObject arrowDown;
  public UIScoutingSearchResultsWidget widget;

  public void OnStart()
  {
    this.mAsc = this.GetDefaultAsc();
    this.toggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggle));
  }

  private void OnToggle(bool inValue)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (!inValue)
    {
      this.mAsc = this.GetDefaultAsc();
      this.widget.ApplyFilter(this.filterType, this.mAsc);
    }
    else
    {
      this.mAsc = !this.mAsc;
      this.widget.ApplyFilter(this.filterType, this.mAsc);
    }
    this.SetIcon();
  }

  public void SetIcon()
  {
    GameUtility.SetActive(this.arrowUp, this.toggle.isOn && this.mAsc);
    GameUtility.SetActive(this.arrowDown, this.toggle.isOn && !this.mAsc);
  }

  private bool GetDefaultAsc()
  {
    switch (this.filterType)
    {
      case UIScoutingFilter.Type.Ability:
      case UIScoutingFilter.Type.RaceCost:
      case UIScoutingFilter.Type.BreakClause:
      case UIScoutingFilter.Type.ContractEnds:
        return true;
      default:
        return false;
    }
  }

  public enum Type
  {
    Name,
    Nationality,
    Age,
    Ability,
    Team,
    RacingSeries,
    RaceCost,
    BreakClause,
    ContractEnds,
  }
}
