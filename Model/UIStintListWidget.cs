// Decompiled with JetBrains decompiler
// Type: UIStintListWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class UIStintListWidget : MonoBehaviour
{
  public GameObject noData;
  public GameObject data;
  public UIGridList gridList;
  private RacingVehicle mVehicle;
  private bool mIsEmpty;

  public void SetVehicle(RacingVehicle inVehicle, PitScreen.Mode inMode)
  {
    this.mVehicle = inVehicle;
    List<SetupStintData> setupStintData = Game.instance.persistentEventData.GetSetupStintData(this.mVehicle);
    this.mIsEmpty = setupStintData.Count == 0;
    if (this.mIsEmpty)
    {
      GameUtility.SetActive(this.noData, true);
      GameUtility.SetActive(this.data, false);
    }
    else
    {
      GameUtility.SetActive(this.noData, false);
      GameUtility.SetActive(this.data, true);
      if (Game.instance.sessionManager.sessionType != SessionDetails.SessionType.Race || inMode == PitScreen.Mode.PreSession)
      {
        SessionDetails.SessionType inCurrentSession = SessionDetails.SessionType.Race;
        for (int index = 0; index < setupStintData.Count; ++index)
        {
          if (setupStintData[index].sessionType != inCurrentSession)
          {
            inCurrentSession = setupStintData[index].sessionType;
            this.AddStintSessionTypeHeader(inCurrentSession);
          }
          this.gridList.CreateListItem<UIStintListEntry>().SetSetupStintData(this.mVehicle, setupStintData[index]);
        }
      }
      else
      {
        for (int index = 0; index < setupStintData.Count; ++index)
          this.gridList.CreateListItem<UIStintListEntry>().SetSetupStintData(this.mVehicle, setupStintData[index]);
      }
    }
  }

  private void AddStintSessionTypeHeader(SessionDetails.SessionType inCurrentSession)
  {
    this.gridList.CreateListItem<UIStintListEntry>().SetSetupStintData(inCurrentSession);
  }

  private void OnDisable()
  {
    this.gridList.DestroyListItems();
  }

  public bool IsEmpty()
  {
    return this.mIsEmpty;
  }
}
