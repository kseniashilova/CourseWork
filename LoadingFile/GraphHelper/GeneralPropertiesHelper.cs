using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphHelper
{
    public static class GeneralPropertiesHelper
    {
        public static List<Tuple<string, string, string, int>> GetTuples(string[] arr)
        {
            List<Tuple<string, string, string, int>> res = new List<Tuple<string, string, string, int>>();
            for (int i = 1; i <arr.Length; i++)
            {
                int w; //вес ребра
                if (!int.TryParse(arr[i].Split(',')[3], out w))
                    throw new ArgumentException("Вес указан не верно");

                res.Add(new Tuple<string, string, string, int>
                    (
                    arr[i].Split(',')[0], 
                    arr[i].Split(',')[1], 
                    arr[i].Split(',')[2], 
                     w
                    )
                    );
            }
            return res;
        }

        /// <summary>
        /// Считает факториал
        /// </summary>
        public static int Fact(int n)
        {
            if (n == 1) return 1;
            return Fact(n - 1) * n;
        }
        
    }
}
