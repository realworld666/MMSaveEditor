// Decompiled with JetBrains decompiler
// Type: SerializableMethodInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Reflection;
using UnityEngine;

[Serializable]
public class SerializableMethodInfo
{
  [SerializeField]
  private string _TypeName;
  [SerializeField]
  private string _MethodName;

  public MethodInfo _MethodInfo
  {
    get
    {
      return System.Type.GetType(this._TypeName).GetMethod(this._MethodName);
    }
  }

  public SerializableMethodInfo(MethodInfo mi)
  {
    this._TypeName = mi.DeclaringType.FullName;
    this._MethodName = mi.Name;
  }
}
