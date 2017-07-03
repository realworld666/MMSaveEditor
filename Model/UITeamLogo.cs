// Decompiled with JetBrains decompiler
// Type: UITeamLogo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class UITeamLogo : MonoBehaviour
{
  private int mTeamID = -1;
  [SerializeField]
  private UITeamLogo.Type type;
  [SerializeField]
  private UITeamLogo.TeamType teamType;
  [SerializeField]
  private Image logo;
  [SerializeField]
  private GameObject unemployed;
  [SerializeField]
  private GameObject customLogo;
  private UITeamCreateLogoStyleEntry mCustomTeamLogo;
  private bool runAwake;

  private void Awake()
  {
    this.LoadCustomTeamLogo();
    if (this.runAwake)
      return;
    this.runAwake = true;
    if (this.type == UITeamLogo.Type.Team)
    {
      if (!((Object) this.logo == (Object) null))
        return;
      this.logo = this.GetComponentInChildren<Image>();
      if (!((Object) this.logo == (Object) null))
        return;
      this.logo = this.GetComponent<Image>();
    }
    else
    {
      if (!((Object) this.logo == (Object) null))
        return;
      this.logo = this.GetComponent<Image>();
    }
  }

  public void SetTeam(Team inTeam)
  {
    this.mTeamID = inTeam == null || inTeam is NullTeam ? -1 : inTeam.teamID;
    this.UpdateLogo();
  }

  public void SetTeam(int teamID)
  {
    this.mTeamID = teamID;
    this.UpdateLogo();
  }

  private void UpdateLogo()
  {
    if ((Object) this.logo == (Object) null || (Object) this.unemployed == (Object) null)
      this.Awake();
    if ((Object) this.logo != (Object) null)
      GameUtility.SetActive(this.logo.gameObject, this.mTeamID != -1);
    if ((Object) this.unemployed != (Object) null)
    {
      GameUtility.SetActive(this.unemployed, this.mTeamID == -1);
      if (this.mTeamID == -1)
        GameUtility.SetActive(this.customLogo, false);
    }
    if (!((Object) this.logo != (Object) null) || this.mTeamID == -1)
      return;
    string inName1 = string.Empty;
    bool inIsActive = Game.IsActive() && Game.instance.player.hasCreatedTeam && Game.instance.player.createdTeamID == this.mTeamID;
    switch (this.type)
    {
      case UITeamLogo.Type.Team:
        if (inIsActive)
        {
          this.LoadCustomTeamLogo();
        }
        else
        {
          Texture2D teamLogo = App.instance.assetManager.GetTeamLogo(this.mTeamID, this.teamType, this.type);
          if ((Object) teamLogo != (Object) null)
          {
            this.logo.sprite = Sprite.Create(teamLogo, new Rect(0.0f, 0.0f, (float) teamLogo.width, (float) teamLogo.height), Vector2.zero);
          }
          else
          {
            switch (this.teamType)
            {
              case UITeamLogo.TeamType.LowRez:
                inName1 = "Small";
                break;
              case UITeamLogo.TeamType.BlackAndWhite:
                inName1 = "White";
                break;
            }
            this.logo.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Logos1, "TeamLogos-TeamLogo" + (object) (this.mTeamID + 1) + inName1);
          }
        }
        GameUtility.SetActive(this.logo.gameObject, !inIsActive);
        GameUtility.SetActive(this.customLogo, inIsActive);
        break;
      case UITeamLogo.Type.Hat:
        if (inIsActive)
        {
          inName1 = "CharacterLogos-HatLogos-CustomHatTeamLogo1";
        }
        else
        {
          Texture2D teamLogo = App.instance.assetManager.GetTeamLogo(this.mTeamID, this.teamType, this.type);
          if ((Object) teamLogo != (Object) null)
            this.logo.sprite = Sprite.Create(teamLogo, new Rect(0.0f, 0.0f, (float) teamLogo.width, (float) teamLogo.height), Vector2.zero);
          else
            inName1 = !this.HasCustomTeamLogo() ? "CharacterLogos-HatLogos-HatTeamLogo" + (object) (this.mTeamID + 1) : "CharacterLogos-HatLogos-CustomHatTeamLogo1";
        }
        this.logo.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Logos1, inName1);
        break;
      case UITeamLogo.Type.Body:
        string inName2;
        if (inIsActive)
        {
          inName2 = "CharacterLogos-BodyLogos-CustomBodyTeamLogo1";
        }
        else
        {
          Texture2D teamLogo = App.instance.assetManager.GetTeamLogo(this.mTeamID, this.teamType, this.type);
          if ((Object) teamLogo != (Object) null)
          {
            this.logo.sprite = Sprite.Create(teamLogo, new Rect(0.0f, 0.0f, (float) teamLogo.width, (float) teamLogo.height), Vector2.zero);
            break;
          }
          inName2 = !this.HasCustomTeamLogo() ? "CharacterLogos-BodyLogos-BodyTeamLogo" + (object) (this.mTeamID + 1) : "CharacterLogos-BodyLogos-CustomBodyTeamLogo1";
        }
        this.logo.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Logos1, inName2);
        break;
    }
  }

  private bool HasCustomTeamLogo()
  {
    return (Object) App.instance.assetManager.GetTeamLogo(this.mTeamID, UITeamLogo.TeamType.HighRez, UITeamLogo.Type.Team) != (Object) null;
  }

  private void LoadCustomTeamLogo()
  {
    if (this.type != UITeamLogo.Type.Team || !Game.IsActive() || !Game.instance.player.hasCreatedTeam)
      return;
    Team entity = Game.instance.teamManager.GetEntity(Game.instance.player.createdTeamID);
    if ((Object) this.mCustomTeamLogo == (Object) null || this.mCustomTeamLogo.styleID != entity.customLogo.styleID)
    {
      if ((Object) this.mCustomTeamLogo != (Object) null)
        UIManager.instance.teamCustomLogoManager.ReturnTeamCustomLogo(this.mCustomTeamLogo);
      this.mCustomTeamLogo = UIManager.instance.teamCustomLogoManager.GetTeamCustomLogoCopy(entity.customLogo.styleID, this.customLogo);
    }
    if (!((Object) this.mCustomTeamLogo != (Object) null))
      return;
    this.mCustomTeamLogo.SetMode(this.teamType != UITeamLogo.TeamType.BlackAndWhite ? UITeamCreateLogo.ColorMode.Colored : UITeamCreateLogo.ColorMode.BlackWhite);
    this.mCustomTeamLogo.SetMainName(entity.customLogo.teamFirstName);
    this.mCustomTeamLogo.SetSubName(entity.customLogo.teamLasttName);
    this.mCustomTeamLogo.RefreshStyle(entity.GetTeamColor());
    GameUtility.SetActive(this.mCustomTeamLogo.gameObject, true);
  }

  public void SetCustomTeamLogo(int inPresetId, string inTeamFirstName, string inTeamLastName, TeamColor inTeamColor)
  {
    if ((Object) this.mCustomTeamLogo == (Object) null || this.mCustomTeamLogo.styleID != inPresetId)
    {
      if ((Object) this.mCustomTeamLogo != (Object) null)
        UIManager.instance.teamCustomLogoManager.ReturnTeamCustomLogo(this.mCustomTeamLogo);
      this.mCustomTeamLogo = UIManager.instance.teamCustomLogoManager.GetTeamCustomLogoCopy(inPresetId, this.customLogo);
    }
    if (!((Object) this.mCustomTeamLogo != (Object) null))
      return;
    this.mCustomTeamLogo.SetMode(this.teamType != UITeamLogo.TeamType.BlackAndWhite ? UITeamCreateLogo.ColorMode.Colored : UITeamCreateLogo.ColorMode.BlackWhite);
    this.mCustomTeamLogo.SetMainName(inTeamFirstName);
    this.mCustomTeamLogo.SetSubName(inTeamLastName);
    this.mCustomTeamLogo.RefreshStyle(inTeamColor);
    GameUtility.SetActive(this.logo.gameObject, false);
    GameUtility.SetActive(this.unemployed, false);
    GameUtility.SetActive(this.customLogo, true);
    GameUtility.SetActive(this.mCustomTeamLogo.gameObject, true);
  }

  private void OnDestroy()
  {
    if (!UIManager.InstanceExists || !((Object) this.mCustomTeamLogo != (Object) null))
      return;
    UIManager.instance.teamCustomLogoManager.ReturnTeamCustomLogo(this.mCustomTeamLogo);
  }

  public enum Type
  {
    Team,
    Hat,
    Body,
  }

  public enum TeamType
  {
    HighRez,
    LowRez,
    BlackAndWhite,
  }
}
