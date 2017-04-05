using System;
using System.Collections.Generic;

namespace EFDataStorage.Entities
{
    public class Topic
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }

        public ICollection<Reply> Replies { get; set; }
    }
}
