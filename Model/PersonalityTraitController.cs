
using FullSerializer;
using System;
using System.Collections.Generic;
using System.Text;

[fsObject("v1", new System.Type[] {}, MemberSerialization = fsMemberSerialization.OptOut)]
public class PersonalityTraitController
{
  public List<PersonalityTrait> permanentPersonalityTraits = new List<PersonalityTrait>();
  public List<PersonalityTrait> temporaryPersonalityTraits = new List<PersonalityTrait>();
  public List<int> mTraitHistory = new List<int>();
  public readonly int mMaxCooldownDaysRange = 180;
  public DateTime cooldownPeriodEnd = new DateTime();
  public DriverStats mDriverStats = new DriverStats();
  public int mLastRandomCooldownDayValue;
  public Driver mDriver;
  public List<PersonalityTrait> mTraits;
    
}
