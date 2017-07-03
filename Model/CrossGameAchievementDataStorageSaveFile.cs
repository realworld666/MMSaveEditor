// Decompiled with JetBrains decompiler
// Type: MM2.CrossGameAchievementDataStorageSaveFile
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using FullSerializer;
using LZ4ps;
using System;
using System.IO;
using System.Text;
using UnityEngine;
using Wenzil.Console;

namespace MM2
{
  internal static class CrossGameAchievementDataStorageSaveFile
  {
    private static readonly string saveFilePath = "Cloud" + (object) System.IO.Path.DirectorySeparatorChar + "Achievements.ach";
    private const int saveSizeLimit = 268435456;
    private const int saveFileSignature = 1630694765;
    private const int saveFileVersion = 1;

    public static bool saveFileExists
    {
      get
      {
        return File.Exists(CrossGameAchievementDataStorageSaveFile.saveFileFullPath);
      }
    }

    private static string saveFileFullPath
    {
      get
      {
        return System.IO.Path.Combine(Application.persistentDataPath, CrossGameAchievementDataStorageSaveFile.saveFilePath);
      }
    }

    private static string saveFileFullBackupPath
    {
      get
      {
        return CrossGameAchievementDataStorageSaveFile.saveFileFullPath + ".bak";
      }
    }

    public static void Save(CrossGameAchievementDataStorage.Storage storage)
    {
      try
      {
        File.Copy(CrossGameAchievementDataStorageSaveFile.saveFileFullPath, CrossGameAchievementDataStorageSaveFile.saveFileFullBackupPath);
      }
      catch (Exception ex)
      {
      }
      try
      {
        fsData data;
        new fsSerializer().TrySerialize<CrossGameAchievementDataStorage.Storage>(storage, out data).AssertSuccessWithoutWarnings();
        byte[] bytes = Encoding.UTF8.GetBytes(fsJsonPrinter.CompressedJson(data));
        GameUtility.Assert(bytes.Length < 268435456, "Uh-oh. Ben has underestimated how large save files might get, and we're about to save a file so large it will be detected as corrupt when loading. Best increase the limit!", (UnityEngine.Object) null);
        byte[] buffer = LZ4Codec.Encode32(bytes, 0, bytes.Length);
        using (FileStream fileStream = File.Create(CrossGameAchievementDataStorageSaveFile.saveFileFullPath))
        {
          using (BinaryWriter binaryWriter = new BinaryWriter((Stream) fileStream))
          {
            binaryWriter.Write(1630694765);
            binaryWriter.Write(1);
            binaryWriter.Write(buffer.Length);
            binaryWriter.Write(bytes.Length);
            binaryWriter.Write(buffer);
          }
        }
      }
      catch (Exception ex)
      {
        Debug.LogErrorFormat("Failed to save CrossGameAchievementDataStorage due to exception: {0}", (object) ex);
      }
    }

    public static CrossGameAchievementDataStorage.Storage Load()
    {
      try
      {
        fsSerializer fsSerializer = new fsSerializer();
        using (FileStream fileStream = File.Open(CrossGameAchievementDataStorageSaveFile.saveFileFullPath, FileMode.Open))
        {
          using (BinaryReader binaryReader = new BinaryReader((Stream) fileStream))
          {
            if (binaryReader.ReadInt32() != 1630694765)
              throw new SaveException("Save file is not a valid save file for this game");
            int num1 = binaryReader.ReadInt32();
            if (num1 < 1)
              throw new SaveException("Save file is an old format, and no upgrade path exists - must be from an old unsupported development version");
            if (num1 > 1)
              throw new SaveException("Save file version is newer than the game version! It's either corrupt, or the game executable is out of date");
            int num2 = binaryReader.ReadInt32();
            int outputLength = binaryReader.ReadInt32();
            if (outputLength > 268435456)
              throw new SaveException("Save file game data size is apparently way too big - file has either been tampered with or become corrupt");
            fsData data;
            fsResult fsResult1 = fsJsonParser.Parse(Encoding.UTF8.GetString(LZ4Codec.Decode32(binaryReader.ReadBytes(num2), 0, num2, outputLength)), out data);
            if (fsResult1.Failed)
              Debug.LogErrorFormat("Error reported whilst parsing serialized Storage data string: {0}", (object) fsResult1.FormattedMessages);
            CrossGameAchievementDataStorage.Storage instance = (CrossGameAchievementDataStorage.Storage) null;
            fsResult fsResult2 = fsSerializer.TryDeserialize<CrossGameAchievementDataStorage.Storage>(data, ref instance);
            if (fsResult2.Failed)
              Debug.LogErrorFormat("Error reported whilst deserializing Storage data: {0}", (object) fsResult2.FormattedMessages);
            return instance;
          }
        }
      }
      catch (Exception ex)
      {
        Debug.LogErrorFormat("Failed to load CrossGameAchievementDataStorage due to exception: {0}", (object) ex);
      }
      return (CrossGameAchievementDataStorage.Storage) null;
    }

    public static ConsoleCommandResult Delete(params string[] inStrings)
    {
      if (!CrossGameAchievementDataStorageSaveFile.saveFileExists)
        return ConsoleCommandResult.Failed((string) null);
      File.Delete(CrossGameAchievementDataStorageSaveFile.saveFileFullPath);
      try
      {
        File.Delete(CrossGameAchievementDataStorageSaveFile.saveFileFullBackupPath);
      }
      catch (Exception ex)
      {
      }
      App.instance.crossGameAchievementData = new CrossGameAchievementDataStorage();
      return ConsoleCommandResult.Succeeded((string) null);
    }
  }
}
