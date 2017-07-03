// Decompiled with JetBrains decompiler
// Type: Sega.PrismUnity
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using Sega.EventDefs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace Sega
{
  [DisallowMultipleComponent]
  internal class PrismUnity : MonoBehaviour
  {
    private static string s_AppID;
    private static string s_UserID;
    private static string s_SessionGUID;
    private static PrismUnity s_instance;
    private bool delayedQuitTriggered;

    public static PrismUnity Instance
    {
      get
      {
        return PrismUnity.s_instance;
      }
    }

    public void SendRaceData(ref RaceEventResults results, RaceEventDetails eventDetails)
    {
      RaceEnd raceEnd = new RaceEnd();
      raceEnd.sessionID = PrismUnity.s_SessionGUID;
      raceEnd.gamecode = PrismUnity.s_AppID;
      raceEnd.steamID = PrismUnity.s_UserID;
      List<RaceEventResults.ResultData> resultData1 = results.GetResultsForSession(SessionDetails.SessionType.Race).resultData;
      RaceEventResults.ResultData resultData2 = (RaceEventResults.ResultData) null;
      RaceEventResults.ResultData resultData3 = (RaceEventResults.ResultData) null;
      for (int index = 0; index < resultData1.Count; ++index)
      {
        if (resultData1[index].team.IsPlayersTeam())
        {
          if (resultData2 == null)
            resultData2 = resultData1[index];
          else
            resultData3 = resultData1[index];
        }
      }
      raceEnd.TeamName = resultData2.team.name;
      raceEnd.TrackName = eventDetails.circuit.locationName;
      raceEnd.TrackLayout = eventDetails.circuit.trackLayout.ToString();
      raceEnd.D1RacePosition = resultData2.position;
      raceEnd.D1GridPosition = resultData2.gridPosition;
      raceEnd.D1BestLapTime = resultData2.bestLapTime;
      raceEnd.D1Laps = resultData2.laps;
      raceEnd.D1Stops = resultData2.stops;
      raceEnd.D2RacePosition = resultData3.position;
      raceEnd.D2GridPosition = resultData3.gridPosition;
      raceEnd.D2BestLapTime = resultData3.bestLapTime;
      raceEnd.D2Laps = resultData3.laps;
      raceEnd.D2Stops = resultData3.stops;
      this.StartCoroutine(this.WaitForJsonRequest(JsonUtility.ToJson((object) raceEnd), false));
    }

    public static void Initialise(string AppID, string UserID)
    {
      PrismUnity.s_AppID = AppID;
      PrismUnity.s_UserID = UserID;
      if ((UnityEngine.Object) PrismUnity.s_instance == (UnityEngine.Object) null)
      {
        GameObject go = new GameObject("SegaPrism");
        UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(go, FirstActiveSceneHolder.firstActiveScene);
        go.AddComponent<PrismUnity>();
      }
      else
        Debug.LogError((object) "Error : Sega Prism initialised twice..", (UnityEngine.Object) null);
    }

    private void OnApplicationQuit()
    {
      if ((UnityEngine.Object) PrismUnity.s_instance != (UnityEngine.Object) this || this.delayedQuitTriggered)
        return;
      Application.CancelQuit();
      PrismUnity.s_instance.SendShutdownAnalytics(PrismUnity.s_AppID, PrismUnity.s_UserID);
      PrismUnity.s_instance = (PrismUnity) null;
    }

    private void Awake()
    {
      if ((UnityEngine.Object) PrismUnity.s_instance != (UnityEngine.Object) null)
      {
        UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
      }
      else
      {
        PrismUnity.s_instance = this;
        this.SendInitAnalytics(PrismUnity.s_AppID, PrismUnity.s_UserID);
      }
    }

    private void SendInitAnalytics(string AppID, string UserID)
    {
      PrismUnity.s_SessionGUID = Guid.NewGuid().ToString("N");
      string json = JsonUtility.ToJson((object) new GameSessionStart() { sessionID = PrismUnity.s_SessionGUID, steamID = PrismUnity.s_UserID, gamecode = PrismUnity.s_AppID, system_processor = SystemInfo.processorType, system_memory = ((long) SystemInfo.systemMemorySize * 1048576L).ToString(), system_os = SystemInfo.operatingSystem, system_display = SystemInfo.graphicsDeviceName, system_display_memory = ((long) SystemInfo.graphicsMemorySize * 1048576L).ToString(), hwid = this.GetSystemFingerPrint(), crc = this.GetAppHash() });
      Console.WriteLine(json);
      this.StartCoroutine(this.WaitForJsonRequest(json, false));
    }

    private void SendShutdownAnalytics(string AppID, string UserID)
    {
      this.StartCoroutine(this.WaitForJsonRequest(JsonUtility.ToJson((object) new GameSessionEnd()
      {
        sessionID = PrismUnity.s_SessionGUID,
        steamID = PrismUnity.s_UserID,
        gamecode = PrismUnity.s_AppID
      }), true));
    }

    private string GetSystemFingerPrint()
    {
      return SystemInfo.deviceUniqueIdentifier;
    }

    private string GetAppHash()
    {
      return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(new ASCIIEncoding().GetBytes(Application.genuine.ToString() + Application.productName + Application.version + Application.unityVersion + Application.genuineCheckAvailable.ToString()))).Replace("-", string.Empty);
    }

    [DebuggerHidden]
    private IEnumerator WaitForJsonRequest(string jsonBlob, bool delayedQuit = false)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new PrismUnity.\u003CWaitForJsonRequest\u003Ec__Iterator10() { jsonBlob = jsonBlob, delayedQuit = delayedQuit, \u003C\u0024\u003EjsonBlob = jsonBlob, \u003C\u0024\u003EdelayedQuit = delayedQuit };
    }
  }
}
