
using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class RadioMessageCarPart : RadioMessage
{
    private bool[] dilemmaFlags = new bool[2];
    private bool[] issuesFlags = new bool[3];
    private List<CarPart> mCarPartsWithIssues = new List<CarPart>();
    private List<CarPart> mAllCarPartsWithIssues = new List<CarPart>();
    private bool[] mHasFailureBeenTriggered;
    private bool[] mRedZoneBeenTriggered;


    private enum CarPartCondition
    {
        Low,
        RedZone,
        Failed,
    }
}
