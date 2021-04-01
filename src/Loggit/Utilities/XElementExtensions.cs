using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Loggit.Utilities
{
	public static class XElementExtensions
	{
		public static XElement? GetFirstDescendantByName(this XElement element, string name)
		{
			return element.Descendants()
				.FirstOrDefault(innerElement => innerElement.Name == name);
		}

		public static string GetFirstDescendantValueByNameOrDefault(this XElement element, string name)
		{
			return element.GetFirstDescendantByName(name)?.Value ?? "";
		}

		public static IEnumerable<XElement> GetDescendantsByName(this XElement element, string name)
		{
			return element.Descendants()
				.Where(innerElement => innerElement.Name == name);
		}
	}
}