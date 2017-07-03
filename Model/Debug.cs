// Decompiled with JetBrains decompiler
// Type: Debug
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Internal;

public static class Debug
{
  public static bool developerConsoleVisible
  {
    get
    {
      return UnityEngine.Debug.developerConsoleVisible;
    }
    set
    {
      UnityEngine.Debug.developerConsoleVisible = value;
    }
  }

  public static bool isDebugBuild
  {
    get
    {
      return UnityEngine.Debug.isDebugBuild;
    }
  }

  public static void Assert(bool condition)
  {
  }

  public static void Assert(bool condition, string message)
  {
  }

  public static void AssertFormat(bool consition, string message, params object[] args)
  {
  }

  public static void Break()
  {
  }

  public static void ClearDeveloperConsole()
  {
  }

  public static void DebugBreak()
  {
  }

  public static void DrawLine(Vector3 start, Vector3 end)
  {
  }

  public static void DrawLine(Vector3 start, Vector3 end, Color color)
  {
  }

  public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration)
  {
  }

  public static void DrawLine(Vector3 start, Vector3 end, [DefaultValue("Color.white")] Color color, [DefaultValue("0.0f")] float duration, [DefaultValue("true")] bool depthTest)
  {
  }

  public static void DrawRay(Vector3 start, Vector3 dir)
  {
  }

  public static void DrawRay(Vector3 start, Vector3 dir, Color color)
  {
  }

  public static void DrawRay(Vector3 start, Vector3 dir, Color color, float duration)
  {
  }

  public static void DrawRay(Vector3 start, Vector3 dir, [DefaultValue("Color.white")] Color color, [DefaultValue("0.0f")] float duration, [DefaultValue("true")] bool depthTest)
  {
  }

  public static void LogInfo(object message, UnityEngine.Object context = null)
  {
  }

  public static void LogInfoCat(params object[] args)
  {
  }

  public static void Log(object message, UnityEngine.Object context = null)
  {
  }

  public static void LogCat(params object[] args)
  {
  }

  public static void LogError(object message, UnityEngine.Object context = null)
  {
    UnityEngine.Debug.LogError(message, context);
  }

  public static void LogErrorCat(params object[] args)
  {
    UnityEngine.Debug.LogError((object) string.Concat(args));
  }

  public static void LogErrorFormat(string format, params object[] args)
  {
    UnityEngine.Debug.LogErrorFormat(format, args);
  }

  public static void LogErrorFormat(UnityEngine.Object context, string format, params object[] args)
  {
    UnityEngine.Debug.LogErrorFormat(context, format, args);
  }

  public static void LogException(Exception exception)
  {
    UnityEngine.Debug.LogException(exception);
  }

  public static void LogException(Exception exception, UnityEngine.Object context)
  {
    UnityEngine.Debug.LogException(exception, context);
  }

  public static void LogFormat(string format, params object[] args)
  {
  }

  public static void LogFormat(UnityEngine.Object context, string format, params object[] args)
  {
  }

  public static void LogWarning(object message, UnityEngine.Object context = null)
  {
  }

  public static void LogWarningCat(params object[] args)
  {
  }

  public static void LogWarningFormat(string format, params object[] args)
  {
  }

  public static void LogWarningFormat(UnityEngine.Object context, string format, params object[] args)
  {
  }
}
