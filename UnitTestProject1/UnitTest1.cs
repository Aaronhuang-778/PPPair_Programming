using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Core;
using System.IO;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            StreamReader reader = new StreamReader("./c_circle/result0.txt");
            string answer = reader.ReadToEnd();
            answer = answer.ToLower().Replace("\r\n", " ").Replace('\n', ' ');

            string[] test = { "-c", "-r", "./c_circle/test0.txt" };
            string result_str = "";

            try
            {
                string[] words = Program.input(test);
                string[] result = new string[20005];

                switch (GlobalPara.type)
                {
                    case 'n':
                        Chain.gen_chains_all_str(words, words.Length, result);
                        break;
                    case 'm':
                        Chain.gen_chain_word_unique_str(words, words.Length, result);
                        break;
                    case 'w':
                        Chain.gen_chain_word_str(words, words.Length, result,
                            GlobalPara.head, GlobalPara.tail, GlobalPara.is_loop);
                        break;
                    case 'c':
                        Chain.gen_chain_char_str(words, words.Length, result,
                            GlobalPara.head, GlobalPara.tail, GlobalPara.is_loop);
                        break;
                }
                for (int i = 0; i < result.Length && result[i] != null && result.Length != 0; i++)
                {
                    if (i != 0) result_str += ' ';
                    result_str += result[i];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Exception]");
                Console.WriteLine(ex.Message);
            }
            Assert.AreEqual(result_str, answer);
        }
    }
}
