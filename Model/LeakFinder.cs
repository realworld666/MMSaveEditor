// Decompiled with JetBrains decompiler
// Type: PSG.Utils.LeakFinder
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Wenzil.Console;

namespace PSG.Utils
{
  public class LeakFinder
  {
    private static List<LeakFinder.ScanDataEntry> _LastScan;
    private static List<LeakFinder.AssemblyInfo> _ScanList;
    private static int _ScanIndex;

    static LeakFinder()
    {
      Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
      LeakFinder._ScanList = new List<LeakFinder.AssemblyInfo>(assemblies.Length);
      foreach (Assembly assembly in assemblies)
      {
        if (assembly == Assembly.GetExecutingAssembly())
          LeakFinder._ScanList.Add(new LeakFinder.AssemblyInfo(assembly, true));
        else
          LeakFinder._ScanList.Add(new LeakFinder.AssemblyInfo(assembly, false));
      }
    }

    public static ConsoleCommandResult Execute(params string[] inStrings)
    {
      if (inStrings.Length > 0)
      {
        string inString = inStrings[0];
        if (inString != null)
        {
          // ISSUE: reference to a compiler-generated field
          if (LeakFinder.\u003C\u003Ef__switch\u0024map42 == null)
          {
            // ISSUE: reference to a compiler-generated field
            LeakFinder.\u003C\u003Ef__switch\u0024map42 = new Dictionary<string, int>(4)
            {
              {
                "clear",
                0
              },
              {
                "list",
                1
              },
              {
                "add",
                2
              },
              {
                "remove",
                3
              }
            };
          }
          int num;
          // ISSUE: reference to a compiler-generated field
          if (LeakFinder.\u003C\u003Ef__switch\u0024map42.TryGetValue(inString, out num))
          {
            switch (num)
            {
              case 0:
                LeakFinder._LastScan = (List<LeakFinder.ScanDataEntry>) null;
                return ConsoleCommandResult.Succeeded("Cleared last scan data.");
              case 1:
                return LeakFinder.ListAssemblies();
              case 2:
                return LeakFinder.AddAssembly(inStrings);
              case 3:
                return LeakFinder.RemoveAssembly(inStrings);
            }
          }
        }
        return ConsoleCommandResult.Failed("Unknown command");
      }
      try
      {
        LeakFinder.Execute();
      }
      catch (Exception ex)
      {
        return ConsoleCommandResult.Failed(ex.Message);
      }
      return ConsoleCommandResult.Succeeded((string) null);
    }

    private static void Execute()
    {
      Debug.LogFormat("{0} Scan Started", (object) LeakFinder._ScanIndex);
      List<short> path = new List<short>();
      HashSet<object> objectsScanned = new HashSet<object>();
      List<LeakFinder.ScanDataEntry> scanDataEntryList = new List<LeakFinder.ScanDataEntry>();
      objectsScanned.Add((object) LeakFinder._ScanList);
      if (LeakFinder._LastScan != null)
        objectsScanned.Add((object) LeakFinder._LastScan);
      using (List<LeakFinder.AssemblyInfo>.Enumerator enumerator = LeakFinder._ScanList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          LeakFinder.AssemblyInfo current = enumerator.Current;
          if (current._ShouldSearch)
          {
            foreach (System.Type type in current._Assembly.GetTypes())
            {
              FieldInfo[] fields = type.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
              for (short index = 0; (int) index < fields.Length; ++index)
              {
                FieldInfo fieldInfo = fields[(int) index];
                if (!fieldInfo.Name.StartsWith("<>f__switch$map"))
                {
                  object obj = (object) null;
                  try
                  {
                    obj = fieldInfo.GetValue((object) null);
                  }
                  catch (Exception ex)
                  {
                  }
                  if (obj != null)
                  {
                    path.Add(index);
                    LeakFinder.Execute_Recursive(type, path, obj, objectsScanned, scanDataEntryList);
                    path.RemoveAt(path.Count - 1);
                  }
                }
              }
            }
          }
        }
      }
      if (LeakFinder._LastScan == null)
      {
        LeakFinder._LastScan = scanDataEntryList;
      }
      else
      {
        LeakFinder.ScanDataEntry.Compare(scanDataEntryList, LeakFinder._LastScan);
        LeakFinder._LastScan = scanDataEntryList;
      }
      Debug.LogFormat("{0} Scan Complete - found {1} Containers", new object[2]
      {
        (object) LeakFinder._ScanIndex,
        (object) LeakFinder._LastScan.Count
      });
      ++LeakFinder._ScanIndex;
    }

    private static void Execute_Recursive(System.Type root, List<short> path, object obj, HashSet<object> objectsScanned, List<LeakFinder.ScanDataEntry> thisScan)
    {
      try
      {
        if (objectsScanned.Contains(obj))
          return;
        objectsScanned.Add(obj);
      }
      catch (NullReferenceException ex)
      {
        return;
      }
      int containerSize = LeakFinder.GetContainerSize(obj);
      if (containerSize > 0)
        thisScan.Add(new LeakFinder.ScanDataEntry()
        {
          _Root = root,
          _Path = path.ToArray(),
          _Count = containerSize
        });
      FieldInfo[] fields = obj.GetType().GetFields();
      for (short index = 0; (int) index < fields.Length; ++index)
      {
        object obj1 = fields[(int) index].GetValue(obj);
        if (obj1 != null)
        {
          path.Add(index);
          LeakFinder.Execute_Recursive(root, path, obj1, objectsScanned, thisScan);
          path.RemoveAt(path.Count - 1);
        }
      }
    }

    private static int GetContainerSize(object obj)
    {
      if (obj is ICollection)
        return (obj as ICollection).Count;
      return 0;
    }

