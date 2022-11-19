/* $LAN=C#$ */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoronoiDiagram.Models
{
    class Voronoi
    {
        public List<PointF> lst_points;
        public List<Edge> lst_hyp;
        public List<Edge> lst_vor;

        public Voronoi()
        {
            lst_points = new List<PointF>();
            lst_hyp = new List<Edge>();
            lst_vor = new List<Edge>();
        }

        public void clearV()
        {
            lst_points.Clear();
            lst_hyp.Clear();
            lst_vor.Clear();
        }
    }
}
