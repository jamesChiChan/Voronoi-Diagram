/* $LAN=C#$ */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoronoiDiagram.Models
{
    class Edge
    {
        public PointF p1;
        public PointF p2;

        public Edge() { }

        public Edge(PointF p1, PointF p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }
    }
}
