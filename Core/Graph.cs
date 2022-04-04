using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Core
{
    public class Graph
    {
        private ArrayList word_list = new ArrayList();
        private Dictionary<char, ArrayList> start_list 
            = new Dictionary<char, ArrayList>();
        private Dictionary<char, ArrayList> end_list 
            = new Dictionary<char, ArrayList>();
        //权重边
        public int [,] adj = null;
        public int original_words_num = 0;

        public Graph() {
        }


        public ArrayList getWordList()
        {
            return word_list;
        }

        public ArrayList getNextWordList(Word w)
        {
            // Next边：节点n末尾字母e -> 以e开头的字母的集合
            if (!start_list.ContainsKey(w.word_tail)) return null;
            ArrayList value = new ArrayList();
            value = (ArrayList) start_list[w.word_tail].Clone();
            while (value != null && value.Contains(w)) value.Remove(w);
            return value;
        }

        public ArrayList getLastNodeList(Word w)
        {
            // Next边：节点n末尾字母e -> 以e开头的字母的集合
            if (!end_list.ContainsKey(w.word_head)) return null;
            ArrayList value = new ArrayList();
            value = (ArrayList) end_list[w.word_head].Clone();
            while (value != null && value.Contains(w)) value.Remove(w);
            return value;
        }

        public void generateE(string[] words)
        {
            adj = new int[words.Length, words.Length];
        }

        public void setE(int i, int j, int weight)
        {
            adj[i, j] = weight;
        }

        public void deletE(int i, int j)
        {
            adj[i, j] = 0;
        }

        public void AddG(string[] words)
        {
            generateE(words);
            for(int i = 0; i < words.Length; i++)
            {
                if (words[i] == null) break;
                if (words[i].Length > 1)
                {
                    Word new_word = new Word(words[i], i);
                    AddG(new_word);
                }
            }
            original_words_num = words.Length;
        }

        public void AddG(Word word)
        {
            
            //构造点
            word_list.Add(word);

            if (start_list.ContainsKey(word.word_head))
            {
                start_list[word.word_head].Add(word);   
            }
            else
            {
                ArrayList temp = new ArrayList();
                temp.Add(word);
                start_list[word.word_head] = temp;
            }
         
            if (end_list.ContainsKey(word.word_tail)) 
                end_list[word.word_tail].Add(word);   
            else
            {
                ArrayList temp = new ArrayList();
                temp.Add(word);
                end_list[word.word_tail] = temp;
            }
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
            //Console.WriteLine("no circle");
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

        public void TopologicalSort(ArrayList sortList)
        {
            bool[] visited = new bool[word_list.Count];
            for (int i = 0; i < word_list.Count; i++) {
                if (visited[i]) continue;
                Stack<Word> stack = new Stack<Word>();
                sortList.Add(stack);
                TopologicalSortUtil(i, visited, stack);
            }
        }

     
        public int longestPathDAG(ArrayList sortList, Stack<Word> result, char head, char tail)
        {
            Dictionary<Word, int> dist = new Dictionary<Word, int>();
            Dictionary<Word, Word> last_edge = new Dictionary<Word, Word>();
           
            int max_dist = 0;
            Word max_des = null;
            ArrayList headWords = null;
            ArrayList tailWords = null;
            if (Char.IsLetter(head)) 
            {
                if (!start_list.ContainsKey(head))
                    throw new ChainNotFoundException(ChainErrorType.code.head_not_found);
                headWords = start_list[head];
                if (headWords == null || headWords.Count <= 0)
                    throw new ChainNotFoundException(ChainErrorType.code.head_not_found);
                foreach (Word headword in headWords)
                    dist[headword] = headword.weight;
            }
            if (Char.IsLetter(tail)) 
            {
                if (!end_list.ContainsKey(tail))
                    throw new ChainNotFoundException(ChainErrorType.code.tail_not_found);
                tailWords = end_list[tail];
                if (tailWords == null || tailWords.Count <= 0)
                    throw new ChainNotFoundException(ChainErrorType.code.tail_not_found);
            }


            foreach (Stack<Word> stack in sortList)
            {
                if (headWords == null)
                    dist[stack.Peek()] = stack.Peek().weight;
                //if (max_des == null) max_des = w_peek;
                bool skip = true;
                foreach (Word w in stack)
                {
                    if (!dist.ContainsKey(w)) continue;
                    if (headWords != null && skip)
                    {
                        if (!headWords.Contains(w)) continue;
                        skip = false;
                    }
                    longestPathEach(w, ref max_des, ref max_dist, dist, last_edge);
                }
            }
            
            if (Char.IsLetter(tail)) 
            {
                max_dist = -1;
                foreach (Word tailWord in tailWords)
                {
                    if (dist.ContainsKey(tailWord) && dist[tailWord] > max_dist)
                    {
                        max_dist = dist[tailWord];
                        max_des = tailWord;
                    }
                }
                if (max_dist < 0) return -1;
            }
            else if (max_des == null) return -1;

            Word res_word = max_des;
            result.Clear();
            result.Push(res_word);
            while (last_edge.ContainsKey(res_word))
            {
                res_word = last_edge[res_word];
                result.Push(res_word);
                //Console.WriteLine(res_word);
            }
            if (Char.IsLetter(head) && res_word.word_head != head) return -1;
            return max_dist;
        }


        private void longestPathEach(Word w, ref Word max_des, ref int max_dist,
            Dictionary<Word, int> dist, Dictionary<Word, Word> last_edge)
        {
            ArrayList next_nodes = getNextWordList(w);
            if (next_nodes != null && next_nodes.Count > 0)
            {
                foreach (Word next_w in next_nodes)
                {
                    if (!dist.ContainsKey(next_w) || dist[next_w] < dist[w] + next_w.weight)
                    {
                        //Console.WriteLine("change " + w + "->" + next_w);
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
        }
    }

}
