// Decompiled with JetBrains decompiler
// Type: UICharacterToolPopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UICharacterToolPopup : MonoBehaviour
{
  private List<UICharacterToolHeaderEntry> mToggles = new List<UICharacterToolHeaderEntry>();
  private List<UICharacterToolHeaderEntry> mHeaders = new List<UICharacterToolHeaderEntry>();
  private List<UICharacterToolHeaderEntry> mExamples = new List<UICharacterToolHeaderEntry>();
  public Button closeButton;
  public Button copyClipboard;
  public Toggle includeHeaders;
  public UICharacterToolPortrait portrait;
  public CharacterCreatorToolScreen screen;
  private UICharacterToolHeaderEntry[] mEntries;
  private Person mPerson;

  public void OnStart()
  {
    this.mEntries = this.gameObject.transform.GetComponentsInChildren<UICharacterToolHeaderEntry>(true);
    int length = this.mEntries.Length;
    for (int index = 0; index < length; ++index)
    {
      UICharacterToolHeaderEntry mEntry = this.mEntries[index];
      switch (mEntry.type)
      {
        case UICharacterToolHeaderEntry.Type.HeaderToggle:
          this.mToggles.Add(mEntry);
          break;
        case UICharacterToolHeaderEntry.Type.Header:
          this.mHeaders.Add(mEntry);
          break;
        case UICharacterToolHeaderEntry.Type.Example:
          this.mExamples.Add(mEntry);
          break;
      }
      mEntry.OnStart();
    }
    this.closeButton.onClick.AddListener(new UnityAction(this.Hide));
    this.copyClipboard.onClick.AddListener(new UnityAction(this.CopyToClipBoard));
    this.SetAllFields();
    this.Hide();
  }

  public void Setup(Person inPerson)
  {
    if (inPerson == null)
      return;
    this.mPerson = inPerson;
    this.portrait.Setup(this.mPerson);
    this.SetExample();
    this.Show();
  }

  public void SetExample()
  {
    int count = this.mExamples.Count;
    for (int index = 0; index < count; ++index)
      this.mExamples[index].Setup(this.mPerson);
  }

  public void SetField(UICharacterToolHeaderEntry.Field inField, bool inDisplay)
  {
    int length = this.mEntries.Length;
    for (int index = 0; index < length; ++index)
    {
      UICharacterToolHeaderEntry mEntry = this.mEntries[index];
      if (mEntry.field == inField && mEntry.type != UICharacterToolHeaderEntry.Type.HeaderToggle)
        mEntry.gameObject.SetActive(inDisplay);
    }
  }

  public void SetAllFields()
  {
    int length = this.mEntries.Length;
    for (int index = 0; index < length; ++index)
    {
      UICharacterToolHeaderEntry mEntry = this.mEntries[index];
      if (mEntry.type == UICharacterToolHeaderEntry.Type.HeaderToggle)
        mEntry.SetField();
    }
  }

  public void Show()
  {
    this.gameObject.SetActive(true);
  }

  public void Hide()
  {
    this.gameObject.SetActive(false);
  }

  private void CopyToClipBoard()
  {
    GUIUtility.systemCopyBuffer = this.FormatData();
  }

  private string FormatData()
  {
    string str = string.Empty;
    if (this.includeHeaders.isOn)
    {
      int count = this.mHeaders.Count;
      for (int index = 0; index < count; ++index)
      {
        UICharacterToolHeaderEntry mHeader = this.mHeaders[index];
        if (mHeader.gameObject.activeSelf)
          str = str + mHeader.GetHeader() + "\t";
      }
      str = str.TrimEnd('\t') + Environment.NewLine;
    }
    List<UICharacterToolEntry> selectedPeople = this.screen.profilesWidget.selectedPeople;
    int count1 = selectedPeople.Count;
    int count2 = this.mExamples.Count;
    for (int index1 = 0; index1 < count1; ++index1)
    {
      Person person = selectedPeople[index1].person;
      if (person != null)
      {
        for (int index2 = 0; index2 < count2; ++index2)
        {
          UICharacterToolHeaderEntry mExample = this.mExamples[index2];
          if (mExample.gameObject.activeSelf)
          {
            string field = mExample.GetField(person);
            str = string.IsNullOrEmpty(field) ? str + " \t" : str + field + "\t";
          }
        }
        str = str.TrimEnd('\t') + Environment.NewLine;
      }
    }
    return str;
  }
}
