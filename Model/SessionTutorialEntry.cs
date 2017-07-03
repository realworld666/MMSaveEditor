// Decompiled with JetBrains decompiler
// Type: SessionTutorialEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SessionTutorialEntry : MonoBehaviour
{
  public Button iconButton;
  public TextMeshProUGUI title;
  public Image icon;
  private DialogRule mRule;

  public DialogRule rule
  {
    get
    {
      return this.mRule;
    }
  }

  public void Awake()
  {
    this.iconButton.onClick.AddListener(new UnityAction(this.OnIconButtonClick));
  }

  public void Setup(DialogRule inRule, SessionTutorialsWidget inWidget)
  {
    if (inRule == null)
      return;
    this.mRule = inRule;
    string tutorialIcon = this.GetTutorialIcon();
    if (tutorialIcon != "Error")
      this.icon.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Logos1, tutorialIcon);
    this.title.text = Localisation.LocaliseID(this.mRule.localisationID, (GameObject) null);
  }

  public void OnIconButtonClick()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (this.mRule == null || this.mRule.triggerQuery == null || this.mRule.triggerQuery.criteriaList.Count <= 0)
      return;
    App.instance.tutorialSimulation.SendTutorial(this.mRule, true);
  }

  public string GetTutorialIcon()
  {
    if (this.mRule != null)
    {
      string userDataValue = this.mRule.GetUserDataValue("Icon");
      if (!string.IsNullOrEmpty(userDataValue))
        return "RaceTutorialIcons-Tutorial_" + userDataValue;
    }
    return "Error";
  }
}
