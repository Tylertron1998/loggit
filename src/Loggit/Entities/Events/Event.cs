using System;

namespace Loggit.Entities.Events
{
	public abstract class Event
	{
		public int Id { get; set; }
		public DateTime Time { get; set; }
		public string PilotName { get; set; }
		public string AircraftName { get; set; }

		public Event(DateTime time, string pilotName, string aircraftName)
		{
			Time = time;
			PilotName = pilotName;
			AircraftName = aircraftName;
		}
	}
}