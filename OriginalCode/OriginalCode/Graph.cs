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
        public static ArrayList word_list = new ArrayList();
        public static Dictionary<char, ArrayList> start_list = new Dictionary<char, ArrayList>();
        public static Dictionary<char, ArrayList> end_list = new Dictionary<char, ArrayList>();

        public Graph() {}
        public ArrayList getNextWordList(Word w)
        {
            // Next边：节点n末尾字母e -> 以e开头的字母的集合
            ArrayList value = new ArrayList();
            start_list.TryGetValue(w.word_tail, out value);
            return value;
        }

        public ArrayList getLastNode(Word w)
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
    }
}
