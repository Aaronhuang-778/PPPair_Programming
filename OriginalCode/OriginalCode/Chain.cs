using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace OriginCode
{
    public static class Chain
    {
        public static int gen_for_gui_para(bool useFileInput, string inputSource,
            char calType, bool isR, char charH, char charT, List<string> result)
        {
            List<string> words = new List<string>();

            InputPara ip = new InputPara();

            if (useFileInput)
            {
                string[] test = { "-" + calType, inputSource };
                words = Program.input(test, out ip);
            }
            else
            {
                ip.head = charH;
                ip.tail = charT;
                ip.is_loop = isR;
                ip.type = calType;
                if (inputSource == null || inputSource.Length == 0)
                    throw new InvalidInputException(InputErrorType.code.no_input_text);
                inputSource = inputSource.ToLower();
                string[] wordsArr = Regex.Split(inputSource, "[^(a-zA-Z)]+");
                words = new List<string>(wordsArr);
                if (words[0] == "") words.RemoveAt(0);
            }

            switch (ip.type)
            {
                case 'n':
                    //统计单词链数量 只传递单词链
                    gen_chains_all(words, words.Count, result);
                    break;
                case 'm':
                    //输出首字母不相同的包含单词数量最多的单词链 只传递单词链
                    gen_chain_word_unique(words, words.Count, result);
                    break;
                case 'w':
                    //需要传入Global的参数进行处理
                    gen_chain_word(words, words.Count, result, charH, charT, isR);
                    break;
                case 'c':
                    //需要传入Global的参数进行处理
                    gen_chain_char(words, words.Count, result, charH, charT, isR);
                    break;
            }
            return 0;
        }

        public static void writeFile(List<string> result)
        {
            string str = System.IO.Directory.GetCurrentDirectory();
            str = str + "\\solution.txt";
            string[] resArray = result.ToArray();
            System.IO.File.WriteAllLines(str, resArray);
        }


        /* -w -h -t -r */
        public static int gen_chain_word(List<string> words, int len, List<string> result,
            char head, char tail, bool enable_loop)
        {
            //if (result == null) result = new List<string>();
            //else result.Clear();
            Word.type = 'w';

            Graph G = new Graph();
            G.AddG(words);

            if (G.isCyclic())
            {
                if (!enable_loop) throw new CircleException();

                ChainAlgorithm ca = new ChainAlgorithm();
                ArrayList tmp = ca.get_RW(G, head, tail);

                if (tmp == null || tmp.Count == 0)
                    throw new ChainNotFoundException();

                foreach (string str in tmp)
                    result.Add(str);

                writeFile(result);
                return 0;
            }

            Stack<Stack<Word>> sortList = new Stack<Stack<Word>>();
            G.TopologicalSort(sortList);

            Stack<Word> res_stack = new Stack<Word>();
            int dist = G.longestPathDAG(sortList, res_stack, head, tail);
            if (dist < 0) throw new ChainNotFoundException();

            foreach (Word w in res_stack)
                result.Add(w.word);

            writeFile(result);
            return 0;
        }


        /* -n */
        public static int gen_chains_all(List<string> words, int len, List<string> result)
        {
            //if (result == null) result = new List<string>();
            //else result.Clear();

            Word.type = 'n';

            Graph G = new Graph();
            G.AddG(words);
            if (G.isCyclic())
                throw new CircleException();

            ChainAlgorithm ca = new ChainAlgorithm();
            ArrayList tmp = ca.get_chains_all(G);
            if (tmp == null || tmp.Count == 0)
                throw new ChainNotFoundException();

            result.Add(tmp.Count.ToString());
            foreach (string word in tmp)
                result.Add(word);

            return 0;
        }


        /* -m */
        public static int gen_chain_word_unique(List<string> words, int len, List<string> result)
        {
            //if (result == null) result = new List<string>();
            //else result.Clear();
            Word.type = 'm';

            Graph G = new Graph();
            G.AddG(words);
            if (G.isCyclic())
                throw new CircleException();
            else Console.WriteLine("no circle");

            ChainAlgorithm ca = new ChainAlgorithm();
            ArrayList tmp = ca.get_chain_word_unique(G);
            if (tmp == null || tmp.Count == 0)
                throw new ChainNotFoundException();

            foreach (string word in tmp)
                result.Add(word);

            writeFile(result);
            return 0;
        }


        /* -c -h -t -r */
        public static int gen_chain_char(List<string> words, int len, List<string> result,
            char head, char tail, bool enable_loop)
        {
            //if (result == null) result = new List<string>();
            //else result.Clear();
            Word.type = 'c';

            Graph G = new Graph();
            G.AddG(words);

            if (G.isCyclic())
            {
                if (!enable_loop) throw new CircleException();

                ChainAlgorithm ca = new ChainAlgorithm();
                ArrayList tmp = ca.get_RC(G, head, tail);

                if (tmp == null || tmp.Count == 0)
                    throw new ChainNotFoundException();

                foreach (string word in tmp)
                    result.Add(word);

                writeFile(result);
                return 0;
            }

            Stack<Stack<Word>> sortList = new Stack<Stack<Word>>();
            G.TopologicalSort(sortList);

            Stack<Word> res_stack = new Stack<Word>();
            int dist = G.longestPathDAG(sortList, res_stack, head, tail);
            if (dist < 0) throw new ChainNotFoundException();

            foreach (Word w in res_stack)
                result.Add(w.word);

            writeFile(result);
            return 0;
        }
    }
}
