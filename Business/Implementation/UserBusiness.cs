using System.Collections.Generic;
using System.Security.Cryptography;
using jmseg.Models;
using jmseg.DAO;
using BCrypt;

namespace jmseg.Business.Implementation
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserDAO dao;

        public UserBusiness(IUserDAO repository)
        {
            dao = repository;
        }

        public User Create(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            // check a password
            //bool validPassword = BCrypt.Net.BCrypt.Verify(userSubmittedPassword, hashedPassword);

            return dao.Create(user);
        }

        public User FindById(long id)
        {
            return dao.FindById(id);
        }

        public User FindByEmail(string email)
        {
            return dao.FindByEmail(email);
        }

        public List<User> FindAll()
        {
            return dao.FindAll();
        }

        public User Update(User user)
        {
            return dao.Update(user);
        }

        public void Delete(long id)
        {
            dao.Delete(id);
        }

        public bool Exists(long id)
        {
            return dao.Exists(id);
        }
    }
}
