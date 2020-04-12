using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphHelper
{
    public static class FileHelper
    {
        /// <summary>
        /// Печатает массив строк
        /// </summary>
        /// <param name="str">массив строк</param>
        public static void Print(string[] str)
        {
            foreach (string s in str)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine();
        }

        
    }
}
