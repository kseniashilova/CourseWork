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
        public static void DrawGraphRandom
            (List<Tuple<string, string, string, int>> arr,
            PictureBox pb, Pen pen, Color start, Color end)
        {
            Graphics gr = Graphics.FromImage(pb.Image);
            List<string>vertexes = PropertiesHelper.Vertexes(arr); //список вершин

            GraphHelper.GroupHelper.SortByValences(ref vertexes, arr); //сортируем список вершин


            List<Tuple<string, Point>> points = new List<Tuple<string, Point>>();
            //распределяем точки
            for(int i = 0; i < vertexes.Count; i++)
            {
                points.Add(new Tuple<string, Point>
                    (vertexes[i],
                    new Point(rnd.Next(pb.Width), rnd.Next(pb.Height))
                    )
                    );
            }


            //массив цветов для каждой вершинки
            Color[] colors = Colors(vertexes, arr, start, end);

           

            //рисуем все ребра
            for (int i = 0; i < arr.Count; i++)
            {
                //находим первую точку
                Point p1 = points.Find(x => x.Item1 == arr[i].Item1).Item2;
                //находим вторую точку
                Point p2 = points.Find(x => x.Item1 == arr[i].Item2).Item2;

                gr.DrawLine(pen, p1, p2);

            }


            //рисуем все точки
            for (int i = 0; i < vertexes.Count; i++)
            {
                Point p1 = points[i].Item2;
                DrawVertex(p1, gr, colors[i], 10);
            }

        }

        public static void DrawVertex(Point p, Graphics gr, Color col, int r)
        {
            gr.FillEllipse(new SolidBrush(col), p.X-r, p.Y-r, 2*r, 2*r);
        }



        public static Color[] Colors(List<string> vertexes, List<Tuple<string, string, string, int>> arr, 
            Color start, Color end)
        {
            List<Tuple<string, int>> val = PropertiesHelper.Valences(arr);
            val.Sort((x, y) => x.Item2.CompareTo(y.Item2)); //сортируем список валентностей
            //количество точек одной валентности
            List<int> amountOfOneColor = new List<int>();
            int amount = 1;
            for(int i = 1; i < val.Count; i++)
            {
                //если одинаковые подряд
                if (val[i] == val[i - 1]) amount++;
                else
                {
                    amountOfOneColor.Add(amount); //добавляем новое количество 
                    amount = 1; //сбрасываем
                }
            }
            Color[] cols = GetColors(amountOfOneColor.Count, start, end);//формируем массив
            Color[] colors = new Color[vertexes.Count]; //результирующий массив с повторами
            int k = 0; //индекс
            for(int i = 0; i < amountOfOneColor.Count; i++)
            {
                for(int j = 0; j < amountOfOneColor[i]; j++)
                {
                    colors[k] = cols[i];
                    k++;
                }
            }
            return colors;
        }

        /// <summary>
        /// Получаем массив цветов
        /// </summary>
        /// <param name="size">размер массива</param>
        /// <param name="first">начальный цвет</param>
        /// <param name="second">конечный цвет</param>
        public static Color[] GetColors(int size, Color first, Color second)
        {
            int rMax = first.R;
            int rMin = second.R;
            int gMax = first.G;
            int gMin = second.G;
            int bMax = first.B;
            int bMin = second.B;

            Color[] colors = new Color[size];

            for (int i = 0; i < size; i++)
            {
                var rAverage = rMin + (int)((rMax - rMin) * i / size);
                var gAverage = gMin + (int)((gMax - gMin) * i / size);
                var bAverage = bMin + (int)((bMax - bMin) * i / size);
                colors[i] = Color.FromArgb(rAverage, gAverage, bAverage);
            }

            return colors;
        }

    }
}
