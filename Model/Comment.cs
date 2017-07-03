
using FullSerializer;
using System.Collections.Generic;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class Comment
{
	public static Dictionary<Comment.CommentType, string> commentSource = new Dictionary<Comment.CommentType, string>() { { Comment.CommentType.BlueFlags, "BlueFlags" }, { Comment.CommentType.Crashes, "Crashes" }, { Comment.CommentType.DefendingPosition, "DefendingPosition" }, { Comment.CommentType.DriverComesIn, "DriverComesIn" }, { Comment.CommentType.DriverDropsPosition, "DriverDropsPosition" }, { Comment.CommentType.DriverExitingPitlane, "DriverExitingPitlane" }, { Comment.CommentType.DriverLeavesPits, "DriverLeavesPits" }, { Comment.CommentType.DriverStartsFlyingLap, "DriverStartsFlyingLap" }, { Comment.CommentType.FastestPitstop, "FastestPitstop" }, { Comment.CommentType.Lockups, "Lockups" }, { Comment.CommentType.MechanicalIssue, "MechanicalIssue" }, { Comment.CommentType.NewDriverFastestLap, "NewDriverFastestLap" }, { Comment.CommentType.NewGreenSectorForPlayer, "NewGreenSectorForPlayer" }, { Comment.CommentType.NewPurpleSector, "NewPurpleSector" }, { Comment.CommentType.Overtakes, "Overtakes" }, { Comment.CommentType.DriverAttemptingOvertake, "DriverAttemptingOvertake" }, { Comment.CommentType.Retirement, "Retirements" }, { Comment.CommentType.Spins, "Spins" }, { Comment.CommentType.TeamOrders, "TeamOrders" }, { Comment.CommentType.PenaltyDriveThrough, "PenaltyDriveThrough" }, { Comment.CommentType.PenaltyTime, "PenaltyTime" }, { Comment.CommentType.New1stPlaceDriver, "New1stPlaceDriver" }, { Comment.CommentType.DriverOutOfFuel, "DriverOutOfFuel" }, { Comment.CommentType.SessionStart, "SessionStarts" }, { Comment.CommentType.SessionEnd, "SessionEnds" }, { Comment.CommentType.NewLap, "NewLap" }, { Comment.CommentType.NewDriverPersonalBestLap, "DriverSetsPB" }, { Comment.CommentType.Collision, "Collision" } };
	public static Dictionary<Comment.CommentType, int> commentPriorityList = new Dictionary<Comment.CommentType, int>() { { Comment.CommentType.BlueFlags, 0 }, { Comment.CommentType.Crashes, 3 }, { Comment.CommentType.DefendingPosition, 0 }, { Comment.CommentType.DriverComesIn, 0 }, { Comment.CommentType.DriverDropsPosition, 0 }, { Comment.CommentType.DriverExitingPitlane, 0 }, { Comment.CommentType.DriverLeavesPits, 0 }, { Comment.CommentType.DriverStartsFlyingLap, 0 }, { Comment.CommentType.FastestPitstop, 0 }, { Comment.CommentType.Lockups, 1 }, { Comment.CommentType.MechanicalIssue, 1 }, { Comment.CommentType.NewDriverFastestLap, 0 }, { Comment.CommentType.NewGreenSectorForPlayer, 0 }, { Comment.CommentType.NewPurpleSector, 1 }, { Comment.CommentType.Overtakes, 0 }, { Comment.CommentType.DriverAttemptingOvertake, 0 }, { Comment.CommentType.Retirement, 2 }, { Comment.CommentType.Spins, 1 }, { Comment.CommentType.TeamOrders, 0 }, { Comment.CommentType.PenaltyDriveThrough, 1 }, { Comment.CommentType.PenaltyTime, 1 }, { Comment.CommentType.New1stPlaceDriver, 1 }, { Comment.CommentType.DriverOutOfFuel, 1 }, { Comment.CommentType.SessionStart, 0 }, { Comment.CommentType.SessionEnd, 0 }, { Comment.CommentType.NewLap, 0 }, { Comment.CommentType.NewDriverPersonalBestLap, 1 }, { Comment.CommentType.Collision, 1 } };
	public TextDynamicData text = new TextDynamicData();
	public Comment.CommentType commentType = Comment.CommentType.BlueFlags;
	public string timeOrLapOfTheComment;
	public Vehicle vehicle;
	public Weather weather;
	public float commentPriority;

	public enum CommentType
	{
		NewDriverFastestLap,
		NewDriverPersonalBestLap,
		DriverLeavesPits,
		DriverComesIn,
		DriverStartsFlyingLap,
		MechanicalIssue,
		Retirement,
		Spins,
		Lockups,
		Crashes,
		Overtakes,
		DriverAttemptingOvertake,
		DefendingPosition,
		TeamOrders,
		BlueFlags,
		DriverDropsPosition,
		NewPurpleSector,
		NewGreenSectorForPlayer,
		FastestPitstop,
		DriverExitingPitlane,
		PenaltyDriveThrough,
		PenaltyTime,
		New1stPlaceDriver,
		DriverOutOfFuel,
		SessionStart,
		SessionEnd,
		NewLap,
		Collision,
	}
}
