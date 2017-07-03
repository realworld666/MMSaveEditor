// Decompiled with JetBrains decompiler
// Type: UIHUBKnowledgeSlot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIHUBKnowledgeSlot : MonoBehaviour
{
  public Button emptyButton;
  public Button filledButton;
  [SerializeField]
  private GameObject mKnowledgeIconContainer;
  public UIMechanicBonusIcon mechanicBonusIcon;
  public TextMeshProUGUI knowledgeName;
  public TextMeshProUGUI knowledgeLevel;
  public TextMeshProUGUI knowledgeBonus;
  public GameObject emptySlot;
  public GameObject filledSlot;
  [SerializeField]
  private Image mKnowledgeIcon;
  [SerializeField]
  private Image mKnowledgeIconBacking;
  [SerializeField]
  private Image mKnowledgeLevelBacking;
  private RacingVehicle mVehicle;
  private UIHUBKnowledgeEntry mKnowledgeEntry;

  private void Awake()
  {
    if (!((Object) this.emptyButton != (Object) null))
      return;
    this.emptyButton.onClick.AddListener(new UnityAction(this.OpenPopup));
    this.filledButton.onClick.AddListener(new UnityAction(this.OpenPopup));
  }

  public void Setup(UIHUBKnowledgeEntry inKnowledgeEntry, RacingVehicle inVehicle, bool isEmpty)
  {
    this.mKnowledgeEntry = inKnowledgeEntry;
    this.SetEmpty(isEmpty);
    this.mVehicle = inVehicle;
  }

  public void SetEmpty(bool isEmpty)
  {
    if (!((Object) this.emptySlot != (Object) null) || !((Object) this.filledSlot != (Object) null))
      return;
    GameUtility.SetActive(this.emptySlot, isEmpty);
    GameUtility.SetActive(this.filledSlot, !isEmpty);
  }

  public void SetupPracticeKnowledgeInfo(PracticeReportSessionData.KnowledgeType inKnowledgeType)
  {
    PracticeReportKnowledgeData knowledgeOfType = Game.instance.persistentEventData.GetPlayerPracticeReportData().GetKnowledgeOfType(inKnowledgeType);
    GameUtility.SetActive(this.mechanicBonusIcon.gameObject, false);
    GameUtility.SetActive(this.mKnowledgeIconContainer, true);
    this.mKnowledgeIcon.sprite = PracticeKnowledge.GetKnowledgeSprite(inKnowledgeType, false);
    this.knowledgeName.text = PracticeKnowledge.GetKnowledgeName(inKnowledgeType);
    StringVariableParser.knowledgeLevel = knowledgeOfType.lastUnlockedLevel;
    StringVariableParser.knowledgeAmount = knowledgeOfType.GetBonus();
    this.knowledgeLevel.text = Localisation.LocaliseID("PSG_10009816", (GameObject) null);
    this.knowledgeBonus.text = Localisation.LocaliseID("PSG_10009817", (GameObject) null);
    Color knowledgeLevelColor = PracticeKnowledge.GetKnowledgeLevelColor(knowledgeOfType, false);
    this.mKnowledgeIconBacking.color = knowledgeLevelColor;
    this.mKnowledgeLevelBacking.color = knowledgeLevelColor;
  }

  public void SetupMechanicBonusInfo(MechanicBonus inMechanicBonus)
  {
    GameUtility.SetActive(this.mechanicBonusIcon.gameObject, true);
    GameUtility.SetActive(this.mKnowledgeIconContainer, false);
    this.mechanicBonusIcon.Setup(true, inMechanicBonus);
    this.knowledgeName.text = Localisation.LocaliseID(inMechanicBonus.nameLocalisationID, (GameObject) null);
    StringVariableParser.knowledgeLevel = inMechanicBonus.level;
    this.knowledgeLevel.text = Localisation.LocaliseID("PSG_10009816", (GameObject) null);
    this.knowledgeBonus.text = Localisation.LocaliseID(inMechanicBonus.textLocalisationID, (GameObject) null);
    this.mKnowledgeLevelBacking.color = inMechanicBonus.GetColor();
  }

  private void OpenPopup()
  {
    UIManager.instance.dialogBoxManager.GetDialog<UIHUBKnowledgePopup>().ActivatePopup(this.mKnowledgeEntry, this.mVehicle);
  }
}
