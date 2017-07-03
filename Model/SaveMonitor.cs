// Decompiled with JetBrains decompiler
// Type: PSG.Utils.SaveMonitor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;

namespace PSG.Utils
{
  public class SaveMonitor : IDisposable
  {
    private Thread mFred;
    private HashSet<object> mIgnoredObjects;
    private HashSet<System.Type> mIgnoredTypes;
    private List<SaveMonitor.CollectionInfo> mCollectionInfoList;
    private StringBuilder mStringBuilder;
    private bool mShouldExit;
    private bool disposedValue;

    public SaveMonitor()
    {
      this.mStringBuilder = new StringBuilder(640);
      this.mIgnoredObjects = new HashSet<object>();
      this.mIgnoredObjects.Add((object) Game.instance);
      this.mIgnoredTypes = new HashSet<System.Type>();
      this.mIgnoredTypes.Add(typeof (long));
      this.mIgnoredTypes.Add(typeof (ulong));
      this.mIgnoredTypes.Add(typeof (short));
      this.mIgnoredTypes.Add(typeof (ushort));
      this.mIgnoredTypes.Add(typeof (int));
      this.mIgnoredTypes.Add(typeof (uint));
      this.mIgnoredTypes.Add(typeof (float));
      this.mIgnoredTypes.Add(typeof (double));
      this.mIgnoredTypes.Add(typeof (IntPtr));
      this.mIgnoredTypes.Add(typeof (UIntPtr));
      this.mCollectionInfoList = new List<SaveMonitor.CollectionInfo>();
      Debug.Assert(Game.instance != null);
      try
      {
        this.FindAllCollections((object) Game.instance, new List<ushort>());
      }
      catch (Exception ex)
      {
        Debug.LogException(ex);
      }
      this.mStringBuilder.Append("SaveMonitor: Found ");
      this.mStringBuilder.Append(this.mCollectionInfoList.Count);
      this.mStringBuilder.Append(" collections.");
      Debug.Log((object) this.mStringBuilder.ToString(), (UnityEngine.Object) null);
      this.mStringBuilder.Length = 0;
      this.StartMonitoring();
    }

    private void FindAllCollections(object root, List<ushort> path)
    {
      if (root is ICollection)
      {
        SaveMonitor.CollectionInfo collectionInfo = new SaveMonitor.CollectionInfo();
        collectionInfo.mCollection = root as ICollection;
        collectionInfo.mCount = collectionInfo.mCollection.Count;
        collectionInfo.mPath = new ushort[path.Count];
        path.CopyTo(collectionInfo.mPath);
        this.mCollectionInfoList.Add(collectionInfo);
      }
      FieldInfo[] fields = root.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
      for (ushort index = 0; (int) index < fields.Length; ++index)
      {
        FieldInfo fieldInfo = fields[(int) index];
        bool flag = false;
        foreach (object customAttribute in fieldInfo.GetCustomAttributes(false))
        {
          if (customAttribute is NonSerializedAttribute)
          {
            flag = true;
            break;
          }
        }
        if (!flag)
        {
          object root1 = fieldInfo.GetValue(root);
          if (root1 != null && !this.mIgnoredObjects.Contains(root1) && !this.mIgnoredTypes.Contains(root1.GetType()))
          {
            this.mIgnoredObjects.Add(root1);
            path.Add(index);
            this.FindAllCollections(root1, path);
            path.RemoveAt(path.Count - 1);
          }
        }
      }
    }

    private void StartMonitoring()
    {
      this.mFred = new Thread(new ThreadStart(this.ThreadMain));
      this.mFred.Start();
    }

    private void ThreadMain()
    {
      try
      {
        while (!this.mShouldExit)
        {
          using (List<SaveMonitor.CollectionInfo>.Enumerator enumerator = this.mCollectionInfoList.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              SaveMonitor.CollectionInfo current = enumerator.Current;
              if (current.mCollection.Count != current.mCount)
              {
                this.mStringBuilder.Length = 0;
                this.mStringBuilder.Append("Collection changed at ");
                this.mStringBuilder.Append(current.Path);
                Debug.LogError((object) this.mStringBuilder.ToString(), (UnityEngine.Object) null);
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        Debug.LogException(ex);
      }
    }

    protected virtual void Dispose(bool disposing)
    {
      if (this.disposedValue)
        return;
      if (disposing)
      {
        this.mShouldExit = true;
        this.mFred.Join();
      }
      this.disposedValue = true;
    }

    public void Dispose()
    {
      this.Dispose(true);
    }

    private class CollectionInfo
    {
      public ICollection mCollection;
      public ushort[] mPath;
      public int mCount;

      public string Path
      {
        get
        {
          StringBuilder stringBuilder = new StringBuilder(640);
          stringBuilder.Append("Game.instance");
          object instance = (object) Game.instance;
          for (int index = 0; index < this.mPath.Length; ++index)
          {
            FieldInfo field = instance.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)[(int) this.mPath[index]];
            stringBuilder.Append(".");
            stringBuilder.Append(field.Name);
            instance = field.GetValue(instance);
          }
          return stringBuilder.ToString();
        }
      }
    }
  }
}
