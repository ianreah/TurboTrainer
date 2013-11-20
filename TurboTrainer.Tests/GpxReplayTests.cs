using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Reactive.Testing;
using NUnit.Framework;
using TurboTrainer.Core;

namespace TurboTrainer.Tests
{
    [TestFixture]
    public class GpxReplayTests
    {
        [Test]
        public void Replay_NoPoints_CompletesImmediately()
        {
            bool completed = false;
            var scheduler = new TestScheduler();

            Enumerable.Empty<GpxPoint>().Replay(scheduler)
                                        .Subscribe(onNext:_ => Assert.Fail(),
                                                   onError:_ => Assert.Fail(),
                                                   onCompleted:() => completed = true);

            scheduler.AdvanceBy(1);
            Assert.That(completed, Is.True);
        }

        [Test]
        public void Replay_OnePoint_CompletedImmediately()
        {
            bool completed = false;
            var scheduler = new TestScheduler();

            var testPoints = new[]
            {
                new GpxPoint(47.644548m, -122.326897m, 4.46m, DateTime.Parse("2009-10-17T18:37:26Z"))
            };

            testPoints.Replay(scheduler)
                      .Subscribe(onNext: _ => Assert.Fail(),
                                 onError: _ => Assert.Fail(),
                                 onCompleted: () => completed = true);

            scheduler.AdvanceBy(1);
            Assert.That(completed, Is.True);
        }

        [Test]
        public void Replay_TwoPoints_ObservesTheSectionImmediatelyThenCompletes()
        {
            bool completed = false;
            var scheduler = new TestScheduler();

            var testPoints = new[]
            {
                new GpxPoint(47.644548m, -122.326897m, 4.46m, DateTime.Parse("2009-10-17T18:37:26Z")),
                new GpxPoint(47.644548m, -122.326897m, 4.94m, DateTime.Parse("2009-10-17T18:37:31Z"))
            };

            var observedSections = new List<GpxSection>();

            testPoints.Replay(scheduler)
                      .Subscribe(onNext: observedSections.Add,
                                 onError: _ => Assert.Fail(),
                                 onCompleted: () => completed = true);

            scheduler.AdvanceBy(1);
            Assert.That(observedSections.Count, Is.EqualTo(1));
            Assert.That(observedSections[0].Start, Is.SameAs(testPoints[0]));
            Assert.That(observedSections[0].End, Is.SameAs(testPoints[1]));
            Assert.That(completed, Is.False);

            // Waits for the time between the last two points before completing
            scheduler.AdvanceBy(TimeSpan.TicksPerSecond * 4);
            Assert.That(observedSections.Count, Is.EqualTo(1));
            Assert.That(completed, Is.False);

            scheduler.AdvanceBy(TimeSpan.TicksPerSecond);
            Assert.That(completed, Is.True);
        }

        [Test]
        public void Replay_SeveralPoints_ObservesSectionsAtCorrectIntervalThenCompletes()
        {
            bool completed = false;
            var scheduler = new TestScheduler();

            var testPoints = new[]
            {
                new GpxPoint(47.644548m, -122.326897m, 4.46m, DateTime.Parse("2009-10-17T18:37:26Z")),
                new GpxPoint(47.644548m, -122.326897m, 4.94m, DateTime.Parse("2009-10-17T18:37:31Z")),
                new GpxPoint(47.644548m, -122.326897m, 6.87m, DateTime.Parse("2009-10-17T18:37:34Z"))
            };

            var observedSections = new List<GpxSection>();

            testPoints.Replay(scheduler)
                      .Subscribe(onNext: observedSections.Add,
                                 onError: _ => Assert.Fail(),
                                 onCompleted: () => completed = true);

            scheduler.AdvanceBy(1);
            Assert.That(observedSections.Count, Is.EqualTo(1));
            Assert.That(observedSections[0].Start, Is.SameAs(testPoints[0]));
            Assert.That(observedSections[0].End, Is.SameAs(testPoints[1]));
            Assert.That(completed, Is.False);

            // Nothing for the time between the first two points
            scheduler.AdvanceBy(TimeSpan.TicksPerSecond * 4);
            Assert.That(observedSections.Count, Is.EqualTo(1));
            Assert.That(completed, Is.False);

            scheduler.AdvanceBy(TimeSpan.TicksPerSecond);
            Assert.That(observedSections.Count, Is.EqualTo(2));
            Assert.That(observedSections[1].Start, Is.SameAs(testPoints[1]));
            Assert.That(observedSections[1].End, Is.SameAs(testPoints[2]));
            Assert.That(completed, Is.False);

            // Waits for the time between the last two points before completing
            scheduler.AdvanceBy(TimeSpan.TicksPerSecond * 2);
            Assert.That(observedSections.Count, Is.EqualTo(2));
            Assert.That(completed, Is.False);

            scheduler.AdvanceBy(TimeSpan.TicksPerSecond);
            Assert.That(completed, Is.True);
        }
    }
}
