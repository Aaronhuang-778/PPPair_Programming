using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using OriginalCode;

namespace OriginalCode
{
    class Program
    {
        public static void input(string[] args)
        {
            //处理参数,更新Global当中的参数
            DealParas dealParas = new DealParas();
            dealParas.dealPara(args);

            //读取单词
            DealWords dealWords = new DealWords();
            dealWords.dealWords();

            //分类调用
            switch (GlobalPara.type)
            {
                case 'n':
                    //统计单词链数量 只传递单词链
                  
                    break;
                case 'm':
                    //输出首字母不相同的包含单词数量最多的单词链 只传递单词链

                    break;
                case 'w':
                    //需要传入Global的参数进行处理

                    break;
                case 'c':
                    //需要传入Global的参数进行处理
                    break;
            }
        }

    }
    class DealWords
    {
        public void dealWords()
        {
            //Console.WriteLine(Path.GetFullPath(GlobalPara.file_name));
            StreamReader reader = new StreamReader(GlobalPara.file_name);
            string[] words = null;
            string str = reader.ReadToEnd();
            str = str.ToLower();
            words = Regex.Split(str, "[^(a-zA-Z)]+");
            for (int i = 0; i < words.Length; i ++)
            {
                if (words[i].Length > 1)
                {
                    Word word = new Word(words[i]);
                    /*
                     * TODO 建立单词表存储单词，数据结构未定
                     */

                }
            }
        }
    }
    //处理参数
    class DealParas
    {
        public void dealPara(string[] args)
        {
            //开始解析命令
            Check c = new Check();
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
                        int flag = c.checkPara(args[i][1]);
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
                            break;
                        }
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
                    if (args[i].IndexOfAny(System.IO.Path.GetInvalidPathChars()) >= 0) // 文件路径字符不合法
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
                        else if (!System.IO.File.Exists(args[i]))
                        {
                            throw new InvalidInputException(InputErrorType.code.file_not_found);
                        }
                        else
                        {
                            GlobalPara.file_name = args[i];
                        }
                    }
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
            Console.WriteLine(GlobalPara.is_loop);
            Console.WriteLine(GlobalPara.head);
            Console.WriteLine(GlobalPara.tail);
            Console.WriteLine(GlobalPara.type);
            Console.WriteLine(GlobalPara.file_name);
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
    }
    //对参数进行检查并且设置
    class Check
    {
        public int checkPara(char c)
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
                    break;
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
                    break;
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
                    break;
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
                    break;
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
                    break;
                case 'h':
                    if (GlobalPara.head == '!')
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                    break;
                case 't':
                    if (GlobalPara.tail == '!')
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                    break;
                default:
                    throw new InvalidInputException(InputErrorType.code.not_support);
                    Environment.Exit(0);
                    break;

            }

            return 0;
        }
    }



}