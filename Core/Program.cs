using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Core
{
    public class Program
    {
        public static string[] input(string[] args)
        {
            //处理参数,更新Global当中的参数
            DealParas.dealPara(args);

            //读取单词
            DealWords.dealWords();

            string str = Directory.GetCurrentDirectory();
            str = str + "\\solution.txt";
            //分类调用
            if (GlobalPara.type == 'c' || GlobalPara.type == 'm' || GlobalPara.type == 'w')
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
            }

            if (DealWords.words == null || DealWords.words.Length == 0 || DealWords.words[0].Length == 0)
                throw new InvalidInputException(InputErrorType.code.empty_string);

            return DealWords.words;
        }

    }
    public static class DealWords
    {
        public static string[] words = null;
        public static void dealWords()
        {
            //Console.WriteLine(Path.GetFullPath(GlobalPara.file_name));
            StreamReader reader = new StreamReader(GlobalPara.file_name);
            string str = reader.ReadToEnd();
            str = str.ToLower();
            words = Regex.Split(str, "[^(a-zA-Z)]+");

            if (words[0] == "")
            {
                for (int i = 0; i < words.Length - 1; i++)
                {
                    words[i] = words[i + 1];
                }
                Array.Resize(ref words, words.Length - 1);
            }

            //for (int i = 0; i < words.Length; i++)  Console.WriteLine(words[i]); 
            //Console.WriteLine(words.Length);
        }

    }
    //处理参数
    public static class DealParas
    {
        public static void dealPara(string[] args)
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
                        i = analysePara(args, i);
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
                    checkInputFile(args[i]);
                    /*
                    if (args[i].IndexOfAny(Path.GetInvalidPathChars()) >= 0) // 文件路径字符不合法
                    {
                        throw new InvalidInputException(InputErrorType.code.illegal_path);
                    }
                    else if (GlobalPara.file_name != null)  //已经保存文件路径，那么此条字符就是错误的内容，格式错误！
                    {
                        throw new InvalidInputException(InputErrorType.code.wrong_format);
                    }
                    else //尚未保存文件路径
                    {
                        if (len < 5)
                        {
                            throw new InvalidInputException(InputErrorType.code.wrong_format);
                        }
                        string file_type = args[i].Substring((len - 4));
                        if (file_type != ".txt")
                        {
                            throw new InvalidInputException(InputErrorType.code.illegal_file_type);
                        }
                        else if (!File.Exists(args[i]))
                        {
                            throw new InvalidInputException(InputErrorType.code.file_not_found);
                        }
                        else
                        {
                            GlobalPara.file_name = args[i];
                        }
                    }
                    */
                }
            }
            //没有单词文件
            if (GlobalPara.file_name == null)
            {
                throw new InvalidInputException(InputErrorType.code.no_filename);
            }
            //全部合法，但是-m和-n不能和其他的进行组合
            if ((GlobalPara.type == 'n' || GlobalPara.type == 'm') && (GlobalPara.is_loop == true || GlobalPara.head != '!' || GlobalPara.tail != '!'))
            {
                throw new InvalidInputException(InputErrorType.code.illegal_para_combination);
            }
        }

        public static int analysePara(string[] args, int i)
        {
            int flag = Check.checkPara(args[i][1]);
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
                        GlobalPara.head = args[i + 1][0];
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
                        GlobalPara.tail = args[i + 1][0];
                        i += 1;
                    }
                }
            }
            else if (flag == 0) //指令重复
            {
                Console.WriteLine("{0}:", args[i][1]);
                throw new InvalidInputException(InputErrorType.code.dupli_para);
            }
            return i;
        }

        public static void checkInputFile(string s)
        {
            if (s.IndexOfAny(System.IO.Path.GetInvalidPathChars()) >= 0) // 文件路径字符不合法
            {
                throw new InvalidInputException(InputErrorType.code.illegal_path);
            }
            else if (GlobalPara.file_name != null)  //已经保存文件路径，那么此条字符就是错误的内容，格式错误！
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
                    GlobalPara.file_name = s;
                }
            }
        }
    }
   
    //全局参数，用于后续调用
    public static class GlobalPara
    {
        public static bool is_loop = false;
        public static char head = '!';
        public static char tail = '!';
        public static char type = '!';
        public static string file_name = null;
        public static void clearGlobal()
        {
            is_loop = false;
         head = '!';
         tail = '!';
         type = '!';
         file_name = null;
    }
    }
    //对参数进行检查并且设置
    public static class Check
    {
        public static int checkPara(char c)
        {
            switch (c)
            {
                case 'n':
                    if (GlobalPara.type == '!')
                    {
                        GlobalPara.type = 'n';
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                case 'm':
                    if (GlobalPara.type == '!')
                    {
                        GlobalPara.type = 'm';
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                case 'w':
                    if (GlobalPara.type == '!')
                    {
                        GlobalPara.type = 'w';
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                case 'c':
                    if (GlobalPara.type == '!')
                    {
                        GlobalPara.type = 'c';
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                case 'r':
                    if (!GlobalPara.is_loop)
                    {
                        GlobalPara.is_loop = true;
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                case 'h':
                    if (GlobalPara.head == '!')
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                case 't':
                    if (GlobalPara.tail == '!')
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                default:
                    throw new InvalidInputException(InputErrorType.code.not_support);

            }

        }
    }



}
