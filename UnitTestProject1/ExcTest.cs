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
            try
            {
                Chain.gen_for_gui_para(false, "", 'w', false, '0', '0', result);
                Assert.Fail("not catch exception");
            }
            catch (InvalidInputException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
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
                    break;
                case InputErrorType.code.not_support:
                    args = new string[] { "-"};
                    break;
                case InputErrorType.code.wrong_format:
                    args = new string[] { "-" };
                    break;
                case InputErrorType.code.wrong_head:
                    args = new string[] { "-w", "-h", "aaa.txt" };
                    break;
                case InputErrorType.code.wrong_tail:
                    args = new string[] { "-w", "-t", "aaa.txt" };
                    break;
                case InputErrorType.code.illegal_path:
                    args = new string[] { "-w", "./not_exist_folder/a.txt" };
                    break;
                case InputErrorType.code.illegal_file_type:
                    args = new string[] { "-w", "aaa.xml" };
                    break;
                case InputErrorType.code.file_not_found:
                    args = new string[] { "-w", "not_exist_file.txt" };
                    break;
                case InputErrorType.code.illegal_para_combination:
                    args = new string[] { "-w", "-c", "aaa.txt" };
                    break;
                case InputErrorType.code.no_filename:
                    args = new string[] { "-w" };
                    break;
                case InputErrorType.code.no_input_text:
                    args = new string[] { "-w", "empty.txt" };
                    break;
                case InputErrorType.code.no_check_mode:
                    args = new string[] { "aaa.txt" };
                    break;
                case InputErrorType.code.unallowed_circle:
                    message = "Your words-file has illegal circle!";
                    break;
                case InputErrorType.code.empty_string:
                    message = "Your input words string to be calculated is empty!";
                    break;
                default:
                    message = "This command is not supported!";
                    break;
            }
            Program.input(args, out para);
        }
    }
}
