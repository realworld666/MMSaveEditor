using FullSerializer;
using System;
using System.Collections.Generic;

[fsObject( MemberSerialization = fsMemberSerialization.OptOut )]
public class CarPartDesign
{
	private CarPart mCarPart;
	private CarPart mLastCarPart;
	public List<CarPartComponent> componentSlots = new List<CarPartComponent>();
	public List<CarPartComponent> componentBonusSlots = new List<CarPartComponent>();
	public List<int> componentBonusSlotsLevel = new List<int>();
	private Notification mNotification;
	private CalendarEvent_v1 mCalendarEvent;

	private Team mTeam;
	private CarPartComponent mRandomComponent;

	private Dictionary<CarPart.PartType, int> seasonPartStartingStat = new Dictionary<CarPart.PartType, int>();
	private Dictionary<int, List<CarPartComponent>> brakeComponents = new Dictionary<int, List<CarPartComponent>>();
	private Dictionary<int, List<CarPartComponent>> engineComponents = new Dictionary<int, List<CarPartComponent>>();
	private Dictionary<int, List<CarPartComponent>> frontWingComponents = new Dictionary<int, List<CarPartComponent>>();
	private Dictionary<int, List<CarPartComponent>> rearWingComponents = new Dictionary<int, List<CarPartComponent>>();
	private Dictionary<int, List<CarPartComponent>> suspensionComponents = new Dictionary<int, List<CarPartComponent>>();
	private Dictionary<int, List<CarPartComponent>> gearboxComponents = new Dictionary<int, List<CarPartComponent>>();
	private Dictionary<int, List<CarPartComponent>> engineGTComponents = new Dictionary<int, List<CarPartComponent>>();
	private Dictionary<int, List<CarPartComponent>> rearWingGTComponents = new Dictionary<int, List<CarPartComponent>>();
	private Dictionary<int, List<CarPartComponent>> brakeGTComponents = new Dictionary<int, List<CarPartComponent>>();
	private Dictionary<int, List<CarPartComponent>> suspensionGTComponents = new Dictionary<int, List<CarPartComponent>>();
	private Dictionary<int, List<CarPartComponent>> gearboxGTComponents = new Dictionary<int, List<CarPartComponent>>();
	public Action OnDesignModified;
	public Action OnPartBuilt;
	private CarPartDesign.Stage mStage;

	private bool mBuildTwoCopies;
	public DateTime startDate;
	public DateTime endDate;
	private bool mAllPartsUnlocked;
	private float mComponentTimeDaysBonus;


	private bool mImidiateFinish;

	public enum Stage
	{
		Idle,
		Designing,
	}
}
