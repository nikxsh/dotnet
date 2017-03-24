using System;
using System.Linq;

namespace EFDemo
{
    public class ForumRepository : IForumRepository
    {
        public IQueryable<Reply> GetRepliesByTopic(int id)
        {
            try
            {
                using (var context = new ForumContext())
                {
                    return context.Reply.Where(x => x.TopicId == id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<Topic> GetTopics()
        {
            try
            {
                using (var context = new ForumContext())
                {
                    return context.Topics;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
