// Decompiled with JetBrains decompiler
// Type: Dilemma
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

public class Dilemma
{
  private string mText = string.Empty;
  private float mDisplayDuration = 5f;
  private Dilemma.Type mType;
  private Driver mDriver;

  public Dilemma.Type type
  {
    get
    {
      return this.mType;
    }
  }

  public Driver driver
  {
    get
    {
      return this.mDriver;
    }
  }

  public string text
  {
    get
    {
      return this.mText;
    }
  }

  public float displayDuration
  {
    get
    {
      return this.mDisplayDuration;
    }
  }

  public void SetDilemma(Dilemma.Type inType, Driver inDriver)
  {
    this.mType = inType;
    this.mDriver = inDriver;
    switch (this.mType)
    {
      case Dilemma.Type.FuelCritical:
        this.mText = "We need to turn the engine mode down, or I'm not going to make it to the end of the race!";
        this.mDisplayDuration = 10f;
        break;
      case Dilemma.Type.TyreWearCritical:
        this.mText = "Boss, my tyres are totally worn! We need to pit for fresh tyres, or I'll lose loads of time!";
        this.mDisplayDuration = 5f;
        break;
      case Dilemma.Type.TyreCompoundWrongForTrackConditions:
        this.mText = "These tyres are totally wrong for these conditions! Can I pit for new tyres?";
        this.mDisplayDuration = 5f;
        break;
    }
  }

  public void DoAssociatedAction()
  {
    switch (this.mType)
    {
    }
  }

  public enum Type
  {
    FuelCritical,
    TyreWearCritical,
    TyreCompoundWrongForTrackConditions,
  }
}
