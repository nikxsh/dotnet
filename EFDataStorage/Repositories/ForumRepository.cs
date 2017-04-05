using EFDataStorage.Contracts;
using EFDataStorage.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFDataStorage.Repositories
{
    public class ForumRepository : IForumRepository
    {
        public IEnumerable<Reply> GetRepliesByTopic(Guid Id)
        {
            try
            {
                using (var context = new ForumContext())
                {
                    return context.Reply.Where(x => x.TopicId == Id).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Topic> GetTopics()
        {
            try
            {
                using (var context = new ForumContext())
                {
                    return context.Topics.OrderByDescending(y => y.Created).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Topic GetTopicById(Guid TopicId)
        {
            try
            {
                using (var context = new ForumContext())
                {
                    return context.Topics.FirstOrDefault(x => x.Id == TopicId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SubmitReply(Reply Reply)
        {
            try
            {
                using (var context = new ForumContext())
                {
                    context.Reply.Add(Reply);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SubmitTopic(Topic Topic)
        {
            try
            {
                using (var context = new ForumContext())
                {
                    context.Topics.Add(Topic);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }      
    }
}
