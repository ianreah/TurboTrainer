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

			var previousTime = firstPoint.Time;
            return Observable.Generate(initialState: gpxPoints.Skip(1).GetEnumerator(),
									   condition: x => x.MoveNext(),
									   iterate: x => { previousTime = x.Current.Time; return x; },
									   resultSelector: x => x.Current,
									   timeSelector: x => x.Current.Time - previousTime,
									   scheduler: scheduler)
							 .StartWith(firstPoint);
		}
	}
}