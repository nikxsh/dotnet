using EFDataStorage.Entities;
using System;
using System.Collections.Generic;

namespace EFDataStorage.Contracts
{
    public interface IForumRepository
    {
        IEnumerable<Reply> GetRepliesByTopic(Guid Id);
        IEnumerable<Topic> GetTopics();      
        Topic GetTopicById(Guid TopicId);
        void SubmitTopic(Topic Topic);
        void SubmitReply(Reply Reply);
       
    }
}
