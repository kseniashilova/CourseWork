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
            Graphics gr = Graphics.FromImage(pb.Image);
            List<string> vertexes = PropertiesHelper.Vertexes(arr); //список вершин


            List<Tuple<string, Point, bool>> points = new List<Tuple<string, Point, bool>>();


            //распределяем точки
            for (int i = 0; i < vertexes.Count; i++)
            {
                points.Add(new Tuple<string, Point, bool>
                    (vertexes[i],
                    new Point(rnd.Next(pb.Width), rnd.Next(pb.Height)),
                    false)
                    ); ;
            }


            //рисуем все ребра
            for (int i = 0; i < arr.Count; i++)
            {
                //находим первую точку
                Tuple<string, Point, bool> t1 =
                points.Find(x => x.Item1 == arr[i].Item1);
                //находим вторую точку
                Tuple<string, Point, bool> t2 =
                points.Find(x => x.Item1 == arr[i].Item2);

                Point p1 = t1.Item2;
                Point p2 = t2.Item2;
                gr.DrawLine(pen, p1, p2);
                bool b1 = t1.Item3;
                bool b2 = t2.Item3;

                int i1 = points.IndexOf(t1);
                int i2 = points.IndexOf(t2);

                if (!b1)
                {
                    DrawVertex(p1, gr, pen.Color, 4);
                    points[i1] = new Tuple<string, Point, bool>(t1.Item1, t1.Item2, true);
                }
                if (!b2)
                {
                    DrawVertex(p2, gr, pen.Color, 4);
                    points[i2] = new Tuple<string, Point, bool>(t2.Item1, t2.Item2, true);
                }
            }


        }

        public static void DrawVertex(Point p, Graphics gr, Color col, int r)
        {
            gr.FillEllipse(new SolidBrush(col), p.X-r, p.Y-r, 2*r, 2*r);
        }




       

    }
}
