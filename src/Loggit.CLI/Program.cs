using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using Loggit.Database;
using Loggit.Entities.Events;
using Loggit.Parsing;

namespace Loggit.CLI
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			
			// TODO: extract logic
			var file = new FileInfo("./test.xml").OpenRead();
			var xml = XDocument.Load(file);
			var parser = new TacviewXmlParser(xml);

			var events = parser.GetEvents();

			using var context = new LoggitDbContext();
				
			foreach(var @event in events)
			{
				switch (@event)
				{
					case TakeoffEvent takeoff:
					{
						context.Takeoffs.Add(takeoff);
						break;
					}
					case LandingEvent landing:
					{
						context.Landings.Add(landing);
						break;
					}
				}
			}

			await context.SaveChangesAsync();

			// if (args[0] == "import")
			// {
			// 	// TODO: extract logic
			// 	var file = new FileInfo(args[1]).OpenRead();
			// 	var xml = XDocument.Load(file);
			// 	var parser = new TacviewXmlParser(xml);
			//
			// 	var events = parser.GetEvents();
			//
			// 	using var context = new LoggitDbContext();
			// 	
			// 	foreach(var @event in events)
			// 	{
			// 		switch (@event)
			// 		{
			// 			case TakeoffEvent takeoff:
			// 			{
			// 				context.Takeoffs.Add(takeoff);
			// 				break;
			// 			}
			// 			case LandingEvent landing:
			// 			{
			// 				context.Landings.Add(landing);
			// 				break;
			// 			}
			// 		}
			// 	}
			//
			// 	await context.SaveChangesAsync();
			// }
		}
		
	}
}