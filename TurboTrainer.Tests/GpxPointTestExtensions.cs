using NUnit.Framework;
using System;
using System.Globalization;
using TurboTrainer.Core;

namespace TurboTrainer.Tests
{
    public static class GpxPointTestExtensions
    {
        public static void AssertPoint(this GpxPoint point, double expectedLatitude, double expectedLongitude, double expectedElevation, string expectedTime)
        {
            Assert.That(point.Latitude, Is.EqualTo(expectedLatitude));
            Assert.That(point.Longitude, Is.EqualTo(expectedLongitude));
            Assert.That(point.Elevation, Is.EqualTo(expectedElevation));
            Assert.That(point.Time, Is.EqualTo(DateTime.Parse(expectedTime, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal)));
        }
    }
}
