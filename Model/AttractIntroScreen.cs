// Decompiled with JetBrains decompiler
// Type: AttractIntroScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;

public class AttractIntroScreen : BaseMovieScreen
{
  public float timeBeforeContinueAllowed = 2f;
  public TextMeshProUGUI continueLabel;
  public GameObject subtitles;
  private float mContinueTimer;
  private bool mIsExiting;

  public bool isReadyToContinue
  {
    get
    {
      if (!this.hasFinished)
        return this.mIsExiting;
      return true;
    }
  }

  public override void OnEnter()
  {
    this.mContinueTimer = 0.0f;
    this.mIsExiting = false;
    GameUtility.SetActive(this.continueLabel.gameObject, false);
    this.PlayMovie(new string[1]{ "AttractIntro" });
    GameUtility.SetActive(this.subtitles, !Localisation.currentLanguage.Contains("English"));
    base.OnEnter();
  }

  protected override void Update()
  {
    this.mContinueTimer += GameTimer.deltaTime;
    bool inIsActive = (double) this.mContinueTimer >= (double) this.timeBeforeContinueAllowed;
    GameUtility.SetActive(this.continueLabel.gameObject, inIsActive);
    if (inIsActive && Input.anyKey)
    {
      this.VideoStop();
      this.AudioStop();
      this.Continue();
    }
    else
      base.Update();
  }

  protected override void Continue()
  {
    if (this.mIsExiting)
      return;
    this.mIsExiting = true;
    base.Continue();
  }
}
