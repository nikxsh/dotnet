using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityFrameworkDemo
{
    public interface IForumRepository
    {
        IEnumerable<Reply> GetRepliesByTopic(Guid Id);
        IEnumerable<Topic> GetTopics();
        IEnumerable<User> GetUsers(int PageSize, int PageNumber, string keyword);
        IEnumerable<KeyValuePair<Guid, string>> GlobalSearch(string keyword);
        int GetUserCount();
        User GetUserById(Guid Id);
        Topic GetTopicById(Guid TopicId);
        void SubmitTopic(Topic Topic);
        void SubmitReply(Reply Reply);
        void SaveUser(User User);
        void EditUser(User User);
        void DeleteUser(Guid UserId);
    }
}
