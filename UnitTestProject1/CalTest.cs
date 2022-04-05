using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Core;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestClass]
    public class CalTest
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
        public void testWHT()
        {
            string testPath = "./w_no_circle/test0.txt";
            List<string> result = new List<string>();
            List<string> answer = new List<string>();
            string last;
            answer.Add("outsmarts");
            answer.Add("stampage");
            
            Chain.gen_for_gui_para(true, testPath, 'w', false, 'o', '0', result);
            Console.WriteLine("[1] -w -h o");
            foreach (string s in result) Console.WriteLine(s);
            Assert.IsTrue(answer.Count == result.Count && result[0][0] == 'o');
            
            result.Clear();
            Chain.gen_for_gui_para(true, testPath, 'w', false, '0', 'e', result);
            Console.WriteLine("[2] -w -t e");
            foreach (string s in result) Console.WriteLine(s);
            Assert.IsTrue(answer.Count == result.Count);
            last = result[result.Count - 1];
            Assert.AreEqual('e', last[last.Length - 1]);

            result.Clear();
            Chain.gen_for_gui_para(true, testPath, 'w', false, 'o', 'e', result);
            Console.WriteLine("[3] -w -h o -t e");
            foreach (string s in result) Console.WriteLine(s);
            Assert.IsTrue(answer.Count == result.Count);
            last = result[result.Count - 1];
            Assert.IsTrue(result[0][0] == 'o' && last[last.Length - 1] == 'e');
        }

        [TestMethod]
        public void testWRHT()
        {
            string testPath = "./w_circle/test0.txt";
            List<string> result = new List<string>();
            string last;

            Chain.gen_for_gui_para(true, testPath, 'w', true, 'c', '0', result);
            Console.WriteLine("[1] -w -r -h c");
            foreach (string s in result) Console.WriteLine(s);
            Assert.IsTrue(result.Count == 7 && result[0][0] == 'c');

            result.Clear();
            Chain.gen_for_gui_para(true, testPath, 'w', true, '0', 'd', result);
            Console.WriteLine("[2] -w -r -t d");
            foreach (string s in result) Console.WriteLine(s);
            Assert.IsTrue(result.Count == 6);
            last = result[result.Count - 1];
            Assert.AreEqual('d', last[last.Length - 1]);

            result.Clear();
            Chain.gen_for_gui_para(true, testPath, 'w', true, 'c', 'd', result);
            Console.WriteLine("[3] -w -r -h c -t d");
            foreach (string s in result) Console.WriteLine(s);
            Assert.IsTrue(result.Count == 6);
            last = result[result.Count - 1];
            Assert.IsTrue(result[0][0] == 'c' && last[last.Length - 1] == 'd');
        }


        [TestMethod]
        public void testCRHT()
        {
            string testPath = "./c_circle/test0.txt";
            List<string> result = new List<string>();
            string last;

            Chain.gen_for_gui_para(true, testPath, 'c', true, 'p', '0', result);
            Console.WriteLine("[1] -c -r -h p");
            foreach (string s in result) Console.WriteLine(s);
            Assert.IsTrue(result.Count == 6 && result[0][0] == 'p');

            result.Clear();
            Chain.gen_for_gui_para(true, testPath, 'c', true, '0', 's', result);
            Console.WriteLine("[2] -c -r -t s");
            foreach (string s in result) Console.WriteLine(s);
            Assert.IsTrue(result.Count == 5);
            last = result[result.Count - 1];
            Assert.AreEqual('s', last[last.Length - 1]);

            result.Clear();
            Chain.gen_for_gui_para(true, testPath, 'c', true, 'p', 's', result);
            Console.WriteLine("[3] -c -r -h p -t s");
            foreach (string s in result) Console.WriteLine(s);
            Assert.IsTrue(result.Count == 5);
            last = result[result.Count - 1];
            Assert.IsTrue(result[0][0] == 'p' && last[last.Length - 1] == 's');
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

                if (ansArr != null && ansArr.Length >= 1 && ansArr[0].Length != 0)
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

        [TestMethod]
        public void testNeighbor()
        {
            List<string> result = new List<string>();
            try
            {
                Chain.gen_for_gui_para(true, "w_no_circle/neighbor.txt", 'w', false, 'w', '0', result);
                Assert.Fail("not catch exception");
            }
            catch (ChainNotFoundException) { }
            catch (Exception)
            {
                Assert.Fail("wrong type of excetion");
            }

            result.Clear();
            try
            {
                Chain.gen_for_gui_para(true, "w_no_circle/neighbor.txt", 'w', false, '0', 'g', result);
                Assert.Fail("not catch exception");
            }
            catch (ChainNotFoundException) { }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Assert.Fail("wrong type of excetion");
            }

            result.Clear();
            try
            {
                Chain.gen_for_gui_para(true, "w_no_circle/neighbor.txt", 'w', false, '0', 's', result);
                Assert.Fail("not catch exception");
            }
            catch (ChainNotFoundException) { }
            catch (Exception)
            {
                Assert.Fail("wrong type of excetion");
            }
        }
    }
}
