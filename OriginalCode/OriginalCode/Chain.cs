using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCode
{
    class Chain
    {

        public Chain() { }

        public void writeFile(string[] result)
        {
            string str = System.IO.Directory.GetCurrentDirectory();
            str = str + "\\solution.txt";
            System.IO.File.WriteAllLines(str, result);
        }
        /* -w -h -t -r */
        public int gen_chain_word(string[] words, int len, string[] result, 
            char head, char tail, bool enable_loop)
        {
            Graph G = new Graph();
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
                    for (int j = 0; j < tmp.Count; j++)
                    {
                        result[j] = (string)tmp[j];
                        Console.WriteLine(result[j]);
                    }
                }

                return 0;
            }

            ArrayList sortList = new ArrayList();
            G.TopologicalSort(sortList);
            Console.WriteLine("sortList: ");
            foreach (Stack<Word> s in sortList)
            {
                foreach (Word sw in s)
                    Console.Write(sw + " ");
                Console.WriteLine();
            }

            Console.WriteLine("longestPathDAG: ");
            Stack<Word> res_stack = new Stack<Word>();
            int dist = G.longestPathDAG(sortList, res_stack, head, tail);
            if (dist < 0) throw new ChainNotFoundException();

            Console.Write("result: ");
            int i = 0;
            foreach (Word w in res_stack)
            {
                Console.Write(w + " ");
                result[i] = w.word;
                i++;
            }
            Console.WriteLine("dist = " + dist);

            writeFile(result);
            return 0;
        }


        /* -n */
        public int gen_chains_all(string[] words, int len, string[] result)
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
            Console.WriteLine(tmp.Count);

            if (tmp != null && tmp.Count > 0)
            {
                for (int i = 0; i < tmp.Count; i++)
                {
                    result[i] = (string)tmp[i];
                    Console.WriteLine(result[i]);
                }
            }
            return 0;
        }


        /* -m */
        public int gen_chain_word_unique(string[] words, int len, string[] result)
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
                    Console.WriteLine(result[i]);
                }
            }

            writeFile(result);
            return 0;
        }


        /* -c -h -t -r */
        public int gen_chain_char(string[] words, int len, string[] result, 
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
                        Console.WriteLine(result[j]);
                    }
                }
                return 0;
            }

            ArrayList sortList = new ArrayList();
            G.TopologicalSort(sortList);
            Console.WriteLine("sortList: ");

            Stack<Word> res_stack = new Stack<Word>();
            int dist = G.longestPathDAG(sortList, res_stack, head, tail);
            if (dist < 0) throw new ChainNotFoundException();

            Console.Write("longestPathDAG: ");
            int i = 0;
            foreach (Word w in res_stack)
            {
                Console.Write(w + " ");
                result[i] = w.word;
                i++;
            }
            Console.WriteLine("dist = " + dist);

            writeFile(result);
            return 0;
        }
    }
}
