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
            if (!enable_loop && G.isCyclic()) 
                throw new CircleException();
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
            ca.get_chains_all(G);
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
            ca.get_chain_word_unique(G);
            return 0;
        }


        /* -c -h -t -r */
        public int gen_chain_char(string[] words, int len, string[] result, 
            char head, char tail, bool enable_loop)
        {
            Graph G = new Graph();
            G.AddG(words);
            if (!enable_loop && G.isCyclic())
                throw new CircleException();
            return 0;
        }
    }
}