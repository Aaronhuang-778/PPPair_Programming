using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Core
{
    public static class Chain
    {
        public static int gen_for_gui_para(bool useFileInput, string inputSource,
            char calType, bool isR, char charH, char charT, ref string[] result)
        {
            string[] words = null;

            InputPara ip = new InputPara();

            if (useFileInput)
            {
                string[] test = { "-" + calType, inputSource };
                words = Program.input(test, out ip);
            }
            else
            {
                ip.head = charH;
                ip.tail = charT;
                ip.is_loop = isR;
                ip.type = calType;
                if (inputSource == null || inputSource.Length == 0)
                    throw new InvalidInputException(InputErrorType.code.no_input_text);
                inputSource = inputSource.ToLower();
                words = Regex.Split(inputSource, "[^(a-zA-Z)]+");
                if (words[0] == "")
                {
                    for (int i = 0; i < words.Length - 1; i++)
                    {
                        words[i] = words[i + 1];
                    }
                    Array.Resize(ref words, words.Length - 1);
                }
            }

            Word.type = ip.type;

            switch (ip.type)
            {
                case 'n':
                    //统计单词链数量 只传递单词链
                    gen_chains_all_str(words, words.Length, result);
                    break;
                case 'm':
                    //输出首字母不相同的包含单词数量最多的单词链 只传递单词链
                    gen_chain_word_unique_str(words, words.Length, result);
                    break;
                case 'w':
                    //需要传入Global的参数进行处理
                    gen_chain_word_str(words, words.Length, result, charH, charT, isR);
                    break;
                case 'c':
                    //需要传入Global的参数进行处理
                    gen_chain_char_str(words, words.Length, result, charH, charT, isR);
                    break;
            }
            return 0;
        }

        public static void writeFile(string[] result)
        {
            string str = System.IO.Directory.GetCurrentDirectory();
            str = str + "\\solution.txt";
            System.IO.File.WriteAllLines(str, result);
        }


        /* -w -h -t -r */
        public static int gen_chain_word_str(string[] words, int len, string[] result, 
            char head, char tail, bool enable_loop)
        {
            Graph G = new Graph();
            //Console.WriteLine(words.Length);
            //Console.WriteLine(words[0]);
            G.AddG(words);
            if (G.isCyclic())
            {
                if (!enable_loop) throw new CircleException();

                ChainAlgorithm ca = new ChainAlgorithm();
                ArrayList tmp = ca.get_RW(G, head, tail);

                if (tmp == null || tmp.Count == 0)
                {
                    throw new ChainNotFoundException();
                }

                if (tmp != null && tmp.Count > 0)
                {
                    Console.WriteLine(tmp.Count);
                    for (int j = 0; j < tmp.Count; j++)
                    {
                        result[j] = (string)tmp[j];
                        //Console.WriteLine(result[j]);
                    }
                }

                return 0;
            }

            ArrayList sortList = new ArrayList();
            G.TopologicalSort(sortList);
            //Console.WriteLine("sortList: ");
            /*foreach (Stack<Word> s in sortList)
            {
                foreach (Word sw in s)
                    Console.Write(sw + " ");
                Console.WriteLine();
            }*/

            //Console.WriteLine("longestPathDAG: ");
            Stack<Word> res_stack = new Stack<Word>();
            int dist = G.longestPathDAG(sortList, res_stack, head, tail);
            if (dist < 0) throw new ChainNotFoundException();

            //Console.Write("result: ");
            int i = 0;
            foreach (Word w in res_stack)
            {
                //Console.Write(w + " ");
                result[i] = w.word;
                i++;
            }
            //Console.WriteLine("dist = " + dist);

            writeFile(result);
            return 0;
        }


        /* -n */
        public static int gen_chains_all_str(string[] words, int len, string[] result)
        {
            Graph G = new Graph();
            G.AddG(words);
            if (G.isCyclic())
                throw new CircleException();

            ChainAlgorithm ca = new ChainAlgorithm();
            ArrayList tmp = ca.get_chains_all(G);
            if (tmp == null || tmp.Count == 0)
            {
                throw new ChainNotFoundException();
            }
            
            //Console.WriteLine(tmp.Count);

            if (tmp != null && tmp.Count > 0)
            {
                result[0] = tmp.Count.ToString();
                for (int i = 0; i < tmp.Count; i++)
                {
                    result[i + 1] = (string)tmp[i];
                    //Console.WriteLine(result[i]);
                }
            }
            return 0;
        }


        /* -m */
        public static int gen_chain_word_unique_str(string[] words, int len, string[] result)
        {
            Graph G = new Graph();
            G.AddG(words);
            if (G.isCyclic())
                throw new CircleException();

            ChainAlgorithm ca = new ChainAlgorithm(); 
            ArrayList tmp = ca.get_chain_word_unique(G);
            if (tmp == null || tmp.Count == 0)
            {
                throw new ChainNotFoundException();
            }

            if (tmp != null && tmp.Count > 0)
            {
                for (int i = 0; i < tmp.Count; i++)
                {
                    result[i] = (string)tmp[i];
                    //Console.WriteLine(result[i]);
                }
            }

            writeFile(result);
            return 0;
        }


        /* -c -h -t -r */
        public static int gen_chain_char_str(string[] words, int len, string[] result, 
            char head, char tail, bool enable_loop)
        {
            Graph G = new Graph();
            G.AddG(words);
            if (G.isCyclic())
            {
                if (!enable_loop) throw new CircleException();

                ChainAlgorithm ca = new ChainAlgorithm();
                ArrayList tmp = ca.get_RC(G, head, tail);

                if (tmp == null || tmp.Count == 0)
                {
                    throw new ChainNotFoundException();
                }

                if (tmp != null && tmp.Count > 0)
                {
                    for (int j = 0; j < tmp.Count; j++)
                    {
                        result[j] = (string)tmp[j];
                        //Console.WriteLine(result[j]);
                    }
                }
                return 0;
            }

            ArrayList sortList = new ArrayList();
            G.TopologicalSort(sortList);
            //Console.WriteLine("sortList: ");

            Stack<Word> res_stack = new Stack<Word>();
            int dist = G.longestPathDAG(sortList, res_stack, head, tail);
            if (dist < 0) throw new ChainNotFoundException();

            //Console.Write("longestPathDAG: ");
            int i = 0;
            foreach (Word w in res_stack)
            {
                //Console.Write(w + " ");
                result[i] = w.word;
                i++;
            }
            //Console.WriteLine("dist = " + dist);

            writeFile(result);
            return 0;
        }
    }
}
