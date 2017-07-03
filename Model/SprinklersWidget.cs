// Decompiled with JetBrains decompiler
// Type: SprinklersWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;

public class SprinklersWidget : MonoBehaviour
{
  public Animator animator;
  public TextMeshProUGUI label;
  private float mDisplayTimer;
  private bool mStayOn;

  public void OnEnter()
  {
    Game.instance.sessionManager.raceDirector.sprinklersDirector.OnSprinklersOn -= new Action(this.Activate);
    Game.instance.sessionManager.raceDirector.sprinklersDirector.OnSprinklersOn += new Action(this.Activate);
    Game.instance.sessionManager.raceDirector.sprinklersDirector.OnSprinklersOff -= new Action(this.Deactivate);
    Game.instance.sessionManager.raceDirector.sprinklersDirector.OnSprinklersOff += new Action(this.Deactivate);
    this.gameObject.SetActive(false);
  }

  private void Activate()
  {
    this.ActivateGameObject(Localisation.LocaliseID("PSG_10011506", (GameObject) null));
    this.mStayOn = true;
  }

  private void Deactivate()
  {
    this.ActivateGameObject(Localisation.LocaliseID("PSG_10011507", (GameObject) null));
    this.mStayOn = false;
  }

  private void ActivateGameObject(string inTitle)
  {
    this.mDisplayTimer = 0.0f;
    this.gameObject.SetActive(true);
    this.label.text = inTitle;
  }

  private void Update()
  {
    if (this.mStayOn)
      return;
    this.mDisplayTimer += Time.deltaTime;
    AnimatorStateInfo animatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
    if ((double) this.mDisplayTimer > 4.5 && animatorStateInfo.IsName("ScaleandAlphaBox"))
      this.animator.SetTrigger("Close");
    if (!animatorStateInfo.IsName("Close") || (double) animatorStateInfo.normalizedTime < 1.0)
      return;
    this.gameObject.SetActive(false);
  }

  private void OnDestroy()
  {
    if (!Game.IsActive())
      return;
    Game.instance.sessionManager.raceDirector.sprinklersDirector.OnSprinklersOn -= new Action(this.Activate);
    Game.instance.sessionManager.raceDirector.sprinklersDirector.OnSprinklersOff -= new Action(this.Deactivate);
  }
}
