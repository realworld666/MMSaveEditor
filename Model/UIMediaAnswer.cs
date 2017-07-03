// Decompiled with JetBrains decompiler
// Type: UIMediaAnswer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIMediaAnswer : MonoBehaviour
{
  public Button button;
  public TextMeshProUGUI asnwerText;
  public Animator animator;
  private TextDynamicData mData;
  private DialogRule mButtonRule;

  private void Awake()
  {
    this.button.onClick.AddListener(new UnityAction(this.OnButtonClick));
  }

  private void OnButtonClick()
  {
    MediaInterviewDialog dialog = UIManager.instance.dialogBoxManager.GetDialog<MediaInterviewDialog>();
    dialog.PlayButtonAnimations(this);
    dialog.SetNextStep(this.mButtonRule);
    dialog.SetOutcomeTargets(this.mButtonRule, true);
  }

  public static bool IsValidImpact(DialogCriteria inCriteria)
  {
    float result = 0.0f;
    return float.TryParse(inCriteria.mCriteriaInfo, out result);
  }

  public void Setup(DialogRule inRule)
  {
    if (!(bool) ((Object) this.animator))
      return;
    this.mData = new TextDynamicData();
    this.animator.Play(AnimationHashes.AnswerIntro);
    this.mButtonRule = inRule;
    this.mData.SetMessageTextFields(this.mButtonRule.localisationID);
    this.asnwerText.text = this.mData.GetText();
  }

  private void Update()
  {
    GameUtility.SetInteractable(this.button, (double) this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0);
  }
}
