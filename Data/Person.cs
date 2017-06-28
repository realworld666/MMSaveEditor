
using System;

public class Person : Entity, IEquatable<Person>, IComparable<Person>
{
	private string mFirstName = string.Empty;
	private string mLastName = string.Empty;
	private string mShortName = string.Empty;
	private string mThreeLetterName = string.Empty;
	public Nationality nationality = new Nationality();
	/*public Portrait portrait = new Portrait();
	public Popularity popularity = new Popularity();
	public Relationships relationships = new Relationships();
	public DialogQueryCreator dialogQuery = new DialogQueryCreator();
	public ContractManagerPerson contractManager = new ContractManagerPerson();
	public ContractPerson contract = new ContractPerson();
	public ContractPerson nextYearContract = new ContractPerson();
	public int peakDuration = RandomUtility.GetRandom( 2, 6 );
	public CareerHistory careerHistory = new CareerHistory();
	private StatModificationHistory mMoraleStatModificationHistory = new StatModificationHistory();*/
	private float mImprovementRateDecay;// = RandomUtility.GetRandom( Person.improvementRateDecayMin, Person.improvementRateDecayMax );
	private bool mIsShortlisted;
	public Person.Gender gender;
	public DateTime dateOfBirth;
	public int weight;
	public int retirementAge;
	public float obedience;
	public DateTime peakAge;
	private float mMorale;

	public enum Gender
	{
		Male,
		Female,
	}

	public int CompareTo( Person inPerson )
	{
		if( inPerson == null )
			return 1;
		return this.name.CompareTo( inPerson.name );
	}

	public bool Equals( Person inPerson )
	{
		if( inPerson == null )
			return false;
		return this.name.Equals( inPerson.name );
	}
}
