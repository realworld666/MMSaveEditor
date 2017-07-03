// Decompiled with JetBrains decompiler
// Type: HQsReplaceMaterialsScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[ExecuteInEditMode]
public class HQsReplaceMaterialsScript : MonoBehaviour
{
  public Material primaryColour;
  public Material secondaryColour;
  public bool changeMaterials;

  private void Update()
  {
    if (!this.changeMaterials)
      return;
    Material material1 = new Material(this.primaryColour);
    Material material2 = new Material(this.secondaryColour);
    MeshRenderer[] componentsInChildren = this.gameObject.GetComponentsInChildren<MeshRenderer>(true);
    Debug.Log((object) ("RUNNING REPLACE SCRIPT ON " + componentsInChildren.Length.ToString() + " RENDERERS"), (Object) null);
    int num = 0;
    for (int index1 = 0; index1 < componentsInChildren.Length; ++index1)
    {
      MeshRenderer meshRenderer = componentsInChildren[index1];
      Material[] sharedMaterials = meshRenderer.sharedMaterials;
      for (int index2 = 0; index2 < sharedMaterials.Length; ++index2)
      {
        if ((Object) sharedMaterials[index2] != (Object) null)
        {
          if (sharedMaterials[index2].name.Contains("TeamColourPrimary"))
          {
            sharedMaterials[index2] = material1;
            ++num;
          }
          if (sharedMaterials[index2].name.Contains("TeamColourSecondary"))
          {
            sharedMaterials[index2] = material2;
            ++num;
          }
        }
      }
      meshRenderer.sharedMaterials = sharedMaterials;
    }
    Debug.Log((object) (num.ToString() + " MATERIALS REPLACED"), (Object) null);
    this.changeMaterials = false;
  }
}
