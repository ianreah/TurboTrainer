using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TurboTrainer.Core;

namespace TurboTrainer.Tests
{
	[TestFixture]
	public class GpxReaderTests
	{
		[Test]
		public void Points_FromSampleGpxDocument_AreCorrect()
		{
			using (var gpxStream = new MemoryStream(Encoding.UTF8.GetBytes(Properties.Resources.SampleGpxDocument)))
			{
				var reader = new GpxReader(gpxStream);

				Assert.That(reader.Points.Count(), Is.EqualTo(3));
				
				reader.Points.ElementAt(0).AssertPoint(47.644548, -122.326897, 4.46, "2009-10-17T18:37:26Z");
				reader.Points.ElementAt(1).AssertPoint(47.644548, -122.326897, 4.94, "2009-10-17T18:37:31Z");
				reader.Points.ElementAt(2).AssertPoint(47.644548, -122.326897, 6.87, "2009-10-17T18:37:34Z");
			}
		}
	}
}
