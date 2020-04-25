using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphHelper
{
    public static class GroupHelper
    {

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



        

    }
}
