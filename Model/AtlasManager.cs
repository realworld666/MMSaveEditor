// Decompiled with JetBrains decompiler
// Type: AtlasManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

public class AtlasManager
{
  private Dictionary<string, Sprite>[] mSprites;
  private bool mIsUsingMods;

  public AtlasManager()
  {
    this.mSprites = new Dictionary<string, Sprite>[11];
    this.LoadDefaultAtlases();
    this.UpdateAtlasesWithMods();
  }

  private void LoadDefaultAtlases()
  {
    int num = 11;
    for (int index1 = 0; index1 < num; ++index1)
    {
      this.mSprites[index1] = new Dictionary<string, Sprite>();
      Sprite[] spriteArray = Resources.LoadAll<Sprite>("UI/Atlases/" + Enum.GetName(typeof (AtlasManager.Atlas), (object) index1) + this.GetResolutionString());
      for (int index2 = 0; index2 < spriteArray.Length; ++index2)
        this.mSprites[index1][spriteArray[index2].name] = spriteArray[index2];
    }
  }

  public void UpdateAtlasesWithMods()
  {
    int num = 11;
    bool flag = false;
    for (int index = 0; index < num; ++index)
    {
      Texture2D atlasTexture = App.instance.assetManager.GetAtlasTexture((AtlasManager.Atlas) index);
      if ((UnityEngine.Object) atlasTexture != (UnityEngine.Object) null)
      {
        flag = true;
        this.mIsUsingMods = true;
        this.ChangeSpriteTexturesWithMods((AtlasManager.Atlas) index, atlasTexture);
      }
    }
    if (flag || !this.mIsUsingMods)
      return;
    this.mIsUsingMods = false;
    this.LoadDefaultAtlases();
  }

  private void ChangeSpriteTexturesWithMods(AtlasManager.Atlas inAtlas, Texture2D inTexture)
  {
    Dictionary<string, Sprite>.Enumerator enumerator = this.mSprites[(int) inAtlas].GetEnumerator();
    Dictionary<string, Sprite> dictionary = new Dictionary<string, Sprite>();
    while (enumerator.MoveNext())
    {
      Sprite sprite = enumerator.Current.Value;
      string key = enumerator.Current.Key;
      dictionary[key] = Sprite.Create(inTexture, sprite.rect, sprite.pivot);
    }
    this.mSprites[(int) inAtlas] = dictionary;
  }

  public Sprite GetSprite(AtlasManager.Atlas inAtlas, string inName)
  {
    Sprite sprite = (Sprite) null;
    if (inAtlas < AtlasManager.Atlas.Count)
    {
      int index = (int) inAtlas;
      try
      {
        sprite = this.mSprites[index][inName];
      }
      catch (KeyNotFoundException ex)
      {
        Debug.LogWarningFormat("AtlasManager couldn't find sprite \"{0}\" in atlas \"{1}\"", new object[2]
        {
          (object) inName,
          (object) inAtlas.ToString()
        });
      }
    }
    return sprite;
  }

  private string GetResolutionString()
  {
    return "@1080";
  }

  public enum Atlas
  {
    Frontend1,
    Shared1,
    Simulation1,
    StartUp1,
    Logos1,
    TrackOutlines1,
    TrackImages,
    HQIcons,
    CharacterPortraits1,
    CarSelectImages1,
    GTCarSelectImages1,
    Count,
  }
}
