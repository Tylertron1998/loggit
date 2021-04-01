using System;

namespace Loggit.Entities.Events
{
	public class TakeoffEvent : Event
	{
		public string? AirportName { get; set; }

		public TakeoffEvent(DateTime time, string? airportName, string pilotName, string aircraftName) : base(time, pilotName, aircraftName)
		{
			AirportName = airportName;
		}
	}
}