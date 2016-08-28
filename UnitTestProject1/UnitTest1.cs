using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        public async void TestMethod2()
        {
            string test = "test user";

            ServiceReference1.Service1Client service = new ServiceReference1.Service1Client();
            ServiceReference1.AuthorResponse auth = await service.GetInfoAsync(test);
            
            Assert.AreEqual("test user", test);
        }
    }
}
