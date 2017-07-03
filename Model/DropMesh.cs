// Decompiled with JetBrains decompiler
// Type: DropMesh
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public static class DropMesh
{
  public static void DoDropMesh(this GameObject inGameObject)
  {
    Transform[] componentsInChildren = inGameObject.GetComponentsInChildren<Transform>();
    Debug.Log((object) ("Number of Objects:" + (object) componentsInChildren.Length), (Object) null);
    foreach (Transform transform in componentsInChildren)
    {
      if (!((Object) transform.gameObject == (Object) inGameObject.gameObject) && !((Object) transform.parent.gameObject != (Object) inGameObject.gameObject))
      {
        Debug.Log((object) (transform.name + "..." + (object) transform.position), (Object) null);
        Vector3 position = transform.position;
        Vector3 vector3_1 = new Vector3(0.0f, -1f, 0.0f);
        Vector3 vector3_2 = new Vector3(0.0f, 0.0f, 0.0f);
        UnityEngine.Debug.DrawRay(position, vector3_1, Color.green);
        RaycastHit hitInfo;
        if (Physics.Raycast(position, vector3_1, out hitInfo, 500f))
          vector3_2 = hitInfo.point;
        transform.position = vector3_2;
      }
    }
  }
}
