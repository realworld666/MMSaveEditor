using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class Player : TeamPrincipal
{
	public static PlayerDefaultData defaultData = new PlayerDefaultData();
	public static float financialStatChangePerWeek = 0.05f;
	public static float loyaltyStatBonusPerWeek = 0.05f;
	public static float loyaltyStatPenaltyForFiring = 0.2f;
	public static float loyaltyStatPenaltyForApplyingOtherTeam = 0.5f;
	public static float raceManagementStatMaxChange = 0.5f;
	public static float raceManagementStatPositionMultiplier = 0.1f;
	public static int jobApplicationDaysForRemoval = 30;
	public int createdTeamID = -1;
	public TrophyHistory trophyHistory = new TrophyHistory();
	public List<DialogCriteria> dialogSystemGlobalMemoryList = new List<DialogCriteria>();
	public PlayerGameData playerGameData = new PlayerGameData();
	private List<PlayerJobApplication> mJobApplications = new List<PlayerJobApplication>();
	private PlayerBackStory mPlayerBackStory = new PlayerBackStory();
	private PromisesController mPromisesController = new PromisesController();
	public const int playerDobMinYear = 1930;
	public const int playerDobMaxYear = 1996;
	public bool hasSetPortrait;
	public bool hasCreatedTeam;
	private Team mPlayerTeam;

	public Team team
	{
		get
		{
			return this.mPlayerTeam;
		}
		set
		{
			this.mPlayerTeam = value;
		}
	}

	public enum LoyaltyChange
	{
		FiringTeamMember,
		ApplyingOtherTeam,
	}
}
