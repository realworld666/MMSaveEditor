// Decompiled with JetBrains decompiler
// Type: ActivateForOwnedDLC
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

public class ActivateForOwnedDLC : MonoBehaviour
{
  public ActivateForOwnedDLC.GameObjectData[] targetData = new ActivateForOwnedDLC.GameObjectData[0];

  private void OnEnable()
  {
    for (int index1 = 0; index1 < this.targetData.Length; ++index1)
    {
      ActivateForOwnedDLC.GameObjectData gameObjectData = this.targetData[index1];
      bool inIsActive = true;
      switch (gameObjectData.target)
      {
        case ActivateForOwnedDLC.dlcTarget.BaseGame:
          inIsActive = !App.instance.dlcManager.IsDlcWithIdInstalled(2);
          break;
        case ActivateForOwnedDLC.dlcTarget.GTSeries:
          inIsActive = App.instance.dlcManager.IsDlcWithIdInstalled(2);
          break;
      }
      for (int index2 = 0; index2 < gameObjectData.targetObjects.Length; ++index2)
        GameUtility.SetActive(gameObjectData.targetObjects[index2], inIsActive);
    }
  }

  public enum dlcTarget
  {
    BaseGame,
    GTSeries,
  }

  [Serializable]
  public struct GameObjectData
  {
    public ActivateForOwnedDLC.dlcTarget target;
    public GameObject[] targetObjects;
  }
}
