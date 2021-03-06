﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphHelper
{

    public static class PropertiesHelper
    {

        #region GetListOfVertexes
        /// <summary>
        /// получаем список вершин, из которых исходят ребра
        /// </summary>
        public static List<string> Vertexes1(List<Tuple<string, string, string, int>> arr)
        {
           
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
                if (vert2.IndexOf(s) == -1) //если vert2 не содержит очередную строку
                {
                    vert2.Add(s); //если уникальный, то добавляем
                }
            }
            return vert2;
        }
        #endregion

        #region Valences

        /// <summary>
        /// Валентности каждой вершины: вершина - валентность
        /// </summary>
        public static List<Tuple<string, int>> Valences(List<Tuple<string, string, string, int>> arr)
        {
            List<Tuple<string, int>> res = new List<Tuple<string, int>>();

            List<string> vert = Vertexes(arr); //получили список вершин
            for (int i = 0; i < vert.Count; i++)
            {
                int val = AmountOfNeighbours(vert[i], vert, arr);
                res.Add(new Tuple<string, int>(vert[i], val));
            }
            return res;
        }

        public static double AverageValences(List<Tuple<string, string, string, int>> arr)
        {
            List<Tuple<string, int>> val = Valences(arr);
            double sum = 0;
            foreach (var item in val)
            {
                sum += item.Item2;
            }
            return sum / val.Count;
        }
        public static int MaxValence(List<Tuple<string, string, string, int>> arr)
        {
            List<Tuple<string, int>> vals = Valences(arr);
            vals.Sort((x, y) => x.Item2.CompareTo(y.Item2));
            return vals[vals.Count - 1].Item2;
        }
        public static int MinValence(List<Tuple<string, string, string, int>> arr)
        {
            List<Tuple<string, int>> vals = Valences(arr);
            vals.Sort((x, y) => x.Item2.CompareTo(y.Item2));
            return vals[0].Item2;
        }

        public static int AmountOfVal1(List<Tuple<string, string, string, int>> arr)
        {
            List<Tuple<string, int>> vals = Valences(arr);
            int res = 0;
            foreach (var v in vals) if (v.Item2 == 1) res++;
            return res;
        }

        #endregion

        #region Weight
        public static double AverageWeight(List<Tuple<string, string, string, int>> arr)
        {
            double sum = 0;
            foreach (var item in arr)
            {
                sum += item.Item4;
            }
            return sum / arr.Count;
        }
        public static int MaxWeight(List<Tuple<string, string, string, int>> arr)
        {
            int max = 0;
            foreach (var item in arr)
            {
                if (item.Item4 > max)
                {
                    max = item.Item4;
                }
            }
            return max;
        }
        public static int MinWeight(List<Tuple<string, string, string, int>> arr)
        {
            int min = arr[0].Item4;
            foreach (var item in arr)
            {
                if (item.Item4 < min) min = item.Item4;
            }
            return min;
        }

        public static double AverageWeightVertexes
            (out int amoutOfEdges, 
            List<string> group, List<Tuple<string, string, string, int>> arr)
        {
            int sum = 0;
            int count = 0;
            foreach (var el in arr)
            {
                int ind1 = group.FindIndex(x => x == el.Item1);
                int ind2 = group.FindIndex(x => x == el.Item2);

                //если ребро существует в группе
                if (ind1 != -1 && ind2 != -1)
                {
                    sum += el.Item4;
                    count++; //увеличиваем счетчик ребер
                }
            }
            amoutOfEdges = count; //количество ребер
            if (count == 0) return 0;
            else return (1.0 * sum / count);
        }

        public static double AverWeightLastVertex(int prevAmount,out int newAmount,
            double prevAver,
            List<string> group, List<Tuple<string, string, string, int>> arr)
        {
            int sum = 0;
            int additionEdges = 0; //дополнительные реба от последней вершины
            string v = group.Last();
            for(int i = 0; i < group.Count; i++)
            {
                int index = arr.FindIndex(x =>
                (x.Item1 == v && x.Item2 == group[i]) ||
                (x.Item2 == v && x.Item1 == group[i]));

                if(index != -1)
                {
                    additionEdges++; //добавляем к количеству ребер
                    sum += arr[index].Item4; //добавляем к сумме
                }
            }

            newAmount = prevAmount + additionEdges;

            //высчитываем новое среднее
            return ((prevAver * prevAmount + sum) / (newAmount));
        }
        #endregion

        #region Clustering
        /// <summary>
        /// Образуют ли заданные вершины треугольник
        /// </summary>
        public static bool IsTriangle(string v1, string v2, string v3, List<Tuple<string, string, string, int>> arr)
        {
            bool b1 = false, b2 = false, b3 = false;
            if (arr.FindIndex(x =>
            (x.Item1 == v1 && x.Item2 == v2) || (x.Item2 == v1 && x.Item1 == v2)) != -1) //есть такое ребро
                b1 = true;

            if (arr.FindIndex(x =>
            (x.Item1 == v1 && x.Item2 == v3) || (x.Item1 == v3 && x.Item2 == v1)) != -1) //есть такое ребро
                b2 = true;

            if (arr.FindIndex(x =>
            (x.Item1 == v2 && x.Item2 == v3) || (x.Item1 == v3 && x.Item2 == v2)) != -1) //есть такое ребро
                b3 = true;

            return (b1 && b2 && b3);
        }



        
        public static int AmountOfTriangleESqrtE(List<Tuple<string, string, string, int>> arr)
        {
            int amount = 0;
            //получаем список вершин
            List<string> vert = Vertexes(arr);
            List<Tuple<string, int>> valences = Valences(arr); //валентности

            for (int i = 0; i < arr.Count; i++)
            {
                string u = arr[i].Item1; //первая вершина
                string v = arr[i].Item2; //вторая вершина

                //если первая вершина имеет валентность меньше
                bool first =
                    valences.Find(x => x.Item1 == u).Item2 < valences.Find(x => x.Item1 == v).Item2;

                if (!first)//меняем местами
                {
                    u = arr[i].Item2;
                    v = arr[i].Item1;
                }
                List<string> neighs = ListOfNeighbours(u, vert, arr); //получаем список соседей
                foreach (var neighbour in neighs)
                {
                    if (IsNeighbour(v, neighbour, arr)) amount++;
                }
            }
            return amount / 3;
        }


        public static double CoeffClustering(List<Tuple<string, string, string, int>> arr)
        {
            int triangles = AmountOfTriangleESqrtE(arr);
            List<string> vert = Vertexes(arr);
            int n = vert.Count;
            int all_triangles = n * (n - 1) * (n - 2) / 6;
            return (0.1 * triangles) / all_triangles;
        }
        #endregion

        #region Neighbour
        /// <summary>
        /// Являются ли вершины соседними
        /// </summary>
        public static bool IsNeighbour(string v1, string v2, List<Tuple<string, string, string, int>> arr)
        {
            return (arr.FindIndex(x =>
             (x.Item1 == v1 && x.Item2 == v2) || (x.Item1 == v2 && x.Item2 == v1)) != -1); //есть такое ребро

        }

        /// <summary>
        /// Список вершин, соседних с данной
        /// </summary>
        public static List<string>
            ListOfNeighbours(string v, List<string> vertexes, List<Tuple<string, string, string, int>> arr)
        {
            List<string> res = new List<string>();

            foreach (var ver in vertexes)
            {
                if (IsNeighbour(v, ver, arr)) res.Add(ver);
            }
            return res;
        }

        /// <summary>
        /// Считает количество соседних вершин
        /// </summary>
        /// <param name="v"></param>
        /// <param name="vertexes"></param>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static int AmountOfNeighbours
            (string v, List<string> vertexes, List<Tuple<string, string, string, int>> arr)
        {
            
            return ListOfNeighbours(v, vertexes, arr).Count;
            
        }
        #endregion

        #region Loops
        
        public static int AmountOfLoops(List<Tuple<string, string, string, int>> arr)
        {
            
            int res = 0;
            foreach (var el in arr)
            {
                if (el.Item1 == el.Item2) res++;
            }
            return res;
        }
        #endregion

        
    }
}

