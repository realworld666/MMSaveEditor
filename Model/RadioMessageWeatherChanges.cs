// Decompiled with JetBrains decompiler
// Type: RadioMessageWeatherChanges
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using FullSerializer;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class RadioMessageWeatherChanges : RadioMessage
{
  private Weather mPreviousWeatherConditions;

  public RadioMessageWeatherChanges(RacingVehicle inVehicle, TeamRadio inTeamRadio)
    : base(inVehicle, inTeamRadio)
  {
    this.mPreviousWeatherConditions = Game.instance.sessionManager.currentSessionWeather.GetCurrentWeather();
    this.messageCategory = RadioMessage.MessageCategory.WeatherChange;
  }

  public override void OnLoad()
  {
  }

  protected override void OnSimulationUpdate()
  {
    if (this.mVehicle.timer.hasSeenChequeredFlag || this.mVehicle.pathState.IsInPitlaneArea())
      return;
    this.CheckForWeatherChanges();
  }

  private void CheckForWeatherChanges()
  {
    Weather currentWeather = Game.instance.sessionManager.currentSessionWeather.GetCurrentWeather();
    if (this.HasStartedRaining(currentWeather))
      this.CreateDialogQuery(currentWeather);
    else if (this.HasStoppedRaining(currentWeather))
      this.CreateDialogQuery(currentWeather);
    this.mPreviousWeatherConditions = currentWeather;
  }

  private void CreateDialogQuery(Weather inCurrentWeather)
  {
    DialogQuery inQuery = new DialogQuery();
    this.AddPersonCriteria(inQuery, (Person) this.mVehicle.driver);
    this.AddWeatherSourceCriteria(inQuery, inCurrentWeather);
    this.dialogRule = this.mQueryCreator.ProcessQueryWithOwnCriteria(inQuery, false);
    if (this.dialogRule == null)
      return;
    this.personWhoSpeaks = (Person) this.mVehicle.driver;
    this.SendRadioMessage();
  }

  private void AddWeatherSourceCriteria(DialogQuery inQuery, Weather inCurrentWeather)
  {
    if (this.HasStartedRaining(inCurrentWeather))
      inQuery.AddCriteria("Source", "WeatherChangeToRainy");
    else if (this.HasStoppedRaining(inCurrentWeather))
    {
      inQuery.AddCriteria("Source", "WeatherChangeRainStops");
    }
    else
    {
      if (!this.IsNowSunny(inCurrentWeather))
        return;
      inQuery.AddCriteria("Source", "WeatherChangeToSunny");
    }
  }

  private bool HasStoppedRaining(Weather inCurrentWeather)
  {
    bool flag1 = this.mPreviousWeatherConditions.rainType != Weather.RainType.None;
    bool flag2 = inCurrentWeather.rainType != Weather.RainType.None;
    if (flag1)
      return !flag2;
    return false;
  }

  private bool HasStartedRaining(Weather inCurrentWeather)
  {
    bool flag1 = this.mPreviousWeatherConditions.rainType != Weather.RainType.None;
    bool flag2 = inCurrentWeather.rainType != Weather.RainType.None;
    if (!flag1)
      return flag2;
    return false;
  }

  private bool IsNowSunny(Weather inCurrentWeather)
  {
    bool flag1 = this.mPreviousWeatherConditions.cloudType == Weather.CloudType.Clear;
    bool flag2 = inCurrentWeather.cloudType == Weather.CloudType.Clear;
    if (!flag1)
      return flag2;
    return false;
  }
}
