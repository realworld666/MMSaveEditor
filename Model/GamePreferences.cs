// Decompiled with JetBrains decompiler
// Type: GamePreferences
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public class GamePreferences
{
  private readonly int[] financeNegativePatternsWithSuffixSymbol = new int[8]{ 4, 5, 6, 7, 8, 10, 13, 15 };
  private PreferencesManager mManager;
  private string mDateFormatString;
  private string mAbbrevDateFormat;
  private string mDateFormatLongString;
  private string mCurrencyLocaleString;
  private PrefGameFinancesNegative.Type mFinanceNegativePattern;
  private PrefGameHeight.Type mHeightUnits;
  private PrefGameWeight.Type mWeightUnits;
  private PrefGameSpeedUnits.Type mSpeedUnits;
  private PrefGameTemperatureUnits.Type mTemperatureUnits;
  private PrefGameRaceLength.Type mRaceLength;
  private PrefGameAIDevDifficulty.Type mAIDevDifficulty;
  private CultureInfo mCurrencyCultre;

  public void Start(PreferencesManager inManager)
  {
    this.mManager = inManager;
  }

  public void Load()
  {
    this.SetLanguage(this.mManager.GetSettingEnum<PrefGameLanguage.Type>(Preference.pName.Game_Language, false));
    this.SetCurrencyLocaleString(this.mManager.GetSettingEnum<PrefGameCurrencySymbol.Type>(Preference.pName.Game_CurrencySymbol, false));
    this.SetTemperatureUnits(this.mManager.GetSettingEnum<PrefGameTemperatureUnits.Type>(Preference.pName.Game_Temperature, false));
    this.SetSpeedUnits(this.mManager.GetSettingEnum<PrefGameSpeedUnits.Type>(Preference.pName.Game_Speed, false));
    this.SetDateFormatString(this.mManager.GetSettingEnum<PrefGameDateFormat.Type>(Preference.pName.Game_DateFormat, false));
    this.SetWeightUnits(this.mManager.GetSettingEnum<PrefGameWeight.Type>(Preference.pName.Game_Weight, false));
    this.SetHeightUnits(this.mManager.GetSettingEnum<PrefGameHeight.Type>(Preference.pName.Game_Height, false));
    this.SetFinanceNegative(this.mManager.GetSettingEnum<PrefGameFinancesNegative.Type>(Preference.pName.Game_FinancesNegative, false));
    this.SetRaceLength(this.mManager.GetSettingEnum<PrefGameRaceLength.Type>(Preference.pName.Game_RaceLenghts, false));
    this.SetAIDevDifficulty(this.mManager.GetSettingEnum<PrefGameAIDevDifficulty.Type>(Preference.pName.Game_AIDevDifficulty, false));
  }

  public void SetLanguage(PrefGameLanguage.Type inLanguage)
  {
  }

  public void SetCurrencyLocaleString(PrefGameCurrencySymbol.Type inCurrencySymbol)
  {
    switch (inCurrencySymbol)
    {
      case PrefGameCurrencySymbol.Type.GBP:
        this.mCurrencyLocaleString = "en-GB";
        break;
      case PrefGameCurrencySymbol.Type.EUR:
        this.mCurrencyLocaleString = "de-DE";
        break;
      case PrefGameCurrencySymbol.Type.USD:
        this.mCurrencyLocaleString = "en-US";
        break;
      default:
        Debug.LogErrorFormat("Could not assign the enum {0}", (object) inCurrencySymbol);
        break;
    }
    this.RefreshCurrencyCultureFormat();
  }

  public void SetFinanceNegative(PrefGameFinancesNegative.Type inCurrencyNegativePattern)
  {
    this.mFinanceNegativePattern = inCurrencyNegativePattern;
    this.RefreshCurrencyCultureFormat();
  }

  public void SetDateFormatString(PrefGameDateFormat.Type inDateFormat)
  {
    switch (inDateFormat)
    {
      case PrefGameDateFormat.Type.DDMMYYYY:
        this.mAbbrevDateFormat = "d MMMM";
        this.mDateFormatString = "dd/MM/yyyy";
        this.mDateFormatLongString = "dd MMMM yyyy";
        break;
      case PrefGameDateFormat.Type.MMDDYYYY:
        this.mAbbrevDateFormat = "MMMM d";
        this.mDateFormatString = "MM/dd/yyyy";
        this.mDateFormatLongString = "MMMM dd yyyy";
        break;
      default:
        Debug.LogErrorFormat("Could not assign the enum {0}", (object) inDateFormat);
        break;
    }
  }

  public void SetRaceLength(PrefGameRaceLength.Type inRaceLength)
  {
    this.mRaceLength = inRaceLength;
  }

  public void SetAIDevDifficulty(PrefGameAIDevDifficulty.Type inAIDevDifficulty)
  {
    this.mAIDevDifficulty = inAIDevDifficulty;
  }

  public void SetTemperatureUnits(PrefGameTemperatureUnits.Type inTemperatureUnits)
  {
    this.mTemperatureUnits = inTemperatureUnits;
  }

  public void SetSpeedUnits(PrefGameSpeedUnits.Type inSpeedUnits)
  {
    this.mSpeedUnits = inSpeedUnits;
  }

  public void SetHeightUnits(PrefGameHeight.Type inHeightUnits)
  {
    this.mHeightUnits = inHeightUnits;
  }

  public void SetWeightUnits(PrefGameWeight.Type inWeightUnits)
  {
    this.mWeightUnits = inWeightUnits;
  }

  private void RefreshCurrencyCultureFormat()
  {
    try
    {
      this.mCurrencyCultre = CultureInfo.CreateSpecificCulture(this.mCurrencyLocaleString);
    }
    catch
    {
      Debug.LogErrorFormat("Could not use requested culture setting of {0}", (object) this.mCurrencyLocaleString);
      return;
    }
    if (Localisation.currentLanguage.Equals("English", StringComparison.Ordinal))
    {
      this.mCurrencyCultre.NumberFormat.CurrencyGroupSeparator = ",";
      this.mCurrencyCultre.NumberFormat.CurrencyDecimalSeparator = ".";
    }
    else
    {
      this.mCurrencyCultre.NumberFormat.CurrencyGroupSeparator = " ";
      this.mCurrencyCultre.NumberFormat.CurrencyDecimalSeparator = ",";
    }
    bool flag = ((IEnumerable<int>) this.financeNegativePatternsWithSuffixSymbol).Contains<int>(this.mCurrencyCultre.NumberFormat.CurrencyNegativePattern);
    switch (this.mFinanceNegativePattern)
    {
      case PrefGameFinancesNegative.Type.MinusSign:
        this.mCurrencyCultre.NumberFormat.CurrencyNegativePattern = !flag ? 1 : 5;
        break;
      case PrefGameFinancesNegative.Type.Brackets:
        this.mCurrencyCultre.NumberFormat.CurrencyNegativePattern = !flag ? 0 : 4;
        break;
      default:
        Debug.LogErrorFormat("Could not find negative finance pattern {0}", (object) this.mFinanceNegativePattern);
        break;
    }
    this.mCurrencyCultre.NumberFormat.CurrencyPositivePattern = !flag ? 0 : 1;
  }

  public PrefGameHeight.Type GetHeightUnits()
  {
    return this.mHeightUnits;
  }

  public PrefGameWeight.Type GetWeightUnits()
  {
    return this.mWeightUnits;
  }

  public PrefGameSpeedUnits.Type GetSpeedUnits()
  {
    return this.mSpeedUnits;
  }

  public PrefGameTemperatureUnits.Type GetTemperatureUnits()
  {
    return this.mTemperatureUnits;
  }

  public PrefGameRaceLength.Type GetRaceLength()
  {
    return this.mRaceLength;
  }

  public PrefGameAIDevDifficulty.Type GetAIDevDifficulty()
  {
    return this.mAIDevDifficulty;
  }

  public string GetCurrentDateFormat()
  {
    return this.mDateFormatString;
  }

  public string GetCurrentAbbrevDateFormat()
  {
    return this.mAbbrevDateFormat;
  }

  public string GetCurrentLongDateFormat()
  {
    return this.mDateFormatLongString;
  }

  public CultureInfo GetCurrencyCultureFormat()
  {
    return this.mCurrencyCultre;
  }
}
