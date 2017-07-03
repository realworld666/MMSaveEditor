// Decompiled with JetBrains decompiler
// Type: CarConditionPanel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class CarConditionPanel : HUDPanel
{
  public CarConditionEntry[] conditionEntry;
  public GameObject gtCarHighlight;
  public GameObject singleSeaterCarHighlight;

  public override HUDPanel.Type type
  {
    get
    {
      return HUDPanel.Type.CarCondition;
    }
  }

  private void Awake()
  {
  }

  private void OnEnable()
  {
    if (this.vehicle == null)
      return;
    GameUtility.SetActive(this.gtCarHighlight, Game.instance.sessionManager.championship.series == Championship.Series.GTSeries);
    GameUtility.SetActive(this.singleSeaterCarHighlight, Game.instance.sessionManager.championship.series == Championship.Series.SingleSeaterSeries);
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    Championship.Series series = this.vehicle.driver.contract.GetTeam().championship.series;
    for (int index = 0; index < this.conditionEntry.Length; ++index)
    {
      CarPart.PartType inType = CarPart.PartType.None;
      switch (series)
      {
        case Championship.Series.SingleSeaterSeries:
          inType = this.conditionEntry[index].partTypeBaseGame;
          break;
        case Championship.Series.GTSeries:
          inType = this.conditionEntry[index].partTypeGT;
          break;
      }
      if (inType != CarPart.PartType.None)
        this.conditionEntry[index].SetPart(this.vehicle.car.GetPart(inType));
      GameUtility.SetActive(this.conditionEntry[index].gameObject, inType != CarPart.PartType.None);
    }
  }

  private void OnDisable()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Back, 0.0f);
  }

  protected override void Update()
  {
    base.Update();
  }
}
