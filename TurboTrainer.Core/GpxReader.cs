using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace TurboTrainer.Core
{
	public class GpxReader
	{
		private readonly Lazy<IEnumerable<GpxPoint>> gpxPoints;

		public GpxReader(Stream stream)
		{
			gpxPoints = new Lazy<IEnumerable<GpxPoint>>(() =>
			{
				var gpxData = (gpxType)new XmlSerializer(typeof (gpxType)).Deserialize(stream);

				return gpxData.trk.SelectMany(trk => trk.trkseg.SelectMany(trkseg => trkseg.trkpt)
								  .Select(trkpt => new GpxPoint(trkpt.lat, trkpt.lon, trkpt.ele, trkpt.time)));
			});
		}

		public IEnumerable<GpxPoint> Points { get { return gpxPoints.Value; } }
	}
}