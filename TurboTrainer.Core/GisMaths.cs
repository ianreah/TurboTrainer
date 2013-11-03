using System;

namespace TurboTrainer.Core
{
    public static class GisMaths
    {
        private const double earthRadius = 6371000; // metres

        public static double Distance(GpxPoint first, GpxPoint last)
        {
            var dLatitude = (last.Latitude - first.Latitude).ToRadians();
            var dLongitude = (last.Longitude - first.Longitude).ToRadians();

            don't compile

            var startLatitude = first.Latitude.ToRadians();
            var endLatitude = last.Latitude.ToRadians();

            var a = Math.Sin(dLatitude / 2) * Math.Sin(dLatitude / 2) +
                    Math.Sin(dLongitude / 2) * Math.Sin(dLongitude / 2) *
                    Math.Cos(startLatitude) * Math.Cos(endLatitude);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return earthRadius * c;
        }

        public static Tuple<double, double> DistanceWithGradient(GpxPoint first, GpxPoint last)
        {
            var distance = Distance(first, last);
            var gradient = (double)(last.Elevation - first.Elevation) / distance * 100;

            return Tuple.Create(distance, gradient);
        }

        public static double ToRadians(this Decimal degrees)
        {
            return Math.PI * (double)degrees / 180.0;
        }
    }

}
