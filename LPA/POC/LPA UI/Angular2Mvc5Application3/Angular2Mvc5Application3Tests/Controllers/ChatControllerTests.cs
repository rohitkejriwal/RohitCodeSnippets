using Microsoft.VisualStudio.TestTools.UnitTesting;
using Angular2Mvc5Application3.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Angular2Mvc5Application3.Controllers.Tests
{
    [TestClass()]
    public class ChatControllerTests
    {
        [TestMethod()]
        public void GetChatListTest()
        {
            //ChatController chat = new ChatController();

            GetChatList();
        }
    }
}