using System;

namespace TurboTrainer.Core
{
    public class GpxSection
    {
        private readonly GpxPoint start;
        private readonly GpxPoint end;

        private readonly Lazy<Tuple<double, double>> distanceWithGradient;
        private readonly Lazy<TimeSpan> timeTaken;

        public GpxSection(GpxPoint start, GpxPoint end)
        {
            this.start = start;
            this.end = end;

            distanceWithGradient = new Lazy<Tuple<double,double>>(() => GisMaths.DistanceWithGradient(start, end));
            timeTaken = new Lazy<TimeSpan>(() => start.Time - start.Time);
        }

        public GpxPoint Start { get { return start; } }
        public GpxPoint End { get { return end; } }
        public TimeSpan TimeTaken { get { return timeTaken.Value; } }
        public double Distance { get { return distanceWithGradient.Value.Item1; } }
        public double Gradient { get { return distanceWithGradient.Value.Item2; } }
    }
}
