using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Core
{
    public class RunCMD
    {
        public static void Main(string[] args)
        {
            string[] test = { "-n", "aaa.txt" };
            try
            {
                //string[] words = Program.input(test);
                string[] words = Program.input(args);
                string[] result = new string[20005];
                //Chain chain = new Chain();
                //foreach (string word in words) Console.WriteLine(word);

                switch (GlobalPara.type)
                {
                    case 'n':
                        //统计单词链数量 只传递单词链
                        Chain.gen_chains_all_str(words, words.Length, result);
                        break;
                    case 'm':
                        //输出首字母不相同的包含单词数量最多的单词链 只传递单词链
                        Chain.gen_chain_word_unique_str(words, words.Length, result);
                        break;
                    case 'w':
                        //需要传入Global的参数进行处理
                        Chain.gen_chain_word_str(words, words.Length, result,
                            GlobalPara.head, GlobalPara.tail, GlobalPara.is_loop);
                        break;
                    case 'c':
                        //需要传入Global的参数进行处理
                        Chain.gen_chain_char_str(words, words.Length, result,
                            GlobalPara.head, GlobalPara.tail, GlobalPara.is_loop);
                        break;
                }
                for (int i = 0; i < result.Length && result[i] != null && result.Length != 0; i++)
                {
                    Console.WriteLine(result[i]);
                }
            }
            catch (InvalidInputException ex)
            {
                Console.WriteLine("[get in main]");
                Console.WriteLine(ex.Message);
            }
            catch (CircleException ex)
            {
                Console.WriteLine("[get in main]");
                Console.WriteLine(ex.Message);
            }
            catch (ChainNotFoundException ex)
            {
                Console.WriteLine("[get in main]");
                Console.WriteLine(ex.Message);
            }

        }
    }
}
