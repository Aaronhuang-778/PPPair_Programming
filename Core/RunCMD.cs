using System;
using System.Collections.Generic;
using System.IO;

namespace Core
{
    
    public class RunCMD
    {
        public static void Main(string[] args)
        {
            try
            {
                InputPara ip = new InputPara();
                List<string> words = Program.input(args, out ip);
                List<string> result = new List<string>();
                switch (ip.type)
                {
                    case 'n':
                        //统计单词链数量 只传递单词链
                        Chain.gen_chains_all(words, words.Count, result);
                        break;
                    case 'm':
                        //输出首字母不相同的包含单词数量最多的单词链 只传递单词链
                        Chain.gen_chain_word_unique(words, words.Count, result);
                        break;
                    case 'w':
                        //需要传入Global的参数进行处理
                        Chain.gen_chain_word(words, words.Count, result,
                            ip.head, ip.tail, ip.is_loop);
                        break;
                    case 'c':
                        //需要传入Global的参数进行处理
                        Chain.gen_chain_char(words, words.Count, result,
                            ip.head, ip.tail, ip.is_loop);
                        break;
                }
                foreach (string res in result)
                    Console.WriteLine(res);
            }
            catch (InvalidInputException ex)
            {
                Console.WriteLine("[Exception in main] " + ex.Message);
            }
            catch (CircleException ex)
            {
                Console.WriteLine("[Exception in main] " + ex.Message);
            }
            catch (ChainNotFoundException ex)
            {
                Console.WriteLine("[Exception in main] " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Exception in main] " + ex.Message);
            }
            //testPerformance();
        }

        public static void testPerformance()
        {
            for (int i = 0; i < 15; i++)
            {
                string path = "./n_no_circle/test" + i.ToString() + ".txt";
                if (!File.Exists(path)) break;
                try
                {
                    Chain.gen_for_gui_para(true, path, 'n', false, '0', '0', new List<string>());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[Exception in main] " + ex.Message);
                }
            }
            for (int i = 0; i < 15; i++)
            {
                string path = "./m_no_circle/test" + i.ToString() + ".txt";
                if (!File.Exists(path)) break;
                try
                {
                    Chain.gen_for_gui_para(true, path, 'm', false, '0', '0', new List<string>());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[Exception in main] " + ex.Message);
                }
            }
            for (int i = 0; i < 15; i++)
            {
                string path = "./w_no_circle/test" + i.ToString() + ".txt";
                if (!File.Exists(path)) break;
                try
                {
                    Chain.gen_for_gui_para(true, path, 'w', false, '0', '0', new List<string>());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[Exception in main] " + ex.Message);
                }
            }
            for (int i = 0; i < 15; i++)
            {
                string path = "./w_circle/test" + i.ToString() + ".txt";
                if (!File.Exists(path)) break;
                try
                {
                    Chain.gen_for_gui_para(true, path, 'w', true, '0', '0', new List<string>());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[Exception in main] " + ex.Message);
                }
            }
            for (int i = 0; i < 15; i++)
            {
                string path = "./c_no_circle/test" + i.ToString() + ".txt";
                if (!File.Exists(path)) break;
                try
                {
                    Chain.gen_for_gui_para(true, path, 'c', false, '0', '0', new List<string>());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[Exception in main] " + ex.Message);
                }
            }
            for (int i = 0; i < 15; i++)
            {
                string path = "./c_circle/test" + i.ToString() + ".txt";
                if (!File.Exists(path)) break;
                try
                {
                    Chain.gen_for_gui_para(true, path, 'c', true, '0', '0', new List<string>());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[Exception in main] " + ex.Message);
                }
            }
        }
    }
}
