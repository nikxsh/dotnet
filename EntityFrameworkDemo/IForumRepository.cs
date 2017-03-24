using System.Linq;

namespace EntityFrameworkDemo
{
    interface IForumRepository
    {
        IQueryable<Reply> GetRepliesByTopic(int id);
        IQueryable<Topic> GetTopics();
    }
}
