using System.Globalization;
using TurboTrainer.Core;

namespace TurboTrainer.Tests
{
	public static class GpxSectionTestExtensions
	{
		public static void AssertSection(this GpxSection gpxSection, GpxPoint expectedStartPoint, GpxPoint expectedEndPoint)
		{
			gpxSection.Start.AssertPoint((double)expectedStartPoint.Latitude, (double)expectedStartPoint.Longitude, (double)expectedStartPoint.Elevation, expectedStartPoint.Time.ToString(CultureInfo.InvariantCulture));
			gpxSection.End.AssertPoint((double)expectedEndPoint.Latitude, (double)expectedEndPoint.Longitude, (double)expectedEndPoint.Elevation, expectedEndPoint.Time.ToString(CultureInfo.InvariantCulture));
		}
	}
}