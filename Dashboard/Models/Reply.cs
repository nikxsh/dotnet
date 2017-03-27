using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dashboard.Models
{
    public class Reply
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }
        public Guid TopicId { get; set; }
    }
}