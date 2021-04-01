using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using JetBrains.Annotations;
using Loggit.Entities.Events;
using Loggit.Exceptions;
using Loggit.Utilities;

namespace Loggit.Parsing
{
	public class TacviewXmlParser
	{
		private readonly XDocument _document;
		private readonly DateTime _missionStartTime;

		public TacviewXmlParser(XDocument document)
		{
			_document = document;
			_missionStartTime = GetMissionStartTime();
		}

		[PublicAPI]
		public DateTime GetMissionStartTime()
		{
			var flightRecordingEntry = _document.Root?.GetFirstDescendantByName("FlightRecording");

			if (flightRecordingEntry is null)
			{
				throw new InvalidTacviewDataException("Mission start time not defined; data must be invalid.");
			}
			
			var stringValue = flightRecordingEntry.GetFirstDescendantValueByNameOrDefault("RecordingTime");

			return DateTime.Parse(stringValue);
		}

		public IEnumerable<Event> GetEvents()
		{
			var eventValues = _document.Root?.GetFirstDescendantByName("Events")?.GetDescendantsByName("Event");

			if (eventValues is null)
			{
				throw new InvalidTacviewDataException("No events to be found.");
			}

			var events = eventValues.Select(element =>
			{
				var timeValue = element.GetFirstDescendantValueByNameOrDefault("Time");

				var actionValue = element.GetFirstDescendantValueByNameOrDefault("Action");
				var primaryObject = element.GetFirstDescendantByName("PrimaryObject");
				var pilotName = primaryObject?.GetFirstDescendantValueByNameOrDefault("Pilot") ?? "";
				var aircraftName = primaryObject?.GetFirstDescendantValueByNameOrDefault("Name") ?? "";

				if (!double.TryParse(timeValue, out var eventTime))
				{
					throw new InvalidTacviewDataException($"No time for element {element.Name})");
				}

				var time = _missionStartTime.AddSeconds(eventTime);

				return actionValue switch
				{
					"HasTakenOff" => ParseTakeoff(element, time, pilotName, aircraftName),
					"HasLanded" => ParseLanding(element, time, pilotName, aircraftName),
					_ => null // TODO: throw exception here when action value is not expected/supported.
				};

			});

			return events.Any(@event => @event is not null) ? events : Enumerable.Empty<Event>();

		}

		private static Event? ParseLanding(XElement element, DateTime time, string pilotName, string aircraftName)
		{
			var airportName = element.GetFirstDescendantByName("Airport")?.GetFirstDescendantValueByNameOrDefault("Name");

			return new LandingEvent(time, airportName, pilotName, aircraftName);
		}
		
		private static Event? ParseTakeoff(XElement element, DateTime time, string pilotName, string aircraftName)
		{
			var airportName = element.GetFirstDescendantByName("Airport")?.GetFirstDescendantValueByNameOrDefault("Name");

			return new TakeoffEvent(time, airportName, pilotName, aircraftName);
		}
	}
}