// Decompiled with JetBrains decompiler
// Type: UISponsorsFilter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISponsorsFilter : MonoBehaviour
{
  public UISponsorsOffers.SponsorFilter filter = UISponsorsOffers.SponsorFilter.BonusFund;
  public Button button;
  public GameObject arrowUp;
  public GameObject arrowDown;

  public void OnStart()
  {
    this.button.onClick.RemoveAllListeners();
    this.button.onClick.AddListener(new UnityAction(this.OnButton));
  }

  private void OnButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    UISponsorsOffers dialog = UIManager.instance.dialogBoxManager.GetDialog<UISponsorsOffers>();
    dialog.Filter(this.filter, true);
    GameUtility.SetActive(this.arrowUp, dialog.filterASC);
    GameUtility.SetActive(this.arrowDown, !dialog.filterASC);
  }

  public void DisableArrows()
  {
    GameUtility.SetActive(this.arrowUp, false);
    GameUtility.SetActive(this.arrowDown, false);
  }
}
