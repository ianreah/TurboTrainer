using System;
using NUnit.Framework;
using TurboTrainer.Core;

namespace TurboTrainer.Tests
{
	[TestFixture]
	public class GpxSectionTests
	{
		private readonly GpxPoint start = new GpxPoint(55.0m, -1.5m, 5.0m, new DateTime(2013, 10, 30, 12, 0, 0));
		private readonly GpxPoint end = new GpxPoint(55.1m, -1.6m, 5.0m, new DateTime(2013, 10, 30, 12, 30, 0));

        private readonly GpxPoint higherEnd = new GpxPoint(55.1m, -1.6m, 7.0m, new DateTime(2013, 10, 30, 12, 30, 0));
        private readonly GpxPoint lowerEnd = new GpxPoint(55.1m, -1.6m, 2.0m, new DateTime(2013, 10, 30, 12, 30, 0));

        [Test]
		public void Start_ReturnsStartPoint()
		{
			Assert.That(new GpxSection(start, end).Start, Is.EqualTo(start));
		}

		[Test]
		public void End_ReturnsEndPoint()
		{
			Assert.That(new GpxSection(start, end).End, Is.EqualTo(end));
		}

		[Test]
		public void TimeTaken_ReturnsTimeDifferenceBetweenStartAndEnd()
		{
			Assert.That(new GpxSection(start, end).TimeTaken, Is.EqualTo(TimeSpan.FromMinutes(30)));
		}

		[Test]
		public void Distance_ReturnsMetresBetweenStartAndEnd()
		{
			Assert.That(new GpxSection(start, end).Distance, Is.EqualTo(12814.79).Within(0.01));
		}

        [Test]
        public void Gradient_WithFlatElevation_ReturnsZero()
        {
            Assert.That(new GpxSection(start, end).Gradient, Is.EqualTo(0));
        }

        [Test]
        public void Gradient_WithHigherEndPoint_ReturnsCorrectPositiveGradient()
        {
            Assert.That(new GpxSection(start, higherEnd).Gradient, Is.EqualTo(0.0156).Within(0.0001));
        }

        [Test]
        public void Gradient_WithLowerEndPoint_ReturnsCorrectNegativeGradient()
        {
            Assert.That(new GpxSection(start, lowerEnd).Gradient, Is.EqualTo(-0.0234).Within(0.0001));
        }
	}
}