    private static ConsoleCommandResult ListAssemblies()
    {
      StringBuilder stringBuilder = new StringBuilder(640);
      for (int index = 0; index < LeakFinder._ScanList.Count; ++index)
      {
        stringBuilder.AppendFormat("{0}:{1} - {2}", (object) index, (object) LeakFinder._ScanList[index]._ShouldSearch, (object) LeakFinder._ScanList[index]._Assembly.ManifestModule.Name);
        stringBuilder.AppendLine();
      }
      return ConsoleCommandResult.Succeeded(stringBuilder.ToString());
    }

    private static ConsoleCommandResult AddAssembly(string[] inStrings)
    {
      if (inStrings.Length < 2)
        return ConsoleCommandResult.Failed("Please enter the index of the assembly you wish to add");
      int result;
      if (!int.TryParse(inStrings[1], out result))
        return ConsoleCommandResult.Failed("Unable to parse " + inStrings[2] + " as int");
      if (result >= 0 && result < LeakFinder._ScanList.Count)
      {
        if (LeakFinder._ScanList[result]._ShouldSearch)
          return ConsoleCommandResult.Failed((string) null);
        LeakFinder._ScanList[result]._ShouldSearch = true;
        LeakFinder._LastScan = (List<LeakFinder.ScanDataEntry>) null;
        return ConsoleCommandResult.Succeeded("Added " + LeakFinder._ScanList[result]._Assembly.FullName + " to search");
      }
      return ConsoleCommandResult.Failed("Index " + (object) result + " out of range. [0-" + (object) LeakFinder._ScanList.Count + "]");
    }

    private static ConsoleCommandResult RemoveAssembly(string[] inStrings)
    {
      if (inStrings.Length < 2)
        return ConsoleCommandResult.Failed("Please enter the index of the assembly you wish to remove");
      int result;
      if (!int.TryParse(inStrings[1], out result))
        return ConsoleCommandResult.Failed("Unable to parse " + inStrings[2] + " as int");
      if (result >= 0 && result < LeakFinder._ScanList.Count)
      {
        if (!LeakFinder._ScanList[result]._ShouldSearch)
          return ConsoleCommandResult.Failed((string) null);
        LeakFinder._ScanList[result]._ShouldSearch = false;
        LeakFinder._LastScan = (List<LeakFinder.ScanDataEntry>) null;
        return ConsoleCommandResult.Succeeded("Removed " + LeakFinder._ScanList[result]._Assembly.FullName + " from search");
      }
      return ConsoleCommandResult.Failed("Index " + (object) result + " out of range. [0-" + (object) LeakFinder._ScanList.Count + "]");
    }

    private class ScanDataEntry
    {
      public System.Type _Root;
      public short[] _Path;
      public int _Count;

      public string Path
      {
        get
        {
          StringBuilder stringBuilder = new StringBuilder(640);
          stringBuilder.Append(this._Root.Name);
          stringBuilder.Append(".");
          FieldInfo field1 = this._Root.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)[(int) this._Path[0]];
          stringBuilder.Append(field1.Name);
          object obj = field1.GetValue((object) null);
          for (int index = 1; index < this._Path.Length; ++index)
          {
            FieldInfo field2 = obj.GetType().GetFields()[(int) this._Path[index]];
            stringBuilder.Append(".");
            stringBuilder.Append(field2.Name);
            obj = field2.GetValue(obj);
          }
          return stringBuilder.ToString();
        }
      }

      public bool HasSamePath(LeakFinder.ScanDataEntry other)
      {
        if (this._Root != other._Root || this._Path.Length != other._Path.Length)
          return false;
        for (int index = 0; index < this._Path.Length; ++index)
        {
          if ((int) this._Path[index] != (int) other._Path[index])
            return false;
        }
        return true;
      }

      public static void Compare(List<LeakFinder.ScanDataEntry> currentList, List<LeakFinder.ScanDataEntry> prevList)
      {
        string str = LeakFinder._ScanIndex.ToString();
        StringBuilder stringBuilder = new StringBuilder(640);
        for (int index1 = 0; index1 < currentList.Count; ++index1)
        {
          LeakFinder.ScanDataEntry current = currentList[index1];
          int index2 = -1;
          for (int index3 = 0; index3 < prevList.Count; ++index3)
          {
            LeakFinder.ScanDataEntry prev = prevList[index3];
            if (current.HasSamePath(prev))
            {
              index2 = index3;
              break;
            }
          }
          if (index2 != -1)
          {
            LeakFinder.ScanDataEntry prev = prevList[index2];
            if (current._Count != prev._Count)
            {
              stringBuilder.Append(str);
              stringBuilder.Append(": ");
              stringBuilder.Append(current.Path);
              stringBuilder.Append(": ");
              stringBuilder.Append(prev._Count);
              stringBuilder.Append(" -> ");
              stringBuilder.Append(current._Count);
              Debug.LogWarning((object) stringBuilder.ToString(), (UnityEngine.Object) null);
              stringBuilder.Length = 0;
            }
            prevList.RemoveAt(index2);
          }
          else
          {
            stringBuilder.Append(str);
            stringBuilder.Append(": ");
            stringBuilder.Append(current.Path);
            stringBuilder.Append(": 0 -> ");
            stringBuilder.Append(current._Count);
            Debug.LogWarning((object) stringBuilder.ToString(), (UnityEngine.Object) null);
            stringBuilder.Length = 0;
          }
        }
      }
    }

    private class AssemblyInfo
    {
      public Assembly _Assembly;
      public bool _ShouldSearch;

      public AssemblyInfo(Assembly assembly, bool shouldSearch)
      {
        this._Assembly = assembly;
        this._ShouldSearch = shouldSearch;
      }
    }
  }
}
