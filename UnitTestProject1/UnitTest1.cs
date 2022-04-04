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
        public string getResult(int i, string testPath, string answer,
            char type, bool isR, char head, char tail)
        {
            try
            {
                string result_str = "";
                List<string> result = new List<string>();
                Chain.gen_for_gui_para(true, testPath, type, isR, head, tail, result);
                foreach (string res in result)
                {
                    if (result_str.Length > 0)
                    {
                        if (type == 'n') result_str += '\n';
                        else result_str += ' ';
                    }
                    result_str += res;
                }
                if (type == 'c') 
                    Console.WriteLine("[" + i.ToString() + "] result=" 
                        + result_str.Replace(" ", string.Empty).Length 
                        + " answer=" + answer.Replace(" ", string.Empty).Length);
                else
                    Console.WriteLine("[" + i.ToString() + "] result="
                        + result_str.Split(' ').Length
                        + " answer=" + answer.Split(' ').Length);
                if (type != 'n')
                {
                    Console.WriteLine("[" + i.ToString() + "] result = " + result_str);
                    Console.WriteLine("[" + i.ToString() + "] answer = " + answer);
                }
                return result_str;
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Exception]" + ex.Message);
                return "[" + i.ToString() + "]" + ex.Message;
            }
        }


        [TestMethod]
        public void testCR()
        {
            for (int i = 0; i < 15; i++)
            {
                string answerPath = "./c_circle/result" + i.ToString() + ".txt";
                string testPath = "./c_circle/test" + i.ToString() + ".txt";
                if (!File.Exists(answerPath)) break;
                StreamReader reader = new StreamReader(answerPath);
                string answer = reader.ReadToEnd();
                answer = answer.ToLower().Replace("\r\n", " ").Replace('\n', ' ');
                string result = getResult(i, testPath, answer, 'c', true, '0', '0');
                Assert.IsTrue(result.Replace(" ", string.Empty).Length
                           >= answer.Replace(" ", string.Empty).Length);
            }
        }

        [TestMethod]
        public void testCN()
        {
            for (int i = 0; i < 15; i++)
            {
                string answerPath = "./c_no_circle/result" + i.ToString() + ".txt";
                string testPath = "./c_no_circle/test" + i.ToString() + ".txt";
                if (!File.Exists(answerPath)) break;
                StreamReader reader = new StreamReader(answerPath);
                string answer = reader.ReadToEnd();
                answer = answer.ToLower().Replace("\r\n", " ").Replace('\n', ' ');
                string result = getResult(i, testPath, answer, 'c', false, '0', '0');
                Assert.IsTrue(result.Replace(" ", string.Empty).Length
                           >= answer.Replace(" ", string.Empty).Length);
            }
        }

        [TestMethod]
        public void testWR()
        {
            for (int i = 0; i < 15; i++)
            {
                string answerPath = "./w_circle/result" + i.ToString() + ".txt";
                string testPath = "./w_circle/test" + i.ToString() + ".txt";
                if (!File.Exists(answerPath)) break;
                StreamReader reader = new StreamReader(answerPath);
                string answer = reader.ReadToEnd();
                answer = answer.ToLower().Replace("\r\n", " ").Replace('\n', ' ');
                string result = getResult(i, testPath, answer, 'w', true, '0', '0');
                Console.WriteLine(result);

                string[] resArr = result.Split(' ');
                string[] ansArr = answer.Split(' ');
                //Assert.IsTrue((ansArr == null) ||
                //(resArr != null && resArr.Length >= ansArr.Length));
            }
        }

        [TestMethod]
        public void testWN()
        {
            for (int i = 0; i < 15; i++)
            {
                string answerPath = "./w_no_circle/result" + i.ToString() + ".txt";
                string testPath = "./w_no_circle/test" + i.ToString() + ".txt";
                if (!File.Exists(answerPath)) break;
                StreamReader reader = new StreamReader(answerPath);
                string answer = reader.ReadToEnd();
                answer = answer.ToLower().Replace("\r\n", " ").Replace('\n', ' ');
                string result = getResult(i, testPath, answer, 'w', false, '0', '0');
                string[] resArr = result.Split(' ');
                string[] ansArr = answer.Split(' ');
                Assert.IsTrue((ansArr == null) ||
                    (resArr != null && resArr.Length >= ansArr.Length));
            }
        }


        [TestMethod]
        public void testN()
        {
            for (int i = 0; i < 15; i++)
            {
                string answerPath = "./n_no_circle/result" + i.ToString() + ".txt";
                string testPath = "./n_no_circle/test" + i.ToString() + ".txt";
                if (!File.Exists(answerPath)) break;
                StreamReader reader = new StreamReader(answerPath);
                string answer = reader.ReadToEnd();
                answer = answer.ToLower().Replace("\r\n", "\n");
                string result = getResult(i, testPath, answer, 'n', false, '0', '0');
                result = result.ToLower().Replace("\r\n", "\n");

                string[] resArr = result.Split('\n');
                string[] ansArr = answer.Split('\n');

                if (ansArr != null && ansArr.Length >= 1)
                {
                    Console.WriteLine("[" + i.ToString() + "] res=" +
                        (resArr.Length - 1).ToString() + " ans=" + ansArr.Length.ToString());
                    Assert.IsTrue(resArr != null && resArr.Length > 1);
                    Assert.IsTrue(Int32.TryParse(resArr[0], out int resNum));
                    Assert.IsTrue(resNum >= ansArr.Length);
                }
            }
        }

        [TestMethod]
        public void testM()
        {
            for (int i = 0; i < 15; i++)
            {
                string answerPath = "./m_no_circle/result" + i.ToString() + ".txt";
                string testPath = "./m_no_circle/test" + i.ToString() + ".txt";
                if (!File.Exists(answerPath)) break;
                StreamReader reader = new StreamReader(answerPath);
                string answer = reader.ReadToEnd();
                answer = answer.ToLower().Replace("\r\n", " ").Replace('\n', ' ');
                string result = getResult(i, testPath, answer, 'm', false, '0', '0');
                string[] resArr = result.Split(' ');
                string[] ansArr = answer.Split(' ');
                Assert.IsTrue((ansArr == null) ||
                    (resArr != null && resArr.Length >= ansArr.Length));
            }
        }
    }
}
