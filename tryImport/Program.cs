using System;
using System.Collections.Generic;
using System.Linq;
using OriginalCode;

namespace tryImport
{
    class Program
    {


        private static IEnumerable<T> GetRow<T>(T[,] array, int index)
        {
            for (int i = 0; i < array.GetLength(1); i++)
            {
                yield return array[index, i];
            }
        }

        private static string[] char2D_To_StringArray(in char[,] words)
        {
            string[] s = new string[20005];
            for (int i = 0; i < words.GetLength(0); i++)
            {
                s[i] = new string(GetRow(words, i).ToArray());
            }
            return s;
        }

        private static void stringArray_To_char2D(in string[] ss, ref char[,] result)
        {
            int i = 0;
            while (i < ss.Length && ss[i] != null && ss[i].Length > 0)
            {
                char[] cs = ss[i].ToCharArray();
                int j = 0;
                foreach (char c in cs)
                    result[i, j++] = c;
                i++;
            }
        }


        public static void writeFile(string[] result)
        {
            string str = System.IO.Directory.GetCurrentDirectory();
            str = str + "\\solution.txt";
            System.IO.File.WriteAllLines(str, result);
        }



        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            char[,] words =
            {
                {'a', 'b'},
                {'b', 'c'}
            };
            Console.WriteLine(words[1, 1]);
            char[,] result = new char[100, 100];

            Chain.gen_chain_word(ref words, 4, ref result, '!', '!', false);
            Console.WriteLine("get result from dll!!!!!!");
            Console.WriteLine(result[0, 0] + "" + result[0, 1] + " " + result[1, 0] + "" + result[1, 1]);
        }
    }
}
