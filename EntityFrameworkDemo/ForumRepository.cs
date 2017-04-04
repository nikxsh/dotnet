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
                    return context.Topics.OrderByDescending(y => y.Created).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Topic GetTopicById(Guid topicId)
        {
            try
            {
                using (var context = new ForumContext())
                {
                    return context.Topics.FirstOrDefault(x => x.Id == topicId);
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

        public IEnumerable<User> GetUsers()
        {
            try
            {
                using (var context = new ForumContext())
                {
                    return context.Users.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveUser(User user)
        {
            try
            {
                using (var context = new ForumContext())
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public User GetUserById(Guid Id)
        {
            try
            {
                using (var context = new ForumContext())
                {
                    return context.Users.Where(x => x.Id == Id).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
