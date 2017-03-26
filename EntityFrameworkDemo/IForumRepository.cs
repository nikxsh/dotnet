using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityFrameworkDemo
{
    interface IForumRepository
    {
        IEnumerable<Reply> GetRepliesByTopic(Guid id);
        IEnumerable<Topic> GetTopics();
        void SubmitTopic(Topic Topic);
    }
}
