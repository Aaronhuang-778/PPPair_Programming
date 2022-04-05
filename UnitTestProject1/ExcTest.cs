using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Core;
using System.IO;
using System.Collections;
using System.Collections.Generic;


namespace UnitTestProject
{

    [TestClass]
    public class ExcTest
    {
        [TestMethod]
        public void testInput()
        {
            for (int i = 0; i <= 12; i++)
            {
                try
                {
                    gen_error_input((InputErrorType.code) i);
                    Assert.Fail("not catch exception");
                }
                catch (InvalidInputException ex)
                {
                    Assert.AreEqual((InputErrorType.code)i, ex.code);
                }
                catch (Exception)
                {
                    Assert.Fail("wrong type of excetion");
                }
            }


            try
            {
                string[] args = new string[] { "-w", "aaa.txt", "bbb.txt" };
                Program.input(args, out InputPara para);
                Assert.Fail("not catch exception");
            }
            catch (InvalidInputException ex)
            {
                Assert.AreEqual(InputErrorType.code.wrong_format, ex.code);
            }
            catch (Exception)
            {
                Assert.Fail("wrong type of excetion");
            }

            try
            {
                string[] args = new string[] { "-w", ".txt" };
                Program.input(args, out InputPara para);
                Assert.Fail("not catch exception");
            }
            catch (InvalidInputException ex)
            {
                Assert.AreEqual(InputErrorType.code.wrong_format, ex.code);
            }
            catch (Exception)
            {
                Assert.Fail("wrong type of excetion");
            }
        }

        [TestMethod]
        public void testWrongHT()
        {
            try
            {
                string[] args = new string[] { "-w", "aaa.txt", "-h" };
                Program.input(args, out InputPara para);
                Assert.Fail("not catch exception");
            }
            catch (InvalidInputException ex)
            {
                Assert.AreEqual(InputErrorType.code.wrong_head, ex.code);
            }
            catch (Exception)
            {
                Assert.Fail("wrong type of excetion");
            }

            try
            {
                string[] args = new string[] { "-w", "aaa.txt", "-h", " " };
                Program.input(args, out InputPara para);
                Assert.Fail("not catch exception");
            }
            catch (InvalidInputException ex)
            {
                Assert.AreEqual(InputErrorType.code.wrong_head, ex.code);
            }
            catch (Exception)
            {
                Assert.Fail("wrong type of excetion");
            }

            try
            {
                string[] args = new string[] { "-w", "aaa.txt", "-h", "~" };
                Program.input(args, out InputPara para);
                Assert.Fail("not catch exception");
            }
            catch (InvalidInputException ex)
            {
                Assert.AreEqual(InputErrorType.code.wrong_head, ex.code);
            }
            catch (Exception)
            {
                Assert.Fail("wrong type of excetion");
            }

            try
            {
                string[] args = new string[] { "-w", "aaa.txt", "-t" };
                Program.input(args, out InputPara para);
                Assert.Fail("not catch exception");
            }
            catch (InvalidInputException ex)
            {
                Assert.AreEqual(InputErrorType.code.wrong_tail, ex.code);
            }
            catch (Exception)
            {
                Assert.Fail("wrong type of excetion");
            }

            try
            {
                string[] args = new string[] { "-w", "aaa.txt", "-t", " " };
                Program.input(args, out InputPara para);
                Assert.Fail("not catch exception");
            }
            catch (InvalidInputException ex)
            {
                Assert.AreEqual(InputErrorType.code.wrong_tail, ex.code);
            }
            catch (Exception)
            {
                Assert.Fail("wrong type of excetion");
            }

            try
            {
                string[] args = new string[] { "-w", "aaa.txt", "-t", "~" };
                Program.input(args, out InputPara para);
                Assert.Fail("not catch exception");
            }
            catch (InvalidInputException ex)
            {
                Assert.AreEqual(InputErrorType.code.wrong_tail, ex.code);
            }
            catch (Exception)
            {
                Assert.Fail("wrong type of excetion");
            }
        }

        [TestMethod]
        public void testDupli()
        {
            foreach (string c in new string[] { "-c", "-w", "-m", "-n", "-r"})
                try
                {
                    string[] args = new string[] { c, c, "aaa.txt" };
                    Program.input(args, out InputPara para);
                    Assert.Fail("not catch exception");
                }
                catch (InvalidInputException ex)
                {
                    Assert.AreEqual(InputErrorType.code.dupli_para, ex.code);
                }
                catch (Exception)
                {
                    Assert.Fail("wrong type of excetion");
                }

            foreach (string c in new string[] { "-h", "-t"})
                try
                {
                    string[] args = new string[] { c, "a", c, "a", "aaa.txt" };
                    Program.input(args, out InputPara para);
                    Assert.Fail("not catch exception");
                }
                catch (InvalidInputException ex)
                {
                    Assert.AreEqual(InputErrorType.code.dupli_para, ex.code);
                }
                catch (Exception)
                {
                    Assert.Fail("wrong type of excetion");
                }
        }

