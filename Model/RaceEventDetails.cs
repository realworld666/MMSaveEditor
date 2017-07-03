
using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class RaceEventDetails
{
  public List<SessionDetails> sessions = new List<SessionDetails>();
  public RaceEventResults results = new RaceEventResults();
  public DateTime eventDate;
  public Circuit circuit;
  private int mSessionID;
  private int mNextSessionID;
  private bool mIsEventEnding;
  private bool mHasEventEnded;
  private int mSessionPointer;
  private int mNextSessionPointer;
  private List<SessionDetails> mPracticeSessions;
  private List<SessionDetails> mQualifyingSessions;
  private List<SessionDetails> mRaceSessions;
    
}
