using System.Linq;

namespace EFDemo
{
    interface IForumRepository
    {
        IQueryable<Reply> GetRepliesByTopic(int id);
        IQueryable<Topic> GetTopics();
    }
}
