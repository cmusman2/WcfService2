using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int test = 2;
            Assert.AreEqual(2, test);
        }

        [TestMethod]
        public void TestMethod2()
        {
            string test = "test user";

            ServiceReference1.Service1Client service = new ServiceReference1.Service1Client();
            ServiceReference1.Auther auth =  service.GetInfo(test);
            
            Assert.AreEqual("test user", test);
        }

        [TestMethod]
        public void TestMethod3()
        {
            string test = "test user";

            Assert.AreEqual("test user", test);
        }
    }
}
