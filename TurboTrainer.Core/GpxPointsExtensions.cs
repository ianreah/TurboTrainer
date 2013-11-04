using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace TurboTrainer.Core
{
	public static class GpxPointsExtensions
	{
		public static IObservable<GpxPoint> Replay(this IEnumerable<GpxPoint> gpxPoints, IScheduler scheduler)
		{
            var firstPoint = gpxPoints.FirstOrDefault();
			if (firstPoint == null)
			{
				return Observable.Empty<GpxPoint>();
			}

            var pointsEnumerator = gpxPoints.Skip(1).GetEnumerator();
            return Observable.Generate(initialState: firstPoint.Time,
									   condition: x => pointsEnumerator.MoveNext(),
									   iterate: x => pointsEnumerator.Current.Time,
									   resultSelector: x => pointsEnumerator.Current,
									   timeSelector: x => pointsEnumerator.Current.Time - x,
									   scheduler: scheduler)
							 .StartWith(firstPoint);
		}

		public static IEnumerable<GpxSection> ToGpxSections(this IEnumerable<GpxPoint> gpxPoints)
		{
			using (var iterator = gpxPoints.GetEnumerator())
			{
				if (!iterator.MoveNext())
				{
					yield break;
				}

				var previous = iterator.Current;
				while (iterator.MoveNext())
				{
					yield return new GpxSection(previous, iterator.Current);
					previous = iterator.Current;
				}
			}
		}
	}
}