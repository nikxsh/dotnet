using System;

namespace EFDataStorage.Entities
{
    public class Reply
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }

        public Guid TopicId { get; set; }
    }
}
