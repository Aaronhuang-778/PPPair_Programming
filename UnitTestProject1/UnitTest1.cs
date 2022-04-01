using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Core;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string[] test = { "-m", "aaa.txt" };
            Core.RunCMD.Main(test);
            Core.GlobalPara.clearGlobal();
        }
        [TestMethod]
        public void TestMethod2()
        {
            string[] test = { "-n", "aaa.txt" };
            Core.RunCMD.Main(test);
            Core.GlobalPara.clearGlobal();

        }
        [TestMethod]
        public void TestMethod3()
        {
            string[] test = { "-w", "aaa.txt", "-r" };
            Core.RunCMD.Main(test);
            Core.GlobalPara.clearGlobal();

        }
        [TestMethod]
        public void TestMethod4()
        {
            string[] test = { "-c", "aaa.txt", "-r" };
            Core.RunCMD.Main(test);
            Core.GlobalPara.clearGlobal();

        }
    }
}
