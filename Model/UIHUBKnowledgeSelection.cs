// Decompiled with JetBrains decompiler
// Type: UIHUBKnowledgeSelection
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIHUBKnowledgeSelection : MonoBehaviour
{
  private PracticeReportSessionData.KnowledgeType mKnowledgeType = PracticeReportSessionData.KnowledgeType.QualifyingTrim;
  public UIHUBKnowledgeSlot knowledgeSlot;
  public Toggle toggle;
  private MechanicBonus mMechanicBonus;
  private UIHUBKnowledgePopup mKnowledgePopup;
  public CanvasGroup canvasGroup;

  public PracticeReportSessionData.KnowledgeType knowledgeType
  {
    get
    {
      return this.mKnowledgeType;
    }
  }

  public MechanicBonus mechanicBonus
  {
    get
    {
      return this.mMechanicBonus;
    }
  }

  public void SetupForPracticeKnowledge(UIHUBKnowledgePopup inKnowledgePopup, PracticeReportSessionData.KnowledgeType inKnowledgeType)
  {
    this.mKnowledgePopup = inKnowledgePopup;
    this.mMechanicBonus = (MechanicBonus) null;
    this.mKnowledgeType = inKnowledgeType;
    this.toggle.onValueChanged.RemoveAllListeners();
    this.toggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggleClick));
    this.knowledgeSlot.SetupPracticeKnowledgeInfo(inKnowledgeType);
  }

  public void SetupForMechanicBonus(UIHUBKnowledgePopup inKnowledgePopup, MechanicBonus inMechanicBonus)
  {
    this.mKnowledgePopup = inKnowledgePopup;
    this.mMechanicBonus = inMechanicBonus;
    this.toggle.onValueChanged.RemoveAllListeners();
    this.toggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggleClick));
    this.knowledgeSlot.SetupMechanicBonusInfo(inMechanicBonus);
  }

  private void OnToggleClick(bool inValue)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (inValue)
      this.SelectionBonus();
    else
      this.DeselectBonus();
  }

  private void SelectionBonus()
  {
    if (this.mMechanicBonus != null)
      this.mKnowledgePopup.SelectMechanicBonus(this.mMechanicBonus);
    else
      this.mKnowledgePopup.SelectKnowledgeBonus(this.mKnowledgeType);
  }

  private void DeselectBonus()
  {
    if (this.mMechanicBonus != null)
      this.mKnowledgePopup.DeselectMechanicBonus(this.mMechanicBonus);
    else
      this.mKnowledgePopup.DeselectKnowledgeBonus(this.mKnowledgeType);
  }
}
