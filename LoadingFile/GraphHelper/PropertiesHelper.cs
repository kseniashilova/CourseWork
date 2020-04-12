using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphHelper
{

    public static class PropertiesHelper
    {
        /// <summary>
        /// получаем список вершин, из которых исходят ребра
        /// </summary>
        public static List<string> Vertexes1(List<Tuple<string, string, string, int>> arr)
        {
            GroupHelper.SortByVertex1(ref arr); //первые вершины в порядке возрастания

            List<string> res =
                new List<string>();

            res.Add(arr[0].Item1); //добавляем первый элемент
            for (int i = 1; i < arr.Count; i++)
            {
                if (arr[i].Item1 != arr[i - 1].Item1)  //попалась новая вершина
                    res.Add(arr[i].Item1);
            }
            return res;
        }

        /// <summary>
        /// Получаем список вершин, в которые входят ребра
        /// </summary>
        public static List<string> Vertexes2(List<Tuple<string, string, string, int>> arr)
        {
            GroupHelper.SortByVertex2(ref arr); //первые вершины в порядке возрастания

            List<string> res = new List<string>();

            res.Add(arr[0].Item2); //добавляем первый элемент
            for (int i = 1; i < arr.Count; i++)
            {
                if (arr[i].Item2 != arr[i - 1].Item2)  //попалась новая вершина
                    res.Add(arr[i].Item2);
            }
            return res; 
        }

        /// <summary>
        /// Получаем список всех вершин
        /// </summary>
        public static List<string> Vertexes(List<Tuple<string, string, string, int>> arr)
        {

            List<string> vert1 = PropertiesHelper.Vertexes1(arr);
            List<string> vert2 = PropertiesHelper.Vertexes2(arr);
            foreach (string s in vert1)
            {
                if (!vert1.Contains(s))
                {
                    vert2.Add(s); //если уникальный, то добавляем
                    Console.WriteLine("Есть уникальный элемент");
                }
            }
            return vert2;
        }


        /// <summary>
        /// Валентности каждой вершины: вершина - валентность
        /// </summary>
        public static List<Tuple<string, int>> Valences(List<Tuple<string, string, string, int>> arr)
        {
            List<Tuple<string, int>> res = new List<Tuple<string, int>>();

            int val;
            int i = 0;
            GroupHelper.SortByVertex2(ref arr); //все вершины на втором месте в порядке возрастания
            while (i < arr.Count - 1)
            {
                val = 1;
                while(i < arr.Count - 1 && arr[i].Item2 == arr[i + 1].Item2)
                {
                    val++;
                    i++;
                }
                //Запишем вершину и ее валентность
                res.Add(new Tuple<string, int>(arr[i].Item1, val));
                i++;
            }



            GroupHelper.SortByVertex1(ref arr); //все вершины на первом месте в порядке возрастания
            i = 0;
            while (i < arr.Count - 1)
            {
                int addition = 1; //на сколько увеличить валентность или какая новая
                while (i < arr.Count - 1 && arr[i].Item2 == arr[i + 1].Item2)
                {
                    addition++;
                    i++;
                }

                int a = res.FindIndex(x => (x.Item1 == arr[i].Item1)); //ииндекс первого вхождения
                if (a != -1)//если уже есть такой элемент
                {
                    res[a] = new Tuple<string, int>(arr[i].Item1, res[a].Item2 + addition);
                }
                else //если вершина еще не попадалась
                {
                    res.Add(new Tuple<string, int>(arr[i].Item1, addition));
                }
                i++;
            }

            return res;
        }

        public static double AverageValences(List<Tuple<string, string, string, int>> arr)
        {
            List<Tuple<string, int>> val = Valences(arr);
            double sum = 0;
            foreach(var item in val)
            {
                sum += item.Item2;
            }
            return sum / val.Count;
        }
    }
}
