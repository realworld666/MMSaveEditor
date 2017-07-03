using System;

public class PreSeasonState : GameState
{
	public enum PreSeasonStage
	{
		[LocalisationID( "PSG_10004841" )] DesignCar,
		[LocalisationID( "PSG_10004841" )] DesigningCar,
		[LocalisationID( "PSG_10004842" )] PartAdapting,
		[LocalisationID( "PSG_10004843" )] ChooseLivery,
		[LocalisationID( "PSG_10004843" )] ChoosingLivery,
		[LocalisationID( "PSG_10004844" )] PreseasonTest,
		[LocalisationID( "PSG_10004844" )] InPreSeasonTest,
		[LocalisationID( "PSG_10004845" )] Finished,
		Count,
	}
}