        private void gen_error_input(InputErrorType.code code)
        {
            string[] args = null;
            InputPara para = null;
            switch (code)
            {
                case InputErrorType.code.dupli_para:
                    args = new string[] { "-w", "-w", "aaa.txt" };
                    Program.input(args, out para);
                    break;
                case InputErrorType.code.not_support:
                    args = new string[] { "-w", "-a", "aaa.txt" };
                    Program.input(args, out para);
                    break;
                case InputErrorType.code.wrong_format:
                    args = new string[] { "-" };
                    Program.input(args, out para);
                    break;
                case InputErrorType.code.wrong_head:
                    args = new string[] { "-w", "-h", "aaa.txt" };
                    Program.input(args, out para);
                    break;
                case InputErrorType.code.wrong_tail:
                    args = new string[] { "-w", "-t", "aaa.txt" };
                    Program.input(args, out para);
                    break;
                case InputErrorType.code.illegal_path:
                    args = new string[] { "-w", "a<.txt" };
                    Program.input(args, out para);
                    break;
                case InputErrorType.code.illegal_file_type:
                    args = new string[] { "-w", "aaa.xml" };
                    Program.input(args, out para);
                    break;
                case InputErrorType.code.file_not_found:
                    args = new string[] { "-w", "not_exist_file.txt" };
                    Program.input(args, out para);
                    break;
                case InputErrorType.code.illegal_para_combination:
                    args = new string[] { "-m", "-r", "aaa.txt" };
                    Program.input(args, out para);
                    break;
                case InputErrorType.code.no_filename:
                    args = new string[] { "-w" };
                    Program.input(args, out para);
                    break;
                case InputErrorType.code.no_input_text:
                    Chain.gen_for_gui_para(false, "", '0', false, '0', '0', null);
                    break;
                case InputErrorType.code.no_check_mode:
                    args = new string[] { "aaa.txt" };
                    Program.input(args, out para);
                    break;
                case InputErrorType.code.empty_string:
                    args = new string[] { "-w", "empty.txt" };
                    Program.input(args, out para);
                    break;
            }
        }

        [TestMethod]
        public void testCal()
        {
            tryGen(true, "c_circle/test0.txt", 'c', true, 'b', '0');
            tryGen(true, "c_circle/test0.txt", 'c', true, 'u', '0');
            tryGen(true, "c_circle/test0.txt", 'c', true, '0', 'b');
            tryGen(true, "c_circle/test0.txt", 'c', true, '0', 'l');
            tryGen(true, "c_circle/test0.txt", 'c', true, 's', 'b');

            tryGen(true, "c_no_circle/test0.txt", 'c', false, 'j', '0');
            tryGen(true, "c_no_circle/test0.txt", 'c', false, 'e', '0');
            tryGen(true, "c_no_circle/test0.txt", 'c', false, '0', 'b');
            tryGen(true, "c_no_circle/test0.txt", 'c', false, '0', 'n');
            tryGen(true, "c_no_circle/test0.txt", 'c', false, 'e', 'n');
            tryGen(true, "c_no_circle/test0.txt", 'c', false, 's', 'n');
            tryGen(true, "c_no_circle/test0.txt", 'c', false, 'j', 's');


            tryGen(true, "w_circle/test0.txt", 'w', true, 'b', '0');
            tryGen(true, "w_circle/test0.txt", 'w', true, 'f', '0');
            tryGen(true, "w_circle/test0.txt", 'w', true, '0', 'b');
            tryGen(true, "w_circle/test0.txt", 'w', true, '0', 'e');
            tryGen(true, "w_circle/test0.txt", 'w', true, 's', 'b');

            tryGen(true, "w_no_circle/test0.txt", 'w', false, 'a', '0');
            tryGen(true, "w_no_circle/test0.txt", 'w', false, 'm', '0');
            tryGen(true, "w_no_circle/test0.txt", 'w', false, '0', 'a');
            tryGen(true, "w_no_circle/test0.txt", 'w', false, '0', 'g');
            tryGen(true, "w_no_circle/test0.txt", 'w', false, 'o', 'g');
            tryGen(true, "w_no_circle/test0.txt", 'w', false, 's', 'g');
            tryGen(true, "w_no_circle/test0.txt", 'w', false, 'o', 's');

        }



        [TestMethod]
        public void testCircle()
        {
            foreach (char c in new char[] { 'c', 'w', 'm', 'n'} )
                try
                {
                    Chain.gen_for_gui_para(true, "c_circle/test0.txt", c, false, '0', '0', new List<string>());
                    Assert.Fail("not catch exception");
                }
                catch (CircleException) {}
                catch (Exception)
                {
                    Assert.Fail("wrong type of excetion");
                }
            Chain.gen_for_gui_para(true, "c_circle/test0.txt", 'c', true, 'a', '0', new List<string>());
            Chain.gen_for_gui_para(true, "w_circle/test0.txt", 'w', true, 'a', '0', new List<string>());
        }

        [TestMethod]
        public void testGUI()
        {
            try
            {
                Chain.gen_for_gui_para(false, null, 'c', false, '0', '0', new List<string>());
                Assert.Fail("not catch exception");
            }
            catch (Exception) { }
            try
            {
                Chain.gen_for_gui_para(false, "", 'c', false, '0', '0', new List<string>());
                Assert.Fail("not catch exception");
            }
            catch (Exception) { }
            try
            {

                Chain.gen_for_gui_para(false, "\r\n", 'c', false, '0', '0', new List<string>());
                Assert.Fail("not catch exception");
            }
            catch (Exception) { }
            try
            {

                Chain.gen_for_gui_para(false, "\r\nab", 'c', false, '0', '0', new List<string>());
                Assert.Fail("not catch exception");
            }
            catch (Exception) { }
        }

        private void tryGen(bool useFileInput, string inputSource, 
            char calType, bool isR, char charH, char charT)
        {
            try
            {
                List<string> result = new List<string>();
                Chain.gen_for_gui_para(useFileInput, inputSource, calType, isR, charH, charT, result);
                Console.WriteLine("get: " + inputSource + " -" + calType + " -h " + charH + " -t " + charT);
                foreach (string item in result) Console.WriteLine(item);
                Assert.Fail("not catch exception");
            }
            catch (ChainNotFoundException ex)
            {
                StringAssert.Contains(ex.Message, "chain");
            }
            catch (Exception)
            {
                Assert.Fail("wrong type of excetion");
            }
        }
    }
}
