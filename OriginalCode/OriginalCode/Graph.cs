using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace OriginalCode
{
    public static class Graph
    {
        public static ArrayList word_list = new ArrayList();
        public static Dictionary<char, ArrayList> start_list = new Dictionary<char, ArrayList>();
        public static Dictionary<char, ArrayList> end_list = new Dictionary<char, ArrayList>();

        public static void AddG(Word word)
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
    }
}
