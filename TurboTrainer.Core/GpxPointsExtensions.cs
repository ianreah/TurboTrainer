using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace TurboTrainer.Core
{
	public static class GpxPointsExtensions
	{
		public static IObservable<GpxSection> Replay(this IEnumerable<GpxPoint> gpxPoints, IScheduler scheduler)
		{
			var sections = gpxPoints.ToGpxSections();

			var firstSection = sections.FirstOrDefault();
			if (firstSection == null)
			{
				return Observable.Empty<GpxSection>();
			}

			return Observable.Generate(initialState: sections.Skip(1).GetEnumerator(),
									   condition: x => x.MoveNext(),
									   iterate: x => x,
									   resultSelector: x => x.Current,
									   timeSelector: x => x.Current.TimeTaken,
									   scheduler: scheduler)
							 .StartWith(firstSection);
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