// Decompiled with JetBrains decompiler
// Type: UITravelTableHeader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UITravelTableHeader : MonoBehaviour
{
  public UITravelSponsorSelect.Filter filter = UITravelSponsorSelect.Filter.TotalBonus;
  public Button button;
  public GameObject arrowUp;
  public GameObject arrowDesc;
  public UITravelSponsorSelect widget;

  public void OnStart()
  {
    this.button.onClick.AddListener(new UnityAction(this.OnButton));
    this.Reset();
  }

  public void Setup(bool inASC)
  {
    this.arrowUp.SetActive(inASC);
    this.arrowDesc.SetActive(!inASC);
  }

  public void Reset()
  {
    this.arrowUp.SetActive(false);
    this.arrowDesc.SetActive(false);
  }

  private void OnButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.widget.OnFilter(this.filter);
  }
}
