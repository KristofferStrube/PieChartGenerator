using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PieGenerator.Drawing
{
    public class Pie
    {
        private static string[] COLORS = { "#0000ff", "#00ffff", "#00ff00", "#ffff00", "#ff0000", "#ff00ff" };
        private static string[] EDGECOLORS = { "#0000dd", "#00dddd", "#00dd00", "#dddd00", "#dd0000", "#dd00dd" };
        public Pie(double radius, double padding)
        {
            Radius = radius;
            Padding = padding;
        }
        public double Radius { get; set; }

        public double Padding { get; set; }

        public List<Slice> Slices = new List<Slice>();

        private string GetColor(string backgroundColor)
        {
            if (backgroundColor == null)
            {
                return COLORS[Slices.Count % COLORS.Length];
            }
            return backgroundColor;
        }
        private string GetEdgeColor(string backgroundColor)
        {
            if (backgroundColor == null)
            {
                return EDGECOLORS[Slices.Count % COLORS.Length];
            }
            return backgroundColor;
        }

        public void AddSliceByAngle(double Angle, string backgroundColor = null, string edgeColor = null)
        {
            Slices.Add(new Slice(this, Angle, GetColor(backgroundColor), GetEdgeColor(edgeColor)));
        }
        public void AddSliceByPercent(double percent, string backgroundColor = null, string edgeColor = null)
        {

            Slices.Add(new Slice(this, Math.PI * 2.0 / 100.0 * percent, GetColor(backgroundColor), GetEdgeColor(edgeColor)));
        }
    }
}
