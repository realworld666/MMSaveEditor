// Decompiled with JetBrains decompiler
// Type: WeatherWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class WeatherWidget : UIBaseSessionTopBarWidget
{
  private Weather mWeather = new Weather();
  public UIWeatherIcon currentWeatherIcons;
  private SessionWeatherDetails mSessionWeatherDetails;
  private SessionInformationDropdown dropdownScript;

  private void Awake()
  {
    this.keybinding = KeyBinding.Name.WeatherConditions;
    this.useKeyBinding = true;
    this.dropdownScript = this.dropdown.GetComponent<SessionInformationDropdown>();
  }

  private void OnEnable()
  {
    if (!Game.IsActive() || Game.instance.sessionManager == null)
      return;
    this.mSessionWeatherDetails = Game.instance.sessionManager.currentSessionWeather;
  }

  public void Update()
  {
    this.mSessionWeatherDetails.GetWeather(this.mSessionWeatherDetails.normalizedTimeElapsed, ref this.mWeather);
    this.currentWeatherIcons.SetIcon(this.mWeather);
    this.CheckKeyBinding();
  }

  public override void ToggleDropdown(bool inValue)
  {
    if (this.dropdownScript.dataType != SessionInformationDropdown.DataType.Clouds)
    {
      inValue = true;
      GameUtility.SetActive(this.dropdown, false);
    }
    if (inValue)
      this.OpenDropdown();
    else
      this.CloseDropdown();
  }

  public override void OpenDropdown()
  {
    this.dropdown.GetComponent<SessionInformationDropdown>().dataType = SessionInformationDropdown.DataType.Clouds;
    GameUtility.SetActive(this.dropdown, true);
  }
}
