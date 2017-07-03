// Decompiled with JetBrains decompiler
// Type: GameObjectExtension
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Reflection;
using UnityEngine;

public static class GameObjectExtension
{
  public static T GetCopyOf<T>(this Component comp, T other) where T : Component
  {
    System.Type type = ((object) comp).GetType();
    if (type != other.GetType())
      return (T) null;
    BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
    foreach (PropertyInfo property in type.GetProperties(bindingAttr))
    {
      if (property.CanWrite)
      {
        try
        {
          property.SetValue((object) comp, property.GetValue((object) other, (object[]) null), (object[]) null);
        }
        catch
        {
        }
      }
    }
    foreach (FieldInfo field in type.GetFields(bindingAttr))
      field.SetValue((object) comp, field.GetValue((object) other));
    return comp as T;
  }

  public static T AddComponent<T>(this GameObject go, T toAdd) where T : Component
  {
    return go.AddComponent<T>().GetCopyOf<T>(toAdd);
  }
}
