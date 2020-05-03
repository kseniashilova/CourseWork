using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using GraphHelper;

namespace LoadingFile
{
    class Program
    {


        static void Main(string[] args)
        {
            //Полный путь.
            const string path =
                "C:/Users/Пользователь/Desktop/CourseWork/table/traced-roi-connections.csv";
            string[] strings = File.ReadAllLines(path); //считываем по строкам

            List<Tuple<string, string, string, int>> lst =
                GeneralPropertiesHelper.GetTuples(strings);






            //List<Tuple<string, int>> res = PropertiesHelper.Valences(lst);
            //int s = 0;
            //int amountOf1Val = 0;
            //int amountOf2Val = 0;
            //int amountOf3Val = 0;
            //int min_degree = res[0].Item2;
            //int max_degree = 0;
            //foreach(var item in res)
            //{
            //    s += item.Item2;
            //    if (item.Item2 == 1) amountOf1Val++;
            //    if (item.Item2 == 2) amountOf2Val++;
            //    if (item.Item2 == 3)amountOf3Val++;
            //    if (item.Item2 < min_degree) min_degree = item.Item2;
            //    if (item.Item2 > max_degree) max_degree = item.Item2;
            //}
            //Console.WriteLine("---------Весь граф-------");
            //Console.WriteLine(res.Count + " - Количество");
            //Console.WriteLine(s/res.Count + " - Средняя валентность");
            //Console.WriteLine(amountOf1Val + " - Валенстности 1");
            //Console.WriteLine(amountOf2Val + " - Валентности 2");
            //Console.WriteLine(amountOf3Val + " - Валентности 3");
            //Console.WriteLine(min_degree + " - минимальная валентность");
            //Console.WriteLine(max_degree + " - максимальная валентность");




            //Нейроны из OPTIC LOBE

            //List<Tuple<string, string, string, int>> optic_lobe =
            //    GroupHelper.FindRegions(lst,
            //    new string[] { "ME(L)", "ME(R)", "LO(L)", "LO(R)", "LOP(L)", "LOP(R)", "LA(L)", "LA(R)" });



            List<Tuple<string, string, string, int>> helper =
                GroupHelper.FindRegions(lst,
                new string[] { "LOP(L)", "LOP(R)" });
            List<List<string>> clust = GroupHelper.ClusteringAlg(helper);
            Console.WriteLine(clust.Count + "  кол-во групп");


        }
    }
}
