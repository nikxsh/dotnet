using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace EntityFrameworkDemo
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

        public IEnumerable<User> GetUsers(int PageSize, int PageNumber, string keyword)
        {
            try
            {
                using (var context = new ForumContext())
                {
                    var records =  context.Users.OrderBy(x => x.UserName).Take(PageSize * PageNumber).Skip(PageSize * (PageNumber - 1)).ToList();
                    if (!string.IsNullOrEmpty(keyword))
                        records = records.Where(x => x.UserName.Contains(keyword) ||
                                                   x.FirstName.Contains(keyword) ||
                                                   x.LastName.Contains(keyword) ||
                                                   x.Email.Contains(keyword) ||
                                                   x.Dob.ToString().Contains(keyword))
                                  .ToList();
                    return records;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<KeyValuePair<Guid,string>> GlobalSearch(string keyword)
        {
            try
            {
                using (var context = new ForumContext())
                {
                    var usersearch =  context.Users.Where(x => x.UserName.Contains(keyword) || 
                                                    x.FirstName.Contains(keyword) || 
                                                    x.LastName.Contains(keyword) || 
                                                    x.Email.Contains(keyword) || 
                                                    x.Dob.ToString().Contains(keyword))
                                             .ToList();

                    var result = usersearch.Select(y => new KeyValuePair<Guid, string>(y.Id, y.UserName));

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetUserCount()
        {
            try
            {
                using (var context = new ForumContext())
                {
                    return context.Users.Count();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveUser(User User)
        {
            try
            {
                using (var context = new ForumContext())
                {
                    context.Users.Add(User);
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

        public void EditUser(User User)
        {
            try
            {
                using (var context = new ForumContext())
                {
                    context.Users.AddOrUpdate(User);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteUser(Guid UserId)
        {  try
            {
                using (var context = new ForumContext())
                {
                    context.Users.Remove(context.Users.Where(x => x.Id == UserId).FirstOrDefault());
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
