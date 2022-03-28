using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using ClassLibrary;


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
        static string[] result = new string[20005];


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


            Console.WriteLine("[gui] before skip");
            Console.WriteLine("useFileInput=" + useFileInput);
            Console.WriteLine("inputSource=" + inputSource);
            Console.WriteLine("calType=" + calType);
            Console.WriteLine("isR=" + isR);
            Console.WriteLine("charH=" + charH);
            Console.WriteLine("charT=" + charT);
            Console.WriteLine("result.length=" + result.Length);

            Chain.test(useFileInput, inputSource, calType, isR, charH, charT, ref result);
            //Chain.gen_for_gui_para(useFileInput, inputSource, calType, isR, charH, charT, ref result);
        }
    }
}
