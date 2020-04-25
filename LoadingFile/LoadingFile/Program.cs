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

            List<Tuple<string, string, string, int>> optic_lobe =
                GroupHelper.FindRegions(lst,
                new string[] { "ME(L)", "ME(R)", "LO(L)", "LO(R)", "LOP(L)", "LOP(R)", "LA(L)", "LA(R)" });


            //List<Tuple<string, int>> res_optic_lobe = PropertiesHelper.Valences(optic_lobe);
            //int s1 = 0;
            //int amountOf1Val1 = 0, amountOf2Val1 = 0,amountOf3Val1 = 0;
            //int min_degree1 = res_optic_lobe[0].Item2;
            //int max_degree1 = 0;
            //foreach (var item in res_optic_lobe)
            //{
            //    s1 += item.Item2;
            //    if (item.Item2 == 1) amountOf1Val1++;
            //    if (item.Item2 == 2) amountOf2Val1++;
            //    if (item.Item2 == 3) amountOf3Val1++;

            //    if (item.Item2 < min_degree1) min_degree1 = item.Item2;
            //    if (item.Item2 > max_degree1) max_degree1 = item.Item2;
            //}

            //double averWeight = PropertiesHelper.AverageWeight(optic_lobe);
            //int max1 = PropertiesHelper.MaxWeight(optic_lobe);
            //int min1 = PropertiesHelper.MinWeight(optic_lobe);
            //Console.WriteLine("---------Оптическая зона-------");
            //Console.WriteLine(res_optic_lobe.Count + " - Количество");
            //Console.WriteLine(s1 / res_optic_lobe.Count + " - Средняя валентность");
            //Console.WriteLine(amountOf1Val1 + " - Валенстности 1");
            //Console.WriteLine(amountOf2Val1 + " - Валентности 2");
            //Console.WriteLine(amountOf3Val1 + " - Валентности 3");
            //Console.WriteLine(min_degree1 + " - минимальная валентность");
            //Console.WriteLine(max_degree1 + " - максимальная валентность");
            //Console.WriteLine(averWeight + " - средний вес ребер");
            //Console.WriteLine(max1 + " - максимальный вес ребра");
            //Console.WriteLine(min1 + " - минимальный вес ребра");





            //Console.WriteLine(optic_lobe.Count + " - количество ребер");

            //double coefClustering = PropertiesHelper.CoeffClustering(optic_lobe);
            //Console.WriteLine(coefClustering + " - коэффициент кластеризации");

            //int amount = PropertiesHelper.AmountOfTriangleVE(optic_lobe);
            //Console.WriteLine(amount + " - Количество треугольников");


            //regs.ForEach(x => Console.WriteLine(x));

            List<string>regs =GeneralPropertiesHelper.GetRegions(lst);
            List<string> regs1 = GroupHelper.SortRegionsBySize(regs, lst);
            List<Tuple<string, int>> regs2 = GeneralPropertiesHelper.GetRegions1(regs, lst);
            regs2.ForEach(x => Console.WriteLine($"{x.Item1}    {x.Item2}"));
            
        }
    }
}
