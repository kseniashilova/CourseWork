using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace GraphHelper
{
    public static class VisualizationHelper
    {
        static Random rnd = new Random();
        public static void DrawSmallGraphRandom
            (List<Tuple<string, string, string, int>> arr,
            PictureBox pb, Pen pen)
        {
            Graphics gr = Graphics.FromImage(pb.Image);
            List<string>vertexes = PropertiesHelper.Vertexes(arr); //список вершин
            List<Tuple<string, int>> vals = PropertiesHelper.Valences(arr);


            List<Tuple<string, Point, Color>> points = new List<Tuple<string, Point, Color>>();

            
            int norm = 255 / PropertiesHelper.MaxValence(arr);
            //распределяем точки
            for (int i = 0; i < vertexes.Count; i++)
            {
                points.Add(new Tuple<string, Point, Color>
                    (vertexes[i],
                    new Point(rnd.Next(pb.Width), rnd.Next(pb.Height)),
                    Color.FromArgb( 255 - vals[i].Item2 * norm, 255 - vals[i].Item2 * norm,255- vals[i].Item2 * norm)
                    )
                    ); ;
            }


            

           

            //рисуем все ребра
            for (int i = 0; i < arr.Count; i++)
            {
                //находим первую точку
                Tuple<string, Point, Color> t1 = 
                points.Find(x => x.Item1 == arr[i].Item1);
                //находим вторую точку
                Tuple<string, Point, Color> t2 =
                points.Find(x => x.Item1 == arr[i].Item2);

                Point p1 = t1.Item2;
                Point p2 = t2.Item2;
                gr.DrawLine(pen, p1, p2);
                Color c1 = t1.Item3;
                Color c2 = t2.Item3;
                DrawVertex(p1, gr, c1, 10);
                DrawVertex(p2, gr, c2, 10);
            }


        }


        public static void DrawBigGraphRandom
            (List<Tuple<string, string, string, int>> arr,
            PictureBox pb, Pen pen)
        {
            //КЛАСТЕРИЗАЦИЯ
            List<List<string>> groups = GroupHelper.ClusteringAlgNew(arr, 7);

            DrawGroups(groups, pb, pen);
        }

        public static void DrawBigGraphRandomAnimation
            (List<Tuple<string, string, string, int>> arr,
            PictureBox pb, Pen pen)
        {
            List<List<string>> groups = GroupHelper.ClusteringAlgAnimation(arr, 7, pb, pen);

        }



        public static void DrawGroups(List<List<string>> groups, PictureBox pb, Pen pen)
        {
            Graphics gr = Graphics.FromImage(pb.Image);
            List<Tuple<Point, int>> points = new List<Tuple<Point, int>>();
            //распределяем точки
            for (int i = 0; i < groups.Count; i++)
            {
                points.Add(new Tuple<Point, int>
                    (new Point(rnd.Next(pb.Width), rnd.Next(pb.Height)),
                    groups[i].Count)
                    ); ;
            }


            //рисуем ребра
            for (int i = 0; i < points.Count - 1; i++)
            {
                gr.DrawLine(pen, points[i].Item1, points[i + 1].Item1);
            }

            int max = 0;
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].Item2 > max) max = points[i].Item2;
            }

            //рисуем точки
            for (int i = 0; i < points.Count; i++)
            {
                DrawVertex(points[i].Item1, gr, pen.Color, 10 * points[i].Item2 / max);
            }
        }

        public static void DrawVertex(Point p, Graphics gr, Color col, int r)
        {
            gr.FillEllipse(new SolidBrush(col), p.X-r, p.Y-r, 2*r, 2*r);
        }




       

    }
}
