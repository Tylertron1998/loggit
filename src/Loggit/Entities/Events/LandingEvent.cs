using System;

namespace Loggit.Entities.Events
{
	public class LandingEvent : Event
	{
		public string? AirportName { get; set; }

		public LandingEvent(DateTime time, string? airportName, string pilotName, string aircraftName) : base(time, pilotName, aircraftName)
		{
			AirportName = airportName;
		}
	}
}