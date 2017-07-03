// Decompiled with JetBrains decompiler
// Type: LiveryIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LiveryIcon : MonoBehaviour
{
  private int mLiveryIndex = -1;
  private int mLiveryPosition = -1;
  public Toggle toggle;
  public TextMeshProUGUI number;
  public LiveryOptionsWidget widget;

  public int liveryIndex
  {
    get
    {
      return this.mLiveryIndex;
    }
  }

  public void Setup(int inLiveryIndex, int inLiveryPosition, int inFriendlyName, bool isOn)
  {
    this.mLiveryIndex = inLiveryIndex;
    this.mLiveryPosition = inLiveryPosition;
    this.number.text = inFriendlyName.ToString();
    this.toggle.group = this.widget.liveryToggleGroup;
    this.toggle.onValueChanged.RemoveAllListeners();
    this.toggle.isOn = isOn;
    this.toggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnLiveryChosen));
    GameUtility.SetActive(this.gameObject, true);
  }

  private void OnLiveryChosen(bool inValue)
  {
    if (!inValue)
      return;
    this.widget.SetLivery(this.mLiveryIndex, this.mLiveryPosition);
  }
}
