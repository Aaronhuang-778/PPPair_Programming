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
using Core;


namespace PPPair_Programming
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        static bool useFileInput = false;
        static string inputSource;
        static char calType = '!';
        static bool isR = false;
        static char charH = '!';
        static char charT = '!';


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
                        charH = '!';
                    else if (Char.IsLetter((textBoxWH.Text)[0]))
                        charH = (textBoxWH.Text)[0];
                    else
                        charH = '!';
                }
                if ((bool)radioButton_paraWT.IsChecked)
                {
                    if (textBoxWT.Text.Length == 0)
                        charT = '!';
                    else if (Char.IsLetter((textBoxWT.Text)[0]))
                        charT = (textBoxWT.Text)[0];
                    else
                        charT = '!';
                }
            }
            else if ((bool)radioButton_paraC.IsChecked)
            {
                calType = 'c';
                isR = (bool)radioButton_paraCR.IsChecked;
                if ((bool)radioButton_paraCH.IsChecked)
                {
                    if (textBoxCH.Text.Length == 0)
                        charH = '!';
                    else if (Char.IsLetter((textBoxCH.Text)[0]))
                        charH = (textBoxCH.Text)[0];
                    else
                        charH = '!';
                }
                if ((bool)radioButton_paraCT.IsChecked)
                {
                    if (textBoxCT.Text.Length == 0)
                        charT = '!';
                    else if (Char.IsLetter((textBoxCT.Text)[0]))
                        charT = (textBoxCT.Text)[0];
                    else
                        charT = '!';
                }
            }

            if ((bool)radioButtonText.IsChecked)
                inputSource = textBoxInput.Text;

            Console.WriteLine("[gui] before skip");
            Console.WriteLine("useFileInput=" + useFileInput);
            //Console.WriteLine("inputSource=" + inputSource);
            Console.WriteLine("calType=" + calType);
            Console.WriteLine("isR=" + isR);
            Console.WriteLine("charH=" + charH);
            Console.WriteLine("charT=" + charT);

            if (inputSource == null || inputSource.Length == 0)
                textBoxResult.Text = "错误提示：\n未输入字符串或选择输入文件";
            else if (calType == '!' || calType == '\0')
                textBoxResult.Text = "错误提示：\n未选择计算方式";
            else if (((bool)radioButton_paraWH.IsChecked || (bool)radioButton_paraCH.IsChecked)
                && (charH == '!' || charH == '\0'))
                textBoxResult.Text = "错误提示：\n未输入开头字符";
            else if (((bool)radioButton_paraWT.IsChecked || (bool)radioButton_paraCT.IsChecked)
                && (charT == '!' || charT == '\0'))
                textBoxResult.Text = "错误提示：\n未输入结尾字符";
            else
            {
                ArrayList result = new ArrayList();
                try
                {
                    //Chain.test(useFileInput, inputSource, calType, isR, charH, charT, ref result);
                    Chain.gen_for_gui_para(useFileInput, inputSource, calType, isR, charH, charT, result);

                    string resultText = "";
                    if (result != null && result.Count > 0)
                        foreach (string res in result)
                            resultText += res + "\n";
                    else resultText = "未获得结果，请重新输入";

                    textBoxResult.Text = resultText;
                    Console.WriteLine("result=" + resultText);
                }
                catch (InvalidInputException ex)
                {
                    Console.WriteLine("[get in gui]");
                    Console.WriteLine(ex.Message);
                    textBoxResult.Text = "错误提示：\n" + ex.Message;
                }
                catch (CircleException ex)
                {
                    Console.WriteLine("[get in gui]");
                    Console.WriteLine(ex.Message);
                    textBoxResult.Text = "错误提示：\n" + ex.Message;
                }
                catch (ChainNotFoundException ex)
                {
                    Console.WriteLine("[get in gui]");
                    Console.WriteLine(ex.Message);
                    textBoxResult.Text = "错误提示：\n" + ex.Message;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[get in gui]");
                    Console.WriteLine(ex.Message);
                    textBoxResult.Text = "错误提示：\n" + ex.Message;
                }
            }

        }

    }

    
}
