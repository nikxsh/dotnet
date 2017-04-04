using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityFrameworkDemo
{
    public interface IForumRepository
    {
        IEnumerable<Reply> GetRepliesByTopic(Guid id);
        IEnumerable<Topic> GetTopics();
        IEnumerable<User> GetUsers();
        User GetUserById(Guid Id);
        Topic GetTopicById(Guid topicId);
        void SubmitTopic(Topic Topic);
        void SubmitReply(Reply Reply);
        void SaveUser(User user);
    }
}
