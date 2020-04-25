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

        public static List<string> GetRegions(List<Tuple<string, string, string, int>> arr)
        {
            List<string> res = new List<string>();
            for (int i = 0; i < arr.Count; i++)
            {
                string reg = arr[i].Item3.Split('(')[0]; //регион
                bool isreg = false;
                foreach (var t in res)
                {
                    if (t == reg) isreg = true;
                }
                //если false, то региона не нашлось, добавим регион
                if (!isreg) res.Add(reg);
            }
            return res;
        }

        public static List<Tuple<string, int>> GetRegions1(List<string> regs, List<Tuple<string, string, string, int>> arr)
        {
            List<Tuple<string, int>> res = new List<Tuple<string, int>>();
            foreach(string s in regs)
            {
                List<Tuple<string, string, string, int>> list = 
                    GroupHelper.FindRegions(arr, s);
                if (list.Count != 0) res.Add(new Tuple<string, int>(s, list.Count)); 
            }
            return res;
        }

    }
}
