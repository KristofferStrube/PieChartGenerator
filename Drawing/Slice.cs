using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PieGenerator.Drawing
{
    public class Slice
    {
        public Slice(Pie Pie, double Angle, string BackgroundColor, string EdgeColor)
        {
            this.Pie = Pie;
            this.Angle = Angle;
            this.BackgroundColor = BackgroundColor;
            this.EdgeColor = EdgeColor;
        }

        public string BackgroundColor { get; set; }

        public string EdgeColor { get; set; }

        private double StartAngle { get { return Pie.Slices.GetRange(0, Pie.Slices.FindIndex(e => e == this)).Sum(s => s.Angle); } }

        private double CentrumOffAngle { get { return (Angle + StartAngle * 2) / 2.0; } }

        public double Angle { get; set; }

        public int TextX { get { return (int)(Pie.Radius + Math.Cos(CentrumOffAngle) * Pie.Radius * 0.66); } }
        public int TextY { get { return (int)(Pie.Radius + Math.Sin(CentrumOffAngle) * Pie.Radius * 0.66); } }

        public int Percent
        {
            get { return (int)(Angle / Math.PI / 2.0 * 100.0); }
            set
            {
                var restSum = Pie.Slices.Where(s => s != this).Sum(s => s.Angle);
                var newAngle = value / 100.0 * Math.PI * 2.0;
                if (restSum + newAngle <= Math.PI * 2.0)
                {
                    Angle = newAngle;
                }
                else
                {
                    Angle = Math.PI * 2.0 - restSum;
                }
            }
        }

        private Pie Pie { get; set; }

        public string Points()
        {
            double LineLength = Pie.Radius - Pie.Padding;

            double XCentrum = Pie.Radius + Math.Cos(CentrumOffAngle) * Pie.Padding;
            double YCentrum = Pie.Radius + Math.Sin(CentrumOffAngle) * Pie.Padding;

            string res = $"{XCentrum.ToString(CultureInfo.InvariantCulture)},{YCentrum.ToString(CultureInfo.InvariantCulture)} ";
            for (double i = StartAngle + Math.Asin(Pie.Padding / Pie.Radius); i < StartAngle + Angle - Math.Asin(Pie.Padding / Pie.Radius); i += 1 / Pie.Radius * 10)
            {
                res += (Math.Cos(i) * LineLength + Pie.Radius).ToString(CultureInfo.InvariantCulture) + "," + (Math.Sin(i) * LineLength + Pie.Radius).ToString(CultureInfo.InvariantCulture) + " ";
            }
            return res;
        }
    }
}
