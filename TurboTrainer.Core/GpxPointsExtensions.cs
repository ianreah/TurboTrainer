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

            var sectionsEnumerator = sections.Skip(1).GetEnumerator();
			return Observable.Generate(initialState: firstSection.TimeTaken,
                                       condition: x => sectionsEnumerator.MoveNext(),
                                       iterate: x => sectionsEnumerator.Current.TimeTaken,
                                       resultSelector: x => sectionsEnumerator.Current,
									   timeSelector: x => x,
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