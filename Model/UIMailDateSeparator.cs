// Decompiled with JetBrains decompiler
// Type: UIMailDateSeparator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIMailDateSeparator : MonoBehaviour
{
  public MailScreen.TimeSeparator timeFilter = MailScreen.TimeSeparator.ThisWeek;
  public List<UIMailEntry> messageEntries = new List<UIMailEntry>();
  public TextMeshProUGUI titleLabel;
  public Toggle button;
  public GameObject container;
  private bool mUpdateFiltersFlag;

  private void Awake()
  {
    Localisation.OnLanguageChange += new Action(this.RefreshText);
  }

  private void Start()
  {
    this.button.onValueChanged.AddListener((UnityAction<bool>) (value => this.SetTimeFilter(value)));
  }

  private void OnDestroy()
  {
    Localisation.OnLanguageChange -= new Action(this.RefreshText);
  }

  private void RefreshText()
  {
    this.titleLabel.text = Localisation.LocaliseEnum((Enum) this.timeFilter);
  }

  public void SetActive(bool inValue)
  {
    GameUtility.SetActive(this.gameObject, inValue);
  }

  public void SetEntriesActive(bool inValue)
  {
    for (int index = 0; index < this.messageEntries.Count; ++index)
      this.messageEntries[index].gameObject.SetActive(inValue);
  }

  public void Setup(MailScreen.TimeSeparator inFilter)
  {
    this.timeFilter = inFilter;
    this.titleLabel.text = Localisation.LocaliseEnum((Enum) this.timeFilter);
  }

  private void SetTimeFilter(bool inValue)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.mUpdateFiltersFlag = true;
  }

  private void LateUpdate()
  {
    if (!this.mUpdateFiltersFlag)
      return;
    this.mUpdateFiltersFlag = false;
    UIManager.instance.GetScreen<MailScreen>().UpdateMailFilter();
  }
}
