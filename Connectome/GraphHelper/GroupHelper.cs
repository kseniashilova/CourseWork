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

        /// <summary>
        /// Находит в списке ребра, относящиеся к региону region
        /// </summary>
        public static List<Tuple<string, string, string, int>>
            FindRegions(List<Tuple<string, string, string, int>> arr, string region)
        {
            List<Tuple<string, string, string, int>> res
                = arr.FindAll(x => x.Item3.Split('(')[0] == region);
            return res;
        }

        /// <summary>
        /// группирует по областям, которые указаны в массиве
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

       
        #endregion

        #region Sorting

        /// <summary>
        /// Сортировка по возрастанию номеров вершин на первом месте
        /// </summary>
        public static void SortByVertex1(ref List<Tuple<string, string, string, int>> arr)
        {
            arr.Sort((x, y) =>
            {
                string s1 = x.Item1;
                string s2 = y.Item1;
                return s1.CompareTo(s2);
            });
        }

        /// <summary>
        /// Сортировка по возрастанию номеров вершин на втором месте
        /// </summary>
        /// <param name="arr"></param>
        public static void SortByVertex2(ref List<Tuple<string, string, string, int>> arr)
        {
            arr.Sort((x, y) =>
            {
                string s1 = x.Item2;
                string s2 = y.Item2;
                return s1.CompareTo(s2);
            });
        }




        #endregion

        #region Clustering

        /// <summary>
        /// Кластеризация.
        /// поместим все вершины в n кластеров(фиксированное количество)
        /// затем поочередно рассматривая каждую вершину будем вычислять
        /// новое среднее расстояние в кластере 
        /// среднее расстояние будем вычислять как средний вес образовавшихся ребер
        ///  вершину будем помещать в тот кластер,
        ///  у которого итоговый средний вес ближе всего к среднему по графу
        /// </summary>>
        public static List<List<string>>
            ClusteringAlgNew(List<Tuple<string, string, string, int>> arr, int n)
        {
            
            double average = PropertiesHelper.AverageWeight(arr);
            List<string> vert = PropertiesHelper.Vertexes(arr); //находим вершины


            
            List<List<string>> clusts = new List<List<string>>(); //лист кластеров

            
            
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
