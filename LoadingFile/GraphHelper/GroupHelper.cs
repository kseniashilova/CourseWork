using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphHelper
{
    public static class GroupHelper
    {


        #region Regions
        public static List<Tuple<string, string, string, int>>
            FindRegions(List<Tuple<string, string, string, int>> arr, string region)
        {
            List<Tuple<string, string, string, int>> res
                = arr.FindAll(x => x.Item3.Split('(')[0] == region);
            return res;
        }

        /// <summary>
        /// группирует по областям, орые указаны в массиве
        /// </summary>
        public static List<Tuple<string, string, string, int>>
           FindRegions(List<Tuple<string, string, string, int>> arr, string[] region)
        {
            List<Tuple<string, string, string, int>> res =
                new List<Tuple<string, string, string, int>>();
            foreach (string reg in region)
            {

                List<Tuple<string, string, string, int>> oneRegion
                = arr.FindAll(x => x.Item3 == reg);
                foreach (var item in oneRegion)
                    res.Add(item);
            }
            return res;
        }

        public static List<Tuple<string, string, string, int>>
            FindRegions(List<Tuple<string, string, string, int>> arr, string region1, string region2)
        {
            List<Tuple<string, string, string, int>> res
                = arr.FindAll(x => (x.Item3 == region1 || x.Item3 == region2));
            return res;
        }


        public static List<List<Tuple<string, string, string, int>>>
            GroupByRegions(List<Tuple<string, string, string, int>> arr)
        {
            List<List<Tuple<string, string, string, int>>> res
                = new List<List<Tuple<string, string, string, int>>>();

            SortByRegions(ref arr); //сортируем

            //найдем  все области
            List<Tuple<string, string, string, int>> one_region
                = new List<Tuple<string, string, string, int>>();

            int i = 1;
            one_region.Add(arr[0]); //добавляем первый элемент
            while (i < arr.Count)
            {
                while (i < arr.Count && arr[i].Item3 == arr[i - 1].Item3)
                {
                    one_region.Add(arr[i]);
                    i++;
                }
                res.Add(one_region);
                one_region = new List<Tuple<string, string, string, int>>();
                i++;
            }
            return res;
        }
        #endregion

        #region Sorting
        public static void SortByRegions(ref List<Tuple<string, string, string, int>> arr)
        {
            arr.Sort((x, y) =>
            {
                string s1 = x.Item3;
                string s2 = y.Item3;
                return s1.CompareTo(s2);
            });
        }

        public static void SortByVertex1(ref List<Tuple<string, string, string, int>> arr)
        {
            arr.Sort((x, y) =>
            {
                string s1 = x.Item1;
                string s2 = y.Item1;
                return s1.CompareTo(s2);
            });
        }

        public static void SortByVertex2(ref List<Tuple<string, string, string, int>> arr)
        {
            arr.Sort((x, y) =>
            {
                string s1 = x.Item2;
                string s2 = y.Item2;
                return s1.CompareTo(s2);
            });
        }

        /// <summary>
        /// Сортируем список вершин по возрастанию валентности
        /// </summary>
        public static void SortByValences
            (ref List<string> vertexes, List<Tuple<string, string, string, int>> arr)
        {
            List<string> vert2 = vertexes;
            vertexes.Sort((x, y) =>
            {
                int a = PropertiesHelper.AmountOfNeighbours(x, vert2, arr);
                int b = PropertiesHelper.AmountOfNeighbours(y, vert2, arr);
                return a.CompareTo(b);
            });
        }

        /// <summary>
        /// Сортируем список регионов по возрастанию размера
        /// </summary>
        public static List<string> SortRegionsBySize
            (List<string> region, List<Tuple<string, string, string, int>> arr)
        {
            List<Tuple<string, int>> sorted = new List<Tuple<string, int>>();
            foreach (string reg in region)
            {
                int size = 0;
                for (int i = 0; i < arr.Count; i++)
                {
                    if (arr[i].Item3 == reg) size++;
                }
                sorted.Add(new Tuple<string, int>(reg, size));
            }

            sorted.Sort((x, y) => y.Item2.CompareTo(x.Item2));
            List<string> res = new List<string>();
            foreach (var item in sorted)
            {
                res.Add(item.Item1);
            }
            return res;
        }

        #endregion

        #region Clustering

        






        public static List<List<string>>
            ClusteringAlgNew(List<Tuple<string, string, string, int>> arr, int n)
        {
            
            double average = PropertiesHelper.AverageWeight(arr);
            List<string> vert = PropertiesHelper.Vertexes(arr); //находим вершины


            
            List<List<string>> clusts = new List<List<string>>(); //лист кластеров

            /*
             * поместим все вершины в n кластеров (фиксированное количество)
             * затем поочередно рассматривая каждую вершину будем вычислять
             * новое среднее расстояние в кластере 
             * среднее расстояние будем вычислять как средний вес образовавшихся ребер
             * вершину будем помещать в тот кластер, у которого итоговый средний вес ближе всего к среднему по графу
             */
            
            //создаем n начальных кластеров
            for (int i = 0; i < n; i++)
            {
                clusts.Add(new List<string>());
            }
            for (int i = 0; i < vert.Count; i++)
            {
                clusts[i % n].Add(vert[i]);
            }

            List<double> prevAverage = new List<double>();
            List<int> prevAmount = new List<int>();
            for(int i = 0; i < n; i++)
            {
                int prev;
                //вычисляем первоначальный средний вес ребер в кластерах
                prevAverage.Add(PropertiesHelper.AverageWeightVertexes(out prev, clusts[i], arr));
                prevAmount.Add(prev);
            }

            //по всем вершинам проходимся
            for (int i = 0; i < vert.Count; i++)
            {
                int indexFirst = i % n; //индекс кластера, в котором вершина находится в начале
                string v = vert[i];
                //пересоздаем новые средние кластеров
                List<double> newAverage = new List<double>();
                List<int> newAmount = new List<int>();

                for (int k = 0; k < n; k++)
                {
                    if (k == indexFirst)
                        clusts[k].Remove(v); //удаляем вершину из того кластера, где она была

                    clusts[k].Add(v); //добавляем в конец
                    //теперь вершина, от которой нужно посчитать
                    //количество новых ребер с остальными и их суммарный вес,
                    //находится в конце
                    //считаем новый средний вес ребер
                    int newAmountEdg;
                    newAverage.Add(PropertiesHelper.AverWeightLastVertex(prevAmount[k],
                        out newAmountEdg,
                        prevAverage[k],
                        clusts[k], arr));
                    //добавляем новое количество 
                    newAmount.Add(newAmountEdg);
                    //удаляем последнюю
                    clusts[k].RemoveAt(clusts[k].Count - 1);
                }


                newAverage = newAverage.Select(x => x - average).ToList();
                int indexSecond = newAverage.IndexOf(newAverage.Min());
                clusts[indexSecond].Add(v); //добавляем в конец нужного кластера    
                //меняем старый список на новый
                prevAverage = new List<double>();
                foreach(double aver in newAverage)
                {
                    prevAverage.Add(aver);
                }
                prevAmount = new List<int>();
                foreach (int amount in newAmount)
                {
                    prevAmount.Add(amount);
                }

            }

            return clusts;

        }





       
        #endregion

    }
}
