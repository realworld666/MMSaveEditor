// Decompiled with JetBrains decompiler
// Type: UITyreSelectionTyreSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class UITyreSelectionTyreSet : MonoBehaviour
{
  private Vector3 mZeroPosition = Vector3.zero;
  private int mTotalIndex = -1;
  public GameObject[] numberContainers;
  public TextMeshProUGUI[] tyreNumbers;
  public UICarSetupTyreIcon[] tyreSets;
  public RectTransform selected;
  private RacingVehicle mVehicle;
  private TyreSet mCurrentTyres;
  private int mMaxTyres;
  private int mTyreIndex;

  public void Setup(RacingVehicle inVehicle, int inCompoundsAvailable, int inMaxTyres)
  {
    this.mVehicle = inVehicle;
    this.mCurrentTyres = this.mVehicle.setup.tyreSet;
    this.mTyreIndex = 0;
    this.mTotalIndex = -1;
    this.mMaxTyres = inMaxTyres;
    for (int index = 0; index < this.numberContainers.Length; ++index)
      this.numberContainers[index].transform.SetAsLastSibling();
    for (int index = 0; index < this.tyreSets.Length; ++index)
      GameUtility.SetActive(this.tyreSets[index].gameObject, index < this.mMaxTyres);
    this.SetTyreOption(SessionStrategy.TyreOption.First, 0, true);
    this.SetTyreOption(SessionStrategy.TyreOption.Second, 1, true);
    this.SetTyreOption(SessionStrategy.TyreOption.Third, 2, inCompoundsAvailable > 2);
  }

  private void SetTyreOption(SessionStrategy.TyreOption inTyreOption, int inIndex, bool inDisplay)
  {
    GameUtility.SetActive(this.numberContainers[inIndex], inDisplay);
    if (!inDisplay)
      return;
    TyreSet[] tyres = this.mVehicle.strategy.GetTyres(inTyreOption);
    ++this.mTotalIndex;
    GameUtility.SetSiblingIndex(this.numberContainers[inIndex].transform, this.mTotalIndex);
    this.tyreNumbers[inIndex].text = tyres.Length.ToString();
    for (int index = 0; index < tyres.Length; ++index)
    {
      if (this.mTyreIndex < this.tyreSets.Length)
      {
        TyreSet inTyreSet = tyres[index];
        this.tyreSets[this.mTyreIndex].SetTyre(inTyreSet);
        if (this.mCurrentTyres == inTyreSet)
        {
          GameUtility.SetParent(this.selected.transform, this.tyreSets[this.mTyreIndex].transform, false);
          this.selected.anchoredPosition = (Vector2) this.mZeroPosition;
        }
        ++this.mTyreIndex;
        ++this.mTotalIndex;
      }
    }
  }
}
