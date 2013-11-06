using System.Globalization;
using TurboTrainer.Core;

namespace TurboTrainer.Tests
{
	public static class GpxSectionTestExtensions
	{
		public static void AssertSection(this GpxSection gpxSection, GpxPoint expectedStartPoint, GpxPoint expectedEndPoint)
		{
			gpxSection.AssertSection(expectedStartPoint.Latitude, expectedStartPoint.Longitude, expectedStartPoint.Elevation, expectedStartPoint.Time.ToString(CultureInfo.InvariantCulture),
				expectedEndPoint.Latitude, expectedEndPoint.Longitude, expectedEndPoint.Elevation, expectedEndPoint.Time.ToString(CultureInfo.InvariantCulture));
		}

		public static void AssertSection(this GpxSection gpxSection, decimal expectedStartLatitude, decimal expectedStartLongitude, decimal expectedStartElevation, string expectedStartTime,
			decimal expectedEndLatitude, decimal expectedEndLongitude, decimal expectedEndElevation, string expectedEndTime)
		{
			gpxSection.Start.AssertPoint(expectedStartLatitude, expectedStartLongitude, expectedStartElevation, expectedStartTime);
			gpxSection.End.AssertPoint(expectedEndLatitude, expectedEndLongitude, expectedEndElevation, expectedEndTime.ToString(CultureInfo.InvariantCulture));
		}
	}
}