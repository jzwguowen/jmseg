using System.Collections.Generic;
using jmseg.Model;
using System;
using System.Linq;

namespace jmseg.DAO.Implementation
{
    public class UserDAO : IUserDAO
    {
        public User Create(User user)
        {
            return new User();
        }

        public User FindById(long id)
        {
            return new User();
        }

        public User FindByEmail(string email)
        {
            return new User();
        }

        public List<User> FindAll()
        {
            List<User> users = new List<User>();
            users.Add(new User());
            users.Add(new User());
            users.Add(new User());

            return users;
        }

        public User Update(User user)
        {
            return new User();
        }

        public void Delete(long id)
        {
            //
        }

        public bool Exists(long? id)
        {
            return false;
        }
    }
}
