using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;


namespace Core
{
    class ChainAlgorithm
    {
        public ChainAlgorithm() { }

        /* -n */
        public ArrayList get_chains_all(Graph G)
        {
            n_Method n_method = new n_Method();
            n_method.startDFS(G);
            
            return n_method.link_set;
        }

        /* -m */
        public ArrayList get_chain_word_unique(Graph G)
        {
            m_Method m_method = new m_Method();
            m_method.startDFS(G);
            return m_method.link_set;
        }

        /* -w -r -h -e */
        public ArrayList get_RW(Graph G, char head, char tail)
        {
            rw_Method rw_method = new rw_Method();
            rw_method.startDFS(G, head, tail);
            return rw_method.link_set;
        }

        /* -c -h -t -r */
        public ArrayList get_RC(Graph G, char head, char tail)
        {
            rc_Method rc_method = new rc_Method();
            rc_method.startDFS(G, head, tail);
            return rc_method.link_set;
        }
    }

    class rc_Method
    {
        public ArrayList link_set = new ArrayList();
        public int max = 0;
        public char head = '!';
        public char tail = '!';


        public void rc_DFS(Graph G, Word word, bool[] visited, ArrayList live_list, int live_max)
        {
            ArrayList next_node = G.getNextWordList(word);
            if (next_node == null)
            {
                return;
            }

            for (int j = 0; j < next_node.Count; j++)
            {
                Word word1 = (Word)next_node[j];
                if (!visited[word1.index])
                {
                    bool[] tmp = new bool[Graph.word_list.Count];
                    for (int k = 0; k < visited.Length; k++)
                    {
                        tmp[k] = visited[k];
                    }
                    tmp[word1.index] = true;

                    ArrayList tmp1 = new ArrayList();
                    for (int k = 0; k < live_list.Count; k++)
                    {
                        tmp1.Add(live_list[k]);
                    }
                    tmp1.Add(word1.word);
                    int tmp_max = live_max + word1.length;

                    if ((this.tail != '!' && word1.word_tail == this.tail) || (this.tail == '!'))
                    {
                        if (tmp_max > max)
                        {
                            link_set.Clear();
                            for (int k = 0; k < tmp1.Count; k++)
                            {
                                link_set.Add(tmp1[k]);
                            }
                            max = tmp_max;
                        }
                    }

                    rc_DFS(G, word1, tmp, tmp1, tmp_max);

                }

            }
        }

        public void startDFS(Graph G, char head, char tail)
        {
            this.head = head;
            this.tail = tail;
            for (int i = 0; i < Graph.word_list.Count; i++)
            {
                Word word = (Word)Graph.word_list[i];
                if (this.head != '!' && word.word_head == this.head)
                {
                    bool[] visited = new bool[Graph.word_list.Count];
                    visited[word.index] = true;
                    ArrayList live_list = new ArrayList();
                    live_list.Add(word.word);
                    int live_max = word.length;
                    rc_DFS(G, word, visited, live_list, live_max);
                }
                else if (this.head == '!')
                {
                    bool[] visited = new bool[Graph.word_list.Count];
                    visited[word.index] = true;
                    ArrayList live_list = new ArrayList();
                    live_list.Add(word.word);
                    int live_max = word.length;
                    rc_DFS(G, word, visited, live_list, live_max);
                }
            }
        }
    }


    class rw_Method
    {
        public ArrayList link_set = new ArrayList();
        public int max = 0;
        public char head = '!';
        public char tail = '!';


