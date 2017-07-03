// Decompiled with JetBrains decompiler
// Type: PoliticsPlayerVoteTrackEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PoliticsPlayerVoteTrackEntry : MonoBehaviour
{
  private List<Circuit> mCircuits = new List<Circuit>();
  public UITrackLayout trackLayout;
  public Flag flag;
  public Button buttonLeft;
  public Button buttonRight;
  public TextMeshProUGUI titleLabel;
  public TextMeshProUGUI circuitNameLabel;
  public TextMeshProUGUI circuitLayoutLabel;
  public PoliticsPlayerVoteStageChooseDetails widget;
  private Circuit mSelectedCircuit;
  private int mSelectedCircuitIndex;

  public Circuit selectedCircuit
  {
    get
    {
      return this.mSelectedCircuit;
    }
  }

  public int selectedCircuitIndex
  {
    get
    {
      return this.mSelectedCircuitIndex;
    }
  }

  public void OnStart()
  {
    this.buttonLeft.onClick.AddListener(new UnityAction(this.OnButtonLeft));
    this.buttonRight.onClick.AddListener(new UnityAction(this.OnButtonRight));
  }

  public void Setup(string inTitle, List<Circuit> inCircuits)
  {
    GameUtility.SetActive(this.gameObject, inCircuits.Count > 0);
    if (!this.gameObject.activeSelf)
      return;
    this.mCircuits = inCircuits;
    if (this.mSelectedCircuit == null || !this.mCircuits.Contains(this.mSelectedCircuit) || (this.mSelectedCircuitIndex < 0 || this.mSelectedCircuitIndex > this.mCircuits.Count - 1))
    {
      this.mSelectedCircuit = this.mCircuits[0];
      this.mSelectedCircuitIndex = 0;
    }
    this.titleLabel.text = inTitle;
    this.RefreshCircuit();
  }

  public void ResetCircuit()
  {
    this.mSelectedCircuit = (Circuit) null;
  }

  private void RefreshCircuit()
  {
    this.SetCurrentCircuit();
    this.UpdateButtonsState();
  }

  private void SetCurrentCircuit()
  {
    if (this.mSelectedCircuit == null)
      return;
    this.trackLayout.SetCircuitIcon(this.mSelectedCircuit);
    this.flag.SetNationality(this.mSelectedCircuit.nationality);
    this.circuitNameLabel.text = Localisation.LocaliseID(this.mSelectedCircuit.locationNameID, (GameObject) null);
    this.circuitLayoutLabel.text = Localisation.LocaliseEnum((Enum) this.mSelectedCircuit.trackLayout);
  }

  private void UpdateButtonsState()
  {
    this.buttonLeft.interactable = this.mSelectedCircuitIndex > 0;
    this.buttonRight.interactable = this.mSelectedCircuitIndex < this.mCircuits.Count - 1;
  }

  private void OnButtonLeft()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    --this.mSelectedCircuitIndex;
    this.mSelectedCircuit = this.mCircuits[this.mSelectedCircuitIndex];
    this.RefreshCircuit();
    this.widget.OnCircuitSelected();
  }

  private void OnButtonRight()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    ++this.mSelectedCircuitIndex;
    this.mSelectedCircuit = this.mCircuits[this.mSelectedCircuitIndex];
    this.RefreshCircuit();
    this.widget.OnCircuitSelected();
  }
}
