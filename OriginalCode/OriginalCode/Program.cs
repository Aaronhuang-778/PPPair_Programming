using System;

namespace t1
{
    class Program
    {
        static void Main(string[] args)
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
                                    c.wrongHead();
                                }
                                else if (args[i + 1].Length > 1 || (args[i + 1][0] < 97 || args[i + 1][0] > 122))
                                {
                                    c.wrongHead();
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
                                    c.wrongTail();
                                }
                                else if (args[i + 1].Length > 1 || (args[i + 1][0] < 97 || args[i + 1][0] > 122))
                                {
                                    c.wrongTail();
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
                            Console.WriteLine("ERR[1] The {0} has already appeared or you have chosen output type!", args[i][1]);
                            break;
                        }
                    }
                    //长度不为2，命令格式错误, 这里可以再细分提示类型
                    else
                    {
                        c.wrongFormat();
                    }
                }
                //检查读取的文件路径
                else
                {
                    if (args[i].IndexOfAny(System.IO.Path.GetInvalidPathChars()) >= 0) // 文件路径字符不合法
                    {
                        c.illegalPath();
                    }
                    else if (GlobalPara.file_name != null)  //已经保存文件路径，那么此条字符就是错误的内容，格式错误！
                    {
                        c.wrongFormat();
                        ;
                    }
                    else //尚未保存文件路径
                    {
                        if (len < 5)
                        {
                            c.wrongFormat();
                        }
                        string file_type = args[i].Substring((len - 4));
                        if (file_type != ".txt")
                        {
                            c.illegalFileType();
                        }
                        else if (!System.IO.File.Exists(args[i]))
                        {
                            c.noFile();
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
                c.noInputFile();
            }
            //全部合法，但是-m和-n不能和其他的进行组合
            if ((GlobalPara.type == 'n' || GlobalPara.type == 'm') && (GlobalPara.is_loop == true || GlobalPara.head != '!' || GlobalPara.tail != '!'))
            {
                c.wrongCombination();
            }
            Console.WriteLine(GlobalPara.is_loop);
            Console.WriteLine(GlobalPara.head);
            Console.WriteLine(GlobalPara.tail);
            Console.WriteLine(GlobalPara.type);
            Console.WriteLine(GlobalPara.file_name);

            //读单词的过程还未完成
            /*
            * 
            *   TODO
            * 
            */

            //分类调用
            switch (GlobalPara.type)
            {
                case 'n':
                    //统计单词链数量
                    break;
                case 'm':
                    //输出首字母不相同的包含单词数量最多的单词链
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
        public void wrongFormat()
        {
            Console.WriteLine("ERR[3] Please check your command's format!");
            Environment.Exit(0);
        }
        public void wrongHead()
        {
            Console.WriteLine("ERR[4] Please check your -h's character!");
            Environment.Exit(0);
        }

        public void wrongTail()
        {
            Console.WriteLine("ERR[5] Please check your -t's character!");
            Environment.Exit(0);
        }

        public void illegalPath()
        {
            Console.WriteLine("ERR[6] Your file path is illegal!");
            Environment.Exit(0);
        }

        public void illegalFileType()
        {
            Console.WriteLine("ERR[7] Your file type is not .txt!");
            Environment.Exit(0);
        }
        public void noFile()
        {
            Console.WriteLine("ERR[8] Can't find your file!");
            Environment.Exit(0);
        }
        public void wrongCombination()
        {
            Console.WriteLine("ERR[9] Please check your parameters combination, we don't support your combination!");
            Environment.Exit(0);
        }
        public void noInputFile()
        {
            Console.WriteLine("ERR[10] Please input your words file!");
            Environment.Exit(0);
        }
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
                        GlobalPara.type = 'w';
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
                    Console.WriteLine("ERR[2] This command is not supported!");
                    Environment.Exit(0);
                    break;

            }

            return 0;
        }
    }



}
