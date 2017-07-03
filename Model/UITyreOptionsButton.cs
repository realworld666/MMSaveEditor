// Decompiled with JetBrains decompiler
// Type: UITyreOptionsButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITyreOptionsButton : MonoBehaviour
{
  private List<UITyreEntry> mTyreEntries = new List<UITyreEntry>();
  public Action OnTyreChange;
  public GameObject options;
  public Transform optionsGrid;
  public Toggle buttonToggle;
  public UITyre uiTyre;
  public UITyreEntry tyreEntryPrefab;
  public TextMeshProUGUI buttonLabel;
  private UITyreEntry mCurrentlySelectedTyreEntry;

  public UITyreEntry currentlySelectedTyre
  {
    get
    {
      return this.mCurrentlySelectedTyreEntry;
    }
  }

  public void PopulateOptions(TyreSet[] tyres)
  {
    for (int index = 0; index < tyres.Length; ++index)
    {
      TyreSet tyre = tyres[index];
      UITyreEntry uiTyreEntry = UnityEngine.Object.Instantiate<UITyreEntry>(this.tyreEntryPrefab);
      uiTyreEntry.transform.SetParent(this.optionsGrid, false);
      uiTyreEntry.uiTyre.SetTyre(tyre);
      uiTyreEntry.SetSelected(false);
      uiTyreEntry.SetParentButton(this);
      this.mTyreEntries.Add(uiTyreEntry);
    }
    this.mTyreEntries[0].SetSelected(true);
    this.mCurrentlySelectedTyreEntry = this.mTyreEntries[0];
    this.uiTyre.SetTyre(this.mTyreEntries[0].uiTyre.tyre);
    this.buttonLabel.text = tyres[0].GetName();
  }

  public void OnMouseEnter()
  {
    if (this.options.activeSelf)
      return;
    this.options.SetActive(true);
  }

  public void OnMouseExit()
  {
    if (!this.options.activeSelf || this.CheckMouseLocation((Vector2) Input.mousePosition, this.options))
      return;
    this.options.SetActive(false);
  }

  public void SelectTyreOption(UITyreEntry selectedTyreEntry)
  {
    this.mTyreEntries.Find((Predicate<UITyreEntry>) (entry => (UnityEngine.Object) entry == (UnityEngine.Object) this.mCurrentlySelectedTyreEntry)).SetSelected(false);
    this.mCurrentlySelectedTyreEntry = selectedTyreEntry;
    this.mTyreEntries.Find((Predicate<UITyreEntry>) (entry => (UnityEngine.Object) entry == (UnityEngine.Object) this.mCurrentlySelectedTyreEntry)).SetSelected(true);
    this.uiTyre.SetTyre(this.mCurrentlySelectedTyreEntry.uiTyre.tyre);
    this.options.SetActive(false);
    this.buttonToggle.isOn = true;
    if (this.OnTyreChange == null)
      return;
    this.OnTyreChange();
  }

  private void Update()
  {
    if (!this.options.activeSelf || this.CheckMouseLocation((Vector2) Input.mousePosition, this.options) || this.CheckMouseLocation((Vector2) Input.mousePosition, this.gameObject))
      return;
    this.options.SetActive(false);
  }

  private bool CheckMouseLocation(Vector2 coords, GameObject gameObj)
  {
    Vector2 point = (Vector2) gameObj.transform.InverseTransformPoint((Vector3) coords);
    return ((RectTransform) gameObj.transform).rect.Contains(point);
  }
}
