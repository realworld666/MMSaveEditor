// Decompiled with JetBrains decompiler
// Type: Singleton`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Reflection;

public class Singleton<T> where T : class
{
  public static T instance
  {
    get
    {
      return Singleton<T>.SingletonCreator.instance;
    }
  }

  public static void EnsureInstanceExists()
  {
    if ((object) Singleton<T>.instance != null)
      return;
    Debug.LogError((object) "Couldn't create Singleton", (UnityEngine.Object) null);
  }

  private static class SingletonCreator
  {
    internal static readonly T instance = Singleton<T>.SingletonCreator.CreateInstance();

    internal static T CreateInstance()
    {
      ConstructorInfo constructor = typeof (T).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, System.Type.DefaultBinder, System.Type.EmptyTypes, (ParameterModifier[]) null);
      if (constructor != null)
        return constructor.Invoke((object[]) null) as T;
      Debug.LogException(new Exception("Singleton class doesn't have a private constructor."));
      return (T) null;
    }
  }
}
