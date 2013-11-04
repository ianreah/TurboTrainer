using System;
using System.Linq;
using NUnit.Framework;
using TurboTrainer.Core;

namespace TurboTrainer.Tests
{
	[TestFixture]
	public class GpxPointsToGpxSectionsTests
	{
		[Test]
		public void ToGpxSections_WithNoPoints_ReturnsEmptyEnumerable()
		{
			CollectionAssert.IsEmpty(Enumerable.Empty<GpxPoint>().ToGpxSections());
		}

		[Test]
		public void ToGpxSections_WithOnePoint_ReturnsEmptyEnumerable()
		{
			var points = new[]
            {
                new GpxPoint(47.644548m, -122.326897m, 4.46m, DateTime.Parse("2009-10-17T18:37:26Z"))
            };

			CollectionAssert.IsEmpty(points.ToGpxSections());
		}

		[Test]
		public void ToGpxSections_WithThreePoints_ReturnsCorrectSections()
		{
			var points = new[]
            {
                new GpxPoint(47.644548m, -122.326897m, 4.46m, DateTime.Parse("2009-10-17T18:37:26Z")),
                new GpxPoint(47.644548m, -125.326897m, 4.94m, DateTime.Parse("2009-10-17T18:37:31Z")),
                new GpxPoint(46.644548m, -121.326897m, 6.87m, DateTime.Parse("2009-10-17T18:37:34Z"))
            };

			var sections = points.ToGpxSections().ToList();

			Assert.That(sections.Count, Is.EqualTo(2));
			sections[0].AssertSection(points[0], points[1]);
			sections[1].AssertSection(points[1], points[2]);
		}
	}
}
