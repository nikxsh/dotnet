using System;

namespace EntityFrameworkDemo
{
    public class Reply
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }

        public Guid TopicId { get; set; }
    }
}
