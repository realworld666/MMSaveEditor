// Decompiled with JetBrains decompiler
// Type: UILocaliseLabel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class UILocaliseLabel : MonoBehaviour
{
  [SerializeField]
  private string mGenderTarget = string.Empty;
  [SerializeField]
  private string mID = string.Empty;
  [SerializeField]
  private string mText = string.Empty;
  public static Action ForceRefreshOnLateUpdate;
  public static Action RefreshTextImmidiatly;
  [SerializeField]
  private bool mOverrideNullGenderRef;
  private TextMeshProUGUI mTextMeshProUGUI;
  private bool mRefresh;

  public string id
  {
    get
    {
      return this.mID;
    }
  }

  public string text
  {
    get
    {
      return this.mText;
    }
  }

  public string genderTarget
  {
    get
    {
      return this.mGenderTarget;
    }
    set
    {
      this.mGenderTarget = value;
    }
  }

  public bool overrideNullGenderRef
  {
    get
    {
      return this.mOverrideNullGenderRef;
    }
    set
    {
      this.mOverrideNullGenderRef = value;
    }
  }

  private void Awake()
  {
    this.mTextMeshProUGUI = this.GetComponent<TextMeshProUGUI>();
    this.mText = this.mTextMeshProUGUI.text;
    Localisation.OnLanguageChange += new Action(this.RefreshText);
    UILocaliseLabel.ForceRefreshOnLateUpdate += new Action(this.ForceRefresh);
    UILocaliseLabel.RefreshTextImmidiatly += new Action(this.ImmidiateRefresh);
  }

  private void OnDestroy()
  {
    Localisation.OnLanguageChange -= new Action(this.RefreshText);
    UILocaliseLabel.ForceRefreshOnLateUpdate -= new Action(this.ForceRefresh);
    UILocaliseLabel.RefreshTextImmidiatly -= new Action(this.ImmidiateRefresh);
  }

  public void OnUnload()
  {
    Localisation.OnLanguageChange -= new Action(this.RefreshText);
    UILocaliseLabel.ForceRefreshOnLateUpdate -= new Action(this.ForceRefresh);
    UILocaliseLabel.RefreshTextImmidiatly -= new Action(this.ImmidiateRefresh);
  }

  private void ImmidiateRefresh()
  {
    this.RefreshText();
  }

  private void ForceRefresh()
  {
    this.mRefresh = true;
  }

  public void RefreshText()
  {
    if (Application.isPlaying)
      this.RefreshText(false, Localisation.currentLanguage);
    else
      this.RefreshText(true, Localisation.currentLanguage);
  }

  public void RefreshText(bool isEditor, string inLanguage)
  {
    if (!this.gameObject.activeSelf && !isEditor || !isEditor && UIManager.instance.isLoadingScreens || string.IsNullOrEmpty(this.mID))
      return;
    this.SetTextFromID(this.mID, inLanguage);
  }

  private void OnEnable()
  {
    this.ForceRefresh();
    if (Application.isPlaying)
      return;
    this.RefreshText();
  }

  public void SetTextFromID(string inID)
  {
    this.SetTextFromID(inID, Localisation.currentLanguage);
  }

  public void SetTextFromID(string inID, string inLanguage)
  {
    this.mID = inID;
    if (!((UnityEngine.Object) this.mTextMeshProUGUI != (UnityEngine.Object) null) || !Localisation.HasID(this.mID))
      return;
    Person person = StringVariableParser.GetObject(this.mGenderTarget) as Person;
    if (person != null)
      StringVariableParser.subject = person;
    if (person == null && this.mOverrideNullGenderRef)
      this.mGenderTarget = "Male";
    this.mText = Localisation.LocaliseID(this.mID, inLanguage, this.gameObject, this.mGenderTarget);
    if (person != null)
      StringVariableParser.subject = (Person) null;
    this.mTextMeshProUGUI.text = this.mText;
  }

  private void LateUpdate()
  {
    if (!this.mRefresh)
      return;
    this.RefreshText();
    this.mRefresh = false;
  }
}
