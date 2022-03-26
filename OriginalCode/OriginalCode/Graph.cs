using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace OriginalCode
{
    public class Graph
    {
        private static ArrayList word_list = new ArrayList();
        private static Dictionary<char, ArrayList> start_list = new Dictionary<char, ArrayList>();
        private static Dictionary<char, ArrayList> end_list = new Dictionary<char, ArrayList>();

        public Graph() {}

        private ArrayList getNextWordList(Word w)
        {
            // Next边：节点n末尾字母e -> 以e开头的字母的集合
            ArrayList value = new ArrayList();
            start_list.TryGetValue(w.word_tail, out value);
            return value;
        }

        private ArrayList getLastNode(Word w)
        {
            // Next边：节点n末尾字母e -> 以e开头的字母的集合
            ArrayList value = new ArrayList();
            end_list.TryGetValue(w.word_head, out value);
            return value;
        }


        public void AddG(string[] words)
        {
            for(int i = 0; i < words.Length; i++)
            {
                Word new_word = new Word(words[i], i);
                AddG(new_word);
            }
        }

        public void AddG(Word word)
        {
            word_list.Add(word);
            ArrayList temp = new ArrayList();

            if (start_list.ContainsKey(word.word_head))
            {
                start_list.TryGetValue(word.word_head, out temp);   
            }
            temp.Add(word);
            start_list[word.word_head] = temp;

            ArrayList temp1 = new ArrayList();
            if (end_list.ContainsKey(word.word_tail))
            {
                end_list.TryGetValue(word.word_tail, out temp1);
            }
            temp1.Add(word);
            end_list[word.word_tail] = temp1;
        }

        private bool isCyclicUtil(int i, bool[] visited, bool[] recStack)
        {
            if (recStack[i]) return true;
            if (visited[i]) return false;
            visited[i] = true;
            recStack[i] = true;
            ArrayList next_node = getNextWordList((Word)word_list[i]);
            if (next_node != null && next_node.Count > 0)
                foreach (Word w in next_node)
                    if (isCyclicUtil(w.index, visited, recStack)) 
                        return true;
            recStack[i] = false;
            return false;
        }

        public bool isCyclic()
        {
            bool[] visited = new bool[word_list.Count];
            bool[] recStack = new bool[word_list.Count];

            for (int i = 0; i < word_list.Count; i++)
                if (isCyclicUtil(i, visited, recStack))
                    return true;
            Console.WriteLine("no circle");
            return false;
        }

        private void TopologicalSortUtil(int i, bool[] visited, Stack<Word> stack)
        {
            visited[i] = true;
            ArrayList next_node = getNextWordList((Word)word_list[i]);
            if (next_node != null && next_node.Count > 0)
            {
                foreach (Word w in next_node)
                {
                    if (visited[w.index]) continue;
                    TopologicalSortUtil(w.index, visited, stack);
                }
            }
            stack.Push((Word) word_list[i]);
        }

        public void TopologicalSort(Stack<Word> stack)
        {
            bool[] visited = new bool[word_list.Count];
            for (int i = 0; i < word_list.Count; i++) {
                if (visited[i]) continue;
                TopologicalSortUtil(i, visited, stack);
            }
        }

        public int longestPathDAG(Stack<Word> sortList, string[] result)
        {
            Dictionary<Word, int> dist = new Dictionary<Word, int>();
            Dictionary<Word, Word> last_edge = new Dictionary<Word, Word>();
            Word w = sortList.Pop();
           
            int max_dist = 0;
            Word max_des = w;

            while (sortList.Count > 0) {
                if (!dist.ContainsKey(w)) dist[w] = w.weight;
                ArrayList next_nodes = getNextWordList(w);

                if (next_nodes != null && next_nodes.Count > 0)
                {
                    foreach (Word next_w in next_nodes)
                    {
                        if (!dist.ContainsKey(next_w) || dist[next_w] < dist[w] + next_w.weight)
                        {
                            Console.WriteLine("change " + w + "->" + next_w);
                            dist[next_w] = dist[w] + next_w.weight;
                            last_edge[next_w] = w;
                            if (dist[next_w] > max_dist)
                            {
                                max_dist = dist[next_w];
                                max_des = next_w;
                            }
                        }
                    }
                }
                w = sortList.Pop();
            }

            Word word = max_des;
            result[0] = word.word;
            int k = 1;
            while (last_edge.ContainsKey(word))
            {
                result[k++] = word.word;
                word = last_edge[word];
            }
            return max_dist;
        }
    }
}
