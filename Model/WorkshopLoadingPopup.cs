// Decompiled with JetBrains decompiler
// Type: WorkshopLoadingPopup
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorkshopLoadingPopup : UIDialogBox
{
  [SerializeField]
  private Slider uploadProgress;
  [SerializeField]
  private TextMeshProUGUI uploadLabel;
  private float mUploadProgress;
  private WorkshopLoadingPopup.PopupMode mPopupMode;

  public void SetupPopupMode(WorkshopLoadingPopup.PopupMode inPopupMode)
  {
    this.mPopupMode = inPopupMode;
  }

  protected override void OnEnable()
  {
    this.mUploadProgress = 0.0f;
    this.uploadProgress.value = 0.0f;
    this.uploadLabel.text = (this.mUploadProgress * 100f).ToString((IFormatProvider) Localisation.numberFormatter) + "%";
  }

  private void Update()
  {
    if (this.mPopupMode != WorkshopLoadingPopup.PopupMode.PublishingItem)
      return;
    float uploadCompletion = App.instance.modManager.modPublisher.uploadCompletion;
    if ((double) uploadCompletion > (double) this.mUploadProgress)
      this.mUploadProgress = uploadCompletion;
    this.uploadProgress.value = this.mUploadProgress;
    this.uploadLabel.text = (this.mUploadProgress * 100f).ToString((IFormatProvider) Localisation.numberFormatter) + "%";
  }

  public enum PopupMode
  {
    PublishingItem,
  }
}
