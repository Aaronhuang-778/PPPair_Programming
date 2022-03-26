using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCode
{
    class Chain
    {

        public Chain() { }


        /* -w -h -t -r */
        public int gen_chain_word(string[] words, int len, string[] result, 
            char head, char tail, bool enable_loop)
        {
            Graph G = new Graph();
            G.AddG(words);
            if (G.isCyclic() && !enable_loop) 
                throw new CircleException();
            Stack<Word> sortList = new Stack<Word>();
            G.TopologicalSort(sortList);
            foreach (Word w in sortList)
            {
                Console.Write(w + " ");
            }
            return 0;
        }


        /* -n */
        public int gen_chains_all(string[] words, int len, string[] result)
        {
            Graph G = new Graph();
            G.AddG(words);
            if (G.isCyclic())
                throw new CircleException();
            return 0;
        }


        /* -m */
        public int gen_chain_word_unique(string[] words, int len, string[] result)
        {
            Graph G = new Graph();
            G.AddG(words);
            if (G.isCyclic())
                throw new CircleException();
            return 0;
        }


        /* -c -h -t -r */
        public int gen_chain_char(string[] words, int len, string[] result, 
            char head, char tail, bool enable_loop)
        {
            Graph G = new Graph();
            G.AddG(words);
            if (G.isCyclic() && !enable_loop)
                throw new CircleException();
            return 0;
        }
    }
}
