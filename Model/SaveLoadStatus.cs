// Decompiled with JetBrains decompiler
// Type: SaveLoadStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadStatus : UIDialogBox
{
  public GameObject saveIcon;
  public GameObject loadIcon;
  public TextMeshProUGUI headingLabel;
  public Image progressBar;

  protected override void OnEnable()
  {
    base.OnEnable();
    switch (App.instance.saveSystem.status)
    {
      case SaveSystem.Status.Loading:
        this.saveIcon.SetActive(false);
        this.loadIcon.SetActive(true);
        this.headingLabel.text = Localisation.LocaliseID("PSG_10008747", (GameObject) null);
        App.instance.saveSystem.OnLoadComplete += new Action(this.OnLoadComplete);
        break;
      case SaveSystem.Status.Saving:
        this.saveIcon.SetActive(true);
        this.loadIcon.SetActive(false);
        this.headingLabel.text = Localisation.LocaliseID("PSG_10010535", (GameObject) null);
        App.instance.saveSystem.OnSaveComplete += new Action(this.OnSaveComplete);
        break;
      default:
        this.saveIcon.SetActive(false);
        this.loadIcon.SetActive(false);
        this.headingLabel.text = string.Empty;
        break;
    }
    this.progressBar.fillAmount = 0.0f;
  }

  protected override void OnDisable()
  {
    base.OnDisable();
    App.instance.saveSystem.OnLoadComplete -= new Action(this.OnLoadComplete);
    App.instance.saveSystem.OnSaveComplete -= new Action(this.OnSaveComplete);
  }

  private void Update()
  {
    this.progressBar.fillAmount = App.instance.saveSystem.CurrentOperationProgress;
  }

  private void OnLoadComplete()
  {
    App.instance.saveSystem.OnLoadComplete -= new Action(this.OnLoadComplete);
    UIManager.instance.dialogBoxManager.HideAll();
  }

  private void OnSaveComplete()
  {
    App.instance.saveSystem.OnSaveComplete -= new Action(this.OnSaveComplete);
    UIManager.instance.dialogBoxManager.GetDialog<SaveLoadDialog>().Refresh();
    this.Hide();
  }
}
