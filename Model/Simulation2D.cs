// Decompiled with JetBrains decompiler
// Type: Simulation2D
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Simulation2D : MonoBehaviour
{
  public Vector2 position = Vector2.zero;
  public Vector2 size = Vector2.zero;
  public float scale = 1f;
  public static Simulation2D instance;
  public RawImage circuitImage;
  public MinimapWidget miniMapWidget;
  public Camera2D camera2D;
  public GameObject rain2DCamera;
  private Camera mCamera;
  private Circuit mCurrentCircuit;

  public Camera camera
  {
    get
    {
      return this.mCamera;
    }
  }

  private void Awake()
  {
    this.mCamera = this.camera2D.GetComponent<Camera>();
    this.mCamera.gameObject.SetActive(false);
    GameUtility.SetActive(this.circuitImage.gameObject, false);
    GameUtility.SetActive(this.miniMapWidget.gameObject, false);
    GameUtility.SetActive(this.rain2DCamera, false);
    this.camera2D.SetSimulation2D(this);
    Simulation2D.instance = this;
  }

  public void ClearSingletonInstance()
  {
    Simulation2D.instance = (Simulation2D) null;
  }

  public void SetCircuit(Circuit inCircuit)
  {
    if (this.mCurrentCircuit == inCircuit)
      return;
    this.mCurrentCircuit = inCircuit;
    this.circuitImage.texture = (Texture) UnityEngine.Resources.Load<Texture2D>("UI/2DTracks/" + this.mCurrentCircuit.spriteName);
  }

  private void SetupMinimap()
  {
    if (this.mCurrentCircuit == null)
      return;
    this.miniMapWidget.Setup();
    string spriteName = this.mCurrentCircuit.spriteName;
    if (spriteName != null)
    {
      // ISSUE: reference to a compiler-generated field
      if (Simulation2D.\u003C\u003Ef__switch\u0024map34 == null)
      {
        // ISSUE: reference to a compiler-generated field
        Simulation2D.\u003C\u003Ef__switch\u0024map34 = new Dictionary<string, int>(16)
        {
          {
            "Ardennes",
            0
          },
          {
            "Beijing",
            1
          },
          {
            "Black Sea",
            2
          },
          {
            "Cape Town",
            3
          },
          {
            "Doha",
            4
          },
          {
            "Dubai",
            5
          },
          {
            "Guildford",
            6
          },
          {
            "Madrid",
            7
          },
          {
            "Milan",
            8
          },
          {
            "Munich",
            9
          },
          {
            "Phoenix",
            10
          },
          {
            "Rio de Janeiro",
            11
          },
          {
            "Singapore",
            12
          },
          {
            "Sydney",
            13
          },
          {
            "Vancouver",
            14
          },
          {
            "Yokohama",
            15
          }
        };
      }
      int num;
      // ISSUE: reference to a compiler-generated field
      if (Simulation2D.\u003C\u003Ef__switch\u0024map34.TryGetValue(spriteName, out num))
      {
        switch (num)
        {
          case 0:
            this.miniMapWidget.SetPositionAndSize(new Vector2(37f, 1f), new Vector2(1436f, 944.7f));
            this.scale = 1.3f;
            break;
          case 1:
            this.miniMapWidget.SetPositionAndSize(new Vector2(83.4f, 50.2f), new Vector2(807.4f, 1064.3f));
            this.scale = 1.3f;
            break;
          case 2:
            this.miniMapWidget.SetPositionAndSize(new Vector2(37f, -0.7f), new Vector2(1434f, 618.91f));
            this.scale = 1.3f;
            break;
          case 3:
            this.miniMapWidget.SetPositionAndSize(new Vector2(180.7f, 64.6f), new Vector2(1360.3f, 795f));
            this.scale = 1.3f;
            break;
          case 4:
            this.miniMapWidget.SetPositionAndSize(new Vector2(-57.1f, 14f), new Vector2(1047f, 1184.3f));
            this.scale = 1.3f;
            break;
          case 5:
            this.miniMapWidget.SetPositionAndSize(new Vector2(71f, 18.2f), new Vector2(1431.4f, 1398f));
            this.scale = 1.3f;
            break;
          case 6:
            this.miniMapWidget.SetPositionAndSize(new Vector2(3f, -19.5f), new Vector2(1307.96f, 1018.9f));
            this.scale = 1.3f;
            break;
          case 7:
            this.miniMapWidget.SetPositionAndSize(new Vector2(5f, -45f), new Vector2(1356f, 1212.8f));
            this.scale = 1.3f;
            break;
          case 8:
            this.miniMapWidget.SetPositionAndSize(new Vector2(-42f, -1f), new Vector2(1166.9f, 1578f));
            this.scale = 1.3f;
            break;
          case 9:
            this.miniMapWidget.SetPositionAndSize(new Vector2(49.5f, 84.23f), new Vector2(1556.1f, 575.1f));
            this.scale = 1.3f;
            break;
          case 10:
            this.miniMapWidget.SetPositionAndSize(new Vector2(0.4f, -1.8f), new Vector2(1354.12f, 672.18f));
            this.scale = 1.3f;
            break;
          case 11:
            this.miniMapWidget.SetPositionAndSize(new Vector2(-78.31f, -10.6f), new Vector2(1293.5f, 881.2f));
            this.scale = 1.3f;
            break;
          case 12:
            this.miniMapWidget.SetPositionAndSize(new Vector2(-80f, -24f), new Vector2(1404f, 1610f));
            this.scale = 1.3f;
            break;
          case 13:
            this.miniMapWidget.SetPositionAndSize(new Vector2(9f, -33.2f), new Vector2(1670f, 1069f));
            this.scale = 1.3f;
            break;
          case 14:
            this.miniMapWidget.SetPositionAndSize(new Vector2(17f, -12.5f), new Vector2(1347f, 716.7f));
            this.scale = 1.3f;
            break;
          case 15:
            this.miniMapWidget.SetPositionAndSize(new Vector2(-27.9f, 3.7f), new Vector2(1543f, 1471.44f));
            this.scale = 1.3f;
            break;
        }
      }
    }
    this.miniMapWidget.RefreshWidget();
  }

  public void Show()
  {
    this.SetupMinimap();
    GameUtility.SetActive(this.circuitImage.gameObject, true);
    GameUtility.SetActive(this.miniMapWidget.gameObject, true);
    GameUtility.SetActive(this.mCamera.gameObject, true);
    GameUtility.SetActive(this.rain2DCamera, true);
    this.camera2D.LateUpdate();
  }

  public void Hide()
  {
    GameUtility.SetActive(this.circuitImage.gameObject, false);
    GameUtility.SetActive(this.miniMapWidget.gameObject, false);
    GameUtility.SetActive(this.mCamera.gameObject, false);
    GameUtility.SetActive(this.rain2DCamera, false);
  }

  private void Update()
  {
    if (this.miniMapWidget.loaded)
      ;
  }
}