        public void rw_DFS(Graph G, Word word, bool[] visited, ArrayList live_list)
        {
            ArrayList next_node = G.getNextWordList(word);
            if (next_node == null )
            {                
               return;
            }

            for (int j = 0; j < next_node.Count; j++)
            {
                Word word1 = (Word)next_node[j];
                if (!visited[word1.index])
                {
                    bool[] tmp = new bool[Graph.word_list.Count];
                    for (int k = 0; k < visited.Length; k++)
                    {
                        tmp[k] = visited[k];
                    }
                    tmp[word1.index] = true;

                    ArrayList tmp1 = new ArrayList();
                    for (int k = 0; k < live_list.Count; k++)
                    {
                        tmp1.Add(live_list[k]);
                    }
                    tmp1.Add(word1.word);
                    if ((this.tail != '!' && word1.word_tail == this.tail) || (this.tail == '!'))
                    {
                        if (tmp1.Count > max)
                        {
                            link_set.Clear();
                            for (int k = 0; k < tmp1.Count; k++)
                            {
                                link_set.Add(tmp1[k]);
                            }
                            max = tmp1.Count;
                        }
                    }

                    rw_DFS(G, word1, tmp, tmp1);

                }

            }
        }

        public void startDFS(Graph G, char head,  char tail)
        {
            this.head = head;
            this.tail = tail;
            for (int i = 0; i < Graph.word_list.Count; i++)
            {
                Word word = (Word)Graph.word_list[i];
                if (this.head != '!' && word.word_head == this.head)
                {
                    bool[] visited = new bool[Graph.word_list.Count];
                    visited[word.index] = true;
                    ArrayList live_list = new ArrayList();
                    live_list.Add(word.word);
                    rw_DFS(G, word, visited, live_list);
                }
                else if (this.head == '!')
                {
                    bool[] visited = new bool[Graph.word_list.Count];
                    visited[word.index] = true;
                    ArrayList live_list = new ArrayList();
                    live_list.Add(word.word);
                    rw_DFS(G, word, visited, live_list);
                }
            }
        }
    }

    class m_Method
    {
        public ArrayList link_set = new ArrayList();
        public int max = 0;

        public void m_DFS(Graph G, Word word, bool[] addAlpha, ArrayList live_list)
        {
            ArrayList next_node = G.getNextWordList(word);

            if (next_node == null)
            {
                if (live_list.Count > max)
                {
                    link_set.Clear();
                    for (int k = 0; k < live_list.Count; k++)
                    {
                        link_set.Add(live_list[k]);
                    }
                    max = live_list.Count;
                }
                return;
            }

            for (int j = 0; j < next_node.Count; j++)
            {
                Word word1 = (Word)next_node[j];
                bool[] tmp = new bool[26];
                for (int k = 0; k < 26; k++)
                {
                    tmp[k] = addAlpha[k];
                }
                if (tmp[word1.word_head - 'a'])
                {
                    if (live_list.Count > max)
                    {
                        link_set.Clear();
                        for (int k = 0; k < live_list.Count; k++)
                        {
                            link_set.Add(live_list[k]);
                        }
                        max = live_list.Count;
                    }
                }
                else
                {
                    tmp[word1.word_head - 'a'] = true;
                    ArrayList tmp1 = new ArrayList();
                    for (int k = 0; k < live_list.Count; k++)
                    {
                        tmp1.Add(live_list[k]);                       
                    }
                    tmp1.Add(word1.word);
                    m_DFS(G, word1, tmp, tmp1);
                }
            }
        }
        public void startDFS(Graph G)
        {
            for (int i = 0; i < Graph.word_list.Count; i++)
            {
                Word word = (Word)Graph.word_list[i];
                bool[] addAlpha = new bool[26];
                addAlpha[word.word_head - 'a'] = true;
                ArrayList live_list = new ArrayList();
                live_list.Add(word.word);
                m_DFS(G, word, addAlpha, live_list);
            }
        }
    }

    class n_Method
    {
        public int num = 0;
        public ArrayList link_set = new ArrayList();
        public n_Method() { }

        public void startDFS(Graph G)
        {
            //graphInitialize(G);
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
                ArrayList last_list = G.getLastNode(word);
                if (last_list == null || last_list.Count == 0)
                {
                    DFS(G, word, live_list, visited);
                }
            }
            return false;
        }
    }
}
