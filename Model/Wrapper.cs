// Decompiled with JetBrains decompiler
// Type: ATM.Wrapper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Runtime.InteropServices;

namespace ATM
{
  public class Wrapper
  {
    [DllImport("AtmAgent", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool AtmCreateConfigurator();

    [DllImport("AtmAgent", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool AtmSetStandAlone(bool standalone);

    [DllImport("AtmAgent", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool AtmCreateAgent();

    [DllImport("AtmAgent", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool AtmStartAgent();

    [DllImport("AtmAgent", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool AtmExecuteNextCommand();

    [DllImport("AtmAgent", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool AtmDestroyAgent();

    [DllImport("AtmAgent", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool AtmRegisterCommandHandler([MarshalAs(UnmanagedType.LPStr)] string commandName, [MarshalAs(UnmanagedType.FunctionPtr)] Wrapper.CommandExecutor executor);

    [DllImport("AtmAgent", CallingConvention = CallingConvention.Cdecl)]
    public static extern void AtmReportCommandSuccess([MarshalAs(UnmanagedType.LPStr)] string result);

    [DllImport("AtmAgent", CallingConvention = CallingConvention.Cdecl)]
    public static extern void AtmReportCommandFailure([MarshalAs(UnmanagedType.LPStr)] string message);

    [DllImport("AtmAgent", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool AtmRegisterRegressionFinishedCallback([MarshalAs(UnmanagedType.FunctionPtr)] Wrapper.RegressionFinishedCallback callback);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void RegressionFinishedCallback();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void CommandExecutor([MarshalAs(UnmanagedType.LPStr)] string input);
  }
}
