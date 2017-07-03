// Decompiled with JetBrains decompiler
// Type: UISponsorsCamera
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;

public class UISponsorsCamera : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
  public GameObject cameraHover;
  public SponsorsScreen screen;
  private int mCameraID;

  public void Setup(int inCameraID)
  {
    if ((Object) this.screen.studioScene != (Object) null)
      this.screen.studioScene.studioSponsorCameraController.Setup(Game.instance.player.team.championship);
    this.mCameraID = inCameraID + 1;
    GameUtility.SetActive(this.cameraHover, false);
  }

  public void OnPointerEnter(PointerEventData inEventData)
  {
    if (!((Object) this.screen.studioScene != (Object) null))
      return;
    this.screen.studioScene.studioSponsorCameraController.SetCamera(this.mCameraID);
    GameUtility.SetActive(this.cameraHover, true);
  }

  public void OnPointerExit(PointerEventData inEventData)
  {
    if (!((Object) this.screen.studioScene != (Object) null))
      return;
    this.screen.studioScene.studioSponsorCameraController.SetCamera(0);
    GameUtility.SetActive(this.cameraHover, false);
  }
}
