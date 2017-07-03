// Decompiled with JetBrains decompiler
// Type: UIPlayerWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPlayerWidget : MonoBehaviour
{
  public UITeamLogo teamLogo;
  public UICharacterPortrait portrait;
  public Flag flag;
  public GameObject team;
  public TextMeshProUGUI playerName;
  public TextMeshProUGUI age;
  public TextMeshProUGUI backStory;
  public Button editAppearance;
  private Player mPlayer;

  public void OnStart()
  {
    if (!((UnityEngine.Object) this.editAppearance != (UnityEngine.Object) null))
      return;
    this.editAppearance.onClick.AddListener(new UnityAction(this.OnEditAppearance));
  }

  public void Setup(Player inPlayer)
  {
    if (inPlayer == null)
      return;
    this.mPlayer = inPlayer;
    this.portrait.SetPortrait((Person) this.mPlayer);
    this.flag.SetNationality(this.mPlayer.nationality);
    this.playerName.text = this.mPlayer.name;
    this.age.text = this.mPlayer.GetAge().ToString();
    StringVariableParser.subject = (Person) this.mPlayer;
    this.backStory.text = this.mPlayer.playerBackStoryString;
    StringVariableParser.subject = (Person) null;
    this.teamLogo.SetTeam(this.mPlayer.team);
  }

  private void OnEditAppearance()
  {
    UIManager.instance.ChangeScreen("CreatePlayerScreen", UIManager.ScreenTransition.None, 0.0f, (Action) null, UIManager.NavigationType.Normal);
  }
}
