
using FullSerializer;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class DriverMentalState
{
	private DriverMentalState.Emotion mEmotion = DriverMentalState.Emotion.Confident;
	private Smiley.Type mSmileyType = Smiley.Type.Happy;

	public DriverMentalState.Emotion emotion
	{
		get
		{
			return this.mEmotion;
		}
	}

	public Smiley.Type smileyType
	{
		get
		{
			return this.mSmileyType;
		}
	}

	public void SetEmotion( DriverMentalState.Emotion inStatus )
	{
		this.mEmotion = inStatus;
		switch( this.mEmotion )
		{
			case DriverMentalState.Emotion.Angry:
			case DriverMentalState.Emotion.VeryAngry:
				this.mSmileyType = Smiley.Type.Angry;
				break;
			case DriverMentalState.Emotion.Nervous:
			case DriverMentalState.Emotion.VeryNervous:
			case DriverMentalState.Emotion.Pessimistic:
				this.mSmileyType = Smiley.Type.Unhappy;
				break;
			case DriverMentalState.Emotion.Confident:
			case DriverMentalState.Emotion.FiredUp:
			case DriverMentalState.Emotion.Excited:
			case DriverMentalState.Emotion.Optimistic:
				this.mSmileyType = Smiley.Type.Happy;
				break;
			case DriverMentalState.Emotion.Confused:
			case DriverMentalState.Emotion.Distracted:
				this.mSmileyType = Smiley.Type.Angry;
				break;
		}
	}

	public string GetText()
	{
		return "\"" + this.mEmotion.ToString() + "\"";
	}

	public enum Emotion
	{
		Angry,
		VeryAngry,
		Nervous,
		VeryNervous,
		Pessimistic,
		Neutral,
		Focused,
		Confident,
		FiredUp,
		Excited,
		Optimistic,
		Confused,
		Distracted,
	}
}
