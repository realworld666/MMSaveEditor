// Decompiled with JetBrains decompiler
// Type: BackgroundScene
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class BackgroundScene : BaseScene
{
  private GameObject[] mCameras;

  private void Awake()
  {
    Transform child = this.transform.FindChild("Cameras");
    if (!((Object) child != (Object) null))
      return;
    int childCount = child.childCount;
    this.mCameras = new GameObject[childCount];
    for (int index = 0; index < childCount; ++index)
    {
      this.mCameras[index] = child.GetChild(index).gameObject;
      this.mCameras[index].SetActive(false);
    }
  }

  public void EnableCamera(string inName)
  {
    if (this.mCameras == null)
      return;
    for (int index = 0; index < this.mCameras.Length; ++index)
    {
      bool flag = this.mCameras[index].name.Equals(inName);
      this.mCameras[index].SetActive(flag);
    }
  }

  public GameObject GetCamera(string inName)
  {
    if (this.mCameras != null)
    {
      for (int index = 0; index < this.mCameras.Length; ++index)
      {
        if (this.mCameras[index].name.Equals(inName))
          return this.mCameras[index];
      }
    }
    return (GameObject) null;
  }
}
