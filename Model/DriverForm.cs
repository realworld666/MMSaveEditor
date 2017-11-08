
using FullSerializer;
using System.Collections.Generic;

[fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
public class DriverForm
{
    private DriverFormDesignData mFormDesignData;
    private float mInitialForm;
    private float[] mFormOverSession = new float[0];
    private Driver mDriver;
    private int mFormChunks;
    private DriverForm.Form mEventForm = DriverForm.Form.Rubish;
    private float mStintStartTime;
    private float mAverageRaceForm;
    private List<DriverForm.FormForEvent> mFormForEvents = new List<DriverForm.FormForEvent>();
    private bool mHasCrashed;
    private float mFormAtCrash;

    [fsObject(MemberSerialization = fsMemberSerialization.OptOut)]
    public class FormForEvent
    {
        public RaceEventDetails raceEvent;
        public Championship championship;
        public float raceSessionAverageForm;

        public float formForUI
        {
            get
            {
                return this.raceSessionAverageForm * 10f;
            }
        }
    }

    private enum Form
    {
        Perfect,
        Good,
        Average,
        Poor,
        Rubish,
    }
}
