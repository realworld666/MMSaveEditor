// Decompiled with JetBrains decompiler
// Type: UIDialogBoxMechanicBonusTooltip
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UIDialogBoxMechanicBonusTooltip : UIDialogBox
{
  [SerializeField]
  private RectTransform rectTransform;
  [SerializeField]
  private UIComponentIcon icon;
  [SerializeField]
  private GameObject locked;
  [SerializeField]
  private TextMeshProUGUI title;
  [SerializeField]
  private TextMeshProUGUI level;
  [SerializeField]
  private TextMeshProUGUI description;
  private MechanicBonus mBonus;

  public void ShowTooltip(bool inUnlocked, MechanicBonus inBonus)
  {
    if (inBonus == null)
      return;
    this.mBonus = inBonus;
    GameUtility.SetTooltipTransformInsideScreen(this.rectTransform, (RectTransform) null, new Vector3(), false, (RectTransform) null);
    this.icon.SetComponent(this.mBonus);
    StringVariableParser.intValue1 = this.mBonus.level;
    this.level.text = Localisation.LocaliseID("PSG_10010415", (GameObject) null);
    this.title.text = string.IsNullOrEmpty(this.mBonus.nameLocalisationID) ? "-" : Localisation.LocaliseID(this.mBonus.nameLocalisationID, (GameObject) null);
    this.description.text = string.IsNullOrEmpty(this.mBonus.textLocalisationID) ? "-" : Localisation.LocaliseID(this.mBonus.textLocalisationID, (GameObject) null);
    GameUtility.SetActive(this.locked, !inUnlocked);
    GameUtility.SetActive(this.gameObject, true);
  }

  private void Update()
  {
    GameUtility.SetTooltipTransformInsideScreen(this.rectTransform, (RectTransform) null, new Vector3(), false, (RectTransform) null);
  }

  public void HideTooltip()
  {
    GameUtility.SetActive(this.gameObject, false);
  }
}
