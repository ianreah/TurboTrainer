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
	    public void Replay_OnePoint_ObservesThePointImmediatelyThenCompletes()
	    {
			bool completed = false;
			var scheduler = new TestScheduler();

			var testPoints = new[]
            {
                new GpxPoint(47.644548m, -122.326897m, 4.46m, DateTime.Parse("2009-10-17T18:37:26Z"))
            };

			var observedPoints = new List<GpxPoint>();

			testPoints.Replay(scheduler)
					  .Subscribe(onNext: observedPoints.Add,
								 onError: _ => Assert.Fail(),
								 onCompleted: () => completed = true);

			scheduler.AdvanceBy(1);
			Assert.That(observedPoints.Count, Is.EqualTo(1));
			Assert.That(observedPoints[0].Elevation, Is.EqualTo(4.46m));
			Assert.That(completed, Is.True);
		}

	    [Test]
        public void Replay_SeveralPoints_ObservedAtCorrectIntervalThenCompletes()
        {
			bool completed = false;
			var scheduler = new TestScheduler();

			var testPoints = new[]
            {
                new GpxPoint(47.644548m, -122.326897m, 4.46m, DateTime.Parse("2009-10-17T18:37:26Z")),
                new GpxPoint(47.644548m, -122.326897m, 4.94m, DateTime.Parse("2009-10-17T18:37:31Z")),
                new GpxPoint(47.644548m, -122.326897m, 6.87m, DateTime.Parse("2009-10-17T18:37:34Z"))
            };

			var observedPoints = new List<GpxPoint>();

			testPoints.Replay(scheduler)
					  .Subscribe(onNext: observedPoints.Add,
								 onError: _ => Assert.Fail(),
								 onCompleted: () => completed = true);

			scheduler.AdvanceBy(1);
			Assert.That(observedPoints.Count, Is.EqualTo(1));
			Assert.That(observedPoints[0].Elevation, Is.EqualTo(4.46m));
			Assert.That(completed, Is.False);

            scheduler.AdvanceBy(TimeSpan.TicksPerSecond * 5);
            Assert.That(observedPoints.Count, Is.EqualTo(2));
            Assert.That(observedPoints[1].Elevation, Is.EqualTo(4.94));
			Assert.That(completed, Is.False);

			scheduler.AdvanceBy(TimeSpan.TicksPerSecond * 3);
            Assert.That(observedPoints.Count, Is.EqualTo(3));
            Assert.That(observedPoints[2].Elevation, Is.EqualTo(6.87));
			Assert.That(completed, Is.True);
        }
    }
}
