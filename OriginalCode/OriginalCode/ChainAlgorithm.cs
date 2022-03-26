using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;


namespace OriginalCode
{
    class ChainAlgorithm
    {
        public ChainAlgorithm() { }

        /* -n */
        public void get_chains_all(Graph G)
        {
            n_Method n_method = new n_Method();
            n_method.startDFS(G);
            Console.WriteLine(n_method.link_set.Count);

            for (int i = 0; i < n_method.link_set.Count; i++)
            {
                Console.WriteLine(n_method.link_set[i]);
            }
        }

        /* -m */
        public void get_chain_word_unique(Graph G)
        {

        }

    }

    class n_Method
    {
        public int num = 0;
        public ArrayList link_set = new ArrayList();
        public n_Method() { }

        public void startDFS(Graph G)
        {
            graphInitialize(G);
            startTopo(G);

        }

        public void graphInitialize (Graph G)
        {
            for (int i = 0; i < Graph.word_list.Count; i++)
            {
                //清除出入度为0的点
                ArrayList next_list = G.getNextWordList((Word)Graph.word_list[i]);
                ArrayList last_list = G.getLastNode((Word)Graph.word_list[i]);
                if (next_list == null
                    && last_list == null)
                {
                    Graph.word_list.RemoveAt(i);
                    i--;
                    continue;
                }
                //初始化边权1
                if (next_list != null)
                {
                    for (int j = 0; j < next_list.Count; j++)
                    {
                        G.setE(((Word)Graph.word_list[i]).index, ((Word)next_list[j]).index, 1);
                    }
                }
            }
        }

        public void DFS( Graph G, Word word, ArrayList live_list, bool[] visited)
        {
            ArrayList next_node = G.getNextWordList(word);
            if(next_node == null)
            {
                return;
            }
            visited[word.index] = true;
            
            for (int j = 0; j < next_node.Count; j ++)
            {
                Word word1 = (Word)next_node[j];
                ArrayList tmp = new ArrayList();
                for (int k = 0; k < live_list.Count; k++)
                {
                    tmp.Add(live_list[k] + " " + word1.word);
                    link_set.Add(live_list[k] + " " + word1.word);
                }
                if (!visited[word1.index])
                {
                    tmp.Add(word1.word);
                    G.deletE(word.index, word1.index);
                }
                DFS(G, word1, tmp, visited);
            }
           
        }

        public bool startTopo(Graph G)
        {
            bool[] visited = new bool[Graph.original_words_num];

            for (int i = 0; i < Graph.word_list.Count; i++) {
                Word word = (Word)Graph.word_list[i];
                ArrayList live_list = new ArrayList();
                live_list.Add(word.word);
                if (G.getLastNode(word) == null)
                {
                    DFS(G, word, live_list, visited);
                }
            }
            Console.WriteLine("no circle");
            return false;
        }
    }
}
