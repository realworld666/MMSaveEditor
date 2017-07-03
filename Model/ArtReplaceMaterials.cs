// Decompiled with JetBrains decompiler
// Type: ArtReplaceMaterials
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[ExecuteInEditMode]
public class ArtReplaceMaterials : MonoBehaviour
{
  public string criteriaName = string.Empty;
  public bool exactMatch;
  public bool partialMatch;
  public Material materialToReplace;
  public bool activate;

  private void Update()
  {
    if (!this.activate || string.IsNullOrEmpty(this.criteriaName) || !((Object) this.materialToReplace != (Object) null) || (!this.partialMatch || this.exactMatch) && (this.partialMatch || !this.exactMatch))
      return;
    Material material = new Material(this.materialToReplace);
    MeshRenderer[] componentsInChildren = this.gameObject.GetComponentsInChildren<MeshRenderer>(true);
    Debug.Log((object) ("RUNNING REPLACE SCRIPT ON " + componentsInChildren.Length.ToString() + " RENDERERS"), (Object) null);
    int num = 0;
    for (int index1 = 0; index1 < componentsInChildren.Length; ++index1)
    {
      MeshRenderer meshRenderer = componentsInChildren[index1];
      Material[] sharedMaterials = meshRenderer.sharedMaterials;
      for (int index2 = 0; index2 < sharedMaterials.Length; ++index2)
      {
        if ((Object) sharedMaterials[index2] != (Object) null && (this.partialMatch && sharedMaterials[index2].name.Contains(this.criteriaName) || this.exactMatch && sharedMaterials[index2].name == this.criteriaName))
        {
          sharedMaterials[index2] = material;
          ++num;
        }
      }
      meshRenderer.sharedMaterials = sharedMaterials;
    }
    Debug.Log((object) ("->>> ( " + num.ToString() + " ) <<<- " + num.ToString() + " MATERIALS REPLACED"), (Object) null);
    this.activate = false;
  }
}
