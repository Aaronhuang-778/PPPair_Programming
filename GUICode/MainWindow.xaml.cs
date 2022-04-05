using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Collections;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace PPPair_Programming
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        static bool useFileInput = false;
        static string inputSource;
        static char calType = '0';
        static bool isR = false;
        static char charH = '0';
        static char charT = '0';


        public MainWindow()
        {
            InitializeComponent();
        }

        public void getInputFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "TXT Files|*.txt";
            string path = Directory.GetCurrentDirectory();
            if (Directory.Exists(path))
            {
                openFileDialog.InitialDirectory = path;
            }
            else
            {
                openFileDialog.InitialDirectory = @"C:\";
            }

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                useFileInput = true;
                string filePath = openFileDialog.FileName;
                inputSource = filePath;
                textBoxInput.Text = filePath;
                Console.WriteLine(filePath);
            }
        }

        public void startCal(object sender, RoutedEventArgs e)
        {
            if ((bool) radioButtonFile.IsChecked)
                useFileInput = true;
            if ((bool) radioButtonText.IsChecked)
                useFileInput = false;

            if ((bool)radioButton_paraN.IsChecked) 
                calType = 'n';
            else if ((bool)radioButton_paraM.IsChecked)
                calType = 'm';
            else if ((bool)radioButton_paraW.IsChecked)
            {
                calType = 'w';
                isR = (bool)radioButton_paraWR.IsChecked;
                if ((bool)radioButton_paraWH.IsChecked)
                {
                    if (textBoxWH.Text.Length == 0)
                        charH = '0';
                    else if (Char.IsLetter((textBoxWH.Text)[0]))
                        charH = (textBoxWH.Text)[0];
                    else
                        charH = '0';
                }
                if ((bool)radioButton_paraWT.IsChecked)
                {
                    if (textBoxWT.Text.Length == 0)
                        charT = '0';
                    else if (Char.IsLetter((textBoxWT.Text)[0]))
                        charT = (textBoxWT.Text)[0];
                    else
                        charT = '0';
                }
            }
            else if ((bool)radioButton_paraC.IsChecked)
            {
                calType = 'c';
                isR = (bool)radioButton_paraCR.IsChecked;
                if ((bool)radioButton_paraCH.IsChecked)
                {
                    if (textBoxCH.Text.Length == 0)
                        charH = '0';
                    else if (Char.IsLetter((textBoxCH.Text)[0]))
                        charH = (textBoxCH.Text)[0];
                    else
                        charH = '0';
                }
                if ((bool)radioButton_paraCT.IsChecked)
                {
                    if (textBoxCT.Text.Length == 0)
                        charT = '0';
                    else if (Char.IsLetter((textBoxCT.Text)[0]))
                        charT = (textBoxCT.Text)[0];
                    else
                        charT = '0';
                }
            }

            if ((bool)radioButtonText.IsChecked)
                inputSource = textBoxInput.Text;

            string inputStr = inputSource.Replace("\r\n", " ");

            Console.WriteLine("[gui] before skip");
            Console.WriteLine("useFileInput=" + useFileInput);
            Console.WriteLine("inputSource=" + inputStr);
            Console.WriteLine("calType=" + calType);
            Console.WriteLine("isR=" + isR);
            Console.WriteLine("charH=" + charH);
            Console.WriteLine("charT=" + charT);

            textBoxResult.IsEnabled = false;
            textBoxResult.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;

            if (inputSource == null || inputSource.Length == 0)
                textBoxResult.Text = "错误提示：\n未输入字符串或选择输入文件";
            else if (calType == '0' || calType == '\0')
                textBoxResult.Text = "错误提示：\n未选择计算方式";
            else if (((bool)radioButton_paraWH.IsChecked || (bool)radioButton_paraCH.IsChecked)
                && (charH == '0' || charH == '\0'))
                textBoxResult.Text = "错误提示：\n未输入开头字符";
            else if (((bool)radioButton_paraWT.IsChecked || (bool)radioButton_paraCT.IsChecked)
                && (charT == '0' || charT == '\0'))
                textBoxResult.Text = "错误提示：\n未输入结尾字符";
            else
            {
                if ((bool)useOurCoreButton.IsChecked) useOurCore();
                else useZFCore();
            }

        }

        public void useOurCore()
        {
            List<string> result = new List<string>();
            try
            {
                Core.Chain.gen_for_gui_para(
                    useFileInput, inputSource, 
                    calType, 
                    isR, charH, charT, 
                    result);
                string resultText = "";
                if (result != null && result.Count > 0)
                {
                    foreach (string res in result) resultText += res + "\n";
                    textBoxResult.IsEnabled = true;
                    textBoxResult.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                }
                else
                {
                    resultText = "未获得结果，请重新输入";
                }

                textBoxResult.Text = resultText;
                Console.WriteLine("result=" + resultText);
            }
            catch (Core.InvalidInputException ex)
            {
                Console.WriteLine("[Exception]");
                Console.WriteLine(ex.Message);
                textBoxResult.Text = "错误提示：\n" + ex.Message;
            }
            catch (Core.CircleException ex)
            {
                Console.WriteLine("[Exception]");
                Console.WriteLine(ex.Message);
                textBoxResult.Text = "错误提示：\n" + ex.Message;
            }
            catch (Core.ChainNotFoundException ex)
            {
                Console.WriteLine("[Exception]");
                Console.WriteLine(ex.Message);
                textBoxResult.Text = "错误提示：\n" + ex.Message;
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Exception]");
                Console.WriteLine(ex.Message);
                textBoxResult.Text = "错误提示：\n" + ex.Message;
            }
        }

        public void useZFCore()
        {
            Console.WriteLine("use zf core");
            List<string> words = null;
            if (useFileInput)
            {
                Console.WriteLine("use file input");
                StreamReader reader = new StreamReader(inputSource);
                string str = reader.ReadToEnd();
                str = str.ToLower();
                string[] wordsArr = Regex.Split(str, "[^(a-zA-Z)]+");
                words = new List<string>(wordsArr);
                if (words[0] == "") words.RemoveAt(0);
                Console.WriteLine("list<string> words = [" + String.Join(", ", words) + "]");
            }
            else
            {
                string[] wordsArr = Regex.Split(inputSource.ToLower(), "[^(a-zA-Z)]+");
                words = new List<string>(wordsArr);
                if (words[0] == "") words.RemoveAt(0);
                Console.WriteLine("list<string> words = [" + String.Join(", ", words) + "]");
            }
            if (words == null || words.Count == 0 || words[0].Length == 0)
            {
                textBoxResult.Text = "错误提示：\n输入字符串为空";
            }
            else
            {
                try
                {
                    List<string> result = new List<string>();
                    switch (calType)
                    {
                        case 'n':
                            //统计单词链数量 只传递单词链
                            ZFCore.PairTestInterface.gen_chains_all(words, result);
                            break;
                        case 'm':
                            //输出首字母不相同的包含单词数量最多的单词链 只传递单词链
                            ZFCore.PairTestInterface.gen_chain_word_unique(words, result);
                            break;
                        case 'w':
                            //需要传入Global的参数进行处理
                            Console.WriteLine("ZFCore.PairTestInterface.gen_chain_word()");
                            Console.WriteLine("list<string> words = [" + String.Join(", ", words) + "]");
                            Console.WriteLine($"char charH={charH}, char charT={charT}, bool isR={isR}");
                            ZFCore.PairTestInterface.gen_chain_word(words, result, charH, charT, isR);
                            break;
                        case 'c':
                            //需要传入Global的参数进行处理
                            ZFCore.PairTestInterface.gen_chain_char(words, result, charH, charT, isR);
                            break;
                    }
                    string resultText = "";
                    if (result != null && result.Count > 0 && result[0] != "")
                    {
                        if (calType == 'n')
                            resultText = result.Count.ToString() + "\n";
                        foreach (string res in result) resultText += res + "\n";
                        textBoxResult.IsEnabled = true;
                        textBoxResult.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                    }
                    else resultText = "未获得结果，请重新输入";

                    textBoxResult.Text = resultText;
                    Console.WriteLine("result=" + resultText);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[Exception from ZFCore]");
                    Console.WriteLine(ex.Message);
                    textBoxResult.Text = "错误提示：\n" + ex.Message;
                }
            }
        }

    }

    
}
