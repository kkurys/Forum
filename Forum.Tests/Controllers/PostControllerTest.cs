using Forum.Controllers;
using Forum.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace Forum.Tests.Controllers
{
    [TestClass]
    public class PostControllerTests
    {

        [TestMethod]
        public void AddNewPost()
        {
            Topic _topic = new Topic();
            PostController _postController = new PostController();

            _topic.ID = 1;

            ActionResult result = _postController.Create(new Post());

            Assert.IsNotNull(result);
            Assert.AreEqual(1, _topic.PostCount);
        }
        public void DeletePost(int ID)
        {

        }

    }
}
