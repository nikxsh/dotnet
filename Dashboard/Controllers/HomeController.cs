using Dashboard.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Dashboard.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() => View();

        public ActionResult Dashboard() => View();

        public ActionResult Table() => View();

        public ActionResult Flot() => View();
        public ActionResult Morris() => View();

        public ActionResult Forms() => View();

        public ActionResult Panels() => View();

        public ActionResult Buttons() => View();

        public ActionResult Notifications() => View();

        public ActionResult Typography() => View();

        public ActionResult Icons() => View();

        public ActionResult Grid() => View();

        public ActionResult Forum() => View();

        [HttpPost]
        public ActionResult Forum(Topic topic)
        {
            var dataobject = new EntityFrameworkDemo.ForumRepository();
            dataobject.SubmitTopic(new EntityFrameworkDemo.Topic { Id = Guid.NewGuid() , Title = topic.Title, Body = topic.Body, Created = DateTime.Now });
            ModelState.Clear();
            return View();
        }

        [HttpGet]
        public ActionResult SubmitReply()
        {
            var id = Guid.Parse(Request.QueryString["TopicId"]);
            return PartialView(new Reply { TopicId = id });
        }

        [HttpPost]
        public ActionResult SaveReply(Reply reply)
        {
            var dataobject = new EntityFrameworkDemo.ForumRepository();
            dataobject.SubmitReply(new EntityFrameworkDemo.Reply { Id = Guid.NewGuid(), Body = reply.Body, Created = DateTime.Now, TopicId = reply.TopicId });
            return View();
        }

        public ActionResult DisplayTopics()
        {
            var dataobject = new EntityFrameworkDemo.ForumRepository();
            var topic = dataobject.GetTopics().Select(x => new Topic { Id = x.Id, Title = x.Title, Body = x.Body, Created = x.Created });
            return PartialView(topic);
        }

    }
}