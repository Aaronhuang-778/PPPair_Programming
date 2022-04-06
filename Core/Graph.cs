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

        public Word words_list
        {
            get => default;
            set
            {
            }
        }

        public ArrayList getWordList()
        {
            return word_list;
        }

        public ArrayList getNextWordList(Word w)
        {
            // Next边：节点n末尾字母e -> 以e开头的字母的集合
            if (!start_list.ContainsKey(w.word_tail)) return null;

            ArrayList value = (ArrayList) start_list[w.word_tail].Clone();
            while (value.Count > 0 && value.Contains(w)) value.Remove(w);
            return value;
        }

        public ArrayList getLastNodeList(Word w)
        {
            // Next边：节点n末尾字母e -> 以e开头的字母的集合
            if (!end_list.ContainsKey(w.word_head)) return null;
            ArrayList value = new ArrayList();
            value = (ArrayList) end_list[w.word_head].Clone();
            while (value.Count > 0 && value.Contains(w)) value.Remove(w);
            return value;
        }

        public void generateE(List<string> words)
        {
            adj = new int[words.Count, words.Count];
        }

        public void setE(int i, int j, int weight)
        {
            adj[i, j] = weight;
        }

        public void deletE(int i, int j)
        {
            adj[i, j] = 0;
        }

        public void AddG(List<string> words)
        {
            generateE(words);
            int i = 0;
            foreach (string word in words)
            {
                if (word.Length < 2) continue;
                Word new_word = new Word(word, i);
                AddG(new_word);
                i++;
            }
            original_words_num = words.Count;
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

        public void TopologicalSort(Stack<Stack<Word>> sortList)
        {
            bool[] visited = new bool[word_list.Count];
            for (int i = 0; i < word_list.Count; i++) {
                if (visited[i]) continue;
                Stack<Word> stack = new Stack<Word>();
                sortList.Push(stack);
                TopologicalSortUtil(i, visited, stack);
            }
        }

     
        public int longestPathDAG(Stack<Stack<Word>> sortList, 
            Stack<Word> result, char head, char tail)
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
                    throw new ChainNotFoundException();
                headWords = start_list[head];
                foreach (Word headword in headWords)
                    dist[headword] = headword.weight;
            }
            if (Char.IsLetter(tail)) 
            {
                if (!end_list.ContainsKey(tail))
                    throw new ChainNotFoundException();
                tailWords = end_list[tail];
            }


            foreach (Stack<Word> stack in sortList)
            {
                if (headWords == null)
                {
                    Word peek = stack.Peek();
                    if (!dist.ContainsKey(peek)) 
                        dist[peek] = peek.weight;
                    //Console.WriteLine("Peek: " + peek.word + " " + peek.weight.ToString());
                }
                //if (max_des == null) max_des = w_peek;
                bool skip = true;
                foreach (Word w in stack)
                {
                    //Console.WriteLine("node: " + w.word);
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
            }

            if (max_dist <= 0 || max_des == null)
                throw new ChainNotFoundException();

            Word res_word = max_des;
            result.Clear();
            result.Push(res_word);
            while (last_edge.ContainsKey(res_word))
            {
                res_word = last_edge[res_word];
                result.Push(res_word);
                //Console.WriteLine(res_word);
            }
            if (result.Count < 2)
                    throw new ChainNotFoundException();
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
                        dist[next_w] = dist[w] + next_w.weight;
                        //Console.WriteLine("change " + w + "->" +
                        //next_w + " " + dist[next_w].ToString());
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
