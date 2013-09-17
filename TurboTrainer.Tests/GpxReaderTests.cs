using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TurboTrainer.Core;

namespace TurboTrainer.Tests
{
	[TestFixture]
	public class GpxReaderTests
	{
		[Test]
		public void Points_FromSampleGpxDocument_AreCorrect()
		{
			using (var gpxStream = new MemoryStream(Encoding.UTF8.GetBytes(Properties.Resources.SampleGpxDocument)))
			{
				var reader = new GpxReader(gpxStream);

				Assert.That(reader.Points.Count(), Is.EqualTo(3));
				
				AssertGpxPoint(reader.Points.ElementAt(0), 47.644548, -122.326897, 4.46, "2009-10-17T18:37:26Z");
				AssertGpxPoint(reader.Points.ElementAt(1), 47.644548, -122.326897, 4.94, "2009-10-17T18:37:31Z");
				AssertGpxPoint(reader.Points.ElementAt(2), 47.644548, -122.326897, 6.87, "2009-10-17T18:37:34Z");
			}
		}

		private static void AssertGpxPoint(GpxPoint gpxPoint, double expectedLatitude, double expectedLongitude, double expectedElevation, string expectedTime)
		{
			Assert.That(gpxPoint.Latitude, Is.EqualTo(expectedLatitude));
			Assert.That(gpxPoint.Longitude, Is.EqualTo(expectedLongitude));
			Assert.That(gpxPoint.Elevation, Is.EqualTo(expectedElevation));
			Assert.That(gpxPoint.Time, Is.EqualTo(DateTime.Parse(expectedTime, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal)));
		}
	}
}
