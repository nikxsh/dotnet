using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dashboard.Models
{
    public class Reply
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }
        public int TopicId { get; set; }
    }
}