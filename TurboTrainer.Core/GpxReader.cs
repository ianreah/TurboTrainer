using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace TurboTrainer.Core
{
	public class GpxReader
	{
		public GpxReader(Stream stream)
		{
			var gpxData = (gpxType)new XmlSerializer(typeof(gpxType)).Deserialize(stream);

			Points = gpxData.trk.SelectMany(trk => trk.trkseg.SelectMany(trkseg => trkseg.trkpt)
								.Select(trkpt => new GpxPoint(trkpt.lat, trkpt.lon, trkpt.ele, trkpt.time)));
		}

		public IEnumerable<GpxPoint> Points { get; private set; }
	}
}