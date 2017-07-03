// Decompiled with JetBrains decompiler
// Type: UICar
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class UICar : MonoBehaviour
{
  [SerializeField]
  private Image carImage;
  [SerializeField]
  private UICar.CarOwner carOwner;

  private void Start()
  {
    if (this.carOwner != UICar.CarOwner.PlayerCar)
      return;
    this.gameObject.GetComponentInChildren<UITeamColor>().enabled = true;
  }

  public void SetTeamColor(Color inTeamColor)
  {
    this.carOwner = UICar.CarOwner.AnyCar;
    this.carImage.color = inTeamColor;
  }

  public enum CarOwner
  {
    AnyCar,
    PlayerCar,
  }
}
