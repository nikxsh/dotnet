using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityFrameworkDemo
{
    public class ForumRepository : IForumRepository
    {
        public IEnumerable<Reply> GetRepliesByTopic(Guid id)
        {
            try
            {
                using (var context = new ForumContext())
                {
                    return context.Reply.Where(x => x.TopicId == id).ToList();
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
                    return context.Topics.ToList();
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
