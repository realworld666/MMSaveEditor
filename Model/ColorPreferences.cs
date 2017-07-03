// Decompiled with JetBrains decompiler
// Type: ColorPreferences
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ColorPreferences
{
  public static readonly Color[] defaultCustomColors = new Color[8]{ GameUtility.HexStringToColour("#E54343"), GameUtility.HexStringToColour("#FFB200"), GameUtility.HexStringToColour("#92437E"), GameUtility.HexStringToColour("#3171CF"), GameUtility.HexStringToColour("#4EAF9E"), GameUtility.HexStringToColour("#756A22"), GameUtility.HexStringToColour("#294B31"), GameUtility.HexStringToColour("#FF6B00") };
  public static readonly int minColors = 1;
  public static readonly int maxColors = 16;
  public static readonly string colorNamePreset = "CustomColor";
  private static List<Color> mColors = new List<Color>();
  private static bool mColorsChanged = false;

  public static Color[] customColors
  {
    get
    {
      return ColorPreferences.mColors.ToArray();
    }
  }

  public static void LoadCustomColors()
  {
    ColorPreferences.mColors.Clear();
    for (int inIndex = 1; inIndex <= ColorPreferences.maxColors; ++inIndex)
    {
      string customColorName = ColorPreferences.GetCustomColorName(inIndex);
      if (PlayerPrefs.HasKey(customColorName))
        ColorPreferences.mColors.Add(ColorPreferences.GetCustomColor(customColorName));
    }
    if (ColorPreferences.mColors.Count <= 0)
      ColorPreferences.SetDefaultColors();
    ColorPreferences.mColorsChanged = false;
  }

  public static void SetDefaultColors()
  {
    for (int index = 0; index < ColorPreferences.defaultCustomColors.Length; ++index)
      ColorPreferences.AddCustomColor(ColorPreferences.defaultCustomColors[index]);
    ColorPreferences.SaveCustomColors();
  }

  public static void SaveCustomColors()
  {
    if (!ColorPreferences.mColorsChanged)
      return;
    for (int inIndex = ColorPreferences.mColors.Count + 1; inIndex <= ColorPreferences.maxColors; ++inIndex)
    {
      string customColorName = ColorPreferences.GetCustomColorName(inIndex);
      if (PlayerPrefs.HasKey(customColorName))
        PlayerPrefs.DeleteKey(customColorName);
    }
    int num = Mathf.Min(ColorPreferences.mColors.Count, ColorPreferences.maxColors);
    for (int index = 0; index < num; ++index)
      ColorPreferences.SaveCustomColor(ColorPreferences.GetCustomColorName(index + 1), ColorPreferences.mColors[index]);
    PlayerPrefs.Save();
    ColorPreferences.mColorsChanged = false;
  }

  public static bool AddCustomColor(Color inColor)
  {
    if (ColorPreferences.HasCustomColor(inColor) || ColorPreferences.mColors.Count >= ColorPreferences.maxColors)
      return false;
    ColorPreferences.mColors.Add(inColor);
    ColorPreferences.mColorsChanged = true;
    return true;
  }

  public static bool RemoveCustomColor(Color inColor)
  {
    for (int index = ColorPreferences.mColors.Count - 1; index >= 0; --index)
    {
      if (ColorPreferences.mColors[index] == inColor)
      {
        ColorPreferences.mColors.RemoveAt(index);
        ColorPreferences.mColorsChanged = true;
        return true;
      }
    }
    return false;
  }

  public static bool CanAddCustomColor(Color inColor)
  {
    if (ColorPreferences.mColors.Count < ColorPreferences.maxColors)
      return !ColorPreferences.HasCustomColor(inColor);
    return false;
  }

  public static bool CanRemoveCustomColor(Color inColor)
  {
    if (ColorPreferences.mColors.Count >= ColorPreferences.minColors + 1)
      return ColorPreferences.HasCustomColor(inColor);
    return false;
  }

  private static bool HasCustomColor(Color inColor)
  {
    int count = ColorPreferences.mColors.Count;
    for (int index = 0; index < count; ++index)
    {
      if (ColorPreferences.mColors[index] == inColor)
        return true;
    }
    return false;
  }

  private static Color GetCustomColor(string inColorName)
  {
    string[] strArray = PlayerPrefs.GetString(inColorName).Split(',');
    return new Color(float.Parse(strArray[0]), float.Parse(strArray[1]), float.Parse(strArray[2]), float.Parse(strArray[3]));
  }

  private static void SaveCustomColor(string inColorName, Color inColor)
  {
    using (GameUtility.StringBuilderWrapper builderSafe = GameUtility.GlobalStringBuilderPool.GetBuilderSafe())
    {
      StringBuilder stringBuilder = builderSafe.stringBuilder;
      stringBuilder.Append(inColor.r.ToString());
      stringBuilder.Append(",");
      stringBuilder.Append(inColor.g.ToString());
      stringBuilder.Append(",");
      stringBuilder.Append(inColor.b.ToString());
      stringBuilder.Append(",");
      stringBuilder.Append(inColor.a.ToString());
      string str = stringBuilder.ToString();
      PlayerPrefs.SetString(inColorName, str);
    }
  }

  private static string GetCustomColorName(int inIndex)
  {
    using (GameUtility.StringBuilderWrapper builderSafe = GameUtility.GlobalStringBuilderPool.GetBuilderSafe())
    {
      StringBuilder stringBuilder = builderSafe.stringBuilder;
      stringBuilder.Append(ColorPreferences.colorNamePreset);
      stringBuilder.Append(inIndex);
      return stringBuilder.ToString();
    }
  }
}
