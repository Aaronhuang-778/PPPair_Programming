using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Core
{
    public class Program
    {
        public static List<string> input(string[] args, out InputPara ip)
        {
            //处理参数,更新Global当中的参数
            ip = new InputPara();
            DealParas.dealPara(args, ip);

            //读取单词
            DealWords.dealWords(ip.file_name);

            string str = Directory.GetCurrentDirectory();
            str = str + "\\solution.txt";
            //分类调用
            /*if (ip.type == 'c' || ip.type == 'm' || ip.type == 'w')
            {
                if (str.IndexOfAny(Path.GetInvalidPathChars()) >= 0) // 文件路径字符不合法
                {
                    throw new InvalidInputException(InputErrorType.code.illegal_path);
                }
                string file_type = str.Substring((str.Length - 4));
                if (file_type != ".txt")
                {
                    throw new InvalidInputException(InputErrorType.code.illegal_file_type);
                }
                else if (!File.Exists(str))
                {
                    throw new InvalidInputException(InputErrorType.code.file_not_found);
                }
            }*/

            if (ip.type == '0')
                throw new InvalidInputException(InputErrorType.code.no_check_mode);

            if (DealWords.words == null || DealWords.words.Count == 0 || DealWords.words[0].Length == 0)
                throw new InvalidInputException(InputErrorType.code.empty_string);

            return DealWords.words;
        }

    }
    public static class DealWords
    {
        public static List<string> words = new List<string>();
        public static void dealWords(string file_name)
        {
            StreamReader reader = new StreamReader(file_name);
            string str = reader.ReadToEnd();
            str = str.ToLower();
            string[] wordsArr = Regex.Split(str, "[^(a-zA-Z)]+");
            words = new List<string>(wordsArr);
            if (words[0] == "") words.RemoveAt(0);
        }

    }
    //处理参数
    public static class DealParas
    {
        public static void dealPara(string[] args, InputPara ip)
        {
            //开始解析命令
            //全部转换为小写
            for (int i = 0; i < args.Length; i++)
            {
                args[i] = args[i].ToLower();
            }
            for (int i = 0; i < args.Length; i++)
            {
                // 检测到‘-’开始寻找参数,规定必须用空格隔开字母，那么-x长度只能为2
                int len = args[i].Length;
                if (args[i][0] == '-')
                {
                    //长度为2：-x
                    if (len == 2)
                    {
                        i = analysePara(args, i, ip);
                    }
                    //长度不为2，命令格式错误, 这里可以再细分提示类型
                    else
                    {
                        throw new InvalidInputException(InputErrorType.code.wrong_format);
                    }
                }
                //检查读取的文件路径
                else
                {
                    checkInputFile(args[i], ip);
                }
            }
            //没有单词文件
            if (ip.file_name == null)
            {
                throw new InvalidInputException(InputErrorType.code.no_filename);
            }
            //全部合法，但是-m和-n不能和其他的进行组合
            if ((ip.type == 'n' || ip.type == 'm') && (ip.is_loop == true || ip.head != '0' || ip.tail != '0'))
            {
                throw new InvalidInputException(InputErrorType.code.illegal_para_combination);
            }
        }

        public static int analysePara(string[] args, int i, InputPara ip)
        {
            int flag = Check.checkPara(args[i][1], ip);
            if (flag == 1)  //当前指令获取成功
            {
                if (args[i][1] == 'h')
                {
                    if (i == args.Length - 1)
                    {

                        throw new InvalidInputException(InputErrorType.code.wrong_head);
                    }
                    else if (args[i + 1].Length > 1 || (args[i + 1][0] < 97 || args[i + 1][0] > 122))
                    {

                        throw new InvalidInputException(InputErrorType.code.wrong_head);
                    }
                    else
                    {
                        ip.head = args[i + 1][0];
                        i += 1;

                    }
                }
                else if (args[i][1] == 't')
                {
                    if (i == args.Length - 1)
                    {
                        throw new InvalidInputException(InputErrorType.code.wrong_tail);
                    }
                    else if (args[i + 1].Length > 1 || (args[i + 1][0] < 97 || args[i + 1][0] > 122))
                    {
                        throw new InvalidInputException(InputErrorType.code.wrong_tail);
                    }
                    else
                    {
                        ip.tail = args[i + 1][0];
                        i += 1;
                    }
                }
            }
            else if (flag == 0) //指令重复
            {
                //Console.WriteLine("{0}:", args[i][1]);
                throw new InvalidInputException(InputErrorType.code.dupli_para);
            }
            return i;
        }

        public static void checkInputFile(string s, InputPara ip)
        {
            if (s.IndexOfAny(Path.GetInvalidPathChars()) >= 0) // 文件路径字符不合法
            {
                throw new InvalidInputException(InputErrorType.code.illegal_path);
            }
            else if (ip.file_name != null)  //已经保存文件路径，那么此条字符就是错误的内容，格式错误！
            {
                throw new InvalidInputException(InputErrorType.code.wrong_format);
            }
            else //尚未保存文件路径
            {
                if (s.Length < 5)
                {
                    throw new InvalidInputException(InputErrorType.code.wrong_format);
                }
                string file_type = s.Substring((s.Length - 4));
                if (file_type != ".txt")
                {
                    throw new InvalidInputException(InputErrorType.code.illegal_file_type);
                }
                else if (!System.IO.File.Exists(s))
                {
                    throw new InvalidInputException(InputErrorType.code.file_not_found);
                }
                else
                {
                    ip.file_name = s;
                }
            }
        }
    }
   
    //全局参数，用于后续调用
    public class InputPara
    {
        public bool is_loop = false;
        public char head = '0';
        public char tail = '0';
        public char type = '0';
        public string file_name = null;

        public InputPara() { }
    }


    //对参数进行检查并且设置
    public static class Check
    {
        public static int checkPara(char c, InputPara ip)
        {
            switch (c)
            {
                case 'n':
                    if (ip.type == '0')
                    {
                        ip.type = 'n';
                        return 1;
                    }
                    break;
                case 'm':
                    if (ip.type == '0')
                    {
                        ip.type = 'm';
                        return 1;
                    }
                    break;
                case 'w':
                    if (ip.type == '0')
                    {
                        ip.type = 'w';
                        return 1;
                    }
                    break;
                case 'c':
                    if (ip.type == '0')
                    {
                        ip.type = 'c';
                        return 1;
                    }
                    break;
                case 'r':
                    if (!ip.is_loop)
                    {
                        ip.is_loop = true;
                        return 1;
                    }
                    break;
                case 'h':
                    if (ip.head == '0')
                    {
                        return 1;
                    }
                    break;
                case 't':
                    if (ip.tail == '0')
                    {
                        return 1;
                    }
                    break;
                default:
                    throw new InvalidInputException(InputErrorType.code.not_support);
            }
            return 0;
        }
    }



}
