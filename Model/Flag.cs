// Decompiled with JetBrains decompiler
// Type: Flag
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class Flag : MonoBehaviour
{
  [SerializeField]
  private Flag.Resolution resolution;

  public void SetNationality(Nationality inNationality)
  {
    if (inNationality == (Nationality) null)
    {
      Debug.LogError((object) "Trying to set flag with a null nationality. Check all warnings for ( No Nationality Found )", (Object) null);
    }
    else
    {
      Image component = this.GetComponent<Image>();
      string str1 = "Flags-64X64-";
      if (this.resolution == Flag.Resolution.Low)
        str1 = "Flags-64X64-";
      Sprite sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, str1 + inNationality.flagSpritePathName);
      Debug.Assert((Object) sprite != (Object) null, string.Format("{0} Flag could not be found: {1}", (object) (str1 + inNationality.flagSpritePathName), (object) inNationality.localisedCountry));
      component.sprite = sprite;
      if (this.resolution != Flag.Resolution.High || !(component.sprite.name != str1 + inNationality.flagSpritePathName))
        return;
      string str2 = "Flags-64X64-";
      component.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, str2 + inNationality.flagSpritePathName);
    }
  }

  public enum Resolution
  {
    Low,
    High,
  }
}
