using System;

namespace TurboTrainer.Core
{
	public class GpxPoint
	{
		private readonly decimal latitude;
		private readonly decimal longitude;
		private readonly decimal elevation;
		private readonly DateTime time;

		public GpxPoint(decimal latitude, decimal longitude, decimal elevation, DateTime time)
		{
			this.latitude = latitude;
			this.longitude = longitude;
			this.elevation = elevation;
			this.time = time;
		}

		public decimal Latitude { get { return latitude; } }
		public decimal Longitude { get { return longitude; } }
		public decimal Elevation { get { return elevation; } }
		public DateTime Time { get { return time; } }
	}
}