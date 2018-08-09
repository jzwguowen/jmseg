using System.Collections.Generic;
using jmseg.Model;
using jmseg.DAO;

namespace jmseg.Business.Implementation
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserDAO dao;

        public UserBusiness(IUserDAO repository)
        {
            dao = repository;
        }

        public User Create(User User)
        {
            return dao.Create(User);
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

        public User Update(User User)
        {
            return dao.Update(User);
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
