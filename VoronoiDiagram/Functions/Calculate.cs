/* $LAN=C#$ */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using VoronoiDiagram.Models;

namespace VoronoiDiagram.Functions
{
    class Calculate
    {
        //中垂線計算 (p1, p2, p1&p2中間點, 法向量)
        public static void calPerpendicularBisector(PointF p1, PointF p2, out PointF p_mid, out PointF v_normal)
        {
            double d_normal; //法向量長度，用於正規化

            p_mid = new PointF();       //兩點中間點
            v_normal = new PointF();    //法向量

            p_mid.X = (p1.X + p2.X) / 2;
            p_mid.Y = (p1.Y + p2.Y) / 2;

            // 法向量
            v_normal.X = -(p2.Y - p1.Y);
            v_normal.Y = (p2.X - p1.X);

            d_normal = Math.Sqrt(Math.Pow(v_normal.X, 2) + Math.Pow(v_normal.Y, 2));
            v_normal.X = v_normal.X / Convert.ToSingle(d_normal);
            v_normal.Y = v_normal.Y / Convert.ToSingle(d_normal);
        }

        //外心計算 三點 利用行列式計算 by 外接圓方程式
        public static PointF calCircumCenter(PointF p1, PointF p2, PointF p3)
        {
            double Ox, Oy, p1_sqr, p2_sqr, p3_sqr, deno;
            double x1 = p1.X, x2 = p2.X, x3 = p3.X, y1 = p1.Y, y2 = p2.Y, y3 = p3.Y;

            p1_sqr = Math.Pow(x1, 2) + Math.Pow(y1, 2);
            p2_sqr = Math.Pow(x2, 2) + Math.Pow(y2, 2);
            p3_sqr = Math.Pow(x3, 2) + Math.Pow(y3, 2);

            deno = 2 * ((x1 * y2 + x2 * y3 + x3 * y1) - (x3 * y2 + x1 * y3 + x2 * y1));  //分母

            Ox = ((p1_sqr * y2 + p2_sqr * y3 + p3_sqr * y1) - (p3_sqr * y2 + p2_sqr * y1 + p1_sqr * y3)) / deno;
            Oy = ((x1 * p2_sqr + x2 * p3_sqr + x3 * p1_sqr) - (x3 * p2_sqr + x2 * p1_sqr + x1 * p3_sqr)) / deno;

            return new PointF(Convert.ToSingle(Ox), Convert.ToSingle(Oy));
        }

        //重心計算 三點
        public static PointF calCentroid(PointF p1, PointF p2, PointF p3)
        {
            return new PointF((p1.X + p2.X + p3.X) / 3, (p1.Y + p2.Y + p3.Y) / 3);
        }

        //排序 points 順序，依 lexical order(先排x再排y) 皆小->大
        public static List<PointF> orderPoints(List<PointF> lst_points)
        {
            return lst_points.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();
        }

        //排序 edges 順序，依 lexical order(先排x再排y) 皆小->大
        public static List<Edge> orderEdges(List<Edge> lst_edges)
        {
            //先排序 edge 兩端點
            foreach(Edge e in lst_edges)
            {
                //線段E x1 y1 x2 y2，座標須滿足x1≦x2 或 x1=x2, y1≦y2
                if (e.p1.X > e.p2.X || (e.p1.X == e.p2.X && e.p1.Y > e.p2.Y))
                {
                    PointF tmp = new PointF();

                    tmp = e.p1;
                    e.p1 = e.p2;
                    e.p2 = tmp;
                }
            }

            //再排序 edges，不同線段之間，依照x1, y1, x2, y2的順序進行排序
            return lst_edges.OrderBy(e => e.p1.X).ThenBy(e => e.p1.Y).ThenBy(e => e.p2.X).ThenBy(e => e.p2.Y).ToList();
        }

        //排序 三點 依逆時針排序，方便判斷法向量的方向
        public static List<PointF> orderVector(List<PointF> lst_points)
        {
            PointF p_ct = calCentroid(lst_points[0], lst_points[1], lst_points[2]); //求出重心

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2 - i; j++)
                {
                    if (!isCounterclockwise(p_ct, lst_points[j], lst_points[j + 1]))
                    {
                        PointF tmp = new PointF();

                        tmp = lst_points[j + 1];
                        lst_points[j + 1] = lst_points[j];
                        lst_points[j] = tmp;
                    }
                }
            }
            return lst_points;
        }
        
        //檢查三點共線
        public static bool isCollinear(PointF p1, PointF p2, PointF p3)
        {
            float ck;

            //檢查斜率是否相等 (y3−y1)(x2−x1)−(y2−y1)(x3−x1)=0
            ck = (p3.Y - p1.Y) * (p2.X - p1.X) - (p2.Y - p1.Y) * (p3.X - p1.X);

            if (Math.Abs(ck) <= 1e-6) return true; //因為是浮點數導致相減不一定為0，所以絕對值後小於一定誤差即可
            else return false;
        }

        //檢查向量逆時針 Ture為逆 False為順
        public static bool isCounterclockwise(PointF c, PointF p1, PointF p2)
        {
            float ck;

            ck = (p1.X - c.X) * (p2.Y - c.Y) - (p1.Y - c.Y) * (p2.X - c.X);

            if (ck <= 1e-6) return true;
            else return false;
        }
    }
}
