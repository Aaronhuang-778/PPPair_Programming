using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Core;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void testCR()
        {
            for (int i = 0; i < 10; i++)
            {
                string answerPath = "./c_circle/result" + i.ToString() + ".txt";
                string testPath = "./c_circle/test" + i.ToString() + ".txt";
                if (!File.Exists(answerPath)) break;
                StreamReader reader = new StreamReader(answerPath);
                string answer = reader.ReadToEnd();
                answer = answer.ToLower().Replace("\r\n", " ").Replace('\n', ' ');
                string result_str = "";

                try
                {
                    List<string> result = new List<string>();
                    Chain.gen_for_gui_para(true, testPath, 'c', true, '0', '0', result);
                    foreach (string res in result)
                    {
                        if (result_str.Length > 0) result_str += ' ';
                        result_str += res;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[Exception]");
                    Console.WriteLine(ex.Message);
                }
                Console.WriteLine("[" + i.ToString() + "] result=" +
                    result_str.Replace(" ", string.Empty).Length +
                    " answer=" + answer.Replace(" ", string.Empty).Length);
                Console.WriteLine("[" + i.ToString() + "] result = " + result_str);
                Console.WriteLine("[" + i.ToString() + "] answer = " + answer);
                Assert.IsTrue(result_str.Replace(" ", string.Empty).Length
                    >= answer.Replace(" ", string.Empty).Length);
            }
        }

        [TestMethod]
        public void testC()
        {
            for (int i = 0; i < 10; i++)
            {
                string answerPath = "./c_no_circle/result" + i.ToString() + ".txt";
                string testPath = "./c_no_circle/test" + i.ToString() + ".txt";
                if (!File.Exists(answerPath)) break;
                StreamReader reader = new StreamReader(answerPath);
                string answer = reader.ReadToEnd();
                answer = answer.ToLower().Replace("\r\n", " ").Replace('\n', ' ');
                string result_str = "";

                try
                {
                    List<string> result = new List<string>();
                    Chain.gen_for_gui_para(true, testPath, 'c', true, '0', '0', result);
                    foreach (string res in result)
                    {
                        if (result_str.Length > 0) result_str += ' ';
                        result_str += res;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[Exception]");
                    Console.WriteLine(ex.Message);
                }
                Console.WriteLine("[" + i.ToString() + "] result=" +
                    result_str.Replace(" ", string.Empty).Length + 
                    " answer=" + answer.Replace(" ", string.Empty).Length);
                Console.WriteLine("[" + i.ToString() + "] result = " + result_str);
                Console.WriteLine("[" + i.ToString() + "] answer = " + answer);
                Assert.IsTrue(result_str.Replace(" ", string.Empty).Length 
                    >= answer.Replace(" ", string.Empty).Length);
            }
        }
    }
}
