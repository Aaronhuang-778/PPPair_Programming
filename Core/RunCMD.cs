using System;
using System.Collections.Generic;
using System.IO;

namespace Core
{
    
    public class RunCMD
    {
        public static void Main(string[] args)
        {
            
        }

        public static void testPerformance()
        {
            for (int i = 0; i < 15; i++)
            {
                string path = "./n_no_circle/test" + i.ToString() + ".txt";
                if (!File.Exists(path)) break;
                try
                {
                    Chain.gen_for_gui_para(true, path, 'n', false, '0', '0', new List<string>());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[Exception in main] " + ex.Message);
                }
            }
            for (int i = 0; i < 15; i++)
            {
                string path = "./m_no_circle/test" + i.ToString() + ".txt";
                if (!File.Exists(path)) break;
                try
                {
                    Chain.gen_for_gui_para(true, path, 'm', false, '0', '0', new List<string>());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[Exception in main] " + ex.Message);
                }
            }
            for (int i = 0; i < 15; i++)
            {
                string path = "./w_no_circle/test" + i.ToString() + ".txt";
                if (!File.Exists(path)) break;
                try
                {
                    Chain.gen_for_gui_para(true, path, 'w', false, '0', '0', new List<string>());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[Exception in main] " + ex.Message);
                }
            }
            for (int i = 0; i < 15; i++)
            {
                string path = "./w_circle/test" + i.ToString() + ".txt";
                if (!File.Exists(path)) break;
                try
                {
                    Chain.gen_for_gui_para(true, path, 'w', true, '0', '0', new List<string>());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[Exception in main] " + ex.Message);
                }
            }
            for (int i = 0; i < 15; i++)
            {
                string path = "./c_no_circle/test" + i.ToString() + ".txt";
                if (!File.Exists(path)) break;
                try
                {
                    Chain.gen_for_gui_para(true, path, 'c', false, '0', '0', new List<string>());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[Exception in main] " + ex.Message);
                }
            }
            for (int i = 0; i < 15; i++)
            {
                string path = "./c_circle/test" + i.ToString() + ".txt";
                if (!File.Exists(path)) break;
                try
                {
                    Chain.gen_for_gui_para(true, path, 'c', true, '0', '0', new List<string>());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[Exception in main] " + ex.Message);
                }
            }
        }
    }
}
